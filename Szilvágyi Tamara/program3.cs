using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp26
{
    
    
    internal class Program
    {
        static void Main(string[] args)
        {
            string utvonal = @"C:\Users\Felhasználó\Documents\Szilvágyi Tamara_C#beadando\V_adatok1.txt";
            Koncert.Hozzaadas(utvonal);
            List<Koncert> koncertek = new List<Koncert>();
            if (File.Exists(utvonal))
            {
                foreach (var sor in File.ReadLines(utvonal, Encoding.UTF8))
                {
                    koncertek.Add(new Koncert(sor));
                }
            }
            foreach (var k in koncertek)
            {
                if (k.Idotartam < 120)
                {
                    k.Idotartam = 120;
                }
            }
            File.WriteAllLines(utvonal, koncertek.Select(k => $"{ k.Varos}\t{k.Ev}\t{k.Honap}\t{k.Nap}\t{k.Kezdes}\t{k.Idotartam}"));
            var rendezett = koncertek
                .OrderBy(k => k.Ev)
                .ThenBy(k => k.Honap)
                .ThenBy(k => k.Nap)
                .ThenBy(k => k.Kezdes)
                .ToList();
            Console.WriteLine("\nKoncertek városonként:");
            var varosok = rendezett.GroupBy(k => k.Varos);
            foreach (var v in varosok)
            {
                Console.WriteLine($"\n{v.Key}:");
                foreach (var k in v)
                {
                    Console.WriteLine($"- {k}");
                }
            }
            Console.WriteLine("\nÖsszesített koncertidő városonként:");
            foreach (var v in varosok)
            {
                int ossz = v.Sum(k => k.Idotartam);
                Console.WriteLine($"{v.Key}: {ossz / 60} óra {ossz % 60} perc");

            }
            Console.WriteLine("\nLeghosszabb koncert:");
            int max = rendezett.Max(k => k.Idotartam);
            var longest = rendezett.Where(k => k.Idotartam == max);
            foreach (var k in longest)
            {
                Console.WriteLine($"{k.Varos} - {k.Idotartam} perc");
            }
            Console.ReadKey();
        }
        
    }
    public class Koncert
    {
        public string Varos { get; set; }
        public int Ev { get; set; }
        public int Honap { get; set; }
        public int Nap { get; set; }
        public string Kezdes { get; set; }
        public int Idotartam { get; set; }

        public Koncert(string sor)
        {
            var a = sor.Split('\t');
            Varos = a[0];
            Ev = int.Parse(a[1]);
            Honap = int.Parse(a[2]);
            Nap = int.Parse(a[3]);
            Kezdes = a[4];
            Idotartam = int.Parse(a[5]);
        }
        public override string ToString()
        {
            return $"{Varos} {Ev}.{Honap}.{Nap}. {Kezdes} - {Idotartam} perc";
        }
        public static void Hozzaadas(string utvonal)
        {
            Console.WriteLine("Új Koncert hozzáadás");
            Console.WriteLine("");
            Console.Write("Város:");
            string varos = Console.ReadLine();
            Console.Write("Év:");
            int ev = int.Parse(Console.ReadLine());
            Console.Write("Hónap:");
            int honap = int.Parse(Console.ReadLine());
            Console.Write("Nap:");
            int nap = int.Parse(Console.ReadLine());
            Console.Write("Kezdes:");
            string kezdes = Console.ReadLine();
            Console.Write("Időtartam:");
            int idotartam = int.Parse(Console.ReadLine());
            string ujsor = $"{varos}\t{ev}\t{honap}\t{nap}\t{kezdes}\t{idotartam}";
            File.AppendAllText(utvonal, ujsor + Environment.NewLine, Encoding.UTF8);
            Console.WriteLine("\nÚj koncert sikeresen hozzáadva a txt-hez!");

        }
    }

}

        
