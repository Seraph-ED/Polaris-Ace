using Content;
using Godot;
using System;

public class LightningAffectedParallaxLayer : ParallaxLayer
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.

    [Export]
    public Color BaseColor = new Color(0, 0, 0);

    [Export]
    public Color FlashColor = new Color(1, 1, 1);

    public override void _Ready()
    {
        
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);
        if(GetParent() is LightningStormBackground)
        {
            LightningStormBackground bg = GetParent() as LightningStormBackground;

            Modulate = Utils.ColorLerp(BaseColor, FlashColor, bg.LightningStrength);

        }

    }


    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
