using Godot;
using System;

public class DescriptionBox : RichTextLabel
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public bool Updated = false;

    public int IndexCurrentlyShowing = 0;

    public override void _Ready()
    {
        
    }

    public override void _Process(float delta)
    {
        LevelSelect levelSelect = (GetParent() as LevelSelect);
        Game game = (GetNode("/root/Game") as Game);

        if (LevelSelect.LevelIndexSelected != IndexCurrentlyShowing)
        {
            Updated = false;
        }

        if (!Updated)
        {
            if (LevelSelect.LevelIndexSelected >= 0 && LevelSelect.LevelIndexSelected < game.Levels.Count)
            {

                Text = (game.Levels[LevelSelect.LevelIndexSelected].Instance() as Level).Description;
                IndexCurrentlyShowing = LevelSelect.LevelIndexSelected;
            }
            else
            {
                Text = "";
                IndexCurrentlyShowing = -1;
            }

            Updated = true;

        }

    }




    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
