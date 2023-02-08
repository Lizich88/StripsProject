using System.Diagnostics.Metrics;
using System.Runtime.CompilerServices;

namespace Comics
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int keuzeLogin, aantalUsers = 0;
            string username, wachtwoord, usernameCheck;
            string[] login = new string[0];

            Console.WriteLine("1. Aanmelden");
            Console.WriteLine("2. Inloggen");
            keuzeLogin = Convert.ToInt32(Console.ReadLine());

            switch (keuzeLogin)
            {
                case 1:
                    StreamReader usercheck = new StreamReader("login.txt", true);
                    while (!usercheck.EndOfStream)
                    {
                        usernameCheck = usercheck.ReadLine();
                        Array.Resize(ref login, (login.Length + 1));
                        login[aantalUsers] += usernameCheck;
                        aantalUsers++;
                    }
                    usercheck.Close();

                    while (true)
                    {

                        Console.WriteLine("Geef een username in.");
                        username = Console.ReadLine();

                        for (int i = 0; i < login.Length; i++)
                        {
                            string[] checkUser = login[i].Split(';');
                            if (username == checkUser[1])
                            {
                                Console.WriteLine("Deze username bestaat al, geef een andere username in.");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Geef een wachtwoord in.");
                                wachtwoord = Console.ReadLine();

                                StreamWriter aanmelden = new StreamWriter("login.txt", true);
                                aanmelden.WriteLine(username + ";" + wachtwoord);
                                aanmelden.Close();

                                Console.WriteLine("Je account is aangemaakt!");
                                Console.ReadKey();

                                inloggen();
                            }
                        }
                    }
                case 2:
                    inloggen();

                    break;
                default:
                    Console.WriteLine("Geef een keuze om je aan te melden of in te loggen.");
                    break;
            }

        }
        static void inloggen()
        {
            int aantalUsers = 0;
            string username, wachtwoord, usernameCheck;
            string[] login = new string[0];
            string stripReeks, lijn;

            StreamReader userlogin = new StreamReader("login.txt");
            while (!userlogin.EndOfStream)
            {
                usernameCheck = userlogin.ReadLine();
                Array.Resize(ref login, (login.Length + 1));
                login[aantalUsers] += usernameCheck;
                aantalUsers++;
            }
            userlogin.Close();

            while (true)
            {
                Console.WriteLine("Geef je username in.");
                username = Console.ReadLine();
                Console.WriteLine("Geef je wachtwoord in.");
                wachtwoord = Console.ReadLine();

                for (int i = 0; i < login.Length; i++)
                {
                    string[] checkUserWachtwoord = login[i].Split(';');

                    if (checkUserWachtwoord[0] == username && checkUserWachtwoord[1] == wachtwoord)
                    {
                        Console.Clear();
                        Console.WriteLine("Welkom " + username + "!" + Environment.NewLine);


                        Console.WriteLine("Welke stripreeks wil je toevoegen aan de collectie?");
                        stripReeks = Console.ReadLine();

                        Console.Clear();
                        string naamStripReeks = stripReeks;
                        stripReeks = stripReeks.Trim();
                        stripReeks = stripReeks.Replace(" ", "");
                        stripReeks += ".txt";

                        do
                        {
                            Console.WriteLine(naamStripReeks + Environment.NewLine);
                            Console.WriteLine("1. Toevoegen van een strip.");
                            Console.WriteLine("2. Wijzigen van een strip.");
                            Console.WriteLine("3. Verwijderen van een strip.");
                            Console.WriteLine("4. Overzicht van de collectie.");
                            Console.WriteLine("5. Kies een random strip");
                            Console.WriteLine("6. Applicatie sluiten" + Environment.NewLine);



                            for (int x = 0; x < 2;)
                            {
                                Console.WriteLine("Geef je keuze: ");
                                int keuze = Convert.ToInt32(Console.ReadLine());
                                Console.Clear();
                                switch (keuze)
                                {
                                    case 1:

                                        try
                                        {
                                            Console.WriteLine("Geef de nummer van de strip in.");
                                            int nummerStrip = Convert.ToInt32(Console.ReadLine());

                                            Console.WriteLine("Geef de naam van de strip in.");
                                            string naamStrip = Console.ReadLine();

                                            Console.WriteLine("Geef de prijs van de strip in.");
                                            double prijsStrip = Convert.ToDouble(Console.ReadLine());

                                            Console.WriteLine("Geef de verschijningdatum van de strip in.");
                                            DateTime verschijningsDatum = Convert.ToDateTime(Console.ReadLine());

                                            Console.WriteLine("Geef de auteur van de strip in.");
                                            string auteurStrip = Console.ReadLine();


                                            StreamWriter writer = new StreamWriter(stripReeks, true);
                                            writer.WriteLine(nummerStrip + ";" + naamStrip + ";" + prijsStrip.ToString("0.00") + ";" + verschijningsDatum.ToString("MMMM yyyy") + ";" + auteurStrip);
                                            writer.Close();

                                            Console.WriteLine(Environment.NewLine + "De strip is aan de collectie toegevoegd!");
                                            Console.ReadKey();
                                        }
                                        catch
                                        {
                                            Console.WriteLine("Je hebt iets verkeerd ingevuld, probeer opnieuw.");
                                            Console.ReadKey();
                                        }
                                        break;

                                    case 2:
                                        string[] strip = new string[0];
                                        string[] parts = new string[5];
                                        int aantalStrips = 0;

                                        try
                                        {

                                            if (System.IO.File.Exists(stripReeks))
                                            {
                                                StreamReader stripsWijzigen = new StreamReader(stripReeks);
                                                Console.WriteLine("Stripnummer".PadLeft(15) + "Stripnaam".PadLeft(20) + "Prijs".PadLeft(15) + "Verschijningsdatum".PadLeft(25) + "Auteur".PadLeft(15));
                                                while (!stripsWijzigen.EndOfStream)
                                                {
                                                    lijn = stripsWijzigen.ReadLine();
                                                    Array.Resize(ref strip, (strip.Length + 1));
                                                    strip[aantalStrips] += lijn;
                                                    aantalStrips++;
                                                    string[] infoStrips = lijn.Split(';');
                                                    Console.WriteLine(infoStrips[0].PadLeft(15) + infoStrips[1].PadLeft(20) + infoStrips[2].PadLeft(15) + infoStrips[3].PadLeft(25) + infoStrips[4].PadLeft(15));
                                                }
                                                stripsWijzigen.Close();

                                                Console.WriteLine(Environment.NewLine + "Welk stripnummer wil je wijzigen?");
                                                int stripNummer = Convert.ToInt32(Console.ReadLine());

                                                int gevondenStrip = 0;
                                                for (int j = 0; j < strip.Length; j++)
                                                {
                                                    parts = strip[j].Split(';');
                                                    int nummerStripWijzigen = Convert.ToInt32(parts[0]);
                                                    if (nummerStripWijzigen == stripNummer)
                                                    {
                                                        Console.WriteLine("Wat wil je aanpassen? (Nummer, naam, prijs, verschijningsdatum of auteur)");
                                                        string keuzeWijzigen = Console.ReadLine();
                                                        keuzeWijzigen = keuzeWijzigen.ToLower();
                                                        switch (keuzeWijzigen)
                                                        {
                                                            case "nummer":
                                                                Console.WriteLine("Geef de nieuwe nummer van de strip in.");
                                                                parts[0] = Console.ReadLine();
                                                                break;
                                                            case "naam":
                                                                Console.WriteLine("Geef de naam van de strip in.");
                                                                parts[1] = Console.ReadLine();
                                                                break;
                                                            case "prijs":
                                                                Console.WriteLine("Geef de prijs van de strip in.");
                                                                parts[2] = Console.ReadLine();
                                                                break;
                                                            case "verschijningsdatum":
                                                                Console.WriteLine("Geef de verschijningdatum van de strip in.");
                                                                parts[3] = Console.ReadLine();
                                                                break;
                                                            case "auteur":
                                                                Console.WriteLine("Geef de auteur van de strip in.");
                                                                parts[4] = Console.ReadLine();
                                                                break;
                                                            default:
                                                                Console.WriteLine("Geef nummer, naam, prijs, verschijningsdatum of auteur in");
                                                                break;
                                                        }

                                                        strip[j] = "";
                                                        strip[j] += (parts[0] + ";" + parts[1] + ";" + parts[2] + ";" + parts[3] + ";" + parts[4]);

                                                        StreamWriter aanpassingenWriter = new StreamWriter(stripReeks);
                                                        for (int y = 0; y < strip.Length; y++)
                                                        {
                                                            aanpassingenWriter.WriteLine(strip[y]);
                                                        }
                                                        aanpassingenWriter.Close();

                                                        Console.Clear();

                                                        Console.WriteLine("De aangepaste informatie is opgeslagen." + Environment.NewLine);

                                                        if (System.IO.File.Exists(stripReeks))
                                                        {
                                                            Console.WriteLine("Stripnummer".PadLeft(15) + "Stripnaam".PadLeft(20) + "Prijs".PadLeft(15) + "Verschijningsdatum".PadLeft(25) + "Auteur".PadLeft(15));
                                                            StreamReader overzichtAangepast = new StreamReader(stripReeks);
                                                            while (!overzichtAangepast.EndOfStream)
                                                            {
                                                                string gewijzigdOverzicht = overzichtAangepast.ReadLine();
                                                                string[] infoStrips = gewijzigdOverzicht.Split(';');
                                                                Console.WriteLine(infoStrips[0].PadLeft(15) + infoStrips[1].PadLeft(20) + infoStrips[2].PadLeft(15) + infoStrips[3].PadLeft(25) + infoStrips[4].PadLeft(15));

                                                            }
                                                            overzichtAangepast.Close();


                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("Er is nog geen stripcollectie aangemaakt.");
                                                        }
                                                        Console.WriteLine(Environment.NewLine + "Druk op enter om verder te gaan.");
                                                        Console.ReadKey();

                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                        catch
                                        {
                                            Console.WriteLine(Environment.NewLine + "Er werden geen strips teruggevonden in de collectie.");
                                            Console.ReadKey();
                                        }
                                        break;

                                    case 3:
                                        string[] strips = new string[0];
                                        string[] onderdelen = new string[5];
                                        string[] aangepastOverzicht;
                                        string line;
                                        int teller = 0;
                                        aantalStrips = 0;

                                        try
                                        {

                                            if (System.IO.File.Exists(stripReeks))
                                            {
                                                StreamReader stripsVerwijderen = new StreamReader(stripReeks);
                                                Console.WriteLine("Stripnummer".PadLeft(15) + "Stripnaam".PadLeft(20) + "Prijs".PadLeft(15) + "Verschijningsdatum".PadLeft(25) + "Auteur".PadLeft(15));
                                                while (!stripsVerwijderen.EndOfStream)
                                                {
                                                    line = stripsVerwijderen.ReadLine();
                                                    Array.Resize(ref strips, (strips.Length + 1));
                                                    strips[aantalStrips] += line;
                                                    aantalStrips++;
                                                    string[] infoStrips = line.Split(';');
                                                    Console.WriteLine(infoStrips[0].PadLeft(15) + infoStrips[1].PadLeft(20) + infoStrips[2].PadLeft(15) + infoStrips[3].PadLeft(25) + infoStrips[4].PadLeft(15));
                                                }
                                                stripsVerwijderen.Close();

                                                Console.WriteLine(Environment.NewLine + "Welke stripnummer wil je verwijderen?");
                                                int teVerwijderenNummer = Convert.ToInt32(Console.ReadLine());

                                                aangepastOverzicht = new string[strips.Length - 1];

                                                for (int z = 0; z < strips.Length; z++)
                                                {
                                                    onderdelen = strips[z].Split(';');
                                                    int nummerStripVerwijderen = Convert.ToInt32(onderdelen[0]);
                                                    if (nummerStripVerwijderen != teVerwijderenNummer)
                                                    {
                                                        aangepastOverzicht[teller] = strips[z];
                                                        teller++;
                                                    }
                                                }
                                                StreamWriter verwijderdWriter = new StreamWriter(stripReeks);
                                                for (int a = 0; a < aangepastOverzicht.Length; a++)
                                                {
                                                    verwijderdWriter.WriteLine(aangepastOverzicht[a]);
                                                }
                                                verwijderdWriter.Close();

                                                Console.Clear();

                                                Console.WriteLine(Environment.NewLine + "De strip is verwijderd." + Environment.NewLine);
                                                Console.ReadKey();

                                                if (System.IO.File.Exists(stripReeks))
                                                {
                                                    Console.WriteLine("Stripnummer".PadLeft(15) + "Stripnaam".PadLeft(20) + "Prijs".PadLeft(15) + "Verschijningsdatum".PadLeft(25) + "Auteur".PadLeft(15));
                                                    StreamReader aangepastOverzichtVerwijderd = new StreamReader(stripReeks);
                                                    while (!aangepastOverzichtVerwijderd.EndOfStream)
                                                    {
                                                        string overzichtVerwijderd = aangepastOverzichtVerwijderd.ReadLine();
                                                        string[] infoStrips = overzichtVerwijderd.Split(';');
                                                        Console.WriteLine(infoStrips[0].PadLeft(15) + infoStrips[1].PadLeft(20) + infoStrips[2].PadLeft(15) + infoStrips[3].PadLeft(25) + infoStrips[4].PadLeft(15));

                                                    }
                                                    aangepastOverzichtVerwijderd.Close();


                                                }
                                                else
                                                {
                                                    Console.WriteLine("Er is nog geen stripcollectie aangemaakt.");
                                                }
                                                Console.WriteLine(Environment.NewLine + "Druk op enter om verder te gaan.");
                                                Console.ReadKey();
                                            }
                                        }
                                        catch
                                        {
                                            Console.WriteLine("Er werden geen strips teruggevonden in de collectie.");
                                            Console.ReadKey();
                                        }
                                        break;

                                    case 4:
                                        if (System.IO.File.Exists(stripReeks))
                                        {
                                            Console.WriteLine("Stripnummer".PadLeft(15) + "Stripnaam".PadLeft(20) + "Prijs".PadLeft(15) + "Verschijningsdatum".PadLeft(25) + "Auteur".PadLeft(15));
                                            StreamReader stripOverzicht = new StreamReader(stripReeks);
                                            while (!stripOverzicht.EndOfStream)
                                            {
                                                string stripLijn = stripOverzicht.ReadLine();
                                                string[] infoStrips = stripLijn.Split(';');
                                                Console.WriteLine(infoStrips[0].PadLeft(15) + infoStrips[1].PadLeft(20) + infoStrips[2].PadLeft(15) + infoStrips[3].PadLeft(25) + infoStrips[4].PadLeft(15));

                                            }
                                            stripOverzicht.Close();


                                        }
                                        else
                                        {
                                            Console.WriteLine("Er is nog geen stripcollectie aangemaakt.");
                                        }
                                        Console.WriteLine(Environment.NewLine + "Druk op enter om verder te gaan.");
                                        Console.ReadKey();
                                        break;

                                    case 5:
                                        int aantalNummerStrips = 0, randomNummer;
                                        string nummerLezen;
                                        string[] stripNummerRandom = new string[0];

                                        if (System.IO.File.Exists(stripReeks))
                                        {
                                            StreamReader randomNummerKiezen = new StreamReader(stripReeks);
                                            while (!randomNummerKiezen.EndOfStream)
                                            {
                                                nummerLezen = randomNummerKiezen.ReadLine();
                                                Array.Resize(ref stripNummerRandom, (stripNummerRandom.Length + 1));
                                                stripNummerRandom[aantalNummerStrips] += nummerLezen;
                                                aantalNummerStrips++;
                                            }
                                            randomNummerKiezen.Close();

                                            Random randomStripnummer = new Random();
                                            randomNummer = randomStripnummer.Next(1, aantalNummerStrips + 1);


                                            for (int b = 0; b < stripNummerRandom.Length; b++)
                                            {
                                                string[] infoStrips = stripNummerRandom[b].Split(';');
                                                int gekozenRandomStrip = Convert.ToInt32(infoStrips[0]);
                                                if (gekozenRandomStrip == randomNummer)
                                                {
                                                    Console.WriteLine("Het gekozen nummer voor de strip is: " + randomNummer + ".");
                                                    Console.WriteLine("De naam van deze strip is: " + infoStrips[1] + ".");
                                                }

                                            }
                                            Console.WriteLine(Environment.NewLine + "Klik op enter om verder te gaan.");
                                            Console.ReadKey();
                                        }
                                        break;

                                    case 6:
                                        Console.WriteLine(Environment.NewLine + "De applicatie word afgesloten.");
                                        Environment.Exit(0);
                                        break;

                                    default:
                                        Console.WriteLine("Je hebt een keuze gemaakt dat er niet bij staat, probeer het opnieuw.");
                                        x--;
                                        break;
                                }
                                Console.Clear();
                                break;
                            }
                        } while (true);
                    }
                }
            }
        }
    }
}