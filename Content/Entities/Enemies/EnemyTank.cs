using Godot;
using System;
using Content;

public class EnemyTank : Character
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.

    public Node target;

    public float turretRotation = 0;

    public override void OnReady()
    {
       // target = null;
        //FlightSpeed = 18f;
        IsEnemy = true;
        Health = 150;
    }

    public void MovementBehavior()
    {

        if (GetParent() is PathEntityContainer)
        {
            PathEntityContainer p = (GetParent() as PathEntityContainer);

            LevelRelativePosition = p.LevelPositionOnpath;
            Rotation = p.RotationOnPath + (Mathf.Pi / 2f);

        }

    }

    public void FindTarget()
    {

        target = Game.CurrentLevel.GetNode("Player");
    }

    public override void Behavior(float delta)
    {
        base.Behavior(delta);

        FindTarget();
        MovementBehavior();

        turretRotation = Utils.TurnTowards(turretRotation, ((target as Character).LevelRelativePosition - LevelRelativePosition).Angle(), Mathf.Deg2Rad(15f));

        (GetNode("Turret") as Sprite).Rotation = turretRotation + (Mathf.Pi / 2f) - Rotation;
    }
    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
