using System.Data.SqlTypes;
using SFML.System;
using System.Linq;
using SFML.Graphics;
using SFML.Window;
namespace pacman;
public class Actor : Entity
{
    private bool wasAligned = false;
    protected float speed = 1.0f;
    protected int direction = 0;
    protected bool moving = true;
    protected Vector2f originalPosition;
    protected float originalSpeed;
    protected Actor() : base("pacman")
    {

    }
    protected bool IsAligned => (int)(Position.X) % 18 == 0 && (int)(Position.Y) % 18 == 0;
    protected void Reset()
    {
        
    }
    protected static Vector2f ToVector(int direction)
    {
        switch (direction)
        {
            case 0:
                return new Vector2f(1, 0);
            case 1:
                return new Vector2f(0, 1);
            case 2:
                return new Vector2f(-1, 0);
            case 3:
                return new Vector2f(0, -1);
            default:
                return new Vector2f(0, 0);
        }
    }
    /// returns true if it doesnt intersect any solid entity
    protected bool IsFree (Scene scene, int direction) 
    {
        Vector2f at = Position + new Vector2f(9, 9);
        at += 18 * ToVector(direction);
        FloatRect rect = new FloatRect(at.X, at.Y, 1, 1);
        return !scene.FindIntersects(rect).Any(e => e.solid);
    }
    protected virtual int PickDirection (Scene scene)
    {
        return 0;
    }
    public override void Update(Scene scene, float deltaTime)
    {
        if (IsAligned)
        {
            if (!wasAligned)
            {
                direction = PickDirection(scene);
            }
            if (moving)
            {
                wasAligned = true;
            }
        }
        else
        {
            wasAligned = false;
        }
        if (!moving) return;
        Position += ToVector(direction) * (speed * deltaTime);
        Position = (int)(Position.X) switch {
            < 0 => new Vector2f(432, Position.Y),
            > 432 => new Vector2f(0, Position.Y),
            _ => Position // om position value inte matcha dem övre så kommer den bara returna position
        };
    }
}