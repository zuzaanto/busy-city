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
        static void Main()
        {

            var g = new Coding();
            g.Key = "AIzaSyBX44diSFIMl07UUFO_dY33DOW6JyWGFQU";
            var r = g.DirInfo(new DirParameters { Origin = "Antyczna 6,Warszawa", Destination = "Centrum,Warszawa", Mode="driving",Units = "metric",Avoid="highways" });
            if (g.DirResult == Coding.Result.OK)
            {

                MessageBox.Show(r.Result[0].routes.Legs[0].Distance.Value.ToString());
                //view results on variable r
            }



            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
