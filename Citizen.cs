using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace busy_city
{
    public class Citizen
    {

        public double pos_x;
        public double pos_y;
        public double dest_x;
        public double dest_y;

        public double velocity;
        public string status;
        public int CitsScooter;
        public int timeToWait;//or rather 'iterations to wait'

        public Location[] stepsToDir;
        public double[] x_stepsToDir;
        public int stepInd;
        public int stepCount;
        public void movCit()
        {
            double x_step = stepsToDir[stepInd].Lat;
            double y_step = stepsToDir[stepInd].Long;
            double diff_x = x_step-pos_x;
            double diff_y = y_step-pos_y;

            double jump = velocity / Math.Pow((Math.Pow(diff_x, 2) + Math.Pow(diff_y, 2)), (0.5));
            if (jump > 1) jump = 1;
            pos_x += diff_x * jump;
            pos_y += diff_y * jump;
        }
        public void drive(double ScoVel)
        {
            status = "driving";
            velocity = ScoVel;
            stepsToDir = null;
            stepInd = 0;
        }
        public void arrived(Random rand, double CitVel)
        {
            pos_x = dest_x;
            pos_y = dest_y;
            timeToWait = 100;
            stepInd = 0;
            stepsToDir = null;
            dest_x = rand.NextDouble() * (city.max_x - city.min_x) + city.min_x;
            dest_y = rand.NextDouble() * (city.max_y - city.min_y) + city.min_y;
            status = "walking";
            velocity = CitVel;
        }
     //   public Location[] stepsToDir;
    }
}
