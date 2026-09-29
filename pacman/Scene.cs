namespace pacman;

public class Scene
{
    public AssetManager AssetManager = new AssetManager();
    public LevelLoader Level = new LevelLoader();
    private List<Entity> entities = new List<Entity>();
    
    public Scene(){}

    public void Spawn(Entity entity)
    {
        entities.Add(entity);
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
    //TODO 10 11

}