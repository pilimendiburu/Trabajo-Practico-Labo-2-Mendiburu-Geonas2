using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//using cPosicion;//como incluimos clase?

namespace tp_2_labo_prueba

{
    public class cTablero
    {
        public int[,] tablero = new int[8, 8];        
        //metodos:

        public cPosicion LiberarPieza(int pieza) {
            cPosicion p_pieza = new cPosicion();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (pieza == tablero[i, j])
                    {
                        p_pieza.fila = i;
                        p_pieza.columna = j;
                        tablero[i, j] = 0;
                        return p_pieza;
                    }
                }
            }
            return p_pieza;//retorna -1
        }
        public cTablero()
        {
            for (int i = 0; i < tablero.GetLength(0); i++)
            {
                for (int j = 0; j < tablero.GetLength(1); j++)
                {
                    tablero[i, j] = 0;
                }
            }
        }
        public void InicializarMatrizEn0()
        {
            for (int i = 0; i < tablero.GetLength(0); i++)
            {
                for (int j = 0; j < tablero.GetLength(1); j++)
                {
                    tablero[i, j] = 0;
                }
            }
        }
        public void ImprimirTablero()
        {
            Console.WriteLine("Tablero\n");
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    Console.Write(" " + tablero[r, c]);

                }
                Console.WriteLine();
            }

        }

    }
}
