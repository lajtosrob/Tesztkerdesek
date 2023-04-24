using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tesztkerdesek
{
    public class Kerdes
    {
        public string kerdes { get; set; }
        public int valasz { get; set; }
        public int pontszam { get; set; }

        public string kategoria { get; set; }


        public Kerdes(string sor1, string sor2)
        {
            //  konstruktor

            //  Mikor adták ki az Aranybullát?  ---> sor1
            //  1222 1 történelem   ----> sor2

            this.kerdes = sor1;

            string[] adatok = sor2.Split(" ");   // a 2. sorban több adat van, ezért feldaraboljuk őket
            this.valasz = int.Parse(adatok[0]);
            this.pontszam = int.Parse(adatok[1]);
            this.kategoria = adatok[2];
        }

    }
}