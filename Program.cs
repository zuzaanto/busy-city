using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Maps;
using Google.Apis;

namespace busy_city
{
    static class Program
    {
        /// <summary>
        //
        /// The main entry point for the application.
        /// </summary>
        /// 

       // Key = "AIzaSyBX44diSFIMl07UUFO_dY33DOW6JyWGFQU";
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            List<Citizen> Citis=new List<Citizen>();
            List<Scooter> Scoots = new List<Scooter>();
            //area of interest, rad.10km, square
            city.max_x = 0.08983 * 10 + city.lng;
            city.min_x = city.lng-0.08983 * 10;
            city.max_y = 0.08983 * 10 + city.lati;
            city.min_y = city.lati - 0.08983 * 10;
            //1degree=111.196672km
            //10km=0.08983degree
            //creating 10 citizens
            for (int i = 1; i < 10; i++)
            {
                Random rand = new Random();
                
                Citizen Cit = new Citizen();
                Cit.pos_x= rand.NextDouble() * (city.max_x - city.min_x) + city.min_x;
                //Citis[i] = new Citizen();
                Random rand2 = new Random();
                Cit.pos_y = rand2.NextDouble() * (city.max_y - city.min_y) + city.min_y;
                Random rand3 = new Random();
                Cit.dest_x = rand3.NextDouble() * (city.max_x - city.min_x) + city.min_x;
                Random rand4 = new Random();
                Cit.dest_y = rand4.NextDouble() * (city.max_y - city.min_y) + city.min_y;
                Citis.Add(Cit);
                MessageBox.Show("poz y");
                MessageBox.Show(Cit.pos_y.ToString());
                MessageBox.Show("poz x");
                MessageBox.Show(Cit.pos_x.ToString());
                //Citis.Insert(1, Cit);

            }
            for (int i = 1; i < 20; i++)
            {
                Random rand = new Random();
                Scooter Sco = new Scooter();
                Sco.pos_x = rand.NextDouble() * (city.max_x - city.min_x) + city.min_x;
                Random rand2 = new Random();
                Sco.pos_y = rand2.NextDouble() * (city.max_y - city.min_y) + city.min_y;
                Scoots.Add(Sco);
                Console.WriteLine(Sco.pos_y);
                Console.WriteLine(Sco.pos_x);
            }

            var g = new Coding();
            g.Key = "AIzaSyBX44diSFIMl07UUFO_dY33DOW6JyWGFQU";
            var r = g.DirInfo(new DirParameters { Origin = "Antyczna 6,Warszawa", Destination = "Centrum,Warszawa", Mode="driving",Units = "metric",Avoid="highways" });
            if (g.DirResult == Coding.Result.OK)
            {

                MessageBox.Show(r.routes[0].Legs[0].Distance.Value.ToString());
            }



            
        }
    }
}
