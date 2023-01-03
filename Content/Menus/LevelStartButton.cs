using Godot;
using System;

public class LevelStartButton : Button
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
        Game game = (GetNode("/root/Game") as Game);
        LevelSelect levelSelect = (GetParent() as LevelSelect);
        int LevelIndexPointedTo = LevelSelect.LevelIndexSelected;

        if (LevelIndexPointedTo < 0)
        {
            return;
        }

        if (game.Briefings[LevelIndexPointedTo] != null)
        {
            (GetNode("/root/Game") as Game).LoadLevelWithBriefing(LevelIndexPointedTo + 1);
        }
        else
        {
            (GetNode("/root/Game") as Game).LoadLevel(LevelIndexPointedTo + 1);
        }


    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
