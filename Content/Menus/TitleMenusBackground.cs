using Godot;
using System;
using Content;

public class TitleMenusBackground : ParallaxBackground
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    [Export]
    public float amplitude = 1f;

    [Export]
    public float frequency = 1f;

    [Export]
    public float T = 1256;

    [Export]
    public float Xmirror = 4800;
    [Export]

    public float Ymirror = 4800;


    [Export]
    public float DistInCycle = 48000;

    [Export]
    public float TimePerCycle = 900;

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
  {

        float primarytimer = Mathf.Sin(Mathf.Pi * 2 * TitleMenusBackgroundPlanes.BigTimer / 3600f);

        float t = primarytimer * T / frequency;

        float t2 = t + 10000;

        double xval = (Mathf.Sin(t / 2f)) + (0.7f * Mathf.Sin(t / -1.28347f)) + (0.4 + Mathf.Sin(t / 2.718f)) + (0.88 * Mathf.Sin(t * 0.8f));

        double yval = (Mathf.Sin(t2 / 2f)) + (0.7f * Mathf.Sin(t2 / -1.28347f)) + (0.4 + Mathf.Sin(t2 / 2.718f)) + (0.88 * Mathf.Sin(t2 * 0.8f));

        Vector2 offset = amplitude * new Vector2(Xmirror * (float)xval, Ymirror * (float)yval);




        ScrollOffset = offset + new Vector2( (TitleMenusBackgroundPlanes.BigTimer % TimePerCycle)/TimePerCycle*-DistInCycle, 0);

        Visible = Game.CurrentLevel == null;

  }
}
