using Godot;
using System;

public class LightningStormBackground : ParallaxBackground
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    [Export]
    public float LightningStrength = 0;

    [Export]
    public float FlashLengthMultiplier = 1;

    public override void _Ready()
    {
        
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);
        if (LightningStrength > 0)
        {
            LightningStrength -= delta*FlashLengthMultiplier;
        }
        //GD.Print("LightningStrengt")

    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
