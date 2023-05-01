using System.Collections.Specialized;
using System.Web;
using System.Xml.Serialization;

namespace Logatron.QrzDotCom
{
    public class Client : IDisposable
    {
        private static readonly string _version = "1.34";
        private static readonly string _baseUrl = "https://xmldata.qrz.com/";
        private static readonly string _queryPath = $"xml/{_version}";

        private readonly HttpClient _client;
        private readonly string _username;
        private readonly string _password;
        private string? _sessionKey;

        public Client(string username, string password)
        {
            _client = new()
            {
                BaseAddress = new Uri(_baseUrl)
            };

            _username = username;
            _password = password;
        }

        public async Task<QrzCallsignResult> QueryCallsign(string callsign)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["callsign"] = callsign;

            var result = await QueryWithLogin(query).ConfigureAwait(false);

            if (result.Callsign == null)
            {
                throw new Exception("Callsign is invalid");
            }

            return new QrzCallsignResult(result.Callsign)
            {
                Message = result.Session?.Message
            };
        }

        public async Task<QrzDxccResult> QueryDxcc(uint dxcc)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["dxcc"] = dxcc.ToString();

            var result = await QueryWithLogin(query).ConfigureAwait(false);

            if (result.Dxcc == null)
            {
                throw new Exception("DXCC is invalid");
            }

            return new QrzDxccResult(result.Dxcc)
            {
                Message = result.Session?.Message
            };
        }

        private async Task<string> LoginAndReturnSessionKey()
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["username"] = _username;
            query["password"] = _password;

            var sessionResult = await Query(query).ConfigureAwait(false);

            if (sessionResult.Session?.Key == null)
            {
                throw new Exception("Could not login");
            }

            return sessionResult.Session.Key;
        }

        private async Task<QrzDatabase> QueryWithLogin(NameValueCollection query)
        {
            // if the session key has not yet been determined, login first
            _sessionKey ??= await LoginAndReturnSessionKey().ConfigureAwait(false);
            query["s"] = _sessionKey;

            // send query
            var result = await Query(query).ConfigureAwait(false);

            // if the key on the result is null, a re-login is required
            if (result.Session?.Key == null)
            {
                _sessionKey = await LoginAndReturnSessionKey().ConfigureAwait(false);
                query["s"] = _sessionKey;

                result = await Query(query).ConfigureAwait(false);

                // if key still is not returned, something else must be wrong
                if (result.Session?.Key == null)
                {
                    throw new Exception("");
                }
            }

            return result;
        }

        private async Task<QrzDatabase> Query(NameValueCollection query)
        {
            UriBuilder builder = new(_baseUrl)
            {
                Port = -1,
                Path = _queryPath,
                Query = query.ToString()
            };

            HttpResponseMessage response = await _client.GetAsync(builder.Uri.PathAndQuery).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                throw new Exception("Call did not return with a success");
            }

            var content = response.Content;

            if (content.Headers.ContentType?.MediaType != "text/xml")
            {
                throw new Exception("Response is not XML");
            }

            string contentString = await content.ReadAsStringAsync().ConfigureAwait(false);

            using StringReader reader = new(contentString);

            XmlSerializer serializer = new(typeof(QrzDatabase));

            if (serializer.Deserialize(reader) is not QrzDatabase result)
            {
                throw new Exception("Could not deserialize QRZDatabase");
            }

            if (result.Version != _version)
            {
                throw new Exception("Versions do not match");
            }

            if (result.Session == null)
            {
                throw new Exception("Session element is invalid");
            }

            if (result.Session.Error != null)
            {
                throw new Exception($"QRZ.com returned error: {result.Session.Error}");
            }

            return result;
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _client.Dispose();
            }
        }
    }
}
