using Godot;
using System;

public class Level2 : Level
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.

    //public override string LevelName => "Test mission 2";

    public int WaveNum = 0;
    

    public override void OnReady()
    {
        WaveNum = 0;

    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void UpdateLevel(float delta)
    {
        if (WaveNum == 0)
        {
            WaveNum = 1;
            ((EntityContainer)GetNode("Wave1")).Active = true;
        }
        else
        {

            if (Enemies.Count == 0)
            {

                ((EntityContainer)GetNode("Wave" + WaveNum.ToString())).Active = false;
                WaveNum++;

                if (HasNode("Wave" + WaveNum.ToString()))
                {
                    ((EntityContainer)GetNode("Wave" + WaveNum.ToString())).Active = true;
                }

                

            }

        }

        


    }

    public override bool CheckWinCondition()
    {
        return WaveNum > 6;
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
