using Godot;
using System;

public class LevelSelectButton : Button
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
        ((Control)GetParent().GetParent()).Visible = false;
        //((Control)GetParent()).Visible = false;
        (GetNode("/root/Game/Menus/LevelSelect") as LevelSelect).Initialize();
        (GetNode("/root/Game/Menus/LevelSelect") as Control).Visible = true;



        //GetParent().GetParent().GetChild<Camera2D>(0).Current = true;
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
