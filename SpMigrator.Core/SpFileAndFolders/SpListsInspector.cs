using Microsoft.SharePoint.Client;
using SpMigrator.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpMigrator.Core.SpFileAndFolders
{
    public class SpListsInspector
    {

        public static string DocumentLibraryContentTypeID = "0x0101";



        public static bool GetSiteLists(SpConnectionManager spConnection, out List<SpLibrary> lstObjs, out string msg)
        {
            msg = "";
            lstObjs = new List<SpLibrary>();

            try
            {
                using (var ctx = spConnection.GetContext())
                {
                    var lists = ctx.Web.Lists;
                    ctx.Load(lists, l => l.Include(x => x.Title, x => x.Id, x => x.ContentTypes.Include(y => y.Name, y => y.Id)));

                    ctx.ExecuteQuery();

                    foreach (List list in lists)
                    {
                        bool match = false;
                        foreach (ContentType ct in list.ContentTypes)
                        {
                            if (ct.Id.ToString().StartsWith(DocumentLibraryContentTypeID))
                            {
                                match = true;
                                break;
                            }
                        }

                        if (match)
                        {
                            lstObjs.Add(new SpLibrary() { Id = list.Id, Title = list.Title });
                        }
                    }

                    lstObjs = lstObjs.OrderBy(x => x.Title).ToList();

                }

            }
            catch (Exception ex)
            {
                //msg = SHOW_FULL_ERRORS ? ex.ToString() : ex.Message;
                msg = ex.Message;
            }

            return msg == "";
        }

    }
}
