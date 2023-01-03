using Godot;
using System;

public class ReturnButton : Button
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    public override void _Pressed()
    {
        //GetParent().SetPauseMode(PauseModeEnum.Stop);
        //SceneTree.pau



        /*if (GetTree() != null)
        {
            Visible = false;

        }*/
        if (GetTree().Paused)
        {
            GetTree().Paused = false;
        }

        (GetNode("/root/Game") as Game).QuitToMissionScreen();
        (GetParent() as Control).Visible = false;


        //GetParent().GetParent().GetChild<Camera2D>(0).Current = true;
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
