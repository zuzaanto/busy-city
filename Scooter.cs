using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace busy_city
{
    public class Scooter
    {
        public double pos_x;
        public double pos_y;
        public double velocity;
        public bool idle;
        public void MovScoot(double new_pos_x,double new_pos_y)
        {
            pos_x = new_pos_x;
            pos_y = new_pos_y;
        }
    }
}
