using System;
using System.Collections.Generic;
using System.Threading;


abstract class Node
{
    public abstract bool Execute();
}


abstract class Composite : Node
{
    protected List<Node> children = new List<Node>();

    public void AddChild(Node child)
    {
        children.Add(child);
    }
}

class Sequence : Composite
{
    public override bool Execute()
    {
        foreach (var child in children)
        {
            if (!child.Execute())
            {
                return false;
            }
        }
        return true;
    }
}


class Selector : Composite
{
    public override bool Execute()
    {
        foreach (var child in children)
        {
            if (child.Execute())
            {
                return true;
            }
        }
        return false;
    }
}


class Root : Composite
{
    public override bool Execute()
    {
        return children.Count > 0 && children[0].Execute();
    }
}


class IsPlayerAtDistance : Node
{
    private int distance;
    public IsPlayerAtDistance(int distance) => this.distance = distance;

    public override bool Execute()
    {
        Console.WriteLine($"Verificando distancia al jugador: {distance}");
        return distance < 5; 
    }
}


class MoveTo : Node
{
    private int x, y, z;
    public MoveTo(int x, int y, int z) => (this.x, this.y, this.z) = (x, y, z);

    public override bool Execute()
    {
        Console.WriteLine($"Moviendo a ({x}, {y}, {z})");
        return true;
    }
}

class Jump : Node
{
    public override bool Execute()
    {
        Console.WriteLine("Saltando...");
        return true;
    }
}

class Wait : Node
{
    private int duration;
    public Wait(int duration) => this.duration = duration;

    public override bool Execute()
    {
        Console.WriteLine($"Esperando {duration} ms...");
        Thread.Sleep(duration);
        return true;
    }
}


class Program
{
    static void Main()
    {
        Root root = new Root();
        Sequence sequence = new Sequence();

        Selector selector = new Selector();
        IsPlayerAtDistance checkDistance = new IsPlayerAtDistance(3);
        MoveTo moveTask = new MoveTo(10, 5, 2);

        selector.AddChild(checkDistance);
        selector.AddChild(moveTask);

        sequence.AddChild(selector);
        sequence.AddChild(new Jump());
        sequence.AddChild(new Wait(1000));

        root.AddChild(sequence);

        Console.WriteLine("Ejecutando Árbol de Comportamiento...");
        root.Execute();
    }
}

