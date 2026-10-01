using SFML.Graphics;

namespace pacman;


public sealed class Scene
{
    public AssetManager AssetManager = new AssetManager();
    public LevelLoader LevelLoader = new LevelLoader();
    public EventHandler EventHandler = new EventHandler();
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
    
    public void Clear()
    {
        for (int i = entities.Count - 1; i >= 0; i--)
        {
            Entity entity = entities[i];
            if (entity.dontDestroyonLoad == false)
            {
                entities.RemoveAt(i);
                entity.Destroy(this);
            }
        }
    }
    public void UpdateAll(float deltaTime)
    {
        
        if (LevelLoader.shouldReload == true)
        {
            LevelLoader.Load(this);
            LevelLoader.shouldReload = false;
        }
        foreach (Entity entity in entities) //update entities
        {
            entity.Update(this, deltaTime);
        }

        EventHandler.Update(this); //updae eventhandler
       
        for (int i = entities.Count - 1; i >= 0; i--) //clear dead ent
        {
            Entity entity = entities[i];
            if (entity.dead == true)
            {
                entities.RemoveAt(i);
                entity.Destroy(this);
            }
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
    {//hje jag heter anton kommerntaren, hej heter jag elnour
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
            if (!entities[i].dead && entities[i] is T typed)
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