using Godot;
using System;
using System.Diagnostics.Contracts;

public class Level1 : Level
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    
    public int WaveNum = 0;

    public bool SetBossPos = false;

    //public override string LevelName ="Mission 1";

    // Called when the node enters the scene tree for the first time.
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

                if (WaveNum == 7)
                {
                    BossWarning.Time = 3;

                    
                }

            }

        }

        if(WaveNum == 7 && Enemies.Count > 0 && !SetBossPos)
        {
            (Enemies[0] as Character).LevelRelativePosition = player.LevelRelativePosition - new Vector2(0, 6000);

            SetBossPos = true;

        }


    }

    public override bool CheckWinCondition()
    {
        return WaveNum > 7;
    }
}
