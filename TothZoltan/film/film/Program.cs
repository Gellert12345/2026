using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace file_ot
{
    // 1. Osztály létrehozása
    public class Film
    {
        public string Cim { get; set; }
        public int Honap { get; set; }
        public int Nap { get; set; }
        public int KezdesPerc { get; set; }
        public int Hossz { get; set; }
        public string Mozi { get; set; }
        public string Varos { get; set; }

        public Film(string[] sor)
        {
            Cim = sor[0];
            Honap = int.Parse(sor[1]);
            Nap = int.Parse(sor[2]);
            KezdesPerc = int.Parse(sor[3]);
            Hossz = int.Parse(sor[4]);
            Mozi = sor[5];
            Varos = sor[6];
        }

        public override string ToString()
        {
            return $"{Cim} {Honap} {Nap} {KezdesPerc} {Hossz} {Mozi} {Varos}";
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            List<Film> filmek = new List<Film>();

            // Adatbeolvasás
            string utvonal = @"C:\Users\Info\Documents\Tóth Zoltán\film.txt";

            if (File.Exists(utvonal))
            {
                foreach (string sor in File.ReadLines(utvonal, Encoding.UTF8))
                {
                    filmek.Add(new Film(sor.Trim().Split('\t')));
                }
            }

            // Rendezés nap és kezdési idő szerint
            var rendezettLista = filmek
                .OrderBy(f => f.Nap)
                .ThenBy(f => f.KezdesPerc)
                .ToList();

            // 2. feladat - Napi filmek listázása
            int minNap = rendezettLista.Min(f => f.Nap);
            int maxNap = rendezettLista.Max(f => f.Nap);

            for (int i = minNap; i <= maxNap; i++)
            {
                Console.WriteLine($"\nMájus {i}.:");

                var napiFilmek = rendezettLista
                    .Where(f => f.Nap == i)
                    .ToList();

                for (int j = 0; j < napiFilmek.Count; j++)
                {
                    Console.WriteLine($"{j + 1}. {napiFilmek[j].Cim} : {napiFilmek[j].Mozi}");
                }
            }

            // 3. feladat - Napi összesített játékidő
            Console.WriteLine("\nNapi összesített játékidő:");

            for (int i = minNap; i <= maxNap; i++)
            {
                int osszPerc = rendezettLista
                    .Where(f => f.Nap == i)
                    .Sum(f => f.Hossz);

                Console.WriteLine($"{i - minNap + 1}. nap: {osszPerc / 60}:{osszPerc % 60:D2}");
            }

            // 4. feladat - Leghosszabb film május 10-én
            Console.WriteLine("\nLeghosszabb film        május 10-én:");

            var tizedikeiFilmek = rendezettLista
                .Where(f => f.Nap == 10)
                .ToList();

            if (tizedikeiFilmek.Any())
            {
                int maxHossz = tizedikeiFilmek.Max(f => f.Hossz);

                var leghosszabbak = tizedikeiFilmek
                    .Where(f => f.Hossz == maxHossz);

                foreach (var f in leghosszabbak)
                {
                    Console.WriteLine($"{f.Cim} {f.Hossz} perc");
                }
            }

            Console.ReadKey();
        }
    }
}