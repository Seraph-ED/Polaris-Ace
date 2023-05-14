using Godot;
using System;

public class VLSAnimation : AnimatedSprite
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
        
        
        if(GetParent() is VLS)
        {
           
            
            VLS parent = GetParent() as VLS;



            Frame = parent.FiringState == 0 && parent.Cooldown.TimeLeft > 0 ? 0 : 1;

            if (parent.MissionKill)
            {
                Frame = 2;
            }

        }
  }
}
