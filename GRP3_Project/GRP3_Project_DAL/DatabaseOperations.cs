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

        /*=====================
         * TODO Andreas Steenackers
         =====================*/
        //ophalen
        public static ToDo OphalenTodo(int todoid)
        {
            using(EventBeheerEntities entities = new EventBeheerEntities())
            {
                var query = entities.ToDo
                    .Where(x => x.ToDoID == todoid);
                return query.SingleOrDefault();
            }
        }

        //toevoegen
        public static int AddToDo(ToDo todo)
        {
            using (EventBeheerEntities entities = new EventBeheerEntities())
            {
                entities.ToDo.Add(todo);
                return entities.SaveChanges();
            }
        }
        //updaten
        public static int UpdateToDo(ToDo todo)
        {
            using (EventBeheerEntities entities = new EventBeheerEntities())
            {
                entities.Entry(todo).State = EntityState.Modified;
                return entities.SaveChanges();
            }
        }
        
        //ophalen lijst
        public static List<ToDo> OphalenToDos()
        {
            using (EventBeheerEntities entities = new EventBeheerEntities())
            {
                var query = entities.ToDo
                    .Include(x => x.Event)
                    .OrderBy(x => x.ToDoID);
                return query.ToList();
            }
        }
              
        /*=====================
         * Location (Bryan Antonis)
         =====================*/

        //Ophalen
        public static List<Locatie> OphalenLocaties()
        {
            using (EventBeheerEntities entities = new EventBeheerEntities())
            {
                var query = entities.Locatie
                    .OrderBy(x => x.Naam);
                return query.ToList();
            }
        }

        //1x ophalen via ID
        public static Locatie OphalenLocatie(int locatieId)
        {
            using (EventBeheerEntities entities = new EventBeheerEntities())
            {
                var query = entities.Locatie
                    .Where(x => x.LocatieID == locatieId)
                    .OrderBy(x => x.Naam);
                return query.SingleOrDefault();
            }
        }

        //Toevoegen
        public static int ToevoegenLocatie(Locatie locatieAdd)
        {
            using (EventBeheerEntities entities = new EventBeheerEntities())
            {
                entities.Locatie.Add(locatieAdd);
                return entities.SaveChanges();
            }
        }

        //Updaten
        public static int UpdatenLocatie(Locatie locatieUpd)
        {
            using (EventBeheerEntities entities = new EventBeheerEntities())
            {
                entities.Entry(locatieUpd).State = EntityState.Modified;
                return entities.SaveChanges();
            }
        }
    }
}
