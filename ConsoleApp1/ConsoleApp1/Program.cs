using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace ConsoleApp1
{
    public class Osoba
    {
        public string Imie{ get; set; }
        public string Partner { get; set; }

        public string Los { get; set; }
        public bool Wylosowany { get; set; }

    }
    public class Dziecko
    {
        public string Imie { get; set; }
        public string Rodzice { get; set; }
        public bool Wylosowany { get; set; }
    }
    internal class Program
    {
        public static bool LosujDziecko()
        {
            try
            {
                List<Dziecko> osoby = new List<Dziecko>()
            {
                new Dziecko{ Imie = "Tymek", Rodzice = "TomekAnia" },
                new Dziecko{ Imie = "Marcysia", Rodzice = "TomekAnia" },
                new Dziecko{ Imie = "Tomek", Rodzice = "AndrzejKasia" },
                new Dziecko{ Imie = "Olek", Rodzice = "AndrzejKasia" },
            };
                List<String> rodzice = new List<string>()
                {
                    "TomekAnia", "AndrzejKasia", "BartekGosia", "Zosia"
                };



                foreach (var r in rodzice)
                {
                    var kostka = new Random();
                    var dozwoloneDzieci = osoby.Where(x => x.Rodzice != r && !x.Wylosowany).ToList();
                    var index = kostka.Next(0, dozwoloneDzieci.Count());
                    dozwoloneDzieci[index].Wylosowany = true;
                    string tekst = String.Format("{0} - Twoj los to: {1}", r, dozwoloneDzieci[index].Imie);
                    
                    File.WriteAllText(String.Format("losy\\dzieciLos{0}.txt", r), tekst);
                }


                if (osoby.Any(x => !x.Wylosowany))
                {
                    throw new Exception();
                }
            }
            catch (Exception e)
            {
                Losuj();
            }


            return true;
        }
        public static bool Losuj()
        {
            try
            {
                List<Osoba> osoby = new List<Osoba>()
            {
                new Osoba{ Imie = "Zosia" },
                new Osoba{ Imie = "Bartek", Partner = "Gosia" },
                new Osoba{ Imie = "Gosia", Partner = "Bartek" },
                new Osoba{ Imie = "Kasia", Partner = "Andrzej" },
                new Osoba{ Imie = "Andrzej", Partner = "Kasia" },
                new Osoba{ Imie = "Tomek", Partner = "Ania" },
                new Osoba{ Imie = "Ania", Partner = "Tomek" },
                new Osoba{ Imie = "Hela", Partner = "Zosia" }
            };

                List<Osoba> wylosowane = new List<Osoba>();


                foreach (var osoba in osoby)
                {
                    var kostka = new Random();
                    var dozwoloneOsoby = osoby.Where(x => x.Imie != osoba.Imie && x.Imie != osoba.Partner && !x.Wylosowany).ToList();
                    var index = kostka.Next(0, dozwoloneOsoby.Count());
                    osoba.Los = dozwoloneOsoby[index].Imie;
                    dozwoloneOsoby[index].Wylosowany = true;
                }

                foreach (var osoba in osoby)
                {
                    string tekst = String.Format("{0} - Twoj los to: {1}", osoba.Imie, osoba.Los);
                    //Console.WriteLine(tekst);
                    File.WriteAllText(String.Format("losy\\los{0}.txt",osoba.Imie), tekst);
                }

                if (osoby.Any(x=>!x.Wylosowany))
                {
                    throw new Exception();
                }
            }
            catch (Exception e)
            {
                Losuj();
            }
            

            return true;
        }
        static void Main(string[] args)
        {
            //Program.Losuj();
            Program.LosujDziecko();
        }
    }
}
