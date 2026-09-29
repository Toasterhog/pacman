namespace pacman;

using SFML.Graphics;
using SFML.System;
using SFML.Window; 
using System;

class Program 
{
    static void Main(string[] args) 
    {
        using (RenderWindow window = new RenderWindow(new VideoMode(828, 900), "Pacman"))
        {
            window.Closed += (o, e) => window.Close();
            
            // TODO: Initialize
            Clock clock = new Clock();
            
            while (window.IsOpen) {
                window.DispatchEvents();
                float deltaTime = clock.Restart().AsSeconds();
                deltaTime = MathF.Min(deltaTime, 0.01f);
                
                // TODO: Updates
                window.Clear(new Color(30,40,60));
                
                // TODO: Drawing
                window.Display();
            }
        }
    }
}
    