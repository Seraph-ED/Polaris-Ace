using Godot;
using System;

public class LevelButton : Button
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.

    public int LevelIndexPointedTo = 0;

    public string LevelName = "";
    public override void _Ready()
    {
        Initialize();
    }

    public void Initialize()
    {
        Game game = (GetNode("/root/Game") as Game);


        LevelName = (game.Levels[LevelIndexPointedTo].Instance() as Level).LevelName;

        Text = LevelName;
    }

   // public override 

    public override void _Pressed()
    {
        GD.Print("Pressed level button corresponding to index: " + LevelIndexPointedTo);
        //Game game = (GetNode("/root/Game") as Game);
        //LevelSelect select = (GetNode("/root/Game/Menus/LevelSelect") as LevelSelect);
        if (LevelSelect.LevelIndexSelected != LevelIndexPointedTo)
        {
            LevelSelect.LevelIndexSelected = LevelIndexPointedTo;
        }
        else
        {
            LevelSelect.LevelIndexSelected = -1;
        }
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
