
/* TASK: Conference Track Management 
 * Created by Albena Roshelova
 */

using System;
using System.IO;

namespace ConferenceTrackManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                TrackBuilder track = new TrackBuilder(string.Concat(System.IO.Directory.GetCurrentDirectory(), @"\TestInput.txt"));

                //Build and display the Track outputs
                track.Build();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ops, something went wrong!!! \n[Exception message]: {0}", ex.Message);
            }
            Console.ReadKey();
        }
    }
}
