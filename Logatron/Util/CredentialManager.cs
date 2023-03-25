using System;

namespace Logatron.Util
{
    internal class CredentialManager
    {
        public static void StoreCredential(string component, string username, string password)
        {
            if (component is null)
            {
                throw new ArgumentNullException(nameof(component));
            }

            if (username is null)
            {
                throw new ArgumentNullException(nameof(username));
            }

            if (password is null)
            {
                throw new ArgumentNullException(nameof(password));
            }

            var resource = $"{App.AppName}/{component}";
            var vault = new Windows.Security.Credentials.PasswordVault();

            // check if credential already exists, in that case remove first
            try
            {
                var credentialsList = vault.FindAllByResource(resource);
                if (credentialsList != null && credentialsList.Count > 0)
                {
                    foreach (var existingCredential in credentialsList)
                    {
                        vault.Remove(existingCredential);
                    }
                }
            }
            catch (Exception)
            {
            }

            // add credential
            var credential = new Windows.Security.Credentials.PasswordCredential(resource, username, password);
            vault.Add(credential);
        }

        public static Windows.Security.Credentials.PasswordCredential? LoadCredential(string component)
        {
            if (component is null)
            {
                throw new ArgumentNullException(nameof(component));
            }

            var resource = $"{App.AppName}/{component}";

            try
            {
                var vault = new Windows.Security.Credentials.PasswordVault();

                var credentialsList = vault.FindAllByResource(resource);
                if (credentialsList == null || credentialsList.Count == 0)
                {
                    return null;
                }

                var credential = credentialsList[0];
                credential.RetrievePassword();
                return credential;
            }
            catch (Exception)
            {
            }

            return null;
        }
    }
}
