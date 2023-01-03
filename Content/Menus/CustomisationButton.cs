using Godot;
using System;

public class CustomisationButton : Button
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

        //((Control)GetParent()).Visible = false;
        (GetNode("/root/Game/Menus/CustomisationMenu") as CustomisationMenu).Initialize();
        Game.instance.SetMenu("CustomisationMenu");
        //(GetNode("/root/Game/Menus/LevelSelect") as Control).Visible = true;
    }
    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
