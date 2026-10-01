using SFML.Graphics;
using SFML.System;

namespace pacman;

public class Ghost : Actor
{
    private Func<List<int>, Scene, Vector2f, int> pickFromValidMovesDel;
    private float frozenTimer = 0f;
    
    public override void Create(Scene scene)
    {
        direction = -1;
        animXOffset = [36, 54];
        if (new Random().Next(0, 2) == 1) //blue
        {
            sprite.TextureRect = new IntRect(36, 0, 18, 18);
            pickFromValidMovesDel = PickFromValidMoves_BLUE;
            speed = 70.0f;
        }
        else //red
        {
            sprite.TextureRect = new IntRect(36, 0, 18, 18);
            pickFromValidMovesDel = PickFromValidMoves_RED;
            speed = 100;
        }

        scene.EventHandler.LoseHealth += OnLoseHealth;
        scene.EventHandler.CandyEaten += OnCandyEaten;
        base.Create(scene);
    }
    private void OnLoseHealth(Scene scene, int amount)
    {
        Reset();
    }

    private void OnCandyEaten(Scene scene, int amount)
    {
        frozenTimer = 5f;
    }

    public override void Update(Scene scene, float deltaTime)
    {
        base.Update(scene, deltaTime);
        frozenTimer = MathF.Max(frozenTimer - deltaTime, 0.0f);
    }

    public override void Render(RenderTarget target)
    {
        if (frozenTimer > 0f)
        {
            IntRect rect = sprite.TextureRect;
            rect.Top = 18;
            sprite.TextureRect = rect;
        }
        else
        {
            IntRect rect = sprite.TextureRect;
            rect.Top = 0;
            sprite.TextureRect = rect;
        }
        base.Render(target);
    }

    protected override int PickDirection(Scene scene)
    {
        List<int> validMoves = new List<int>();
        for (int i = 0; i < 4; i++)
        {
            if ((i + 2) % 4 == direction) continue;
            if(IsFree(scene, i)) validMoves.Add(i);
        }

        int res =  pickFromValidMovesDel(validMoves, scene, Position);
        return res;
    }

    protected override void CollideWith(Scene scene, Entity e)
    {
        if (e is Pacmannen)
        {
            if (frozenTimer <= 0)
            {
                scene.EventHandler.PublishLostHealth(1);
            }
            Reset();
        }
    }
    
    private static int PickFromValidMoves_RED(List<int> moves, Scene scene, Vector2f selfPosition)
    {
        int r = new Random().Next(0, moves.Count);
        return moves[r];
    }
    private static int PickFromValidMoves_BLUE(List<int> moves, Scene scene, Vector2f selfPosition)
    {
        if(scene.FindByType<Pacmannen>(out Pacmannen pac))
        {
            Vector2f towardPac = pac.Position - selfPosition;
            float highestDotResultYet = -1000f;
            int bestMoveYet = -1;
            foreach (int move in moves)
            {
                Vector2f moveAsVec = ToVector(move);
                float dotResult = DotProductForSFMLVectors(moveAsVec, towardPac);
                if (dotResult > highestDotResultYet)
                {
                    highestDotResultYet = dotResult;
                    bestMoveYet = move;
                }
                else if (dotResult == highestDotResultYet) // om två moves är like bra: slumpa
                {
                    if( new Random().Next(0, 2) == 1 ) bestMoveYet = move;
                }
            }
            return bestMoveYet;
        }
        return new Random().Next(0, moves.Count);
    }
    /// Samma som längderna av vektorerna multiplicerad * cos av vinkeln mellan dem 
    public static float DotProductForSFMLVectors( Vector2f a, Vector2f b){ return a.X * b.X + a.Y * b.Y; } //SFML 2.x har tydligen ingen inbygd vectoralgebra
}