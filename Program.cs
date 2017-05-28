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

        // Key = "";
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            List<Citizen> Citis = new List<Citizen>();
            List<Scooter> Scoots = new List<Scooter>();
            int numberOfCitizens=10;
            int numberOfScooters = 20;
            double ScoVelocity= 0.0076;
            double CitVelocity= 0.001559;
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
                Cit.velocity = CitVelocity;//10m jump per iteration
                Cit.stepInd = 0;
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
            double ScootsDist = 90000000000000;
            int closestScootInd = 55;
            string export;
            var g = new Coding();
            //!!!!!!!!!!!!paste your own Google Api Key here: 
            g.Key = "";
            System.IO.StreamWriter file = new System.IO.StreamWriter("C:\\ositions1.txt");

            //while (true)
            for (int kj=0;kj<5000;kj++)
            {
                for (int i = 0; i < numberOfCitizens; i++)
                {

                    if (Citis[i].timeToWait == 0)
                    {
                        Orig = Convert.ToString(Citis[i].pos_x) + "," + Convert.ToString(Citis[i].pos_y);

                        //finding closest scooter
                        if (Citis[i].status == "walking")
                        {
                            //1st case scenario
                            //finding route
                            if (Citis[i].stepsToDir == null)
                            {
                                Citis[i].stepInd = 0;
                                closestScootInd = 0;
                                for (int j = 0; j < numberOfScooters; j++)
                                {
                                    if (Scoots[j].idle && (Math.Pow(Citis[i].pos_x - Scoots[j].pos_x, 2) + Math.Pow(Citis[i].pos_y - Scoots[j].pos_y, 2) < ScootsDist))
                                        closestScootInd = j;

                                    ScootsDist = (Math.Pow(Citis[i].pos_x - Scoots[j].pos_x, 2) + Math.Pow(Citis[i].pos_y - Scoots[j].pos_y, 2));
                                }
                                Citis[i].CitsScooter = closestScootInd;

                                dest = Convert.ToString(Scoots[Citis[i].CitsScooter].pos_x) + "," + Convert.ToString(Scoots[Citis[i].CitsScooter].pos_y);
                                var r_cit = g.DirInfo(new DirParameters { Origin = Orig, Destination = dest, Mode = Citis[i].status, Units = "metric", Avoid = "highways" });

                                Citis[i].stepCount = r_cit.routes[0].Legs[0].Steps.Length;
                                Citis[i].stepsToDir = new Location[Citis[i].stepCount];

                                for (int j1 = 0; j1 < r_cit.routes[0].Legs[0].Steps.Length; j1++)
                                {
                                    Citis[i].stepsToDir[j1] = r_cit.routes[0].Legs[0].Steps[j1].EndLocation;
                                }
                                //making 1st step
                                Citis[i].movCit();
                                //if 1st step made:
                                if (Math.Abs(Citis[i].pos_x - Citis[i].stepsToDir[0].Lat) < 0.00001 && Math.Abs(Citis[i].pos_y - Citis[i].stepsToDir[0].Long) < 0.00001)
                                {
                                    Citis[i].stepInd = 1;
                                }

                            }
                            //2nd,3rd and 4th cases scenarios
                            //if route known
                            else
                            {
                                //if at dest (scoot's pos)
                                if (Citis[i].stepInd >= Citis[i].stepCount)
                                {
                                    //4th case scenario
                                    if (Scoots[Citis[i].CitsScooter].idle)
                                    {
                                        //start a scooter
                                        Citis[i].drive(ScoVelocity);
                                        Scoots[Citis[i].CitsScooter].idle = false;
                                    }
                                    //3rd case scenario
                                    //find another scooter
                                    else
                                    {
                                        Citis[i].stepsToDir = null;
                                    }
                                }
                                //2nd case scenario
                                else
                                {
                                    Citis[i].movCit();
                                    //if step done:
                                    if (Math.Abs(Citis[i].pos_x - Citis[i].stepsToDir[Citis[i].stepInd].Lat) < 0.0001 && Math.Abs(Citis[i].pos_y - Citis[i].stepsToDir[Citis[i].stepInd].Long) < 0.0001)
                                    {
                                        Citis[i].stepInd++;
                                    }
                                    }
                                }
                        }
                        //when driving
                        if (Citis[i].status == "driving")
                        {
                            
                            
                            //5th case scenario
                            if (Citis[i].stepsToDir == null)
                            {
                                dest = Convert.ToString(Citis[i].dest_x) + "," + Convert.ToString(Citis[i].dest_y);
                                var r_scoot = g.DirInfo(new DirParameters { Origin = Orig, Destination = dest, Mode = Citis[i].status, Units = "metric", Avoid = "highways" });
                                Citis[i].stepCount = r_scoot.routes[0].Legs[0].Steps.Length;
                                Citis[i].stepsToDir = new Location[Citis[i].stepCount];
                                for (int j1 = 0; j1 < r_scoot.routes[0].Legs[0].Steps.Length; j1++)
                                {
                                    Citis[i].stepsToDir[j1] = r_scoot.routes[0].Legs[0].Steps[j1].EndLocation;
                                }
                                //making 1st step
                                Citis[i].stepInd = 0;
                                Citis[i].movCit();
                                Scoots[Citis[i].CitsScooter].MovScoot(Citis[i].pos_x, Citis[i].pos_y);
                                //if 1st step made:
                                if (Math.Abs(Citis[i].pos_x - Citis[i].stepsToDir[0].Lat) < 0.00001 && Math.Abs(Citis[i].pos_y - Citis[i].stepsToDir[0].Long) < 0.00001)
                                {
                                    Citis[i].stepInd = 1;
                                }
                            }
                            //6th and 7th case scenarios
                            else
                            {
                                //7th case scenario
                                if (Citis[i].stepInd == Citis[i].stepCount)
                                {

                                    Scoots[Citis[i].CitsScooter].idle = true;
                                    Citis[i].pos_x = Citis[i].dest_x;
                                    Citis[i].pos_y = Citis[i].dest_y;
                                    Citis[i].arrived(rand, CitVelocity);
                                }
                                //6th case scenario
                                else
                                {
                                    Citis[i].movCit();
                                    Scoots[Citis[i].CitsScooter].MovScoot(Citis[i].pos_x,Citis[i].pos_y);
                                    

                                    //if step done:
                                    if (Math.Abs(Citis[i].pos_x - Citis[i].stepsToDir[Citis[i].stepInd].Lat) < 0.0001 && Math.Abs(Citis[i].pos_y - Citis[i].stepsToDir[Citis[i].stepInd].Long) < 0.0001)
                                    {
                                        Citis[i].stepInd++;
                                    }
                                }
                                
                            }
                        }
                    }
                    else
                    {
                        Citis[i].timeToWait = Citis[i].timeToWait - 1;
                    }
                    export = i + ".  " + Citis[i].pos_x + ", " + Citis[i].pos_y + ", " + Citis[i].stepInd+"  "+ Citis[i].status + "  " + Citis[i].CitsScooter + "\n";

                    file.WriteLine(export);
                }

            }
            file.Close();
        }
        


        }
}

