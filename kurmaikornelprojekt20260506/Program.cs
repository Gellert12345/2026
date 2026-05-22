using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace konyvek
{
    
    public class Konyv
    {
        public string Cim { get; set; }
        public int Ev { get; set; }
        public int Oldal { get; set; }
        public string Szerzo { get; set; }
        public string Kategoria { get; set; }

        public Konyv(string[] sor)
        {
            Cim = sor[0];
            Ev = int.Parse(sor[1]);
            Oldal = int.Parse(sor[2]);
            Szerzo = sor[3];
            Kategoria = sor[4];
        }

        public override string ToString()
        {
            return $"{Cim} ({Ev}) - {Oldal} oldal";
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            List<Konyv> konyvek = new List<Konyv>();

            string utvonal = @"C:\Users\Kornél\source\repos\kurmaikornelprojekt20260506\konyvek.txt";

            if (File.Exists(utvonal))
            {
                foreach (string sor in File.ReadLines(utvonal, Encoding.UTF8))
                {
                    konyvek.Add(new Konyv(sor.Trim().Split('\t')));
                }
            }

            
            var rendezett = konyvek.OrderBy(k => k.Ev).ToList();

            Console.WriteLine("Könyvek év szerint:");
            foreach (var k in rendezett)
            {
                Console.WriteLine(k);
            }

            
            Console.WriteLine("\nKategóriák:");

            var kategoriak = rendezett.Select(k => k.Kategoria).Distinct();

            foreach (var kat in kategoriak)
            {
                Console.WriteLine($"\n{kat}:");
                var lista = rendezett.Where(k => k.Kategoria == kat);

                foreach (var k in lista)
                {
                    Console.WriteLine($"- {k.Cim}");
                }
            }

         
            int osszOldal = konyvek.Sum(k => k.Oldal);
            Console.WriteLine($"\nÖsszes oldal: {osszOldal}");

           
            int maxOldal = konyvek.Max(k => k.Oldal);
            var leghosszabb = konyvek.Where(k => k.Oldal == maxOldal);

            Console.WriteLine("\nLeghosszabb könyv:");
            foreach (var k in leghosszabb)
            {
                Console.WriteLine($"{k.Cim} - {k.Oldal} oldal");
            }

            Console.ReadKey();
        }
    }
}