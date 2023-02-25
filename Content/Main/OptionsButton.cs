using Godot;
using System;

public class OptionsButton : Button
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
        Node menuContainer = GetNode("/root/Game/Menus");

        for (int i = 0; i < menuContainer.GetChildCount(); ++i)
        {
            if (menuContainer.GetChild(i) is Control)
            {
                (menuContainer.GetChild(i) as Control).Visible = false;
            }
        }
        // (GetNode("/root/Game/Menus/LevelSelect") as LevelSelect).Initialize();
        (GetNode("/root/Game/Menus/SettingsMenu") as Control).Visible = true;
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
