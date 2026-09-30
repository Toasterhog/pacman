using SFML.Graphics;

namespace pacman;

public class Scene
{
    public AssetManager AssetManager = new AssetManager();
    public LevelLoader LevelLoader = new LevelLoader();
    private List<Entity> entities = new List<Entity>();

    public Scene()
    {
        LevelLoader.Load(this);
    }

    public void Spawn(Entity entity)
    {
        entities.Add(entity);
        entity.Create(this);
    }

    public void DeSpawn(Entity entity)
    {
        
    }
    private void Clear()
    {
        for (int i = entities.Count - 1; i >= 0; i--)
        {
            Entity entity = entities[i];
            entities.RemoveAt(i);
            entity.Destroy(this);
        }
    }

    public void RenderAll(RenderTarget target)
    {
        foreach (Entity entity in entities)
        {
            entity.Render(target);
        }
    }
    public IEnumerable<Entity> FindIntersects(FloatRect bounds)
    {
        int lastEntity = entities.Count - 1;
        for (int i = lastEntity; i >= 0; i--)
        {
            Entity entity = entities[i];
            if (entity.dead) continue;
            if (entity.Bounds.Intersects(bounds))
            {
                yield return entity;
            }
        }
    }
    
    //public void Update(float deltaTime){}
    //TODO 10 11

}