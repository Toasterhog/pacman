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
    public void UpdateAll(float deltaTime)
    {
        foreach (Entity entity in entities)
        {
            entity.Update(this, deltaTime);
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
    {//hje jag heter anton kommerntaren
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
    public bool FindByType<T>(out T found) where T : Entity
    {
        for (int i = 0; i < entities.Count; i++)
        {
            if (entities[i] is T match)
            {
                found = match;
                return true;
            }
            Entity entity = entities[i];
            if (!entity.dead && entity is T typed)
            {
                found = typed;
                return true;
            }
        }
        found = null;
        return false;
    } // säger bara att T måste vara en entity eller att den måste ärva något från entity
    //public void Update(float deltaTime){}
    //TODO 10 11
    

}