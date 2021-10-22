using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tp_2_labo_prueba
{
    public class cPosicion
    {
        Random r;
        
        public int fila;
        public int columna;
        public cPosicion() {
            r = new Random();
            fila = -1;
            columna = -1;
        }
        public void EleccionAlAzar() {
            fila=r.Next(8);
            columna = r.Next(8);
        }
        
    }
}
