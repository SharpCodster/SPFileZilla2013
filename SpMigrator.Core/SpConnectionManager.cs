using Microsoft.SharePoint.Client;
using System;
using System.Net;
using System.Security;

namespace SpMigrator.Core
{
    public class SpConnectionManager
    {
        private string _username;
        private string _password;
        private string _domain;

        public string SiteUrl { get; set; }
        public bool IsSpOnline { get; set; }
        public int RequestTimeout { get; set; }

        public SpConnectionManager(string username, string password) : this(username, password, String.Empty) { }

        public SpConnectionManager(string username, string password, string domain)
        {
            if (String.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException("The username is null or empty");
            }

            if (String.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("The password is null or empty");
            }

            _username = username.Trim();
            _password = password.Trim();
            _domain = domain.Trim();
            IsSpOnline = true;
            RequestTimeout = 1000000;
        }

        public ClientContext GetContext()
        {
            if (String.IsNullOrEmpty(SiteUrl))
            {
                throw new InvalidOperationException("The SharePoint site URL is not defined");
            }

            ClientContext ctx = new ClientContext(SiteUrl);

            if (IsSpOnline)
            {
                SecureString secureString = GetSecureString(_password);
                ctx.Credentials = new SharePointOnlineCredentials(_username, secureString);
            }
            else
            {
                ctx.Credentials = new NetworkCredential(_username, _password, _domain);
                ctx.ExecutingWebRequest += new EventHandler<WebRequestEventArgs>(ctx_ExecutingWebRequest_FixForMixedMode);
            }

            ctx.RequestTimeout = RequestTimeout;

            return ctx;
        }

        private SecureString GetSecureString(string source)
        {
            if (String.IsNullOrWhiteSpace(source))
            {
                return null;
            }
            else
            {
                SecureString result = new SecureString();
                foreach (char c in source.ToCharArray())
                {
                    result.AppendChar(c);
                }

                return result;
            }
        }

        private void ctx_ExecutingWebRequest_FixForMixedMode(object sender, WebRequestEventArgs e)
        {
            // to support mixed mode auth
            e.WebRequestExecutor.RequestHeaders.Add("X-FORMS_BASED_AUTH_ACCEPTED", "f");
        }

    }
}
