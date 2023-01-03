using Godot;
using System;

public class Level3 : Level
{

    public int WaveNum = 0;

    public bool SetBossPos = false;

    public int WaveNumMax = 8;

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

                if(WaveNum < WaveNumMax)
                {
                    ((EntityContainer)GetNode("Wave" + WaveNum.ToString())).Active = false;
                }else if(WaveNum >= WaveNumMax)
                {
                    if (HasNode("BossContainer"))
                    {
                        ((EntityContainer)GetNode("BossContainer")).Active = true;
                    }
                }

                
                WaveNum++;

                if(WaveNum < WaveNumMax)
                {
                    if (HasNode("Wave" + WaveNum.ToString()))
                    {
                        ((EntityContainer)GetNode("Wave" + WaveNum.ToString())).Active = true;
                    }
                }
                else if(WaveNum == WaveNumMax)
                {
                    if (HasNode("BossContainer"))
                    {
                        ((EntityContainer)GetNode("BossContainer")).Active = true;
                    }
                }

                if (WaveNum == WaveNumMax)
                {
                    BossWarning.Time = 3;


                }

            }

        }

        if (WaveNum == WaveNumMax && Enemies.Count > 0 && !SetBossPos)
        {
            (Enemies[0] as Character).LevelRelativePosition = player.LevelRelativePosition - new Vector2(0, 6000);

            SetBossPos = true;

        }


    }

    public override bool CheckWinCondition()
    {
        return WaveNum > WaveNumMax;
    }
}
