using Godot;
using System;

public class ShipComponent : Character
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.

    [Export]
    public float SubsystemWeight = 1.0f;

    [Export] public bool Critical = false;

    [Export] public bool CompletelyDestroyed = false;


   

    public override void Kill()
    {
        //isActive = false;
        var scene = GD.Load<PackedScene>("res://Content/VFX/Explosion.tscn");

        Explosion instance = scene.Instance() as Explosion;
        Game.CurrentLevel.AddChild(instance);
        instance.GlobalPosition = GlobalPosition;
        if (CompletelyDestroyed)
        {
            QueueFree();
        }
        else
        {
            MissionKill = true;
        }
        
    }


    public override void _Ready()
    {
        
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
