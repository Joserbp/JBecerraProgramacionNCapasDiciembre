using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public class Conexion
    {
        static public string Get()
        {
            string conexion = "Data Source=.;Initial Catalog=JBecerraProgramacionNCapasDiciembre;User ID=sa;Password=pass@word1";
            return conexion;

            //return "Data Source=.;Initial Catalog=JBecerraProgramacionNCapasDiciembre;User ID=sa;Password=pass@word1";
        }
    }
}
