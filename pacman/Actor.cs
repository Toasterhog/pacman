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
    protected float originalSpeed = 1.0f;
    protected Actor() : base("pacman") { }
    public override void Create(Scene scene)
    {
        base.Create(scene);
        originalPosition = Position;
        originalSpeed = speed;
    }
    protected bool IsAligned => (int)(Position.X) % 18 == 0 && (int)(Position.Y) % 18 == 0;
    protected void Reset()
    {
        Position = originalPosition;
        speed = originalSpeed;
        wasAligned = false;
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
        base.Update(scene, deltaTime);
        if (IsAligned)
        {
            if (!wasAligned) // kollar om was alignet är false och då kommer den välja en ny direction, om den är true så kan man inte bya direction
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
        Animate(deltaTime);
    }

    private float animationTime = 0;
    private const float SPF = 1.0f / 10.0f; //seconds per frame, så att man vet hår läng tid mellan frame byte
    protected int[] animXOffset = [0, 0]; //gets set in the Create() of the children 
    protected void Animate(float deltatime)
    {
        animationTime += deltatime;
        IntRect rect = sprite.TextureRect;
        if (animationTime > 2 * SPF)
        {
            animationTime -= 2 * SPF;
            rect.Left = animXOffset[0];
        }else if  (animationTime > SPF)
        {
            rect.Left = animXOffset[1];
        }
        sprite.TextureRect = rect;
    }
}