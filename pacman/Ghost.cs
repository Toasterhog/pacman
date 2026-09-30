using SFML.Graphics;
using SFML.System;

namespace pacman;

public class Ghost : Actor
{
    public override void Create(Scene scene)
    {
        direction = -1;
        speed = 100.0f;
        base.Create(scene);
        sprite.TextureRect = new IntRect(36, 0, 18, 18);
    }

    protected override int PickDirection(Scene scene)
    {
        List<int> validMoves = new List<int>();
        for (int i = 0; i < 4; i++)
        {
            if ((i + 2) % 4 == direction) continue;
            if(IsFree(scene, i)) validMoves.Add(i);
        }

        int r = new Random().Next(0, validMoves.Count);
        return validMoves[r];
    }

    // private Func<List<int>, Scene, int> pickFromValidMovesDel = PickFromValidMoves_BLUE
    //
    // private int PickFromValidMoves_RED(List<int> moves)
    // {
    //     return new Random().Next(0, moves.Count);
    // }
    // private int PickFromValidMoves_BLUE(List<int> moves, Scene scene)
    // {
    //     if(scene.FindByType<Pacmannen>(out Pacmannen pm))
    //     {
    //         Vector2f pmPos = pm.Position;
    //         Vector2f dir = pmPos - Position;
    //         int preferred;
    //
    //         if (Math.Abs(dir.X) > Math.Abs(dir.Y))
    //         {
    //             if (dir.Y > 0) { preferred = 1; }
    //             else { preferred = 3; }
    //         }
    //         else
    //         {
    //             if (dir.X > 0) { preferred = 0; }
    //             else { preferred = 2; }
    //         }
    //
    //         foreach (int m in moves)
    //         {
    //             if (m == preferred)
    //             {
    //                 return preferred;
    //             }
    //         }
    //     }
    //     return new Random().Next(0, moves.Count);
    // }
}