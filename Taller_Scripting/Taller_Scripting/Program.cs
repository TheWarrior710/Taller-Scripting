using System;
using System.Threading;
using System.Collections.Generic;


abstract class NodoBT
{
    public abstract bool Ejecutar();
}

class NodoRaiz : NodoBT
{
    private NodoBT hijo;

    public NodoRaiz(NodoBT hijo)
    {
        this.hijo = hijo;
    }

    public override bool Ejecutar()
    {
        return hijo.Ejecutar();
    }
}


abstract class NodoCompuesto : NodoBT
{
    protected List<NodoBT> hijos = new List<NodoBT>();

    public void AgregarHijo(NodoBT hijo)
    {
        hijos.Add(hijo);
    }
}


class NodoSecuencia : NodoCompuesto
{
    public override bool Ejecutar()
    {
        foreach (var hijo in hijos)
        {
            if (!hijo.Ejecutar())
            {
                return false; // Si falla un hijo, la secuencia también falla
            }
        }
        return true; // Todos los hijos fueron exitosos
    }
}

class NodoSelector : NodoCompuesto
{
    private Func<bool> Evaluar;

    public NodoSelector(Func<bool> evaluar)
    {
        this.Evaluar = evaluar;
    }

    public override bool Ejecutar()
    {
        if (!Evaluar()) return false;

        foreach (var hijo in hijos)
        {
            if (hijo.Ejecutar())
            {
                return true; 
            }
        }
        return false;
    }
}

class NodoTarea : NodoBT
{
    private Func<bool> accion;

    public NodoTarea(Func<bool> accion)
    {
        this.accion = accion;
    }

    public override bool Ejecutar()
    {
        return accion();
    }
}

class Program
{
    static float posicionJugador = 0;
    static float posicionObjetivo = 10;
    static float distanciaValida = 2;
    static float velocidad = 1;
    static int tiempoEspera = 1000;

    static bool EvaluarDistancia()
    {
        return Math.Abs(posicionObjetivo - posicionJugador) <= distanciaValida;
    }

    static bool MoverseHaciaObjetivo()
    {
        if (posicionJugador < posicionObjetivo)
        {
            posicionJugador += velocidad;
        }
        else if (posicionJugador > posicionObjetivo)
        {
            posicionJugador -= velocidad;
        }
        Console.WriteLine($"Jugador en posición: {posicionJugador}");
        return posicionJugador == posicionObjetivo;
    }

    static bool Esperar()
    {
        Console.WriteLine("Esperando...");
        Thread.Sleep(tiempoEspera);
        return true;
    }

    static void Main()
    {
        NodoSelector selectorDistancia = new NodoSelector(EvaluarDistancia);
        selectorDistancia.AgregarHijo(new NodoTarea(MoverseHaciaObjetivo));

        NodoSecuencia secuencia = new NodoSecuencia();
        NodoSelector selectorGeneral = new NodoSelector(() => true);
        selectorGeneral.AgregarHijo(selectorDistancia);
        secuencia.AgregarHijo(selectorGeneral);
        secuencia.AgregarHijo(new NodoTarea(Esperar));

        NodoRaiz raiz = new NodoRaiz(secuencia);

        while (!raiz.Ejecutar())
        {
            Console.WriteLine("Ejecutando ciclo de comportamiento...");
        }
        Console.WriteLine("Objetivo alcanzado.");
    }
}
