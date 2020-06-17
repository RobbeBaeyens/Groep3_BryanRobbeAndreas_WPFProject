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
        //ophalen lijst
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
        //ophalen 1 via ID
        public static Event OphalenEvent(int eventId)
        {
            using (EventBeheerEntities entities = new EventBeheerEntities())
            {
                var query = entities.Event
                    .Include(x => x.Eventtype)
                    .Where(x => x.EventID == eventId);
                return query.SingleOrDefault();
            }
        }
        //toevoegen
        public static int AddEvent(Event eventitem)
        {
            using (EventBeheerEntities entities = new EventBeheerEntities())
            {
                entities.Event.Add(eventitem);
                return entities.SaveChanges();
            }
        }
        //updaten
        public static int UpdateEvent(Event eventitem)
        {
            try
            {
                using (EventBeheerEntities entities = new EventBeheerEntities())
                {
                    entities.Entry(eventitem).State = EntityState.Modified;
                    return entities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                //FileOperations.FoutLoggen(ex);
                return 0;
            }
        }

        /*=====================
         * EventType
         =====================*/
        //ophalen
        public static List<Eventtype> OphalenEventTypes()
        {
            using (EventBeheerEntities entities = new EventBeheerEntities())
            {
                var query = entities.Eventtype
                    .OrderBy(x => x.EventtypeID);
                return query.ToList();
            }
        }
    }
}
