using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using TagLib;
using System.Data.SqlClient;
using System.Data;

namespace ADO_testing_1
{
    class Program
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        static SqlConnection connection = new SqlConnection(connectionString);

        static void Main(string[] args)
        {
            //DBManipulation.GetConnectionInfo();
            //Console.Read();
            
            bool switcher = true;

            string directory = @"E:\Musick";

            while (switcher)
            {
                try
                {
                    AudioFilesList testAu = new AudioFilesList(directory);
                    if (testAu.Tracks.Count > 0)
                    {
                        Console.WriteLine("Success1..");

                        foreach(var Au1 in testAu.Tracks)
                        {
                            DBManipulation.InsertTrack(Au1);
                        }

                        Console.WriteLine("Success2..");

                        switcher = false;
                    }
                    else Console.WriteLine("Folders doesn't contain .mp3 files");
                }
                catch (NullReferenceException ex)
                {
                    ex = new NullReferenceException();
                    Console.WriteLine("Null Reference");
                }
            }

            Console.Read();
        }
    }
}
