using SFML.Graphics;

namespace pacman;

public delegate void ValueChangedEvent(Scene scene, int value); 
public sealed class Scene
{
    public event ValueChangedEvent GainScore;
    public event ValueChangedEvent LoseHealth;
    public AssetManager AssetManager = new AssetManager();
    public LevelLoader LevelLoader = new LevelLoader();
    private List<Entity> entities = new List<Entity>();
    private int scoreGained;
    private int lostHealth;
    public void PublishGainScore(int amount) => scoreGained += amount;
    public void PublishLostHealth(int amount) => lostHealth += amount;
    public Scene()
    {
        LevelLoader.Load(this);
    }

    public void Spawn(Entity entity)
    {
        entities.Add(entity);
        entity.Create(this);
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
            if (scoreGained != 0)
            {
                GainScore?.Invoke(this, scoreGained);
                scoreGained = 0;
            }

            if (lostHealth != 0)
            {
                LoseHealth?.Invoke(this, lostHealth);
                lostHealth = 0;
            }
            
        }
        for (int i = entities.Count - 1; i >= 0; i--)
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