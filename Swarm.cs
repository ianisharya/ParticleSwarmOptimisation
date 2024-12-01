#pragma warning disable CS8604 // Possible null reference argument.

namespace ParticleSwarmOptimisation;

/// <summary>
/// Represents the swarm of particles and the logic to perform the PSO algorithm.
/// </summary>
/// <remarks>Author: Anish Arya</remarks>
public class Swarm
{
    private readonly List<Particle> _particles = new List<Particle>(); // List of all particles in the swarm.

    private readonly Random _random = new Random(); // Random number generator for generating random positions and velocities.

    public Vector? SBest { get; private set; } // Global best position (sbest) found by the entire swarm.

    public double SBestValue { get; private set; } = double.MaxValue; // The value of the objective function at the global best position.

    private readonly double _w; // Inertia weight influencing the particle's momentum (controls exploration vs. exploitation).

    private readonly double _cp; // Cognitive acceleration coefficient (how much a particle is influenced by its own best-known position).

    private readonly double _cs; // Social acceleration coefficient (how much a particle is influenced by the global best position found by the swarm).

    private readonly Func<Vector, double> _objectiveFunction; // The objective function being minimized by the swarm.


    // Constructor
    public Swarm(
      [DisallowNull] int particleCount = 30, // number of particles in the swarm
      [DisallowNull] double w = 0.4,
      [DisallowNull] double cp = 2.0,
      [DisallowNull] double cs = 1.5,
                     Func<Vector, double>? objectiveFunction = null,
      [DisallowNull] double xMin = -10,
      [DisallowNull] double xMax = 10, // domain bounds
      [DisallowNull] double yMin = -10,
      [DisallowNull] double yMax = 10) // domain bounds
    {

        // Validate parameters
        if (particleCount <= 0) throw new ArgumentException("Particle count must be greater than zero.");

        if (w < 0 || w > 1) throw new ArgumentException("Inertia weight w must be between 0 and 1.");

        if (cp < 1 || cp > 2) throw new ArgumentException("Cognitive particle acceleration coefficient cp must be between 1 and 2.");

        if (cs < 1 || cs > 2) throw new ArgumentException("Social swarm acceleration coefficient cs must be between 1 and 2.");

        if (objectiveFunction == null) throw new ArgumentNullException(nameof(objectiveFunction), "Objective function cannot be null.");

        if (xMin >= xMax || yMin >= yMax) throw new ArgumentException("Invalid domain bounds.");

        _w = w;
        _cp = cp;
        _cs = cs;
        _objectiveFunction = objectiveFunction;

        // Initialize particles with random positions and velocities
        for (int i = 0; i < particleCount; i++)
        {

            var position = new Vector(
              xMin + _random.NextDouble() * (xMax - xMin),
              yMin + _random.NextDouble() * (yMax - yMin));

            var velocity = new Vector(
              (_random.NextDouble() - 0.5) * (xMax - xMin) / 10,
              (_random.NextDouble() - 0.5) * (yMax - yMin) / 10);

            var particle = new Particle(position, velocity);

            _particles.Add(particle); // add the particle

            // Calculate the initial objective function value for this particle.
            double value = _objectiveFunction(position);
            particle.PBestValue = value;

            if (value < SBestValue)
            {
                SBestValue = value;
                SBest = position;
            }
        }
    }


    /// <summary>
    /// Updates the position and velocity of each particle according to the PSO formula.
    /// </summary>
    public void Update()
    {
        foreach (var particle in _particles)
        {
            double r1 = _random.NextDouble();
            double r2 = _random.NextDouble();

            // Calculate cognitive and social components
            var cognitive = _cp * r1 * (particle.PBest - particle.CurrentPosition);

            var social = _cs * r2 * (SBest - particle.CurrentPosition);

            // Update velocity
            particle.CurrentVelocity = _w * particle.CurrentVelocity + cognitive + social;

            // Update position
            particle.CurrentPosition += particle.CurrentVelocity;

            // Evaluate the objective function at the new position
            double value = _objectiveFunction(particle.CurrentPosition);

            // Update personal best if necessary
            if (value < particle.PBestValue)
            {
                particle.PBestValue = value;
                particle.PBest = particle.CurrentPosition;
            }

            // Update global best if necessary
            if (value < SBestValue)
            {
                SBestValue = value;
                SBest = particle.CurrentPosition;
            }
        }
    }


    /// <summary>
    /// Runs the PSO algorithm for a specified number of steps.
    /// </summary>
    public void Run(
      [DisallowNull] int steps)
    {
        if (steps <= 0) throw new ArgumentException("Number of steps must be greater than zero.");

        for (int step = 0; step < steps; step++)
        {
            Update();
            Console.WriteLine($"Step {step + 1}: Best Value = {SBestValue:F6} at Position {SBest}");
        }
    }
}