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
// buat hashtable
using System.Collections;

namespace ASD_TUGAS2
{
    class Program
    {
      
        static void Main(string[] args)
        {
            Console.WriteLine("LOADING....");
            Baca_hitung();

            // bkin layar buat masukin kata yang dicari
            // lalu edit distance yang diinginkan

            // buat koding untuk edit distance 
            // buat koding untuk sorting
        }
        public static void Baca_hitung()
        {
            //pattern buat split nya ketmu buahahah
            string line;
            string pattern = @"[\s\n\p{P}-[']]+";
            Regex rgx = new Regex(pattern);
            // ampe sini
            Hashtable kata = new Hashtable();
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
                     //Mendata(element);

                    //coba pke hashtable
                    if (kata.ContainsKey(element.ToLower()))
                    {
                        int nilaidulu = (int)kata[element.ToLower()];
                        kata[element.ToLower()] = nilaidulu + 1;

                    }
                    else
                    {
                        kata.Add(element.ToLower(), 1);
                    }
                     
                }
                


            }
            sr.Close();
            //cuma ngetes aja (bisa dihapus pas kumpul)
                    //foreach (DictionaryEntry entry in kata)
                    //{
                    //    Console.WriteLine("{0}, {1}", entry.Key, entry.Value);
                    //    Mendata(entry.Key.ToString(), entry.Value.ToString());
                    //}
            
            

            Console.WriteLine("selesai");
            Console.ReadLine();
            //Console.Clear();
        }
        
        // buat cek, jadi masukin ke file,, kalau ud jadi bisa diapus
        public static void Mendata(string input, string input2)
        {
            string dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string filecp = dir + @"\data.txt";
            //StreamReader sr = new StreamReader(file);
            if (!File.Exists(filecp))
            {
                // Create a file to write to. kalau belom ada filenya 
                using (StreamWriter swnew = File.CreateText(filecp))
                {
                    
                    swnew.WriteLine(input + "\t" + input2);
                }
            }
            //kalau ud ada file yang mau ditulis
            else
            {
                using (FileStream fs = new FileStream(filecp, FileMode.Append, FileAccess.Write))
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine(input + "\t" + input2);
                }
            }
        }
    }
}
