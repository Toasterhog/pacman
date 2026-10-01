using SFML.Window;
using SFML.Graphics;
using SFML.System;
namespace pacman;
public class GUI : Entity
{
    private Text scoreText = new Text();
    private int maxHealth = 5;
    private int currentHealth;
    private int currentScore = 0;
    public GUI() : base("pacman"){}

    public override void Create(Scene scene)
    {
        base.Create(scene);
        scoreText.Font = AssetManager.GameFont;
        scoreText.DisplayedString = "Score";
        scoreText.CharacterSize = 100;
        scoreText.Scale = 0.30f * new Vector2f(1, 1);
        currentHealth = maxHealth;
        sprite.TextureRect = new IntRect(5 * 18, 0, 18, 18);
        scene.EventHandler.LoseHealth += OnLoseHealth;
        scene.EventHandler.GainScore += OnGainScore;
    }

    public override void Render(RenderTarget target)
    {
        sprite.Position = new Vector2f(36, 396);
        for (int i = 0; i < maxHealth; i++) {
            sprite.TextureRect = i < currentHealth
                ? new IntRect(72, 36, 18, 18) // Full heart
                : new IntRect(72, 0, 18, 18); // Empty heart
            base.Render(target); 
            sprite.Position += new Vector2f(18, 0);
        }
        scoreText.DisplayedString = $"Score: {currentScore}";
        scoreText.Position = new Vector2f(414 - scoreText.GetGlobalBounds().Width, 396);
        target.Draw(scoreText);
    }
    private void OnLoseHealth(Scene scene, int amount) {
        currentHealth-= amount;
        if (currentHealth <= 0)
        {
            dontDestroyonLoad = false;
            scene.LevelLoader.shouldReload = true;
        }
    }
    private void OnGainScore(Scene scene, int amount)
    {
        currentScore += amount;
        if (!scene.FindByType<Coin>(out _)) {
            dontDestroyonLoad = true;
            scene.LevelLoader.shouldReload = true;
        }
    }
}