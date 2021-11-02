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
        cTablero matrizFatales;
        cPosicion[,] Tableros;
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
                pos_piezas.tablero[arrayPiezas[2].pos.fila, arrayPiezas[2].pos.columna] = ((int)arrayPiezas[2].tipoPieza);//marco la posicion en la matriz de posiciones

                //torre 2
                cPosicion aux = new cPosicion();//creo una posicion auxiliar que uso para inicializar valores
                //aux = arrayPiezas[2].pos;//le cambie pq sino nunca entraba al while
                aux.fila = (int)arrayPiezas[2].pos.fila;
                aux.columna = (int)arrayPiezas[2].pos.columna;

                while (casillas_amenazadas.tablero[aux.fila, aux.columna] != 0 || aux.fila == arrayPiezas[2].pos.fila || arrayPiezas[2].pos.columna == aux.columna)
                {
                    aux.EleccionAlAzar();//selecciono una posicion hasta que cumpla con los criterios que necesito

                }
                arrayPiezas[3].pos.fila = (int)aux.fila;//igualo a esa posicion encontrada
                arrayPiezas[3].pos.columna = (int)aux.columna;//igualo a esa posicion encontrada

                pos_piezas.tablero[aux.fila, aux.columna] = 5;//torre2
                casillas_amenazadas.AmenazasMovimientoTorre(cant_amenazasxCasillas.tablero, pos_piezas.tablero, arrayPiezas[3], true);

                //alfil 1
                while (casillas_amenazadas.tablero[aux.fila, aux.columna] != 0 || matriz_alfil.tablero[aux.fila, aux.columna] != 1)//condicion para ver si se puede mover el alfil{
                {
                    aux.EleccionAlAzar();
                }
                pos_piezas.tablero[aux.fila, aux.columna] = 6;//alfil 1
                arrayPiezas[4].pos.fila = (int)aux.fila;
                arrayPiezas[4].pos.columna = (int)aux.columna;

                casillas_amenazadas.AmenazasMovimientoAlfil(cant_amenazasxCasillas.tablero, pos_piezas.tablero, arrayPiezas[4], true);

                //alfil 2
                while (casillas_amenazadas.tablero[aux.fila, aux.columna] != 0 && matriz_alfil.tablero[aux.fila, aux.columna] != 2)//condicion para ver si se puede mover el alfil{
                {
                    aux.EleccionAlAzar();
                }
                pos_piezas.tablero[aux.fila, aux.columna] = 7;//alfil 2
                arrayPiezas[5].pos.fila = (int)aux.fila;
                arrayPiezas[5].pos.columna = (int)aux.columna;

                casillas_amenazadas.AmenazasMovimientoAlfil(cant_amenazasxCasillas.tablero, pos_piezas.tablero, arrayPiezas[5], true);

                //reina
                while (casillas_amenazadas.tablero[aux.fila, aux.columna] != 0
                    || (casillas_amenazadas.tablero[aux.fila, aux.columna] == 6 ||
                    casillas_amenazadas.tablero[aux.fila, aux.columna] == 2) && ((aux.columna == arrayPiezas[2].pos.columna ||
                    aux.fila == arrayPiezas[2].pos.fila) || (aux.columna == arrayPiezas[3].pos.columna ||
                    aux.fila == arrayPiezas[3].pos.fila)))
                {
                    //lo pongo en un lugar donde NO haya amenazas
                    aux.EleccionAlAzar();
                }
                pos_piezas.tablero[aux.fila, aux.columna] = 8;
                arrayPiezas[6].pos.fila = aux.fila;
                arrayPiezas[6].pos.columna = aux.columna;

                casillas_amenazadas.AmenazasMovimientoReina(cant_amenazasxCasillas.tablero, pos_piezas.tablero, arrayPiezas[6], true);

                //para que no me salte un -1 chequeo que hayan posiciones libres
                casillas_amenazadas.ChequeoCasillerosLibres();
                if (casillas_amenazadas.casillas_no_amenazadas == 0 && ChequearTablero() == true)//no deberia pasar porque no estan todas las fichass
                {
                    copiarPosiciones();
                    cant_tab_generados++;
                    Console.WriteLine("\nTengo tablero n°:" + cant_tab_generados);
                    pos_piezas.ImprimirTablero();
                    casillas_amenazadas.ImprimirTablero();
                }
                else
                {
                    //posicione todas las casillas al azar, ahora tengo que colocar donde encuentre una posicion vacia
                    //caballo1
                    int[] cuartoMin = tableroMinAmenazas();
                    cPosicion auxLibre = new cPosicion();
                    auxLibre = cant_amenazasxCasillas.BuscarPosicionLibre((int)arrayPiezas[0].tipoPieza, arrayPiezas, matriz_alfil.tablero, cuartoMin[0], cuartoMin[1]);
                    arrayPiezas[0].pos.fila = (int)auxLibre.fila;//pongo el caballo 1
                    arrayPiezas[0].pos.columna = (int)auxLibre.columna;//pongo el caballo 1

                    pos_piezas.tablero[arrayPiezas[0].pos.fila, arrayPiezas[0].pos.columna] = 2;
                    casillas_amenazadas.AmenazasMovimientoCaballos(cant_amenazasxCasillas.tablero, pos_piezas.tablero, arrayPiezas[0], true);

                    //caballo2
                    cuartoMin = tableroMinAmenazas();
                    auxLibre = cant_amenazasxCasillas.BuscarPosicionLibre((int)arrayPiezas[1].tipoPieza, arrayPiezas, matriz_alfil.tablero, cuartoMin[0], cuartoMin[1]);
                    arrayPiezas[1].pos.fila = (int)auxLibre.fila;
                    arrayPiezas[1].pos.columna = (int)auxLibre.columna;

                    pos_piezas.tablero[arrayPiezas[1].pos.fila, arrayPiezas[1].pos.columna] = 3;
                    casillas_amenazadas.AmenazasMovimientoCaballos(cant_amenazasxCasillas.tablero, pos_piezas.tablero, arrayPiezas[1], true);

                    //rey
                    cuartoMin = tableroMinAmenazas();
                    auxLibre = cant_amenazasxCasillas.BuscarPosicionLibre((int)arrayPiezas[7].tipoPieza, arrayPiezas, matriz_alfil.tablero, cuartoMin[0], cuartoMin[1]);
                    arrayPiezas[7].pos.fila = (int)auxLibre.fila;
                    arrayPiezas[7].pos.columna = (int)auxLibre.columna;

                    pos_piezas.tablero[arrayPiezas[7].pos.fila, arrayPiezas[7].pos.columna] = 9;
                    casillas_amenazadas.AmenazasMovimientoRey(cant_amenazasxCasillas.tablero, pos_piezas.tablero, arrayPiezas[7], true);

                    //me fijo si el tablero esta completo
                    casillas_amenazadas.ChequeoCasillerosLibres();
                    if (casillas_amenazadas.casillas_no_amenazadas == 0&&ChequearTablero()==true)
                    {
                        copiarPosiciones();
                        cant_tab_generados++;
                        Console.WriteLine("\nTengo tablero n°:" + cant_tab_generados);
                        pos_piezas.ImprimirTablero();
                        casillas_amenazadas.ImprimirTablero();
                        //ACA TAMBIEN TENDRIAMOS QUE ROTAR EL TABLERO!!

                    }
                    else
                    {
                        int contador = 0;
                        while (contador < 10)
                        {
                            //Busco donde esta la pieza con la poscion mas amenazada

                                cant_amenazasxCasillas.retornoMax();
                                int max = casillas_amenazadas.tablero[cant_amenazasxCasillas.pos_max_amenazas.fila, cant_amenazasxCasillas.pos_max_amenazas.columna];//el valor de la pieza en el mas amenazas
                                cPosicion aux2 = new cPosicion();
                                cuartoMin = tableroMinAmenazas();                                
                                aux2 = casillas_amenazadas.BuscarPosicionLibre(max, arrayPiezas, matriz_alfil.tablero, cuartoMin[0], cuartoMin[1]);//pongo la posicion de esa pieza en otro lugar libre, pongo la pieza en 0, marco el valor nuevo en el tablero y completo amenazas
                           
                                arrayPiezas[max - 2].pos.fila = (int)aux2.fila;//nunca tiene que ser -1
                                arrayPiezas[max - 2].pos.columna = (int)aux2.columna;//nunca tiene que ser -1

                                pos_piezas.LiberarPieza(max);
                                pos_piezas.tablero[arrayPiezas[max - 2].pos.fila, arrayPiezas[max - 2].pos.columna] = (int)arrayPiezas[max - 2].tipoPieza;
                                casillas_amenazadas.InicializarMatrizEn0();
                                cant_amenazasxCasillas.InicializarMatrizEn0();
                                casillas_amenazadas.AmenazarTablero(cant_amenazasxCasillas.tablero, pos_piezas.tablero, arrayPiezas, true);
                                //movi la pieza y reamenace todo
                                casillas_amenazadas.ChequeoCasillerosLibres();
                                // pos_piezas.ImprimirTablero();
                                // Console.WriteLine("\nTablero chequeo:\n");
                                // casillas_amenazadas.ImprimirTablero();
                                //TAL VEZ NOS CONVIENE HACER INTERCAMBIOS ACA TAMBIEN -> en una de esas hace un tablero
                            
                            if (casillas_amenazadas.casillas_no_amenazadas == 0 && ChequearTablero() == true)
                            {
                                copiarPosiciones();

                                cant_tab_generados++;
                                Console.WriteLine("\nTengo tablero n°:" + cant_tab_generados);
                                pos_piezas.ImprimirTablero();
                                casillas_amenazadas.ImprimirTablero();
                                break;
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
            matrizFatales = new cTablero();
            Tableros = new cPosicion[cant_tableros_a_generar, 8];
            for (int i = 0; i < cant_tableros_a_generar; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Tableros[i, j] = new cPosicion();
                }
            }
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
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    cuartoTablero[i, j] = 0;
                }
            }
            switch (cuarto)
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
                        //break;
                    }
                case 2://2ndo cuarto
                    {
                        for (int i = 0; i < cuartoTablero.GetLength(0); i++)
                        {
                            for (int j = 4; j < 4 + cuartoTablero.GetLength(1); j++)
                            {
                                cuartoTablero[i, j - 4] = pos_piezas.tablero[i, j];
                            }
                        }
                        return cuartoTablero;
                        //break;
                    }
                case 3://3ercuarto
                    {
                        for (int i = 4; i < 4 + cuartoTablero.GetLength(0); i++)
                        {
                            for (int j = 0; j < cuartoTablero.GetLength(1); j++)
                            {
                                cuartoTablero[i - 4, j] = pos_piezas.tablero[i, j];
                            }
                        }
                        return cuartoTablero;
                        //break;
                    }
                case 4:// 4to cuarto
                    {
                        for (int i = 4; i < 4 + cuartoTablero.GetLength(0); i++)
                        {
                            for (int j = 4; j < 4 + cuartoTablero.GetLength(1); j++)
                            {
                                cuartoTablero[i - 4, j - 4] = pos_piezas.tablero[i, j];
                            }
                        }
                        return cuartoTablero;
                        //break;
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
            PegarCuartosEnMatriz_Sol(cuarto2, R1, R2);

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
            PegarCuartosEnMatriz_Sol(cuarto1, R1, R2);
        }
        public void PegarCuartosEnMatriz_Sol(int[,] cuarto, int R1, int R2)
        {
            for (int i = R1; i < R1 + 4; i++)
            {
                for (int j = R2; j < R2 + 4; j++)
                {
                    if (cuarto[i - R1, j - R2] != 0)
                    {
                        arrayPiezas[cuarto[i - R1, j - R2] - 2].pos.fila = i;
                        arrayPiezas[cuarto[i - R1, j - R2] - 2].pos.columna = j;
                    }

                    pos_piezas.tablero[i, j] =cuarto[i - R1, j - R2];
                }
            }
        }
        public bool ChequearTablero()
        {

            int contador = 0;
            for (int i = 0; i < pos_piezas.tablero.GetLength(0); i++)
            {
                for (int j = 0; j < pos_piezas.tablero.GetLength(1); j++)
                {
                    if (cant_tab_generados == 0)
                        return true;
                    if (arrayPiezas[i].pos.fila == (int)Tableros[j, i].fila && arrayPiezas[i].pos.columna ==(int) Tableros[j, i].columna)//hago variar la fila (osea el tablero) 
                        contador++;
                }
            }
            if (contador == 64)
                return false;
            else
                return true;
        }
        public void copiarPosiciones()
        {
            for (int i = 0; i < 8; i++)
            {
                if (cant_tab_generados < 10)
                {

                    Tableros[cant_tab_generados, i].fila = (int)arrayPiezas[i].pos.fila;
                    Tableros[cant_tab_generados, i].columna = (int)arrayPiezas[i].pos.columna;
                }

            }
        }
        public int[] tableroMinAmenazas()
        {
            int[] rango = new int[2];
            int[] cont = new int[4];
            for (int i = 0; i < 4; i++)
            {
                cont[i] = 0;
            }
            int cuartoMinAmenazas = 0;

            //primer cuarto
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (casillas_amenazadas.tablero[i, j] == 0)
                        cont[0]++;
                }
            }

            //segundo cuarto
            for (int i = 0; i < 4; i++)
            {
                for (int j = 4; j < 8; j++)
                {
                    if (casillas_amenazadas.tablero[i, j] == 0)
                        cont[1]++;
                }
            }

            //tercer cuarto
            for (int i = 4; i < 8; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (casillas_amenazadas.tablero[i, j] == 0)
                        cont[2]++;
                }
            }

            //cuarto cuarto
            for (int i = 4; i < 8; i++)
            {
                for (int j = 4; j < 8; j++)
                {
                    if (casillas_amenazadas.tablero[i, j] == 0)
                        cont[3]++;
                }
            }

            //busco el cuarto con menor amenazas
            int minAmenazas = cont[0];
            for (int i = 0; i < 4; i++)
            {
                if (cont[i] > minAmenazas)//significa que el cuarto tiene MAS casillas sin amenazas
                {
                    minAmenazas = cont[i];
                    cuartoMinAmenazas = i;
                }
            }
            switch (cuartoMinAmenazas)
            {
                case 0:
                    {
                        rango[0] = 0;
                        rango[1] = 0;
                        break;
                    }

                case 1:
                    {
                        rango[0] = 0;
                        rango[1] = 4;
                        break;
                    }
                case 2:
                    {
                        rango[0] = 4;
                        rango[1] = 0;
                        break;
                    }
                case 3:
                    {
                        rango[0] = 4;
                        rango[1] = 4;
                        break;
                    }

                default:
                    break;
            }
            return rango;

        }

        public void TableroYaGenerado()
        {
          
                copiarPosiciones();
                cant_tab_generados++;
                Console.WriteLine("\nTengo tablero n°:" + cant_tab_generados);
                pos_piezas.ImprimirTablero();
                casillas_amenazadas.ImprimirTablero();
                //ACA TAMBIEN TENDRIAMOS QUE ROTAR EL TABLERO!!
                //---------------------------------------------------------------------
                IntercambiarCuartoTablero(1, 2);
                cant_amenazasxCasillas.InicializarMatrizEn0();
                casillas_amenazadas.InicializarMatrizEn0();
                casillas_amenazadas.AmenazarTablero(cant_amenazasxCasillas.tablero, pos_piezas.tablero, arrayPiezas, true);
                casillas_amenazadas.ChequeoCasillerosLibres();
                if (casillas_amenazadas.casillas_no_amenazadas == 0 && ChequearTablero() == true)
                {
                    //copiar matriz
                    copiarPosiciones();
                    cant_tab_generados++;
                    Console.WriteLine("\nTengo tablero n°:" + cant_tab_generados);
                    pos_piezas.ImprimirTablero();
                    casillas_amenazadas.ImprimirTablero();
                    IntercambiarCuartoTablero(2, 3);
                    cant_amenazasxCasillas.InicializarMatrizEn0();
                    casillas_amenazadas.InicializarMatrizEn0();
                    casillas_amenazadas.AmenazarTablero(cant_amenazasxCasillas.tablero, pos_piezas.tablero, arrayPiezas, true);
                    casillas_amenazadas.ChequeoCasillerosLibres();
                    if (casillas_amenazadas.casillas_no_amenazadas == 0 && ChequearTablero() == true)
                    {
                        copiarPosiciones();

                        cant_tab_generados++;
                        Console.WriteLine("\nTengo tablero n°:" + cant_tab_generados);
                        pos_piezas.ImprimirTablero();
                        casillas_amenazadas.ImprimirTablero();
                        IntercambiarCuartoTablero(3, 4);
                        cant_amenazasxCasillas.InicializarMatrizEn0();
                        casillas_amenazadas.InicializarMatrizEn0();
                        casillas_amenazadas.AmenazarTablero(cant_amenazasxCasillas.tablero, pos_piezas.tablero, arrayPiezas, true);
                        casillas_amenazadas.ChequeoCasillerosLibres();
                        if (casillas_amenazadas.casillas_no_amenazadas == 0 && ChequearTablero() == true)
                        {
                            copiarPosiciones();

                            cant_tab_generados++;
                            Console.WriteLine("\nTengo tablero n°:" + cant_tab_generados);
                            pos_piezas.ImprimirTablero();
                            casillas_amenazadas.ImprimirTablero();
                            IntercambiarCuartoTablero(1, 4);
                            cant_amenazasxCasillas.InicializarMatrizEn0();
                            casillas_amenazadas.InicializarMatrizEn0();
                            casillas_amenazadas.AmenazarTablero(cant_amenazasxCasillas.tablero, pos_piezas.tablero, arrayPiezas, true);
                            casillas_amenazadas.ChequeoCasillerosLibres();
                            if (casillas_amenazadas.casillas_no_amenazadas == 0 && ChequearTablero() == true)
                            {
                                copiarPosiciones();

                                cant_tab_generados++;
                                Console.WriteLine("\nTengo tablero n°:" + cant_tab_generados);
                                pos_piezas.ImprimirTablero();
                                casillas_amenazadas.ImprimirTablero();
                                IntercambiarCuartoTablero(1, 3);
                                cant_amenazasxCasillas.InicializarMatrizEn0();
                                casillas_amenazadas.InicializarMatrizEn0();
                                casillas_amenazadas.AmenazarTablero(cant_amenazasxCasillas.tablero, pos_piezas.tablero, arrayPiezas, true);
                                casillas_amenazadas.ChequeoCasillerosLibres();
                                if (casillas_amenazadas.casillas_no_amenazadas == 0 && ChequearTablero() == true)
                                {
                                    copiarPosiciones();

                                    cant_tab_generados++;
                                    Console.WriteLine("\nTengo tablero n°:" + cant_tab_generados);
                                    pos_piezas.ImprimirTablero();
                                    casillas_amenazadas.ImprimirTablero();
                                    IntercambiarCuartoTablero(4, 2);
                                    cant_amenazasxCasillas.InicializarMatrizEn0();
                                    casillas_amenazadas.InicializarMatrizEn0();
                                    casillas_amenazadas.AmenazarTablero(cant_amenazasxCasillas.tablero, pos_piezas.tablero, arrayPiezas, true);
                                    casillas_amenazadas.ChequeoCasillerosLibres();
                                    if (casillas_amenazadas.casillas_no_amenazadas == 0 && ChequearTablero() == true)
                                    {
                                        copiarPosiciones();

                                        cant_tab_generados++;
                                        Console.WriteLine("\nTengo tablero n°:" + cant_tab_generados);
                                        pos_piezas.ImprimirTablero();
                                        casillas_amenazadas.ImprimirTablero();

                                    }
                                }
                            }
                        }
                    }
                }
            

        }
    }
}

