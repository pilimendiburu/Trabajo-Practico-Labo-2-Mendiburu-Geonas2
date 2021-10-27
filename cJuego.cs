using System;
using System.Collections.Generic;
using System.Text;

namespace tp_2_labo_prueba
{

    class cJuego
    {
        int cant_tableros_a_generar;//a hacer
        int cant_tab_generados;//ya hechos
        cTablero matriz_alfil;
        Amenazas casillas_amenazadas;
        cTablero pos_piezas;
        Amenazas cant_amenazasxCasillas;
        //int[,] cuartoTablero=new int[4,4];

        public Pieza[] arrayPiezas;
        public void GenerarTableros()
        {
            cant_tab_generados = 0;//ponco mi cantidad de tableros generados en 0
                                   //hago el while
            
            while (cant_tab_generados < cant_tableros_a_generar)//-> necesito completar n tableros
            {
                pos_piezas = new cTablero();//nuevo tablero, nuevas matrices
                casillas_amenazadas = new Amenazas();
                cant_amenazasxCasillas = new Amenazas();

                //torre 1
                arrayPiezas[2].pos.EleccionAlAzar();//elijo la posición de mi torre al azar.
                casillas_amenazadas.AmenazasMovimientoTorre(cant_amenazasxCasillas.tablero, pos_piezas.tablero, arrayPiezas[2], true);//marco las amenazas
                pos_piezas.tablero[arrayPiezas[2].pos.fila, arrayPiezas[2].pos.columna] = (int)arrayPiezas[2].tipoPieza;//marco la posicion en la matriz de posiciones

                //torre 2
                cPosicion aux = new cPosicion();//creo una posicion auxiliar que uso para inicializar valores
                aux = arrayPiezas[2].pos;//le cambie pq sino nunca entraba al while
                while (pos_piezas.tablero[aux.fila, aux.columna] != 0 && aux.fila == arrayPiezas[2].pos.fila && arrayPiezas[2].pos.columna == aux.columna)
                {
                    aux.EleccionAlAzar();//selecciono una posicion hasta que cumpla con los criterios que necesito
                }
                arrayPiezas[3].pos = aux;//igualo a esa posicion encontrada
                pos_piezas.tablero[aux.fila, aux.columna] = 5;//torre2
                casillas_amenazadas.AmenazasMovimientoTorre(cant_amenazasxCasillas.tablero, pos_piezas.tablero, arrayPiezas[3], true);

                //alfil 1
                while (pos_piezas.tablero[aux.fila, aux.columna] != 0 || matriz_alfil.tablero[aux.fila, aux.columna] != 1)//condicion para ver si se puede mover el alfil{
                {
                    aux.EleccionAlAzar();
                }
                pos_piezas.tablero[aux.fila, aux.columna] = 6;//alfil 1
                arrayPiezas[4].pos = aux;
                casillas_amenazadas.AmenazasMovimientoAlfil(cant_amenazasxCasillas.tablero, pos_piezas.tablero, arrayPiezas[4], true);

                //alfil 2
                while (pos_piezas.tablero[aux.fila, aux.columna] != 0 && matriz_alfil.tablero[aux.fila, aux.columna] != 2)//condicion para ver si se puede mover el alfil{
                {
                    aux.EleccionAlAzar();
                }
                pos_piezas.tablero[aux.fila, aux.columna] = 7;//alfil 2
                arrayPiezas[5].pos = aux;
                casillas_amenazadas.AmenazasMovimientoAlfil(cant_amenazasxCasillas.tablero, pos_piezas.tablero, arrayPiezas[5], true);

                //reina
                while (pos_piezas.tablero[aux.fila, aux.columna] != 0
                    || (casillas_amenazadas.tablero[aux.fila, aux.columna] == 6 ||
                    casillas_amenazadas.tablero[aux.fila, aux.columna] == 2) && ((aux.columna == arrayPiezas[2].pos.columna ||
                    aux.fila == arrayPiezas[2].pos.fila) || (aux.columna == arrayPiezas[3].pos.columna ||
                    aux.fila == arrayPiezas[3].pos.fila)))
                {
                    aux.EleccionAlAzar();
                }
                pos_piezas.tablero[aux.fila, aux.columna] = 8;
                arrayPiezas[6].pos = aux; 
                casillas_amenazadas.AmenazasMovimientoReina(cant_amenazasxCasillas.tablero, pos_piezas.tablero, arrayPiezas[6], true);

                //para que no me salte un -1 chequeo que hayan posiciones libres
                casillas_amenazadas.ChequeoCasillerosLibres();
                if (casillas_amenazadas.casillas_no_amenazadas == 0)//no deberia pasar porque no estan todas las fichass
                {
                    cant_tab_generados++;
                    Console.WriteLine("\nTengo tablero n°:" + cant_tab_generados);
                    pos_piezas.ImprimirTablero();
                    casillas_amenazadas.ImprimirTablero();
                }
                else
                {
                    //posicione todas las casillas al azar, ahora tengo que colocar donde encuentre una posicion vacia
                    //caballo1
                    arrayPiezas[0].pos = cant_amenazasxCasillas.BuscarPosicionLibre((int)arrayPiezas[0].tipoPieza, arrayPiezas, matriz_alfil.tablero);//pongo el caballo 1
                    pos_piezas.tablero[arrayPiezas[0].pos.fila, arrayPiezas[0].pos.columna] = 2;
                    casillas_amenazadas.AmenazasMovimientoCaballos(cant_amenazasxCasillas.tablero, pos_piezas.tablero, arrayPiezas[0], true);

                    //caballo2
                    arrayPiezas[1].pos = cant_amenazasxCasillas.BuscarPosicionLibre((int)arrayPiezas[1].tipoPieza, arrayPiezas, matriz_alfil.tablero);
                    pos_piezas.tablero[arrayPiezas[1].pos.fila, arrayPiezas[1].pos.columna] = 3;
                    casillas_amenazadas.AmenazasMovimientoCaballos(cant_amenazasxCasillas.tablero, pos_piezas.tablero, arrayPiezas[1], true);

                    //rey
                    arrayPiezas[7].pos = cant_amenazasxCasillas.BuscarPosicionLibre((int)arrayPiezas[7].tipoPieza, arrayPiezas, matriz_alfil.tablero);
                    pos_piezas.tablero[arrayPiezas[7].pos.fila, arrayPiezas[7].pos.columna] = 9;
                    casillas_amenazadas.AmenazasMovimientoRey(cant_amenazasxCasillas.tablero, pos_piezas.tablero, arrayPiezas[7], true);

                    //me fijo si el tablero esta completo
                    casillas_amenazadas.ChequeoCasillerosLibres();
                    if (casillas_amenazadas.casillas_no_amenazadas == 0)
                    {
                        cant_tab_generados++;
                        Console.WriteLine("\nTengo tablero n°:" + cant_tab_generados);
                        pos_piezas.ImprimirTablero();
                        casillas_amenazadas.ImprimirTablero();
                    }
                    else
                    {
                        int contador = 0;
                        while (contador < 5)
                        {

                            //Busco la poscion mas amenazada
                            cant_amenazasxCasillas.retornoMax();
                            int max = casillas_amenazadas.tablero[cant_amenazasxCasillas.pos_max_amenazas.fila, cant_amenazasxCasillas.pos_max_amenazas.columna];//el valor de la pieza en el mas amenazas
                                                                                                                                                           //pongo la posicion de esa pieza en otro lugar libre, pongo la pieza en 0, marco el valor nuevo en el tablero y completo amenazas
                            arrayPiezas[max - 2].pos = casillas_amenazadas.BuscarPosicionLibre(max, arrayPiezas, matriz_alfil.tablero);//nunca tiene que ser -1
                            pos_piezas.LiberarPieza(max);
                            pos_piezas.tablero[arrayPiezas[max - 2].pos.fila, arrayPiezas[max - 2].pos.columna] = (int)arrayPiezas[max - 2].tipoPieza;
                            casillas_amenazadas.InicializarMatrizEn0();
                            cant_amenazasxCasillas.InicializarMatrizEn0();
                            casillas_amenazadas.AmenazarTablero(cant_amenazasxCasillas.tablero, pos_piezas.tablero, arrayPiezas, true);
                            //movi la pieza y reamenace todo
                            casillas_amenazadas.ChequeoCasillerosLibres();
                            if (casillas_amenazadas.casillas_no_amenazadas == 0)
                            {
                                cant_tab_generados++;
                                Console.WriteLine("\nTengo tablero n°:" + cant_tab_generados);
                                pos_piezas.ImprimirTablero();
                                casillas_amenazadas.ImprimirTablero();
                            }
                            else
                            {
                                contador++;
                            }
                        }

                    }
                }


            }
        }
        public cJuego()
        {
            cant_tableros_a_generar = 10;//inicializo 10 pa que funque
            cant_tab_generados = 0;
            matriz_alfil = new cTablero();
            casillas_amenazadas = new Amenazas();
            pos_piezas = new cTablero();
            cant_amenazasxCasillas = new Amenazas();
            arrayPiezas = new Pieza[8];//yo recibiria una por parametro

        }
        public void InicializarTableroAlfil()
        {
            int cont = 0;
            for (int i = 0; i < matriz_alfil.tablero.GetLength(0); i++)
            {
                for (int j = 0; j < matriz_alfil.tablero.GetLength(1); j++)
                {
                    if (cont % 2 == 0 || cont == 0)
                    {
                        matriz_alfil.tablero[i, j] = 1;
                    }
                    else
                    {
                        matriz_alfil.tablero[i, j] = 2;
                    }
                    cont++;
                }
            }
        }
        public int[,] LlenarCuartoTablero(int cuarto)
        {
            int[,] cuartoTablero = new int[4, 4];
            for(int i=0;i<4;i++)
            {
                for(int j=0;j<4; i++)
                {
                    cuartoTablero[i, j] = 0;
                }
            }
            switch(cuarto)
            {
                case 1:
                    {
                        for (int i = 0; i < cuartoTablero.GetLength(0); i++)
                        {
                            for (int j = 0; j < cuartoTablero.GetLength(1); j++)
                            {
                                cuartoTablero[i, j] = pos_piezas.tablero[i, j];
                            }
                        }
                        return cuartoTablero;
                        break;
                    }                 
                case 2://2ndo cuarto
                    {
                        for (int i = 0; i < cuartoTablero.GetLength(0); i++)
                        {
                            for (int j = 4; j < 4+cuartoTablero.GetLength(1); j++)
                            {
                                cuartoTablero[i, j-4] = pos_piezas.tablero[i, j];
                            }
                        }
                        return cuartoTablero;
                        break;
                    }
                case 3://3ercuarto
                    {
                        for (int i = 4; i <4+ cuartoTablero.GetLength(0); i++)
                        {
                            for (int j = 0; j < cuartoTablero.GetLength(1); j++)
                            {
                                cuartoTablero[i-4, j] = pos_piezas.tablero[i, j];
                            }
                        }
                        return cuartoTablero;
                        break;
                    }
                case 4:// 4to cuarto
                    {
                        for (int i = 4; i < 4 + cuartoTablero.GetLength(0); i++)
                        {
                            for (int j = 4; j <4+ cuartoTablero.GetLength(1); j++)
                            {
                                cuartoTablero[i - 4, j-4] = pos_piezas.tablero[i, j];
                            }
                        }
                        return cuartoTablero;
                        break;
                    }
                default:
                    return cuartoTablero;
            }
          

        }
        public void IntercambiarCuartoTablero(int c1, int c2)
        {
            int[,] cuarto1 = new int[4, 4];
            cuarto1 = LlenarCuartoTablero(c1);
            int[,] cuarto2 = new int[4, 4];
            cuarto2 = LlenarCuartoTablero(c2);
            //RANGO:
            int R1 = 0;
            int R2 = 0;
            switch (c1)
            {
                case 1:
                    {
                        R1 = 0;
                        R2 = 0;
                    }
                    break;
                case 2:
                    {
                        R1 = 0;
                        R2 = 4;
                    }
                    break;
                case 3:
                    {
                        R1 = 4;
                        R2 = 0;
                    }
                    break;
                case 4:
                    {
                        R1 = 4;
                        R2 = 4;
                    }
                    break;

                default:
                    {
                        Console.WriteLine("No existe ese cuarto de tablero");
                    }
                    break;
            }
            PegarCuartosEnMatriz_Sol(cuarto1, R1, R2);

            switch (c2)
            {
                case 1:
                    {
                        R1 = 0;
                        R2 = 0;
                    }
                    break;
                case 2:
                    {
                        R1 = 0;
                        R2 = 4;
                    }
                    break;
                case 3:
                    {
                        R1 = 4;
                        R2 = 0;
                    }
                    break;
                case 4:
                    {
                        R1 = 4;
                        R2 = 4;
                    }
                    break;

                default:
                    {
                        Console.WriteLine("No existe ese cuarto de tablero");
                    }
                    break;
            }
            PegarCuartosEnMatriz_Sol(cuarto2, R1, R2);
        }

        public void PegarCuartosEnMatriz_Sol(int[,] cuarto, int R1, int R2)
        {
            for(int i=R1; i<R1+4; i++)
            {
                for(int j=R1; i<R2+4; i++)
                {
                    pos_piezas.tablero[i, j] = cuarto[i - R1, j - R2];
                }
            }
        }
          
    }
    
}

