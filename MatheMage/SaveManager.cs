using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MatheMage
{
    class SaveManager
    {
        public static void Saver (string[] input)
        {
            input[0] = "test";
            File.WriteAllLines("Save.txt", input);

        }

        public static string[] Loader()
        {

            //List<string> output = new List<string>();
            string[] outp;

            //using (FileStream reader = new FileStream(@"Save.txt", FileMode.OpenOrCreate))
            //{
            if (File.Exists("Save.txt"))
            {
                outp = File.ReadAllLines("Save.txt", Encoding.ASCII);
            }
            else
                outp = new string[1] { "nothing"};
            //}

            return outp;
        }

        public static bool DevMode()
        {
            if (File.Exists("DevMode.txt"))
                return true;
            else
                return false;
        }
    }
}
