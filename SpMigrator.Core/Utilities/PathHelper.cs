using System;
using System.Text.RegularExpressions;

namespace SpMigrator.Core.Utilities
{
    internal class PathHelper
    {
        public static bool IsNull(object x)
        {
            if ((x == null)
                || (Convert.IsDBNull(x))
                || x.ToString().Trim().Length == 0)
                return true;
            else
                return false;
        }

        public static string SafeTrim(object x)
        {
            if (IsNull(x))
                return "";
            else
                return x.ToString().Trim();
        }

        public static string CombinePaths(object path1, object path2)
        {
            if (IsNull(path1) && IsNull(path2))
            {
                return "";
            }
            else if (IsNull(path1))
            {
                return SafeTrim(path2);
            }
            else if (IsNull(path2))
            {
                return SafeTrim(path1);
            }
            else
            {
                return String.Concat(SafeTrim(path1).TrimEnd(new char[] { '/' }), "/", SafeTrim(path2).TrimStart(new char[] { '/' }));
            }
        }


        public static string CleanFilenameForSP(string s, string r)
        {
            var pattern = string.Concat("[", @"\~\""\#\%\&\*\:\<\>\?\/\\\{\|\}", "]"); // ~ " # % & * : < > ? / \ { | }
            return Regex.Replace(s, pattern, r);
        }

    }
}
