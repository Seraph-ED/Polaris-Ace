using Content;
using Godot;
using System;

public class TargetRadar : Character
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    Node target;

    public float turretRotation = 0;

    public override void OnReady()
    {
        // target = null;
        //FlightSpeed = 18f;
        IsEnemy = true;
        Health = 450;
    }

    public void FindTarget()
    {

        target = Game.CurrentLevel.GetNode("Player");
    }

    public override void Behavior(float delta)
    {
        base.Behavior(delta);

        //FindTarget();

        turretRotation += Mathf.Deg2Rad(3) * delta * 60;

        (GetNode("Antenna") as Sprite).Rotation = turretRotation + (Mathf.Pi / 2f);
        (GetNode("DetectionArc") as Node2D).Rotation = turretRotation + (Mathf.Pi / 2f);
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
