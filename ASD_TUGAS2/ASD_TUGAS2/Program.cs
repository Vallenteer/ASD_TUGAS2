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
            //Hashtable kata= Baca_hitung();
            
            Console.Clear();
            // bkin layar buat masukin kata yang dicari
            // lalu edit distance yang diinginkan
           
            string select_words;
            int edit_toleran;
            Console.Write("Masukan Kata : ");
            select_words = Console.ReadLine();
            Console.Write("Masukan Edit Distance ( minimum 0 ) : ");
            edit_toleran = Convert.ToInt32(Console.ReadLine());

            // array ini bsa di sort, makasih :D tapi lom ada nilai edit distance .-.
            string[] kata_array= masuk_kata(select_words,edit_toleran);

            //foreach (var item in kata_array)
            //{
            //    Console.WriteLine(item);
            //}
            //buat bandingin dan buat keluarin, not sorting yet...



           
            Console.ReadLine();
        }

        public static string[] masuk_kata(string select_words, int edit_toleran)
        {
            List<string> list_kata = new List<string>();
       
            Hashtable kata = Baca_hitung();
            int counter = 0;
            //buat bandingin dan buat keluarin, not sorting yet...
            foreach (string key in kata.Keys)
            {
                int editdistance = edit_distance(select_words, key);
                if (editdistance <= edit_toleran)
                {
                    
                    list_kata.Add((key));
                    //Console.WriteLine("{0} \t  {1} \t {2}", key, editdistance, kata[key]);
                    counter++;
                }
            }
            if (counter == 0)
            {
                Console.WriteLine("Tidak ada Kata yang sesuai");
                
            }
            string[] kata_array = list_kata.ToArray();
            return kata_array ;
        }


        public static int edit_distance( string kata1, string kata2)
        {
            // sumber : http://www.dotnetperls.com/

            int asli = kata1.Length;
            int  banding= kata2.Length;
            int[,] toleransi = new int[asli + 1, banding + 1];

            // Step 1
            if (asli == 0)
            {
                return banding;
            }

            if (banding == 0)
            {
                return asli;
            }

            // Step 2
            for (int i = 0; i <= asli; toleransi[i, 0] = i++)
            {
            }

            for (int j = 0; j <= banding; toleransi[0, j] = j++)
            {
            }

            // Step 3
            for (int i = 1; i <= asli; i++)
            {
                //Step 4
                for (int j = 1; j <= banding; j++)
                {
                    // Step 5
                    int cost = (kata2[j - 1] == kata1[i - 1]) ? 0 : 1;

                    // Step 6
                    toleransi[i, j] = Math.Min(
                        Math.Min(toleransi[i - 1, j] + 1, toleransi[i, j - 1] + 1),
                        toleransi[i - 1, j - 1] + cost);
                }
            }
            // Step 7
            return toleransi[asli, banding];

        
        }
        static Hashtable Baca_hitung()
        {

           // Console.WriteLine("LOADING....");
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
                //Quicksort(result, 0, result.Length - 1); //harusnya yang baris ini udah bkin sort di sblum hash

                foreach (string element in result)
                {

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
           // Console.WriteLine("selesai");
            //Console.Clear();
            return kata;
            //Console.ReadLine();
            
        }

        public static void Quicksort(IComparable[] elements, int left, int right)
        {
            int i = left, j = right;
            IComparable pivot = elements[(left + right) / 2];

            while (i <= j)
            {
                while (elements[i].CompareTo(pivot) < 0)
                {
                    i++;
                }

                while (elements[j].CompareTo(pivot) > 0)
                {
                    j--;
                }

                if (i <= j)
                {
                    // Swap
                    IComparable tmp = elements[i];
                    elements[i] = elements[j];
                    elements[j] = tmp;

                    i++;
                    j--;
                }
            }

            // Recursive calls
            if (left < j)
            {
                Quicksort(elements, left, j);
            }

            if (i < right)
            {
                Quicksort(elements, i, right);
            }
        }
    }
}
