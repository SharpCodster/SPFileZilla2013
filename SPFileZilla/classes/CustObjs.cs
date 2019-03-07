using SpMigrator.Core.Eums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BandR.CustObjs
{
    public class ProfileDetail
    {
        public string profileName { get; set; }
        public string siteUrl { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string domain { get; set; }
        public bool isSpOnline { get; set; }
    }

    public class SessionDetail
    {
        public string localPath { get; set; }
        public int winWidth { get; set; }
        public int winHeight { get; set; }

        public string editorFontSize { get; set; }
        public string editorColorIsWhite { get; set; }
        public string editorTextIsWrap { get; set; }

        public string spUrl { get; set; }
        public bool isSPOnline { get; set; }

        public SessionDetail()
        {
            localPath = "C:\\";
            winWidth = 0;
            winHeight = 0;

            editorFontSize = "9";
            editorColorIsWhite = "1";
            editorTextIsWrap = "0";

            spUrl = "";
            isSPOnline = false;
        }
    }

    public class SortingFSObject
    {
        public string name { get; set; }
        public string path { get; set; }
        public long? size { get; set; }
        public DateTime? dtmodified { get; set; }
    }

}
