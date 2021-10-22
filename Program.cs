using System;

namespace tp_2_labo_prueba
{
    class Program
    {
        static void Main(string[] args)
        {
            cJuego partida = new cJuego();//tuvimos en cuenta los bordes del tablero haciendo los randoms de 0 a 8
            partida.InicializarTableroAlfil();
            partida.arrayPiezas = CrearPiezas();
            partida.GenerarTableros();
            Console.ReadKey();
        }
        static Pieza[] CrearPiezas()
        {
            Pieza[] piezas = new Pieza[8];
            for (int i = 0; i < 8; i++)
            {
                piezas[i] = new Pieza((e_Pieza)i + 2);
            }
            return piezas;
        }
    }
}
