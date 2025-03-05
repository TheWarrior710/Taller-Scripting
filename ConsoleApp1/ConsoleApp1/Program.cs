namespace ConsoleApp1
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    abstract class NodoBT
    {
        public abstract bool Ejecutar();
    }

    // Nodo Selector: Evalúa hijos hasta que uno tenga éxito
    class NodoSelector : NodoBT
    {
        private List<NodoBT> hijos = new List<NodoBT>();

        public void AgregarHijo(NodoBT hijo) => hijos.Add(hijo);

        public override bool Ejecutar()
        {
            foreach (var hijo in hijos)
            {
                if (hijo.Ejecutar()) return true; // Si un hijo tiene éxito, el selector también
            }
            return false;
        }
    }

    // Nodo Secuencia: Evalúa todos sus hijos en orden
    class NodoSecuencia : NodoBT
    {
        private List<NodoBT> hijos = new List<NodoBT>();

        public void AgregarHijo(NodoBT hijo) => hijos.Add(hijo);

        public override bool Ejecutar()
        {
            foreach (var hijo in hijos)
            {
                if (!hijo.Ejecutar()) return false; // Si un hijo falla, la secuencia falla
            }
            return true;
        }
    }

    // Nodo Raíz
    class NodoRaiz : NodoBT
    {
        private NodoBT hijo;

        public NodoRaiz(NodoBT hijo) { this.hijo = hijo; }

        public override bool Ejecutar()
        {
            return hijo?.Ejecutar() ?? false;
        }
    }

    // Nodo Evaluar Distancia
    class NodoEvaluarDistancia : NodoBT
    {
        private Func<int> obtenerDistancia;
        private int distanciaValida;

        public NodoEvaluarDistancia(Func<int> obtenerDistancia, int umbral)
        {
            this.obtenerDistancia = obtenerDistancia;
            distanciaValida = umbral;
        }

        public override bool Ejecutar()
        {
            int distancia = obtenerDistancia();
            Console.WriteLine($"Evaluando distancia: {distancia}");
            return distancia <= distanciaValida;
        }
    }

    // Nodo Moverse
    class NodoMoverse : NodoBT
    {
        private Action avanzar;
        private Func<bool> objetivoAlcanzado;

        public NodoMoverse(Action avanzar, Func<bool> objetivoAlcanzado)
        {
            this.avanzar = avanzar;
            this.objetivoAlcanzado = objetivoAlcanzado;
        }

        public override bool Ejecutar()
        {
            if (!objetivoAlcanzado())
            {
                avanzar();
                return false; // Todavía no ha terminado
            }
            return true; // Objetivo alcanzado
        }
    }

    // Nodo Esperar
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

    // Programa principal
    class Program
    {
        static void Main()
        {
            int distanciaActual = 10;
            int distanciaValida = 5;
            int posicionActual = 10;
            int posicionObjetivo = 0;
            int tiempoEspera = 2;

            // Funciones para actualizar distancia y posición
            Func<int> obtenerDistancia = () => posicionActual - posicionObjetivo;
            Action avanzar = () =>
            {
                posicionActual--;
                Console.WriteLine($"Moviéndose... posición actual: {posicionActual}");
            };
            Func<bool> objetivoAlcanzado = () => posicionActual <= posicionObjetivo;

            // Crear nodos
            NodoBT nodoEvaluar = new NodoEvaluarDistancia(obtenerDistancia, distanciaValida);
            NodoBT nodoMoverse = new NodoMoverse(avanzar, objetivoAlcanzado);
            NodoBT nodoEsperar = new NodoEsperar(tiempoEspera);

            // Crear selector y secuencia
            NodoSelector selector = new NodoSelector();
            selector.AgregarHijo(nodoEvaluar);

            NodoSecuencia secuencia = new NodoSecuencia();
            secuencia.AgregarHijo(selector);
            secuencia.AgregarHijo(nodoMoverse);
            secuencia.AgregarHijo(nodoEsperar);

            // Crear nodo raíz
            NodoRaiz arbol = new NodoRaiz(secuencia);

            // Ejecutar el árbol con una condición de salida
            while (!objetivoAlcanzado())
            {
                Console.WriteLine("\nEjecutando Árbol de Comportamiento...");
                arbol.Ejecutar();
            }

            Console.WriteLine("¡Objetivo alcanzado! Fin del programa.");
        }
    }

}
