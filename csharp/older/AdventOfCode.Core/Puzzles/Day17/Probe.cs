using System.Numerics;

namespace AdventOfCode.Core.Puzzles.Day17;

public class Probe
{
    public Vector2 pos {get; private set;}
    public int highestY {get; private set;} = int.MinValue;
    public Vector2 initalVelocity {get; private set;}

    protected Vector2 vel;
    protected Vector2[] targetArea;

    public Probe(Vector2 initialVel, Vector2[] targetArea)
    {
        this.initalVelocity = initialVel;
        vel = initialVel;
        pos = new Vector2(0, 0);
        this.targetArea = targetArea;
    }    

    public bool Update()
    {
        pos += vel;
        vel.X = vel.X  > 0 ? vel.X - 1 : 0;
        vel.Y--;

        if(pos.Y > highestY) highestY = (int)pos.Y;

        return pos.X >= targetArea[0].X && pos.Y >= targetArea[0].Y && pos.X <= targetArea[1].X && pos.Y <= targetArea[1].Y;
    }
}