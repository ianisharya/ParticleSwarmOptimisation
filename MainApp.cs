global using System.Diagnostics.CodeAnalysis;

namespace ParticleSwarmOptimisation;

class MainApp
{
    static void Main()
    {
        // First function: f(x, y) = (x - 1)^2 + 10(x^2 - y)^2
        Func<Vector, double> function1 = v => Math.Pow(v.X - 1, 2) + 10 * Math.Pow(v.X * v.X - v.Y, 2);

        Console.WriteLine("\nMinimizing f(x, y) = (x - 1)^2 + 10(x^2 - y)^2 on [-1, 1.5] x [-1, 1.5]:\n");
        var swarm1 = new Swarm(
          particleCount: 30,
          w: 0.4,
          cp: 2.0,
          cs: 1.5,
          objectiveFunction: function1,
          xMin: -1,
          xMax: 1.5,
          yMin: -1,
          yMax: 1.5);
        swarm1.Run(steps: 100);

        Console.WriteLine("\n=================================================================\n");

        // Second function: f(x, y) = xe^(-x^2 - y^2)
        Func<Vector, double> function2 = v => v.X * Math.Exp(-(v.X * v.X + v.Y * v.Y));

        Console.WriteLine("\nMinimizing f(x, y) = xe^(-x^2 - y^2) on [-2, 2] x [-2, 2]:\n");
        var swarm2 = new Swarm(
          particleCount: 30,
          w: 0.4,
          cp: 2.0,
          cs: 1.5,
          objectiveFunction: function2,
          xMin: -2,
          xMax: 2,
          yMin: -2,
          yMax: 2);
        swarm2.Run(steps: 100);

        Console.WriteLine("\n=================================================================\n");

        Console.WriteLine("\nHit Enter/Return key to exit:\n");
        Console.ReadLine();
    }
}