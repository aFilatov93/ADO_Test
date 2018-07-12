using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TagLib;

namespace ADO_testing_1
{
    class AudioFilesList
    {
        private string dir;
        private string[] subDirs;
        private List<AudioFile> tracks;
        private string[] tracksPaths;

        public List<AudioFile> Tracks
        {
            get
            {
                return tracks;
            }
        }

        public AudioFilesList(string directory)
        {
            dir     = directory;
            subDirs = Directory.GetFiles(@"" + dir, "*.mp3", SearchOption.AllDirectories);
            tracks  = subDirs.Select(file => new AudioFile(file)).ToList();
            tracksPaths = new string[tracks.Count];
        }

        public void ShowTags()
        {
            if (tracks.Count > 0)
            {
                foreach (var a in tracks)
                {
                    a.ShowTags();
                    Console.WriteLine();
                }
            }
            else Console.WriteLine("List is empty");
        }

        public void ShowFilesDirectories()
        {
            if (tracks.Count > 0)
            {
                for (var i = 0; i < tracks.Count; i++)
                {
                    tracksPaths[i] = tracks[i].Path;
                }

                foreach (var a in tracksPaths)
                {
                    Console.WriteLine(a);
                }
            }
            else Console.WriteLine("List is empty");
        }

        public void CreateFilesDirectoriesTxt()
        {
            if (tracks.Count > 0)
            {
                for (var i = 0; i < tracks.Count; i++)
                {
                    tracksPaths[i] = tracks[i].Path;
                }

                System.IO.File.WriteAllLines(@"" + dir + "/Total.txt", tracksPaths);

                Console.WriteLine("Total.txt created at: {0}.", dir);
            }
            else Console.WriteLine("List is empty");
        }
    }
}
