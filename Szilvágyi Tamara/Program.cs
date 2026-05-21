using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp27
{
    public class Konyv
    {
        public string Cim { get; set; }
        public string Szerzo { get; set; }
        public int Ev { get; set; }
        public int Oldalszam { get; set; }
        public string Mufaj { get; set; }
        public int Ar { get; set; }

        public Konyv(string sor)
        {
            var a = sor.Split('\t');
            Cim = a[0];
            Szerzo = a[1];
            Ev = int.Parse(a[2]);
            Oldalszam = int.Parse(a[3]);
            Mufaj = a[4];
            Ar = int.Parse(a[5]);
        }
        public override string ToString()
        {
            return $"{Cim} {Szerzo} ({Ev}) {Oldalszam} oldal, Műfaj:{Mufaj}, Ár: {Ar}";
        }
        public static void Hozzaadas(String utvonal)
        {
            Console.WriteLine("Új könyv hozzáadása\n");
            Console.WriteLine("Cím:");
            string cim = Console.ReadLine();
            Console.WriteLine("Szerző:");
            string szerzo = Console.ReadLine();
            Console.WriteLine("Kiadás éve:");
            int ev = int.Parse(Console.ReadLine());
            Console.WriteLine("Oldalszám:");
            int oldalszam = int.Parse(Console.ReadLine());
            Console.WriteLine("Műfaj:");
            string mufaj = Console.ReadLine();
            Console.WriteLine("Ár:");
            int ar = int.Parse(Console.ReadLine());
            string ujsor = $"{cim}\t{szerzo}\t{ev}\t{oldalszam}\t{mufaj}\t{ar}";
            File.AppendAllText(utvonal, ujsor + Environment.NewLine, Encoding.UTF8);
            Console.WriteLine("\nA könyv sikeresen hozzáadva!");
        }

        internal class Program
        {
            static void Main(string[] args)
            {
                string utvonal = @"C:\Users\Felhasználó\Documents\Szilvágyi Tamara C#beadando2\könyv_adatok1.txt";
                Konyv.Hozzaadas(utvonal);
                List<Konyv> konyvek = new List<Konyv>();
                if (File.Exists(utvonal))
                {
                    foreach (var sor in File.ReadLines(utvonal, Encoding.UTF8))
                    {
                        konyvek.Add(new Konyv(sor));
                    }
                }
                Console.WriteLine("qnKönyvek műfajonként:");
                var mufajok = konyvek.GroupBy(k => k.Mufaj);
                foreach (var m in mufajok)
                {
                    Console.WriteLine($"\n{m.Key} ({m.Count()} db):");
                    foreach (var k in m)
                    {
                        Console.WriteLine($"-{k}");
                    }
                }
                Console.WriteLine("\nLeghosszabb könyv:");
                int minOldal = konyvek.Min(k => k.Oldalszam);
                foreach (var k in konyvek.Where(k => k.Oldalszam == minOldal))
                {
                    Console.WriteLine($"{k.Cim}-{k.Oldalszam} oldal");

                }
                Console.WriteLine("\nLegújabb konyv:");
                int maxEv = konyvek.Max(k => k.Ev);
                foreach (var k in konyvek.Where(k => k.Ev == maxEv))
                {
                    Console.WriteLine($"{k.Cim}-{k.Ev}");
                }
                Console.WriteLine("\nLegdragabb könyv:");
                int MaxAr = konyvek.Max(k => k.Ar);
                foreach (var k in konyvek.Where(k => k.Ar == MaxAr))
                {
                    Console.WriteLine($"{k.Cim}-{k.Ar} Ft");

                }

                double atlag = konyvek.Average (k => k.Oldalszam);
                Console.WriteLine($"\nÁtlagos oldalszám: {atlag:F1} oldal");
            }
        }
    }
}