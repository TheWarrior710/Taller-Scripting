using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taller_Scripting
{
    class NodoEvaluarDistancia : NodoBT
    {
        private int distanciaObjetivo;
        private int distanciaValida;

        public NodoEvaluarDistancia(int distancia, int umbral)
        {
            distanciaObjetivo = distancia;
            distanciaValida = umbral;
        }

        public override bool Ejecutar()
        {
            return distanciaObjetivo <= distanciaValida;
        }
    }

    class NodoMoverse : NodoBT
    {
        private int posicionActual;
        private int posicionObjetivo;

        public NodoMoverse(int posicionInicial, int objetivo)
        {
            posicionActual = posicionInicial;
            posicionObjetivo = objetivo;
        }

        public override bool Ejecutar()
        {
            while (posicionActual < posicionObjetivo)
            {
                posicionActual++;
                Console.WriteLine($"Moviéndose... posición actual: {posicionActual}");
                Thread.Sleep(500); 
            }
            return true;
        }
    }

    class NodoEsperar : NodoBT
    {
        private int tiempoEspera;

        public NodoEsperar(int tiempo)
        {
            tiempoEspera = tiempo;
        }

        public override bool Ejecutar()
        {
            Console.WriteLine($"Esperando {tiempoEspera} segundos...");
            Thread.Sleep(tiempoEspera * 1000);
            return true;
        }
    }
}
