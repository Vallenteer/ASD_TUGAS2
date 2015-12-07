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
            Hashtable kata = Baca_hitung();
            //menghilangkan enter,,
            kata.Remove("");
            string select_words, editstring;
            int edit_toleran;

            bool kondisi;
            int cek;
            //looping user supaya tidak exit tanpa perintahnya user
            while (true)
            {
                cek = 0;
                do
                {
                    Console.Clear();
                    Console.WriteLine("Untuk keluar ketikan -1");
                    Console.Write("Masukan Kata : ");
                    select_words = Console.ReadLine();
                    if (select_words != "" && select_words != "\t")
                    {
                        cek=1;
                    }
                } while (cek==0);
                
                if (select_words == "-1")
                {
                    Environment.Exit(0);
                }
                do
                {
                    Console.Clear();
                    Console.WriteLine("Untuk keluar ketikan -1");
                    Console.WriteLine("Masukan Kata : {0}", select_words);
                    Console.Write("Masukan Edit Distance maksimal ( minimum 0 ) : ");
                    editstring = Console.ReadLine();
                    kondisi = int.TryParse(editstring, out edit_toleran);
                    if (kondisi == true)
                    {
                        if (edit_toleran < -1)
                        {
                            kondisi = false;
                        }
                        continue;
                    }
                    Console.WriteLine("Angka yang anda masukan salah!");
                    Console.WriteLine("Tekan sembarang untuk memasukan Edit Distance maksimal kembali...");
                    Console.ReadLine();
                   
                } while (kondisi==false);

                if (edit_toleran == -1)
                {
                    Environment.Exit(0);
                }
                Console.WriteLine("Kata\t\tJumlah Edit Distance\t\tJumlah Kata dalam Text");
                Console.WriteLine("=======================================================================================");
                masuk_kata(select_words, edit_toleran, kata);
                Console.Write("Tekan Sembarang untuk melihat Edit Distance lagi..");
                Console.ReadKey();
            }
        }

        public static void masuk_kata(string select_words, int edit_toleran, Hashtable kata)
        {
            List<string> list_kata = new List<string>();
            int counter = 0;
            int batas = 0;
            int jumlahbateskata = 0;
            int batesmaksimal=0;
            while (batas <= edit_toleran)
            {
                foreach (string key in kata.Keys)
                {
                    //cari yang sesuai edit distance sesuai batas,
                    int editdistance = edit_distance(select_words, key);
                    if (editdistance > batesmaksimal)
                    {
                        batesmaksimal = editdistance;
                    }
                    if (editdistance == batas )
                    {

                        list_kata.Add((key));
                      
                        counter++;
                        jumlahbateskata++;
                    }
                }
                if (edit_toleran > batesmaksimal)
                {
                    edit_toleran = batesmaksimal;
                }
                if (jumlahbateskata != 0)
                {
                    //cetak sesuai batas
                    string[] kata_array = list_kata.ToArray();
                    Quicksort(kata_array, 0, kata_array.Length - 1);
                    foreach (string isi in kata_array)
                    {
                        Console.WriteLine("{0}" + "\t\t\t" + "{1}" + "\t\t\t\t" + "{2}", isi, batas, kata[isi]);
                    }
                    //menghilangkan isi list untuk berikutnya
                    list_kata.Clear();
                   
                }
                batas++;
                jumlahbateskata = 0;
            }
            if (counter == 0)
            {
                Console.WriteLine("Tidak ada Kata yang sesuai");
                batas = edit_toleran;
            }
            Console.WriteLine("Jumlah Kata yang mempunyai Edit distance maksimal {0} dari kata \"{1}\" adalah = {2}",edit_toleran,select_words,counter );

            int panjang1 = list_kata.Count;

        }


        public static int edit_distance(string kata1, string kata2)
        {
            // sumber : http://www.dotnetperls.com/

            int asli = kata1.Length;
            int banding = kata2.Length;
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

            Console.WriteLine("LOADING....");
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
            //Referensi dari http://snipd.net/quicksort-in-c
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
