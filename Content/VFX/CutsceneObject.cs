using Godot;
using System;

public class CutsceneObject : KinematicBody2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    [Export]
    public Vector2 ConstantVelocity = Vector2.Zero;

    public override void _Ready()
    {
        
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);
        MoveAndCollide(ConstantVelocity*delta*60);

    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
