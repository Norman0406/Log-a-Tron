using System.Net.Http.Headers;
using System.Web;

namespace Logatron.LotW
{
    public class Client : IDisposable
    {
        private static readonly string _baseUrl = "https://lotw.arrl.org";
        private static readonly string _queryPath = "lotwuser/lotwreport.adi";

        private readonly HttpClient _client;
        private readonly string _username;
        private readonly string _password;

        public Client(string username, string password)
        {
            _client = new()
            {
                BaseAddress = new Uri(_baseUrl)
            };

            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _username = username;
            _password = password;
        }

        public async Task<Adif.File> Query()
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["login"] = _username;
            query["password"] = _password;
            query["qso_query"] = "1";
            query["qso_qsl"] = "yes";

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

            if (content.Headers.ContentType?.MediaType != "application/x-arrl-adif")
            {
                throw new Exception("Response is not ADIF");
            }

            string contentString = await content.ReadAsStringAsync().ConfigureAwait(false);

            return Adif.Parser.Parse(contentString);
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
