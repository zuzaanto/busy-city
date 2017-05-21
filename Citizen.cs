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
    }
}
