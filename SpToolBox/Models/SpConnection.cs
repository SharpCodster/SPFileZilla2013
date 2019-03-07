using GalaSoft.MvvmLight;
using SpMigrator.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpToolBox.Models
{
    public class SpConnection : ObservableObject
    {

        private string _url;

        private string _username;
        private string _password;

        private string _domain;

        private bool _isSharepointOnline;


        public string Url
        {
            get { return _url; }
            set { Set<string>(() => this.Url, ref _url, value); }
        }

        public string Username
        {
            get { return _username; }
            set { Set<string>(() => this.Username, ref _username, value); }
        }

        public string Password
        {
            get { return _password; }
            set { Set<string>(() => this.Password, ref _password, value); }
        }

        public string Domain
        {
            get { return _domain; }
            set { Set<string>(() => this.Domain, ref _domain, value); }
        }


        public bool IsSharepointOnlie
        {
            get { return _isSharepointOnline; }
            set { Set<bool>(() => this.IsSharepointOnlie, ref _isSharepointOnline, value); }
        }



        public SpConnectionManager GetConnectionManager()
        {
            SpConnectionManager manager = new SpConnectionManager(_username, _password, _domain);

            manager.IsSpOnline = _isSharepointOnline;
            manager.SiteUrl = Url;

            return manager;
        }

    }
}
