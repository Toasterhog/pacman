namespace pacman;
using SFML.System;
public class LevelLoader
{
    // public enum TileContent { empty, wall, point, candy }
    // public TileContent[,] Map = new TileContent[23,23];
    // public readonly Dictionary<char, Func<Entity>> loaders = new Dictionary<char, Func<Entity>> 
    // {
    //     {'#', () => new Wall()},
    //     {'.', () => new Wall()}
    // };
    public readonly Dictionary<char, Entity?> char2Entity = new Dictionary<char, Entity?>
    {
        {'#', new Wall()},
        {'.', null},
        {'|', null},
    };

    Entity? GetEType(char c)
    {
        switch (c)
        {
            case '#':
                return new Wall();
            case 'p':
                return new Pacmannen();
            case 'g':
                return new Ghost();
            default:
                return null;
        }
    }

    public void Load(Scene scene)
    {
        Console.Write("loadededed");
        string filepath = AssetManager.levelFilePath;
        int y = -1; //så första blir 0
        foreach (var textRow in File.ReadLines(filepath, System.Text.Encoding.UTF8))
        {
            y++;
            for (int i = 0; i < textRow.Length; i++)
            {
                char characther = textRow[i];
                Vector2f position = new Vector2f(i, y) * 18f;
                Entity entityToAdd = GetEType(characther);
                if (entityToAdd != null)
                {
                    entityToAdd.Position = position; 
                    scene.Spawn(entityToAdd);
                }
               
            }
            
        }
        
    }
}