using Content;
using Godot;
using System;

public class EnemyDrone : Character
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public Node target;

    public Node ConnectedAWACS = null;

    public float FlightSpeed = 0;

    public bool TargetInFiringArc = false;

    public int AIState = 0;

    public float AITimer = 0;

    public float FlareCooldown = 0;

    public float FlareTime = 0;

    public override void OnReady()
    {
        SetProcess(true);
        SetPhysicsProcess(true);
        target = null;
        
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

    public void SpawnFlare(Vector2 shootVel)
    {
        var scene = GD.Load<PackedScene>("res://Content/Entities/Projectiles/Flare.tscn");

        //Flare instance = scene.Instance() as Flare;
        //Game.CurrentLevel.AddChild(instance);
        Game.CurrentLevel.SpawnProjectile(scene, LevelRelativePosition, shootVel, Rotation, 0);


        //instance.set
    }

    public void DropFlares()
    {
        if (((Timer)GetNode("FlareTimer")).TimeLeft <= 0)
        {
            (GetNode("FlareTimer") as Timer).Start(0.2f);
            SpawnFlare(Velocity + new Vector2(40, 0).Rotated(Rotation));
            SpawnFlare(Velocity + new Vector2(-40, 0).Rotated(Rotation));
        }
    }

    public void CheckIncomingMissiles()
    {
        for(int i = 0; i < Game.CurrentLevel.ProjectileContainer.GetChildCount(); ++i)
        {
            Node n = Game.CurrentLevel.ProjectileContainer.GetChild(i);


            if(n is MissileFriendly)
            {
                MissileFriendly missile = n as MissileFriendly;

                if(missile.target == this as Character)
                {
                    FlareTime = 2;
                    FlareCooldown = 7;
                }


            }


        }




    }

    public override void Behavior(float delta)
    {
        FindTarget();


        if (FlareCooldown > 0)
        {
            FlareCooldown -= delta;
        }
        else
        {
            CheckIncomingMissiles();
        }

        if (FlareTime > 0)
        {
            MissileJamming = true;
            DropFlares();
            FlareTime -= delta;
        }
        else
        {
            MissileJamming = false;
        }
       

        Player player = target as Player;

        switch (AIState)
        {
            default:

                FlightSpeed = Mathf.Max(player.Velocity.Length()-0.4f, 24);
                
                Vector2 toTarget1 = (player.LevelRelativePosition - LevelRelativePosition);
                toTarget1 = toTarget1.Normalized();
                toTarget1 *= 1.2f;

                Velocity += toTarget1;

                // }


                Velocity = Velocity.Normalized() * FlightSpeed;

                Rotation = Velocity.Angle() + (float)Utils.Pi / 2f;

                if (TargetInFiringArc)
                {
                    ShootAtTarget();
                }

                if (AIState == 0)
                {
                    if (AITimer > 2.5f)
                    {
                        AIState = 1;
                        AITimer = 0;
                    }
                }else if (AIState == 2)
                {
                    if (AITimer > 2.5f)
                    {
                        AIState = 3;
                        AITimer = 0;
                    }
                }


                break;
            case 1:
            case 3:
                FlightSpeed = Mathf.Max(player.Velocity.Length() + 3, 24);

                Vector2 toTarget2 = (player.LevelRelativePosition - LevelRelativePosition);
                toTarget2 = toTarget2.Normalized();
                toTarget2 *= 1.9f;

                Velocity += toTarget2;

                // }


                if (Velocity.Length() < FlightSpeed * 0.5f)
                {
                    Velocity = Velocity.Normalized() * FlightSpeed * 0.5f;
                }
                else if (Velocity.Length() > FlightSpeed * 1.6f)
                {
                    Velocity = Velocity.Normalized() * FlightSpeed * 1.5f;
                }


                Rotation = Velocity.Angle() + (float)Utils.Pi / 2f;

                if (TargetInFiringArc)
                {
                    ShootAtTarget();
                }

                if (AIState == 1)
                {
                    if (AITimer > 0.5f)
                    {
                        AIState = 2;
                        AITimer = 0;
                    }
                }
                else if (AIState == 3)
                {
                    if (AITimer > 2.5f)
                    {
                        AIState = 0;
                        AITimer = 0;
                    }
                }


                break;

        }


        AITimer += delta;

    }
}
