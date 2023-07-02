using Godot;
using System;

public class WeaponCrosshairBomb : Sprite
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.

    Player player => (GetParent().GetParent().GetParent() as Player);
    public override void _Ready()
    {
        
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {

        if (player == null||Game.CurrentLevel==null)
        {
            
            return;
        }

        
        
        float v = (7 + player.Velocity.Length()) * (0.3f*60f);

        if (Visible)
        {
            Position = new Vector2(0, -v);
        }
    }
}
