namespace pacman;
using SFML.Graphics;

public class AssetManager
{
    public static readonly string AssetPath = "assets/";
    // private readonly Dictionary<string, Texture> textures;
    // private readonly Dictionary<string, Font> fonts;
    //
    // public AssetManager() {
    //     textures = new Dictionary<string, Texture>();
    //     fonts    = new Dictionary<string, Font>();
    // }
    //
    // public Texture LoadTexture(string name)
    // {
    //     
    // }
    //
    // public Font LoadFont(string name)
    // {
    //     
    // }
    public static readonly Texture GameTexture = new Texture($"{AssetPath}pacman.png");
    public static readonly Font GameFont = new Font($"{AssetPath}pixel-font.ttf");
    public static string levelFilePath = $"{AssetPath}maze.txt";
}