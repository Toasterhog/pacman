namespace pacman;
using SFML.System;
public class LevelLoader
{
    // public enum TileContent { empty, wall, point, candy }
    // public TileContent[,] Map = new TileContent[23,23];
    public readonly Dictionary<char, Func<Entity>> loaders = new Dictionary<char, Func<Entity>> 
    {
        {'#', () => new Wall()},
        {'.', () => new Wall()}
    };

    public void Load(Scene scene)
    {
        string filepath = AssetManager.levelFilePath;
        int y = -1; //så första blir 0
        foreach (var textRow in File.ReadLines(filepath, System.Text.Encoding.UTF8))
        {
            y++;
            for (int i = 0; i < textRow.Length; i++)
            {
                char characther = textRow[i];
                Vector2i positon = new Vector2i(i, y);
                Entity entityToAdd = loaders[characther].Invoke(); //borde inte va så här de har tänkt
                scene.Spawn(entityToAdd);
            }
            
        }
        
    }
}