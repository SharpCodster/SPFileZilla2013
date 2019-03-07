using SpMigrator.Core.Eums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpMigrator.Core.Entities
{
    public class SpFileSystemItem
    {
        public NodeType treeNodeType { get; set; }

        public string url { get; set; }
        public string name { get; set; }

        public int? length { get; set; }
        public DateTime? dtModified { get; set; }

        // special
        public int folderLevel { get; set; }
    }
}
