using Godot;
using System;

public class Flare : Projectile
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Lifespan = 3.0f;
    }

    public override void Behavior(float delta)
    {
        Velocity *= 0.9f * (60f * delta);
    }

    public override void OnHit(Node hitNode)
    {
        
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
