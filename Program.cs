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
            List<Citizen> Citis = new List<Citizen>();
            List<Scooter> Scoots = new List<Scooter>();
            //area of interest, rad.10km, square
            city.max_x = 0.08983 * 10 + city.lng;
            city.min_x = city.lng - 0.08983 * 10;
            city.max_y = 0.08983 * 10 + city.lati;
            city.min_y = city.lati - 0.08983 * 10;
            //1degree=111.196672km
            //10km=0.08983degree
            //creating 10 citizens
            Random rand = new Random();
            //initialising citizens
            for (int i = 0; i < 10; i++)
            {
                

                Citizen Cit = new Citizen();
                Cit.pos_x = rand.NextDouble() * (city.max_x - city.min_x) + city.min_x;
                //Citis[i] = new Citizen();
                
                Cit.pos_y = rand.NextDouble() * (city.max_y - city.min_y) + city.min_y;
                
                Cit.dest_x = rand.NextDouble() * (city.max_x - city.min_x) + city.min_x;
                
                Cit.dest_y = rand.NextDouble() * (city.max_y - city.min_y) + city.min_y;
                Cit.status = "walking";
                Cit.timeToWait = 0;
                Cit.velocity = 0.0001559;//10m jump per iteration
                Citis.Add(Cit);

                //Citis.Insert(1, Cit);

            }
            //initialising scooters
            for (int i = 0; i < 20; i++)
            {
                
                Scooter Sco = new Scooter();
                Sco.pos_x = rand.NextDouble() * (city.max_x - city.min_x) + city.min_x;

                Sco.pos_y = rand.NextDouble() * (city.max_y - city.min_y) + city.min_y;
                Sco.idle = true;
                Sco.velocity = 0;
                Scoots.Add(Sco);
            }
            string Orig;
            string dest;
            double x_step;
            double y_step;
            double diff_x;
            double diff_y;
            double angle;
            double jump;
            double dist;
            double ScootsDist=90000000000000;
            int closestScootInd=55;
            string export;
            var g = new Coding();
            //!!!!!!!!!!!!paste your own Google Api Key here: 
            g.Key = "AIzaSyBX44diSFIMl07UUFO_dY33DOW6JyWGFQU";
            System.IO.StreamWriter file = new System.IO.StreamWriter("C:\\positions1.txt");

            while (true)
            {
                for (int i = 0; i < 10; i++)
                {
                    if (Citis[i].timeToWait == 0)
                    {
                        Orig = Convert.ToString(Citis[i].pos_x) + "," + Convert.ToString(Citis[i].pos_y);
                        dest = Convert.ToString(Citis[i].dest_x) + "," + Convert.ToString(Citis[i].dest_y);
                        if (Citis[i].status == "walking")
                        {
                            for (int j = 0; j < 10; j++)
                            {
                                if (Scoots[j].idle && (Math.Pow(Citis[i].pos_x - Scoots[j].pos_x, 2) + Math.Pow(Citis[i].pos_y - Scoots[j].pos_y, 2) < ScootsDist))
                                    closestScootInd = j;

                                ScootsDist = (Math.Pow(Citis[i].pos_x - Scoots[j].pos_x, 2) + Math.Pow(Citis[i].pos_y - Scoots[j].pos_y, 2));


                            }
                            dest = Convert.ToString(Scoots[closestScootInd].pos_x) + "," + Convert.ToString(Scoots[closestScootInd].pos_y);
                            if (Math.Abs(Citis[i].pos_x- Scoots[closestScootInd].pos_x)<0.0001 && Math.Abs(Citis[i].pos_y - Scoots[closestScootInd].pos_y)<0.0001 && closestScootInd != 55)
                            {
                                Citis[i].status = "driving";
                                Citis[i].velocity = 0.0016;
                                Citis[i].CitsScooter = closestScootInd;
                                Scoots[Citis[i].CitsScooter].idle = false;
                            }
                        }
                        //when arrived
                        if (Citis[i].status == "driving" && Math.Abs(Citis[i].pos_x-Citis[i].dest_x)<0.0001 && Math.Abs(Citis[i].pos_y-Citis[i].dest_y)<0.0001)
                        {
                            Scoots[Citis[i].CitsScooter].idle = true;
                            Citis[i].timeToWait = 100;
                        }

                        var r_cit = g.DirInfo(new DirParameters { Origin = Orig, Destination = dest, Mode = Citis[i].status, Units = "metric", Avoid = "highways" });
                        x_step = r_cit.routes[0].Legs[0].Steps[0].EndLocation.Lat;
                        y_step = r_cit.routes[0].Legs[0].Steps[0].EndLocation.Long;
                        diff_x = Citis[i].pos_x - x_step;
                        diff_y = Citis[i].pos_y - y_step;
                        angle = Math.Atan(diff_x / diff_y);
                        jump = Citis[i].velocity / Math.Pow((Math.Pow(diff_x, 2) + Math.Pow(diff_y, 2)), (0.5));
                        if (jump > 1) jump = 1;
                        dist = r_cit.routes[0].Legs[0].Steps[0].Distance.Value;
                        Citis[i].pos_x += diff_x * jump;

                        Citis[i].pos_y += diff_y * jump;
                        if (Citis[i].status == "driving")
                        {
                            Scoots[Citis[i].CitsScooter].pos_x += diff_x * jump;

                            Citis[i].pos_y += diff_y * jump;
                        }
                    }
                    else
                    {
                        Citis[i].timeToWait = Citis[i].timeToWait - 1;
                    }
                    export = i + ".  " + Citis[i].pos_x + ", " + Citis[i].pos_y + ", " + Citis[i].status+"\n";
                    
                    file.WriteLine(export);

                    
                }
                
            }
            file.Close();
        }
    }
}
