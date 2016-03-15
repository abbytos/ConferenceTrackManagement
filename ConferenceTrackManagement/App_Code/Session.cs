using System;
using System.Collections.Generic;

namespace ConferenceTrackManagement
{

    /// <summary>
    /// SessionTime consist a set of AM and PM constants 
    /// </summary>
    public enum SessionTime
    {
        AM,
        PM
    }

    /// <summary>
    /// The Session class is responsible for creating multiple talks.
    /// </summary>
    public class Session
    {
        //track day
        private int day;
        //balance value
        private int balance;
        //Morning sessions begin at 9am and must finish by 12 noon, for lunch.
        private const int timeAM = 9;
        //Lunch session start at 12pm
        private const int timeLunch = 12;
        //Afternoon sessions begin at 1pm and must finish in time for the networking 
        private const int timePM = 13;
        //The networking event can start no earlier than 4:00 and no later than 5:00 
        private const int timeNetwork = 17;
        //Lunch Time
        private const string lunchTime = "Lunch Time";
        //Network Event
        private const string networkEvent = "Networking Event";
        //morning session is 3 hrs = 180 minutes
        private const int morningSessionDuration = 180;
        //afternoon session is 4 hrs = 240 min
        private const int afternoonSessionDuration = 240;
        //Session time (AM/PM)
        private SessionTime sessionTime;
        //List of Talk objects
        private List<Talk> scheduledTalks;


        /// <summary>
        /// Session constructor. 
        /// </summary>
        /// <param name="day">the track day, can be 1 or 2</param>
        /// <param name="sessionTime">the session time, can be AM / PM</param>
        public Session(int day, SessionTime sessionTime)
        {
            this.day = day;
            this.sessionTime = sessionTime;
            scheduledTalks = new List<Talk>();
            balance = sessionTime == SessionTime.AM ? morningSessionDuration : afternoonSessionDuration;
        }

        /// <summary>
        /// Accept the Talk object
        /// </summary>
        /// <param name="talk">the Talk object</param>
        /// <returns>true if the item has been added to list, otherwise false</returns>
        public bool Accept(Talk talk)
        {
            if (talk.Duration <= balance)
            {
                //Add the talk object
                scheduledTalks.Add(talk);

                //Decrese the balance value
                balance -= talk.Duration;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Display the tracks.
        /// </summary>
        public void Display()
        {
            if (sessionTime == SessionTime.AM)
            {
                Console.WriteLine("\nTrack {0}", day);
            }

            DateTime dt = DateTime.Today.AddHours(sessionTime == SessionTime.AM ? timeAM : timePM);
            
            foreach (Talk task in scheduledTalks)
            {
                //Display the start time and name of the talk
                Console.WriteLine("{0} {1}", dt.ToString("hh:mmtt"), task.Name);

                //Add the duration time to DateTime
                dt = dt.AddMinutes(task.Duration);
            }
            //is it a lunch time,
            if (sessionTime == SessionTime.AM)
            {
                dt = Max(dt, DateTime.Today.AddHours(timeLunch));
            }
            //or time for networking event.
            else
            {
                dt = Max(dt, DateTime.Today.AddHours(timeNetwork));
            }

            //Display the lunch time or network event depends on the session time
            Console.WriteLine("{0} {1}", dt.ToString("hh:mmtt"), sessionTime == SessionTime.AM ? lunchTime : networkEvent);
        }

        /// <summary>
        /// Compare DateTime objects
        /// </summary>
        /// <param name="dateTime1">the dateTime1</param>
        /// <param name="dateTime2">the dateTime2</param>
        /// <returns>if the values is 0 return dateTime1, otherwise return dateTime2</returns>
        private DateTime Max(DateTime dateTime1, DateTime dateTime2)
        {
            return DateTime.Compare(dateTime1, dateTime2) < 0 ? dateTime1 : dateTime2;
        }
    }
}
