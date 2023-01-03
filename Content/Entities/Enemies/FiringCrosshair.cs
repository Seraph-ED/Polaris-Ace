using Godot;
using System;
using Content;

public class FiringCrosshair : Line2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public float TimeProportion = 1f;

    public override void _Ready()
    {
        
    }

    public override void _PhysicsProcess(float delta)
    {
        DefaultColor = Utils.ColorLerp(new Color(1, 0, 0, 0.5f), new Color(1, 1, 0, 0.5f), TimeProportion);
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
