using Godot;
using System;

public class CatAnim : Sprite
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
        if(GetParent() is CarrierCatapult)
        {
            cat = GetParent() as CarrierCatapult;

            if (cat.LaunchState == 1 && cat.LaunchTimer <= cat.LaunchAnimLength)
            {
                float lrp = Mathf.Clamp((1f - (cat.LaunchTimer / cat.LaunchAnimLength)), 0, 1);
                Position = new Vector2(1000 * lrp, 0) ;
            }else if(cat.LaunchState == 2)
            {
                float lrp = Mathf.Clamp(cat.LaunchTimer / cat.RetractAnimLength, 0, 1);
                Position = new Vector2(1000 * lrp, 0);
            }else
            {
                Position = Vector2.Zero;
            }
        }
        else
        {
            cat = null;
            cat.Position = Vector2.Zero;
        }

        
        


    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
