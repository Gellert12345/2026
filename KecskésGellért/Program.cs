using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace KosarlabdaBeadando
{
    public class Merkozes
    {
        public string Csapat{ get; set; }
        public string Ellenfel{ get; set; }
        public int Honap{ get; set; }
        public int Nap{ get; set; }
        public int Pont{ get; set; }
        public string Varos{ get; set; }

        public Merkozes(string sor)
        {
            string[] a=sor.Split('\t');

            Csapat = a[0];
            Ellenfel=a[1];
            Honap=int.Parse(a[2]);
            Nap = int.Parse(a[3]);
            Pont=int.Parse(a[4]);
            Varos = a[5];
        }

        public override string ToString()
        {
            return Csapat+"-"+Ellenfel+" | "+Pont+" pont";
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            List<Merkozes> meccsek = new List<Merkozes>();

            string utvonal=@"C:\kosar\meccsek.txt";

            if(File.Exists(utvonal))
            {
                foreach(string sor in File.ReadLines(utvonal,Encoding.UTF8))
                {
                    meccsek.Add(new Merkozes(sor));
                }
            }

            var rendezett = meccsek.OrderBy(m => m.Nap)
            .ThenBy(m=>m.Pont).ToList();

            Console.WriteLine("Kosárlabda mérkőzések:\n");

            foreach(var m in rendezett)
            {
                Console.WriteLine(m.ToString());
            }

            Console.WriteLine("\nVárosonkénti meccsek:");

            var varosok = rendezett.GroupBy(m=>m.Varos);

            foreach(var v in varosok)
            {
                Console.WriteLine("\n"+v.Key);

                foreach(var m in v)
                {
                    Console.WriteLine("- "+m.Csapat+" vs "+m.Ellenfel);
                }
            }

            Console.WriteLine("\nÖsszes pont városonként:");

            foreach(var v in varosok)
            {
                int osszPont=v.Sum(m=>m.Pont);

                Console.WriteLine(v.Key+": "+osszPont+" pont");
            }

            Console.WriteLine("\nLegtöbb pontot szerző csapat:");

            int maxPont = rendezett.Max(m=>m.Pont);

            var legjobb = rendezett.Where(m=>m.Pont==maxPont);

            foreach(var m in legjobb)
            {
                Console.WriteLine(m.Csapat+" - "+m.Pont+" pont");
            }

            Console.ReadKey();
        }
    }
}
Pécs Tigers	Szeged Bulls	5	12	88	Pécs
    Budapest Wolves	Győr Hawks	5	14	76	Budapest
    Debrecen Stars	Miskolc Lions	5	16	92	Debrecen
    Pécs Tigers	Kaposvár Sharks	5	18	81	Pécs
    Szeged Bulls	Budapest Wolves	5	20	95	Szeged