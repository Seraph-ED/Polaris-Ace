using Godot;
using System;
using Content;

public class EnemyTankSam : Character
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.

    public Node target;

    public bool TargetInFiringArc = false;

    public float turretRotation = 0;

    public override void OnReady()
    {
       // target = null;
        //FlightSpeed = 18f;
        IsEnemy = true;
        Health = 150;
    }

    public void SetTargetInArc(Node body)
    {
        TargetInFiringArc = true;

    }

    public void UnsetTargetInArc(Node body)
    {
        TargetInFiringArc = false;


    }

    public void ShootAtTarget()
    {
        if (((Timer)GetNode("ReloadTimer")).TimeLeft < 0.1f)
        {
            (GetNode("ReloadTimer") as Timer).Start(5f);
            SpawnMissile();
        }

    }

    public void SpawnMissile()
    {
        var scene = GD.Load<PackedScene>("res://Content/Entities/Projectiles/MissileHostile.tscn");

        Game.CurrentLevel.SpawnProjectile(scene, LevelRelativePosition + new Vector2(0, 0).Rotated(turretRotation), Velocity + new Vector2(45, 0).Rotated(turretRotation), turretRotation + (Mathf.Pi / 2f), 20);
        //Game.CurrentLevel.SpawnProjectile(scene, LevelRelativePosition + new Vector2(0, -13).Rotated(turretRotation), Velocity + new Vector2(45, 0).Rotated(turretRotation), turretRotation + (Mathf.Pi / 2f), 5);


        //instance.set
    }


    public void MovementBehavior()
    {

        if(GetParent() is PathEntityContainer)
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
        //base.Behavior(delta);

        FindTarget();
        MovementBehavior();

        if(((target as Character).LevelRelativePosition - LevelRelativePosition).Length() < 22000)
        {
            turretRotation = Utils.TurnTowards(turretRotation, ((target as Character).LevelRelativePosition - LevelRelativePosition).Angle(), Mathf.Deg2Rad(3f * delta * 60f));
        }

        if (HasNode("Turret/FiringCrosshair"))
        {
            (GetNode("Turret/FiringCrosshair") as FiringCrosshair).TimeProportion = Mathf.Clamp(((Timer)GetNode("ReloadTimer")).TimeLeft - 0.1f, 0f, 1f);
        }

        (GetNode("Turret") as Sprite).Rotation = turretRotation + (Mathf.Pi / 2f) - Rotation;

        if (TargetInFiringArc)
        {
            ShootAtTarget();
        }

    }
    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
