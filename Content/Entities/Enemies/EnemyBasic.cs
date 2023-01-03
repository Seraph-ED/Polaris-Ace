using Content;
using Godot;
using System;

public class EnemyBasic : Character
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public Node target;

    public float FlightSpeed = 0;

    public bool TargetInFiringArc = false;

    public override void OnReady()
    {
        SetProcess(true);
        SetPhysicsProcess(true);
        target = null;
        FlightSpeed = 18f;
        IsEnemy = true;
        Health = 49;
    }

    

    public void FindTarget()
    {

        target = Game.CurrentLevel.GetNode("Player");
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
            (GetNode("ReloadTimer") as Timer).Start(0.3f);
            SpawnBullet();
        }
            
    }

    public void SpawnBullet()
    {
        string str = "res://Content/Entities/Projectiles/BulletHostile.tscn";

        //BulletHostile instance = scene.Instance() as BulletHostile;
        Game.CurrentLevel.SpawnProjectile(str, LevelRelativePosition + new Vector2(0, -50).Rotated(Rotation), Velocity + new Vector2(0, -50).Rotated(Rotation), Rotation, 10);


        //instance.set
    }

    public override void Behavior(float delta)
    {
        FindTarget();

        Player player = target as Player;


        Vector2 toTarget = (player.LevelRelativePosition - LevelRelativePosition);
        toTarget = toTarget.Normalized();
        toTarget *= 0.4f;

        Velocity += toTarget;

        // }


        Velocity = Velocity.Normalized() * FlightSpeed;

        Rotation = Velocity.Angle() + (float)Utils.Pi / 2f;

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
