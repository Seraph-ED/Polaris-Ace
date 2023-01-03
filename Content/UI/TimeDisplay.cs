using Godot;
using System;

public class TimeDisplay : Label
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    public Level currentLevel;

    public void UpdateTimerText(float timer)
    {
        int secondsInMinute = (int)(timer % 60f);

        int Minutes = (int)((timer - secondsInMinute) / 60f);

        string a1 = secondsInMinute < 10 ? "0" : "";
        string a2 = Minutes < 10 ? "0" : "";

        Text = a2+Minutes.ToString() + ":" + a1+secondsInMinute.ToString();


    }

    public override void _Process(float delta)
    {
        //base._Process(delta);
        currentLevel = Game.CurrentLevel;
        if (currentLevel == null)
        {
            //GetCurrentLevel();

            return;
        }
        UpdateTimerText(currentLevel.TimeLeft);

        


    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
