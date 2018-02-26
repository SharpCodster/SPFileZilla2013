using Microsoft.SharePoint.Client;
using System;
using System.Net;
using System.Security;

namespace SpMigrator.Core
{
    public class SpContextHelper
    {
        public static ClientContext GetContext(SpConnectionInfo connInfo)
        {
            ClientContext ctx = new ClientContext(connInfo.SiteUrl);
            
            if (connInfo.IsSpOnline)
            {
                SecureString secureString = GetSecureString(connInfo.Password);
                ctx.Credentials = new SharePointOnlineCredentials(connInfo.Username, secureString);
            }
            else
            {
                ctx.Credentials = new NetworkCredential(connInfo.Username, connInfo.Password, connInfo.Domain);
                ctx.ExecutingWebRequest += new EventHandler<WebRequestEventArgs>(ctx_ExecutingWebRequest_FixForMixedMode);
            }

            ctx.RequestTimeout = 1000000;

            return ctx;
        }

        public static SecureString GetSecureString(string source)
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

        static void ctx_ExecutingWebRequest_FixForMixedMode(object sender, WebRequestEventArgs e)
        {
            // to support mixed mode auth
            e.WebRequestExecutor.RequestHeaders.Add("X-FORMS_BASED_AUTH_ACCEPTED", "f");
        }
    }
}
