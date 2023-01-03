using Content;
using Godot;
using System;

public class EnemyInterceptor : Character
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public Node target;

    public float FlightSpeed = 41;

    public bool TargetInFiringArc = false;

    public bool PassedTarget = false;

    public int AIState = 0;

    public float AITimer = 0;

    public override void OnReady()
    {
        SetProcess(true);
        SetPhysicsProcess(true);
        target = null;
        //FlightSpeed = 18f;
        IsEnemy = true;
        Health = 80;
    }

    public void Start()
    {
        Velocity = new Vector2(0, -20);
        IsEnemy = true;
        //SetProcess(true);
        //SetPhysicsProcess(true);
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

    public void SpawnMissile()
    {
        var scene = GD.Load<PackedScene>("res://Content/Entities/Projectiles/MissileHostile.tscn");

        Projectile instance = scene.Instance() as MissileHostile;
        Game.CurrentLevel.SpawnProjectile(scene, LevelRelativePosition + new Vector2(0, -50).Rotated(Rotation), new Vector2(0, -30).Rotated(Rotation), Rotation, 30);



        //instance.set
    }


    public override void Behavior(float delta)
    {
        FindTarget();

        AITimer += delta;

        switch (AIState)
        {

            default:

                Player player = target as Player;


                Vector2 toTarget = (player.LevelRelativePosition - LevelRelativePosition);
                toTarget = toTarget.Normalized();
                toTarget *= 1.3f;

                Velocity += toTarget;

                // }


                Velocity = Velocity.Normalized() * FlightSpeed;

                Rotation = Velocity.Angle() + (float)Utils.Pi / 2f;

                if (TargetInFiringArc)
                {
                    AITimer = 0;
                    AIState = 1;
                    SpawnMissile();
                }

                /*if (AITimer > 10.0)
                {
                    AITimer = 0;
                    AIState = 1;
                }*/

                break;

            case 1:
                /*Player player2 = target as Player;


                Vector2 toTarget2 = ((player2.Position - (player2.Velocity * 30)) - Position);


                toTarget = toTarget2.Normalized();
                toTarget *= 2.4f;

                Velocity += toTarget;*/

                // }


                Velocity = Velocity.Normalized() * FlightSpeed;

                Rotation = Velocity.Angle() + (float)Utils.Pi / 2f;

                /*if (TargetInFiringArc)
                {
                    ShootAtTarget();
                }*/

                if (AITimer > 3.0)
                {
                    //SpawnMissile();

                    AITimer = 0;
                    AIState = 0;
                }

                break;

        }

    }

    
}
