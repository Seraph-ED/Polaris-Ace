using Content;
using Godot;
using System;

public class EnemyMig29 : Character
{
   
    public Node target;

    public float FlightSpeed = 0;

    public bool TargetInFiringArc = false;

    public int AIState = 0;

    public float AITimer = 0;

    public override void OnReady()
    {
        SetProcess(true);
        SetPhysicsProcess(true);
        target = null;
        FlightSpeed = 18f;
       
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
                toTarget *= 0.4f;

                Velocity += toTarget;

                // }


                Velocity = Velocity.Normalized() * FlightSpeed;

                Rotation = Velocity.Angle() + (float)Utils.Pi / 2f;

                if (TargetInFiringArc)
                {
                    ShootAtTarget();
                }

                if (AITimer > 10.0)
                {
                    AITimer = 0;
                    AIState = 1;
                }

                break;

            case 1:
                Player player2 = target as Player;

                Vector2 behind = player2.Velocity;

                Vector2 toTarget2 = ((player2.LevelRelativePosition - (behind.Normalized()*300)) - LevelRelativePosition);


                toTarget = toTarget2.Normalized();
                toTarget *= 2.4f;

                Velocity += toTarget;

                // }


                if (Velocity.Length() < FlightSpeed * 0.8f)
                {
                    Velocity = Velocity.Normalized() * FlightSpeed * 0.8f;
                }
                else if (Velocity.Length() > FlightSpeed * 1.4f)
                {
                    Velocity = Velocity.Normalized() * FlightSpeed * 1.4f;
                }

                Rotation = Velocity.Angle() + (float)Utils.Pi / 2f;

                if (TargetInFiringArc)
                {
                    ShootAtTarget();
                }

                if (AITimer > 3.0)
                {
                    SpawnMissile();

                    AITimer = 0;
                    AIState = 0;
                }

                break;

        }

    }

}
