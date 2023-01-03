using Godot;
using System;

public class LaserProjectileDrawer : Sprite
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    public override void _Draw()
    {
        base._Draw();

        float len = Mathf.Abs((GetParent() as LaserProjectile).beamLength);

        for(float i = 0; i < len; i += 10)
        {
            DrawTexture(Texture, Position + new Vector2(10*i, -25).Rotated(Rotation+Mathf.Pi));
        }
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);

        Update();
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
