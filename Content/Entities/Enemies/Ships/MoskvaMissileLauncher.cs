using Godot;
using System;

public class MoskvaMissileLauncher : ShipComponent
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    public override void Kill()
    {
        for(int i = -120; i <= 120; i += 40)
        {
            Game.CurrentLevel.PlaceExplosion(LevelRelativePosition + new Vector2(i, 0).Rotated(Rotation), 0.5f);
        }
        
        
        base.Kill();
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
