using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace pacman;

public class Pacmannen : Actor
{
    public override void Create(Scene scene)
    {
        base.Create(scene);
        sprite.TextureRect = new IntRect(0, 0, 18, 18);
        speed = 100;
    }

    protected override int PickDirection(Scene scene)
    {
        int dir = direction;
        if (Keyboard.IsKeyPressed(Keyboard.Key.Right) || Keyboard.IsKeyPressed(Keyboard.Key.D))
        {
            dir = 0;
            moving = true;
        }
        if (Keyboard.IsKeyPressed(Keyboard.Key.Down) || Keyboard.IsKeyPressed(Keyboard.Key.S))
        {
            dir = 1;
            moving = true;
        }
        if (Keyboard.IsKeyPressed(Keyboard.Key.Left) || Keyboard.IsKeyPressed(Keyboard.Key.A))
        {
            dir = 2;
            moving = true;
        }
        if (Keyboard.IsKeyPressed(Keyboard.Key.Up) || Keyboard.IsKeyPressed(Keyboard.Key.W))
        {
            dir = 3;
            moving = true;
        }

        int[] spriteYOffsets = [0, 3, 2, 1];
        sprite.TextureRect = new IntRect(0, spriteYOffsets[direction]*18, 18, 18);
        if (IsFree(scene, dir)) return dir;
        if (!IsFree(scene, direction)) moving = false;
        return direction;
        
    }

    //hej - lukas
}