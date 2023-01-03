using Godot;
using System;

public class LaserLengthRaycast : RayCast2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    public override void _PhysicsProcess(float delta)
    {
        //base._PhysicsProcess(delta);
        if (GetCollider() != null)
        {
            (GetParent() as LaserProjectile).beamLength = (GetCollisionPoint() - GlobalPosition).Length();
        }
        else
        {
            (GetParent() as LaserProjectile).beamLength = 5000;
        }
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
