﻿using System;
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
         * EVENT Robbe Baeyens
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
                    .Include(x => x.ToDo)
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
                return 0;
            }
        }
        //deleten
        public static int DeleteEvent(Event eventitem)
        {
             using (EventBeheerEntities entities = new EventBeheerEntities())
             {
                 entities.Entry(eventitem).State = EntityState.Deleted;
                 return entities.SaveChanges();
             }
        }

        /*=====================
         * EventType Robbe Baeyens
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
         *  TODO Robbe Baeyens
         =====================*/
        //ophalen lijst complete
        public static List<ToDo> OphalenToDosComplete(int eventId)
        {
            using (EventBeheerEntities entities = new EventBeheerEntities())
            {
                var query = entities.ToDo
                    .Include(x => x.Event)
                    .Where(x => x.EventID == eventId)
                    .Where(x => x.Afgewerkt == true)
                    .OrderBy(x => x.ToDoID);
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
                    .Include(x => x.Event)
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
            try
            {
                using (EventBeheerEntities entities = new EventBeheerEntities())
                {
                    entities.Entry(todo).State = EntityState.Modified;
                    return entities.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                return 0;
            }
        }
        //deleten
        public static int DeleteToDo(ToDo todo)
        {
            using (EventBeheerEntities entities = new EventBeheerEntities())
            {
                entities.Entry(todo).State = EntityState.Deleted;
                return entities.SaveChanges();
            }
        }

        //ophalen lijst
        public static List<ToDo> OphalenToDos(int eventId)
        {
            using (EventBeheerEntities entities = new EventBeheerEntities())
            {
                var query = entities.ToDo
                    .Include(x => x.Event)
                    .Where(x => x.EventID == eventId)
                    .OrderBy(x => x.Volgnr);
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
    }
}
