using Content;
using Godot;
using System;

public class GameCamera : Camera2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
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
        if (!GetParent().HasNode("Player") || (GetParent() as Level).player == null)
        {
            return;
        }

        Position = (GetParent() as Level).player.Position;
        //Rotation += delta * Mathf.Deg2Rad(180);
        
    }
}
