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

    private Texture TheTexture
    {
        get => new Texture($"{AssetPath}pacman.png");
    } 
    private Texture TheFont
    {
        get => new Texture($"{AssetPath}pixel-font.ttf");
    } 
    private Texture TheLvel
    {
        get => new Texture($"{AssetPath}level.txt");
    } 

}