using Godot;
using System;

public class FinalBossIntroCutscene : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.

    public Level6 LV;

    public override void _Ready()
    {

    }


    public float GetProportionate(float f)
    {
        if (LV != null)
        {
            return  ((1-f) * LV.CutsceneLength);
        }
        else
        {
            return 0;
        }
    }

    public int NearestFrame(float v1, float v2, float delta, float scale = 1)
    {
        return ((int)(v1 / (delta*scale)) - (int)(v2 / (delta*scale)));
    }

    public override void _Process(float delta)
    {
        base._Process(delta);

        if (LV == null)
        {
            LV = GetParent<Level6>();
        }
        else
        {
            if (!LV.Cutscene)
            {
                Visible = false;
                SetProcess(false);
                return;
            }
            
            float timer = LV.CutsceneTimer;


            if(NearestFrame(timer, GetProportionate(0.01f), delta, 1.6f)==0)
            {
                //Game.CurrentLevel.player
                BossWarning.Time = 5;
            }


            // GD.Print("Frame Diff: " + frame);

            if (NearestFrame(timer, GetProportionate(0.75f), delta, 1.6f) == 0)
            {
                LV.PlaceLightningExternal(1f);
            }


        }


    }

    
}
