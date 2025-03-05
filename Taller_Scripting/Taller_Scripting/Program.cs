using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Threading;

namespace Taller_Scripting
{
    class Program
    {
    static void Main()
    {
        int distanciaActual = 10;
        int distanciaValida = 5;
        int posicionInicial = 10;
        int posicionObjetivo = 0;
        int tiempoEspera = 2;

     
        NodoBT nodoEvaluar = new NodoEvaluarDistancia(distanciaActual, distanciaValida);
        NodoBT nodoMoverse = new NodoMoverse(posicionInicial, posicionObjetivo);
        NodoBT nodoEsperar = new NodoEsperar(tiempoEspera);

       
        NodoSelector selector = new NodoSelector();
        selector.AgregarHijo(nodoEvaluar);

        NodoSecuencia secuencia = new NodoSecuencia();
        secuencia.AgregarHijo(selector);
        secuencia.AgregarHijo(nodoMoverse);
        secuencia.AgregarHijo(nodoEsperar);

  
        NodoRaiz arbol = new NodoRaiz(secuencia);

    
        while (true)
        {
            Console.WriteLine("Ejecutando Árbol de Comportamiento...");
            arbol.Ejecutar();
        }
    }
}
}
