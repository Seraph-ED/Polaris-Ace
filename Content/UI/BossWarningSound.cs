using Godot;
using System;

public class BossWarningSound : AudioStreamPlayer
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    bool Queued = false;

    public override void _Process(float delta)
    {
        base._Process(delta);

        if(BossWarning.Time % 1f > 0.95f&&!Playing)
        {
            Play(0);
        }
        
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
