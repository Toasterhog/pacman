namespace pacman;
public delegate void ValueChangedEvent(Scene scene, int value); 
public class EventHandler
{
    public event ValueChangedEvent GainScore;
    public event ValueChangedEvent LoseHealth;
    public event ValueChangedEvent CandyEaten;
    private int scoreGained = 0;
    private int lostHealth = 0;
    private int candyEaten = 0;
    public void PublishGainScore(int amount) => scoreGained += amount;
    public void PublishLostHealth(int amount) => lostHealth += amount;
    public void PublishCandyEaten(int amount) => candyEaten += amount;

    public void Update(Scene scene)
    {
        if (scoreGained != 0)
        {
            GainScore?.Invoke(scene, scoreGained);
            scoreGained = 0;
        }
        if (lostHealth != 0)
        {
            LoseHealth?.Invoke(scene, lostHealth);
            lostHealth = 0;
        }
        if (candyEaten != 0)
        {
            CandyEaten?.Invoke(scene, candyEaten);
            candyEaten = 0;
        }
    }
    
}