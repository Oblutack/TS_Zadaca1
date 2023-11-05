using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Inicijalizacija niza stanova
        Stan[] stanovi = new Stan[4];

        // Kreiranje instanci različitih tipova stanova
        stanovi[0] = new NenamjestenStan(50, Lokacija.Gradsko, true);
        stanovi[1] = new NenamjestenStan(80, Lokacija.Prigradsko, true);
        stanovi[2] = new NamjestenStan(40, Lokacija.Prigradsko, true, 2000, 2);
        stanovi[3] = new NamjestenStan(80, Lokacija.Gradsko, false, 3000, 6);

        // Ispis zaglavlja tablice
        Console.WriteLine("Površina Lokacija Namješten Internet Vrijednost namještaja Broj aparata");

        // Iteriranje kroz sve stanove i ispisivanje njihovih karakteristika
        foreach (Stan stan in stanovi)
        {
            stan.Ispisi();
        }

        // Unos minimalne i maksimalne površine za pretragu
        int minPovrsina = 0;
        int maxPovrsina = 0;
        Console.WriteLine("Unesite minimalnu željenu površinu");
        while (!Int32.TryParse(Console.ReadLine(), out minPovrsina) || minPovrsina < 0)
        {
            Console.WriteLine("Unos nije ispravan");
        }
        Console.WriteLine("Unesite maksimalnu željenu površinu");
        while (!Int32.TryParse(Console.ReadLine(), out maxPovrsina) || maxPovrsina < 0)
        {
            Console.WriteLine("Unos nije ispravan");
        }

        // Iteriranje kroz stanove i ispisivanje onih koji odgovaraju kriterijima površine
        foreach (Stan stan in stanovi)
        {
            if (stan.BrojKvadrata >= minPovrsina && stan.BrojKvadrata <= maxPovrsina)
            {
                stan.Ispisi();
                Console.WriteLine("Ukupna cijena najma stana je {0:F2}.", stan.ObracunajCijenuNajma());
            }
        }

        // Čekanje na unos korisnika prije nego završi izvođenje programa
        Console.ReadLine();
    }
}

// Enumeracija za definiranje lokacija stanova
enum Lokacija
{
    Gradsko,
    Prigradsko
}

// Apstraktna klasa koja predstavlja opću strukturu stanova
abstract class Stan
{
    public int BrojKvadrata { get; }
    public Lokacija Lokacija { get; }
    public bool Namjesten { get; }
    public bool Internet { get; }

    public Stan(int brojKvadrata, Lokacija lokacija, bool namjesten, bool internet)
    {
        BrojKvadrata = brojKvadrata;
        Lokacija = lokacija;
        Namjesten = namjesten;
        Internet = internet;
    }

    // Apstraktne metode koje će biti implementirane u podklasama
    public abstract void Ispisi();
    public abstract decimal ObracunajCijenuNajma();
}

// Konkretna podklasa koja predstavlja nenamješten stan
class NenamjestenStan : Stan
{
    public NenamjestenStan(int brojKvadrata, Lokacija lokacija, bool internet)
        : base(brojKvadrata, lokacija, false, internet)
    {
    }

    public override void Ispisi()
    {
        Console.WriteLine($"{BrojKvadrata} {Lokacija} Nenamjesten {Internet}");
    }

    public override decimal ObracunajCijenuNajma()
    {
        decimal osnovnaCijena = Lokacija == Lokacija.Gradsko ? 200 : 150;
        decimal cijenaKvadrata = 1;
        decimal cijena = osnovnaCijena + BrojKvadrata * cijenaKvadrata;
        if (Internet)
        {
            cijena *= 1.02m;
        }
        return cijena;
    }
}

// Konkretna podklasa koja predstavlja namješten stan
class NamjestenStan : Stan
{
    public decimal VrijednostNamjestaja { get; }
    public int BrojAparata { get; }

    public NamjestenStan(int brojKvadrata, Lokacija lokacija, bool internet, decimal vrijednostNamjestaja, int brojAparata)
        : base(brojKvadrata, lokacija, true, internet)
    {
        VrijednostNamjestaja = vrijednostNamjestaja;
        BrojAparata = brojAparata;
    }

    public override void Ispisi()
    {
        Console.WriteLine($"{BrojKvadrata} {Lokacija} Namjesten {Internet} {VrijednostNamjestaja} {BrojAparata}");
    }

    public override decimal ObracunajCijenuNajma()
    {
        decimal osnovnaCijena = Lokacija == Lokacija.Gradsko ? 200 : 150;
        decimal cijenaKvadrata = 1;
        decimal cijena = osnovnaCijena + BrojKvadrata * cijenaKvadrata;
        if (Internet)
        {
            cijena *= 1.02m;
        }

        if (BrojAparata < 3)
        {
            cijena += 0.01m * VrijednostNamjestaja;
        }
        else
        {
            cijena += 0.02m * VrijednostNamjestaja;
        }
        return cijena;
    }
}