using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tesztkerdesek
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Kerdes> kerdesek = new List<Kerdes>();
        Kerdes kerdes6 = null; // Ha nem írnánk ide a "null"-t, a kezdő érték akkor is null lenne.
        public MainWindow()
        {
            InitializeComponent();
            Feladat1();
            Feladat2();
            Feladat3();
            Feladat4();
            Feladat5();
            Feladat8();

        }

        private void Feladat1()
        {

            StreamReader sr = new StreamReader("tesztkerdesek.txt");
            while (!sr.EndOfStream)
            {
                string sor1 = sr.ReadLine();
                string sor2 = sr.ReadLine();

                kerdesek.Add(new Kerdes(sor1, sor2));
            }
            sr.Close();
        }

        //-------------------------------------------------------------------------------------------------------------
        private void Feladat2()  // Hány feladat van az adatfájlban?
        {
            lblfeladat2.Content = $"Az állományban {kerdesek.Count} db kérdés van.";
        }
        //-------------------------------------------------------------------------------------------------------------
        private void Feladat3() // Határozza meg hány történelmi kérdés van a feladatban,
                                // s ezek közül hány feladat ér 1, 2 ill. 3 pontot.
                                // Írassa ki képernyőre is.
        {
            int pont1 = 0;
            int pont2 = 0;
            int pont3 = 0;

            foreach (Kerdes kerdes in kerdesek)
            {
                if (kerdes.kategoria == "történelem")
                {
                    if (kerdes.pontszam == 1)
                        pont1++;
                    else if (kerdes.pontszam == 2)
                        pont2++;
                    else if (kerdes.pontszam == 3)
                        pont3++;
                }
                lblfeladat3.Content = $"Az adatfájlban {pont1 + pont2 + pont3} történelem kérdés van\n " +
                    $"1 pontos kérdésből {pont1} feladat,\n " +
                    $"2 pontos kérdésből {pont2} feladat,\n " +
                    $"3 pontos kérdésből {pont3} feladat van.";
            }
        }
        //--------------------------------------------------------------------------------------------------------------------   
        private void Feladat4()  // Határozza meg a minimum és maximum értékeket.
        {
            int minErtek = kerdesek[0].valasz;
            int maxErtek = kerdesek[1].valasz;
            for (int i = 1; i < kerdesek.Count; i++)
            {
                if (minErtek > kerdesek[i].valasz)
                    minErtek = kerdesek[i].valasz;

                if (maxErtek < kerdesek[i].valasz)
                    maxErtek = kerdesek[i].valasz;

            }
            lblfeladat4.Content = string.Format("A legmagasabb érték a válaszok között: {0}\n" +
                                                 " A legalacsonyabb érték a válaszok között : {1}", maxErtek, minErtek);

        }
        //-----------------------------------------------------------------------------------------------------------------
        private void Feladat5()  // ComboBox tartalmának feltöltése.

        {                         //  HashSet --->  Hiába adom hozzá ugyanazt az értéket,
                                  //  akkor is csak 1x fog a listában szerepelni.

            HashSet<string> kategoriak = new HashSet<string>();
            foreach (Kerdes k in kerdesek)
            {
                kategoriak.Add(k.kategoria);
            }
            foreach (string ka in kategoriak)
            {
                cbofeladat6.Items.Add(ka);
            }

        }
        //----------------------------------------------------------------------------------------------------------------------

        /*  6. feladat: A kiválasztott témakörből véletlenszerűen sorsoljon ki egy kérdést!
                        Sorsoláskor ügyeljen arra, hogy az adott témakörbe eső valamennyi feladatnak
                        legyen esélye.
                        Írassa ki a kérdést és kérje be a felhasználó válaszát.
                        Az ellenőrzés gombra kattintva felugró ablakban jelenjen meg a válaszra 
                        kapott pontszám, valamint "Helytelen válasz esetén o pont jár". 
                        A program indításakor az ellenőrző gomb legyen inaktív és csak a kérdés 
                        után legyen elérhető.  */

        private void cbofeladat6_ComboBox(object sender, SelectionChangedEventArgs e)
        {
            Random rnd = new Random();
            List<Kerdes> kategoriaKerdesek = new List<Kerdes>();
            foreach (Kerdes k in kerdesek)
            {
                if (k.kategoria == cbofeladat6.SelectedItem.ToString())
                    kategoriaKerdesek.Add(k);

            }
            int i = rnd.Next(kategoriaKerdesek.Count);   // 0 <= i < kategoriaKerdesek


            // Kerdes kerdes6 = kerdesek[i];   ----> 

            /* Mivel ez a deklaráció kötődik a következő feladathoz, mint kérdés-válasz kapcsolat,
               de ott nem fog látszani, ezért a classba (fent) a függvényt ki kell vinni.
               Miután kivittük a classba, magára a változó deklarálására  így már nem lesz szükség, 
               hanem  megadjuk, h ennek az oszálynak a változója legyen ----> */

            this.kerdes6 = kategoriaKerdesek[i];
            lblfeladat7.Content = this.kerdes6.kerdes;

        }

        //--------------------------------------------------------------------------------------------------------------
        private void btnEllenorzes_Click(object sender, RoutedEventArgs e)
        {
            if (txtValasz.Text == this.kerdes6.valasz.ToString())  // Mivel a típusok eltérőek,a txtValasz.Text -- string, a kerdes6 -- int, 
                                                                   // ezért valamelyik oldalt konvertálni kell. 
                                                                   // Figyelembe vehető, hogy a válasz lehet string is, hisz csak egyetlen 
                                                                   // jó megoldása van a feladatoknak. ---> ToString()

            {
                MessageBox.Show(string.Format("Pontszám: {0}", this.kerdes6.pontszam));
            }
            // A "this" - t (142,149,155) nem kötelező kiírni,
            // viszont utala arra, h ez a változó nem egy
            // lokális adatfüggvényben deklarált változó,
            // hanem egy osztályszintű változó.
            // Kód olvasásnál sokat segít, mert információ tartalma van.

            else
            {
                MessageBox.Show($"Pontszám: 0\n Helyes válasz:{this.kerdes6.valasz}");
            }
        }
        //-------------------------------------------------------------------------------------------------------

        private void Feladat8()  // 10 véletlenszerű kérdésekből álló feladatsort kiíratása.
        {
            Random rnd = new Random();
            HashSet<int> indexek = new HashSet<int>();
            do
            {
                indexek.Add(rnd.Next(kerdesek.Count));  // Ezt azért fontos így kiíratni, mert a random index-ek miatt egy kérdés
                                                        // kétszer is előfordulhat.... viszont így nem fog hozzáadódni
                                                        // a 10-es listához, mivel a kérdés már 1x szerepelt.
            }
            while (indexek.Count < 10 && indexek.Count < kerdesek.Count);  //  i.Count < kerdesek.Count ---> ezt azért érdemes kiíratni, mert előfordulhat,
                                                                           // hogy az adott kategóriában nincs 10 elem. Ha egy ilyenre kerülne sor, akkor 
                                                                           // enélkül egy örök ciklus lenne.


            int OsszPontszam = 0;                     // Összeadjuk a pontszámokat
            foreach (int index in indexek)
            {
                OsszPontszam += kerdesek[index].pontszam;
            }

            //----------------------------------------------------------------------------------------------------------------------

            // Fájlaba menteni a 10 random feladatot, aminek a végén ki kell íratni, hány pont kapható összesen a feladatosrra.

            StreamWriter sw = new StreamWriter("tesztPontszam.txt");
            foreach (int index in indexek)         // Ismét végig kell menni a kérdéseken
            {
                sw.WriteLine($"{kerdesek[index].pontszam} {kerdesek[index].valasz} {kerdesek[index].kerdes}");
            }
            sw.WriteLine($"A feladatsorból öszesen {OsszPontszam} pont adható.");
            sw.Close();
        }
    }

}

/* Elvégzett feladatok: - Fájlolvasás
 *                      - Hány feladat van az adatfájlban?
                        - Határozza meg hány történelmi kérdés van a feladatban,
                          s ezek közül hány feladat ér 1, 2 ill. 3 pontot. Írassa ki képernyőre is.                          
                        - Határozza meg hány történelmi kérdés van a feladatban,
                          s ezek közül hány feladat ér 1, 2 ill. 3 pontot. Írassa ki képernyőre is.
                        - Határozza meg a minimum és maximum értékeket.
                        - ComboBox tartalmának feltöltése.  HashSet --->  Hiába adom hozzá ugyanazt az elemet, akkor
                                                                          is csak 1x fog a listában szerepelni
                        -    A kiválasztott témakörből véletlenszerűen sorsoljon ki egy kérdést!
                         Sorsoláskor ügyeljen arra, hogy az adott témakörbe eső valamennyi feladatnak
                         legyen esélye.
                         Írassa ki a kérdést és kérje be a felhasználó válaszát.
                         Az ellenőrzés gombra kattintva felugró ablakban jelenjen meg a válaszra 
                         kapott pontszám, valamint "Helytelen válasz esetén o pont jár". 
                         A program indításakor az ellenőrző gomb legyen inaktív és csak a kérdés 
                         után legyen elérhető.                                          
                                   
                        - Ellenőrzés GOMB aktiválása.
                        - 10 véletlenszerű kérdésekből álló feladatsort kiíratása
                        - pontszámok összeadása,---> mennyi adható összesen a feladatsorra
                        - Fájlbaírás
 Használt elemek az xaml-ben : label, ComboBox, TextBox, Button*/