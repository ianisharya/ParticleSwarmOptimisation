namespace ParticleSwarmOptimisation;

/// <summary>
/// Represents a single particle in the Particle Swarm Optimization (PSO) algorithm.
/// Each particle has a position and velocity in the search space and keeps track of its personal best position and the corresponding function value.
/// The particle moves through the search space, adjusting its velocity based on its personal experience and the swarm's collective experience.
/// </summary>
/// <remarks>Author: Anish Arya</remarks>
public class Particle
{
    // The current position of the particle in the search space (Vector with X and Y coordinates).
    public Vector CurrentPosition { get; set; }

    // The current velocity of the particle, which determines how it moves in the search space.
    public Vector CurrentVelocity { get; set; }

    // The personal best position found by the particle so far (the best position based on past evaluations).
    public Vector PBest { get; set; }

    // The value of the objective function at the personal best position (lower is better).
    public double PBestValue { get; set; }

    // Constructor
    public Particle(
      [DisallowNull] Vector position,
      [DisallowNull] Vector velocity)
    {
        this.CurrentPosition = position;
        this.CurrentVelocity = velocity;
        this.PBest = position;
        // Set the initial personal best value to a large number, since the particle has not yet evaluated any positions.
        this.PBestValue = double.MaxValue;
    }
}