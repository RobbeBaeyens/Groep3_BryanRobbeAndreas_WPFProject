using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace GRP3_Project_DAL
{
    public class DatabaseOperations
    {
        /*=====================
         * EVENT
         =====================*/
        //ophalen
        public static List<Event> OphalenEvents()
        {
            using (EventBeheerEntities entities = new EventBeheerEntities())
            {
                var query = entities.Event
                    .Include(x => x.Eventtype)
                    .OrderBy(x => x.EventID);
                return query.ToList();
            }
        }
    }
}
