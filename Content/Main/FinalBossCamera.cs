using Content;
using Godot;
using System;

public class FinalBossCamera : Camera2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    [Export]
    public Curve InterpCurve;

    public Node n1;

    public Node n2;

    public override void _Ready()
    {
        SetProcess(true);
        Current = true;
    }

    public void Start()
    {
        //SetProcess(true);
        

    }




    //public override 

    //ref nod

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if(Game.CurrentLevel is Level6 && (Game.CurrentLevel as Level6).Cutscene)
        {
            n1 = Game.CurrentLevel.GetNode("Cutscene/CutscenePlayer");
            n2 = Game.CurrentLevel.GetNode("Cutscene/CutscenePrototype");

            Level6 lol = Game.CurrentLevel as Level6;

            if(n1!=null&&n2!=null&&n1 is Node2D&&n2 is Node2D)
            {
                GlobalPosition = Utils.VecLerp((n1 as Node2D).GlobalPosition, (n2 as Node2D).GlobalPosition, InterpCurve.Interpolate(1-(lol.CutsceneTimer/lol.CutsceneLength)) );
            }


            return;
           
        }
        
        
        
        
        if (!GetParent().HasNode("Player") || (GetParent() as Level).player == null)
        {
            return;
        }

        Position = (GetParent() as Level).player.Position;
        //Rotation += delta * Mathf.Deg2Rad(180);
        
    }
}
