using Content;
using Godot;
using System;

public class EnemyNavalFighter : Character
{
    public Node target;

    public float FlightSpeed = 0;

    public bool TargetInFiringArc = false;

    public int TargetInBehindArc = 0;

    public int AIState = 0;

    public float AITimer = 0;

    public override void OnReady()
    {
        SetProcess(true);
        SetPhysicsProcess(true);
        target = null;
        FlightSpeed = 22f;

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

    public void SetTargetInLeftArc(Node body)
    {
        TargetInBehindArc = 2;

    }
    public void SetTargetInRightArc(Node body)
    {
        TargetInBehindArc = 3;

    }

    public void UnsetTargetInBehindArc(Node body)
    {
        TargetInBehindArc = 0;


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

            case 0:
            case 2:
            case 3:
                Player player = target as Player;


                Vector2 behind2 = player.Velocity;

                Vector2 toTarget = (player.LevelRelativePosition - LevelRelativePosition);

                if (AIState == 2)
                {
                    toTarget = (player.LevelRelativePosition - LevelRelativePosition + (behind2.Normalized().Rotated(Mathf.Deg2Rad(-70))*300f));
                }

                if (AIState == 3)
                {
                    toTarget = (player.LevelRelativePosition - LevelRelativePosition + (behind2.Normalized().Rotated(Mathf.Deg2Rad(70)) * 300f));
                }

                toTarget = toTarget.Normalized();

                if (AIState == 0)
                {
                    toTarget *= 0.6f;
                }
                else
                {
                    toTarget *= 1.3f;
                }

                Velocity += toTarget;

                // }


                if (Velocity.Length() < FlightSpeed * 0.86f)
                {
                    Velocity = Velocity.Normalized() * FlightSpeed * 0.86f;
                }
                else if (Velocity.Length() > FlightSpeed * 1.1f)
                {
                    Velocity = Velocity.Normalized() * FlightSpeed * 1.1f;
                }

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
                else
                {
                    AIState = TargetInBehindArc;
                }

                break;

            case 1:
                Player player2 = target as Player;

                Vector2 behind = player2.Velocity;

                Vector2 toTarget2 = ((player2.LevelRelativePosition - (behind.Normalized() * 600)) - LevelRelativePosition);


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
