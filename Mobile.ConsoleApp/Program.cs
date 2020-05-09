using System;
using MobileLibrary;

namespace ConsoleApp
{
    internal class Program
    {
        /// <summary>
        /// Takes the reference of an array of Mobilephones and checks if a new phones can be saved.
        /// If it is possible a new phones will be created, if not the User will be informed.
        /// </summary>
        /// <param name="mobilephones"></param>
        private static void SaveMobilePhone(ref Mobile[] mobilephones)
        {
            Console.Clear();
            int i = 0;

            //Look for free position
            while (mobilephones[i] != null && i < mobilephones.Length)
            {
                i++;
            }

            //Check if Array is full or not
            if (mobilephones[i] == null)
            {
                Console.Write("Bitte Nummer des Telefons eingeben: ");
                mobilephones[i] = new Mobile(Console.ReadLine());
                Console.Write("Bitte Namen des Telefons eingeben: ");
                mobilephones[i].Name = Console.ReadLine();
                Console.Write("Telefon wurde gespeichert! Zum Fortsetzen beliebige Taste drücken . . .");
            }
            else
            {
                Console.WriteLine("Maximale Anzahl erreicht. Telefon konnte nicht gespeichert werden");
                Console.Write("Zum Fortsetzen beliebige Taste drücken . . .");
            }
            Console.ReadKey();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="mobilephones"></param>
        private static void StartCall(ref Mobile[] mobilephones)
        {
            string nameOfActive;
            string nameOfPassive;
            int indexActive = 0;
            int indexPassive = 0;

            Console.Clear();
            Console.Write("Bitte Namen des Anrufers eingeben: ");
            nameOfActive = Console.ReadLine();

            while (mobilephones[indexActive] != null && !mobilephones[indexActive].Name.Equals(nameOfActive) && indexActive < mobilephones.Length)
            {
                indexActive++;
            }
            if (mobilephones[indexActive] != null && mobilephones[indexActive].Name.Equals(nameOfActive))
            {
                Console.Write("Bitte Namen des Gesprächpartners eingeben: ");
                nameOfPassive = Console.ReadLine();
                while (!mobilephones[indexPassive].Name.Equals(nameOfPassive) && indexPassive < mobilephones.Length && mobilephones[indexPassive] != null)
                {
                    indexPassive++;
                }
                if (mobilephones[indexPassive].Name.Equals(nameOfPassive))
                {
                    bool callStatus = mobilephones[indexActive].StartCallTo(mobilephones[indexPassive]);
                    if (callStatus)
                    {
                        Console.WriteLine($"Das Telefonat zwischen {mobilephones[indexActive].Name} und {mobilephones[indexPassive].Name} wurde erfolgreich gestartet.");
                    }
                    else
                    {
                        Console.WriteLine("Es konnte keine Gesprächsverbindung hergestellt werden.");
                    }
                }
                else
                {
                    Console.WriteLine("Es gibt kein Telefon mit diesem Namen.");
                }
            }
            else
            {
                Console.WriteLine("Ein Telefon mit diesem Namen wurde nicht gefunden.");
            }

            Console.Write("Zum Fortsetzen beliebige Taste drücken . . .");
            Console.ReadKey();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="mobilephones"></param>
        private static void StopCall(ref Mobile[] mobilephones)
        {
            string name;
            int position = 0;

            Console.Clear();
            Console.Write("Name des Telefons eingeben: ");
            name = Console.ReadLine();
            while (!mobilephones[position].Name.Equals(name) && position < mobilephones.Length && mobilephones[position] != null)
            {
                position++;
            }

            if (mobilephones[position].Name.Equals(name))
            {
                bool hasCallStopped = mobilephones[position].StopCall();
                if (hasCallStopped)
                {
                    Console.WriteLine($"Das Telefonat von {mobilephones[position].Name} wurde beendet.");
                }
                else
                {
                    Console.WriteLine("Stoppen nicht möglich, da kein laufendes Telefonat");
                }
            }
            else
            {
                Console.WriteLine("Es wurde kein Gerät mit diesem Namen gefunden.");
            }

            Console.Write("Zum Fortsetzen beliebige Taste drücken . . .");
            Console.ReadKey();
        }

        private static void ShowInvoice(ref Mobile[] mobilephones)
        {
            string name;
            int position = 0;

            Console.Clear();
            Console.Write("Name des Telefons eingeben: ");
            name = Console.ReadLine();
            while (!mobilephones[position].Name.Equals(name) && position < mobilephones.Length && mobilephones[position] != null)
            {
                position++;
            }

            if (mobilephones[position].Name.Equals(name))
            {
                Console.WriteLine("");
                Console.WriteLine($"Sekunden aktiv:          {mobilephones[position].SecondsActive}");
                Console.WriteLine($"Sekunden passiv:         {mobilephones[position].SecondsPassive}");
                Console.WriteLine($"Gesamtrechnung in Cents: {mobilephones[position].CentsToPay}");
                Console.WriteLine("");
            }
            else
            {
                Console.WriteLine("Es wurde kein Gerät mit diesem Namen gefunden.");
            }

            Console.Write("Zum Fortsetzen beliebige Taste drücken . . .");
            Console.ReadKey();
        }

        public static void Main(string[] args)
        {
            string input;
            Mobile[] mobilephones = new Mobile[10];

            do
            {
                Console.Clear();
                Console.WriteLine("Mobiltelefone");
                Console.WriteLine("===============================================");
                Console.WriteLine("(1) Neues Telefon erstellen");
                Console.WriteLine("(2) Gespräch zwischen 2 Telefonen starten");
                Console.WriteLine("(3) Laufendes Gespräch beenden");
                Console.WriteLine("(4) Rechnung für Telefon anzeigen");
                Console.WriteLine("===============================================");
                Console.Write("Bitte Auswahl aus 1-4 treffen(# zum Beenden): ");
                input = Console.ReadLine().ToLower().Trim();
                switch (input)
                {
                    case "1":
                        SaveMobilePhone(ref mobilephones);
                        break;

                    case "2":
                        StartCall(ref mobilephones);
                        break;

                    case "3":
                        StopCall(ref mobilephones);
                        break;

                    case "4":
                        ShowInvoice(ref mobilephones);
                        break;

                    case "#":
                        break;

                    default:
                        Console.Write("Ungültige Eingabe! Zum Fortsetzen beliebige Taste drücken . . .");
                        Console.ReadKey();
                        break;
                }
            } while (input != "#");
        }
    }
}