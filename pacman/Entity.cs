using SFML.Graphics;
using SFML.System;

namespace pacman;

public abstract class Entity
{
    public bool dead = false;
    protected readonly Sprite sprite;
    protected readonly string texturename;
    public virtual bool solid => false;
    public virtual FloatRect Bounds => sprite.GetGlobalBounds();
    
    public Vector2f Position
    {
        get => sprite.Position;
        set => sprite.Position = value;
    }
    
    public Entity(string textureName)
    {
        sprite = new Sprite();
        this.texturename = textureName;
    }

    public virtual void Create(Scene scene)
    {
        sprite.Texture = AssetManager.GameTexture;
    }

    public virtual void Destroy(Scene scene)
    {
        
    }
    

    public virtual void Update(Scene scene, float deltaTime)
    {
        
    }

    public virtual void Render(RenderTarget target)
    {
        target.Draw(sprite);
    }
    
}