using Godot;
using System;

public class BossWarningLabel : Label
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        float alpha = Mathf.Sin(Mathf.Pi * BossWarning.Time);

        float alphaSquared = alpha * alpha;

        AddColorOverride("font_color", new Color(1, 0, 0, alphaSquared));
    }
}
