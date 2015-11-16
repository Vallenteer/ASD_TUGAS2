using System;
using System.Data;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Text.RegularExpressions;

namespace ASD_TUGAS2
{
    class Program
    {
        // setting warna console
        [DllImport("kernel32.dll", ExactSpelling = true)]

        private static extern IntPtr GetConsoleWindow();
        private static IntPtr ThisConsole = GetConsoleWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]

        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        private const int HIDE = 0;
        private const int MAXIMIZE = 3;
        private const int MINIMIZE = 6;
        private const int RESTORE = 9;
        static void Main(string[] args)
        {
            //setting ukuran console,, gk jalan... somehow
            //Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            //ShowWindow(ThisConsole, MAXIMIZE);
            ////setting warna console dan tulisan
            //Console.BackgroundColor = ConsoleColor.White;
            //Console.ForegroundColor = ConsoleColor.Black;
            //masukin file
            Console.WriteLine("LOADING....");
            Baca_hitung();
        }
        public static void Baca_hitung()
        {
            //pattern buat split nya ketmu buahahah
            string line;
            string pattern = @"[\s\n\p{P}-[']]+";
            Regex rgx = new Regex(pattern);
            // ampe sini

            string dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string file = dir + @"\hound.txt";
            StreamReader sr = new StreamReader(file);
            while ((line = sr.ReadLine()) != null)
            {
                string[] result = rgx.Split(line);
                //membuat baris list kata2 //disini yang lama...
                foreach (string element in result)
                {
                    //test biar tau waktu baca, cm butuh paling lama 5 detik,
                    //Console.WriteLine("a");
                    // ini yang lama, dibawah ini buat tulis ke file
                     Mendata(element);
                }
                


            }
            sr.Close();

            //string[] words = rgx.Split(text);
            //Console.WriteLine(words[0]);
            //Console.WriteLine(words[1]);
            //Console.WriteLine(words[2]);
            //Console.WriteLine(words[3]);
            Console.WriteLine("selesai");
            Console.ReadLine();
            //Console.Clear();



        }

        public static void Mendata(string input)
        {
            string dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string filecp = dir + @"\data.txt";
            //StreamReader sr = new StreamReader(file);
            if (!File.Exists(filecp))
            {
                // Create a file to write to. kalau belom ada filenya 
                using (StreamWriter swnew = File.CreateText(filecp))
                {
                    
                    swnew.WriteLine(input);
                }
            }
            //kalau ud ada file yang mau ditulis
            else
            {
                using (FileStream fs = new FileStream(filecp, FileMode.Append, FileAccess.Write))
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine(input);
                }
            }
        }
    }
}
