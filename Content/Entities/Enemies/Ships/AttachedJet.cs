using Godot;
using System;

public class AttachedJet : Sprite
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.

    private CarrierCatapult cat;
    public override void _Ready()
    {
        
    }

    public override void _PhysicsProcess(float delta)
    {
        if (GetParent().GetParent() is CarrierCatapult)
        {
            cat = GetParent().GetParent() as CarrierCatapult;

            Visible = cat.LaunchState == 1;
        }
        else
        {
            Visible = false;
        }





    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
