using SFML.Graphics;
using SFML.System;
namespace pacman;

public class Coin : Entity
{
    public Coin() : base("pacman"){}

    public override void Create(Scene scene)
    {
        base.Create(scene);
        sprite.TextureRect = new IntRect(36, 36, 18, 18);
    }
    protected override void CollideWith(Scene scene, Entity e)
    {
        if (e is Pacmannen)
        {
            scene.EventHandler.PublishGainScore(100);
            dead = true;
        }
    }
}


public class Candy : Entity
{
    public Candy() : base("pacman"){}
    public override void Create(Scene scene)
    {
        base.Create(scene);
        sprite.TextureRect = new IntRect(36, 54, 18, 18);
    }
    protected override void CollideWith(Scene scene, Entity e)
    {
        if (e is Pacmannen)
        {
            scene.EventHandler.PublishCandyEaten(1);
            dead = true;
            
        }
    }
}