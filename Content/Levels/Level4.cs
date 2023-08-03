using Godot;
using System;

public class Level4 : Level
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.

    public bool CarrierDowned = false;
    public override bool CheckWinCondition()
    {
        return CarrierDowned;//base.CheckWinCondition();
    }

    public override void UpdateLevel(float delta)
    {
       // base.UpdateLevel(delta);
        GD.Print("Carrier Downed:" + CarrierDowned);
    }


    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
