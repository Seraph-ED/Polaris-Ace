using Godot;
using System;

public class HelicopterRotors : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    [Export]
    float RotationSpeedDegrees = 40;

    public override void _Ready()
    {
        
    }

    public override void _PhysicsProcess(float delta)
    {
        RotationDegrees += RotationSpeedDegrees * delta * 60;
    }
    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
