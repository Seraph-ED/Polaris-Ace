using Godot;
using System;

public class ContinueButton : Button
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
        base._Pressed();

        (GetParent() as LevelBriefing).AdvancePage();
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {



        if ((GetParent() as LevelBriefing).LastPage)
        {
            Text = "Sortie";
        }
        else
        {
            Text = "Continue";
        }
    }
}
