using Godot;
using System;

public class GateSprite : Sprite
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.

    [Export]
    public float OpenOffset = 145;

    [Export]
    public float CloseOffset = 145;

    [Export]
    public float ProportionateOpening = 0;


    public override void _Ready()
    {
        
    }

    public override void _PhysicsProcess(float delta)
    {
        

        if(GetParent() is GateSprite)
        {
            ProportionateOpening = ((GateSprite)GetParent()).ProportionateOpening;
        }else if(GetParent() is BaseGate)
        {
            ProportionateOpening = ((BaseGate)GetParent()).ProportionateOpening;
        }

        Position = new Vector2(0, Mathf.Lerp(CloseOffset, OpenOffset, ProportionateOpening));


    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
