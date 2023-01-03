using Godot;
using System;

public class StartButton : Button
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

        GetTree().CallGroup("Main", "Start");
        ((Control)GetParent()).Visible = false;

        //GetParent().GetParent().GetChild<Camera2D>(0).Current = true;
    }

    public void Start()
    {
        //Visible = false;
        
    }

    [Signal]
    public delegate void StartButtonPressed();

    //public signal
    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
