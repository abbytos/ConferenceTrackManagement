using System;
using System.Collections.Generic;
using System.Linq;

namespace ConferenceTrackManagement
{
    /// <summary>
    /// TrackBuilder class is responsible for building and displaying the Tracks.
    /// </summary>
    public class TrackBuilder
    {
        //file path
        private string path;
        //collection of Talk objects
        private TalkCollection collection;

        /// <summary>
        /// TrackBuilder Constructor
        /// </summary>
        /// <param name="path">the file path</param>
        public TrackBuilder(string path)
        {
            this.path = path;
            this.collection = new TalkCollection().GetCollection(path);
        }

        
        /// <summary>
        /// Build the Track for each day and display the result
        /// </summary>
        public void Build()
        {
            //Create a list of four session objects for each morning and afternoon sessions
            List<Session> sessionLst = new List<Session>() { new Session(1, SessionTime.AM), 
                new Session(1, SessionTime.PM),new Session(2, SessionTime.AM), new Session(2, SessionTime.PM)}; 

            //Sort the collection of Talk objects in descending order by duration
            var list = collection.OrderByDescending(x => x.Duration).ToList();

            //Traverse the collection
            while(list.Count > 0)
            {
                //Get the first Talk object
                Talk talk = list.ElementAt(0);

                foreach (Session session in sessionLst)
                {
                    //Allocating the current Talk to the first Session whit enough capacity
                    if(session.Accept(talk))
                    {
                        //Remove the Talk object
                        list.RemoveAt(0);
                        break;
                    }
                }

                if (list.Count > 0 && talk == list.ElementAt(0))
                {
                    throw new Exception("Error - could not fit the talks into the list of sessions.");
                }
            }

            //Display all tracks
            foreach (Session session in sessionLst)
            {
                session.Display();
            }
        }
    }
}
