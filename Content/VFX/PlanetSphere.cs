using Godot;
using System;

public class PlanetSphere : CSGSphere
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    [Export]
    float radScalar = 1.0f;

    public override void _Ready()
    {
        Rotation = Vector3.Zero;
        RadialSegments = 200;
        Rings = 100;
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);

        if (Game.CurrentLevel != null && Game.CurrentLevel.player != null)
        {
            float alt = 30000*radScalar;
            float oppx = Game.CurrentLevel.player.Velocity.x;
            float oppy = Game.CurrentLevel.player.Velocity.y;



            float xangle = -Mathf.Tan(oppx / alt);
            float yangle = -Mathf.Tan(oppy / alt);



            RotateY(xangle);
            RotateX(yangle);
        }

        
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
