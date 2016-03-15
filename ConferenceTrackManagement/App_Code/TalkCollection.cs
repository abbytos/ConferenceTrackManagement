using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace ConferenceTrackManagement
{
    /// <summary>
    /// Collection of Talk objects. This class implements IEnumerable<Talk> so that it can be used ???? 
    /// </summary>
    public class TalkCollection : IEnumerable<Talk>
    {
        //List of Talk objects
        private List<Talk> talkList;

        /// <summary>
        /// TalkCollection Constructor
        /// </summary>
        public TalkCollection()
        {
            //Initialization
            talkList = new List<Talk>();
        }

        /// <summary>
        /// Build a collection of Talk objects
        /// </summary>
        /// <param name="path">the file path</param>
        /// <returns>the collection of Talk objects</returns>
        public TalkCollection GetCollection(string path)
        {
            string line;
            var collection = new TalkCollection();
            
            if (path == string.Empty)
            {
                throw new ArgumentNullException("Error - file path is empty.");
            }

            StreamReader file = null;

            try
            {
                // Read the file 
                file = new StreamReader(path);

                while ((line = file.ReadLine()) != null)
                {
                    //Create talk object
                    collection.talkList.Add(CreateObject(line));
                }
            }
            catch (IOException)
            {
                throw;
            }
            finally
            {
                //close the file
                if (file != null)
                {
                    file.Close();
                }
            }

            //and retrun the talk collection
            return collection;
        }

        /// <summary>
        /// Implementation for the GetEnumerator method.
        /// </summary>
        /// <returns>an enumerator that iterates through the collection</returns>
        public IEnumerator<Talk> GetEnumerator()
        {
            return talkList.GetEnumerator();
        }

        /// <summary>
        /// Implementation for the GetEnumerator method.
        /// </summary>
        /// <returns>the current object GetEnumerator()</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Creates a simple business object
        /// </summary>
        /// <param name="line">the text</param>
        /// <returns>the object</returns>
        private Talk CreateObject(string line)
        {
            var talk = new Talk()
            {
                Name = line,
                Duration = GetDuration(line)
            };
            return talk;
        }

        /// <summary>
        /// Parser to extract the duration time of each talk
        /// </summary>
        /// <param name="line">the line to parse</param>
        /// <returns>the duration time</returns>
        private int GetDuration(string line)
        {
            string duration = string.Empty;
            string[] items = line.Split(' ');

            foreach (string item in items)
            {
                if (item.Contains("min"))
                {
                    duration = item.Substring(0, item.Length - 3);
                }

                if (item.Equals("lightning"))
                {
                    duration = "5";
                }
            }
            return Int32.Parse(duration);
        }
    }
}
