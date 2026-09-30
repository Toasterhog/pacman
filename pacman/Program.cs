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
            window.SetView(new View(new FloatRect(18, 0, 414, 450)));
            Clock clock = new Clock();
            Scene scene = new Scene();
            
            
            while (window.IsOpen) {
                window.DispatchEvents();
                float deltaTime = clock.Restart().AsSeconds();
                deltaTime = MathF.Min(deltaTime, 0.1f);
                
                // TODO: Updates
                
                // TODO: Drawing
                window.Clear(new Color(30,40,60));
                scene.RenderAll(window);
                window.Display();
            }
        }
    }
}
    