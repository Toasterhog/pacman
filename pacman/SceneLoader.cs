namespace pacman;
using SFML.System;
public class LevelLoader
{
    public bool shouldReload = false;
    
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
            case '.':
                return new Coin();
            case 'c':
                return new Candy();
            default:
                return null;
        }
    }

    public void Load(Scene scene)
    {
        scene.Clear();
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
        if (scene.FindByType<GUI>(out GUI found) == false)
        {
            scene.Spawn(new GUI());
        }
    }
}