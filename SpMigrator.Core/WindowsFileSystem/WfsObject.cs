using SpMigrator.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpMigrator.Core.WindowsFileSystem
{
    public class WfsObject
    {

        private WfsInspector _ins;
        public WfsInspector Inspector {
            get
            {
                if (_ins == null)
                {
                    _ins = new WfsInspector();
                }
                return _ins;
            }
            set { _ins = value; }
        }




        public string Name { get; set; }
        public string Path { get; set; }
        public WfsType Type { get; set; }

        public bool HasLoaded { get; set; }


        public List<WfsObject> Childs { get; set; }

        public WfsObject Load()
        {
            Childs = Inspector.GetItems(Path);
            return this;
        }


    }
}
