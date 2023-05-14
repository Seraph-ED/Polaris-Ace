using Godot;
using System;

public class MoskvaGunBarrelAnim : Sprite
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.

    public float Recoil = 0;

    [Export]
    public float Damping = 1;

    public override void _Ready()
    {
        
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);


        if (Recoil > 0)
        {
            Recoil -= delta;
        }

        Offset = new Vector2(Damping * -Recoil, 0);



    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
