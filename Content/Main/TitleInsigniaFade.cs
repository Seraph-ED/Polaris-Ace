using Godot;
using System;

public class TitleInsigniaFade : TextureRect
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    [Export]
    float maxScale = 1.6f;

    [Export]
    NodePath timerPath;

    public override void _Ready()
    {
        
    }

    public override void _Process(float delta)
    {
        //base._Process(delta);

        if (timerPath == null || GetNode(timerPath) == null|| !(GetNode(timerPath) is Timer))
        {
            return;
        }

        Timer t = GetNode(timerPath) as Timer;

        float proptimer = (t.TimeLeft/t.WaitTime);

        

        RectScale = Vector2.One * Mathf.Lerp(1f, maxScale, 1-proptimer);


        float proptimer2 = Mathf.Clamp((proptimer * 1.1f) - 0.1f, 0, 1f);

        Modulate = new Color(1, 1, 1, proptimer2);
        RectPosition = -(RectSize * RectScale / 2f);

        if (t.TimeLeft == 0)
        {
            t.Start(t.WaitTime);
        }
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
