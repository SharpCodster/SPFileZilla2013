namespace SpMigrator.Core
{
    public class SpConnectionInfo
    {
        public SpConnectionInfo(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public SpConnectionInfo(string username, string password, string domain)
        {
            Username = username;
            Password = password;
            Domain = domain;
        }

        public string SiteUrl { get; set; }

        public string Username { get; private set; }
        public string Password { get; private set; }
        public string Domain { get; private set; }

        public bool IsSpOnline { get; set; }
    }
}
