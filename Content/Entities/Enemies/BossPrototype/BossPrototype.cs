using Content;
using Godot;
using System;
using System.Drawing;

public class BossPrototype : Character
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public Node target;

    public float FlightSpeed = 0;

    public bool TargetInFiringArc = false;

    public int AIState = 2;

    public float AITimer = 0f;

    

    public float FlareCooldown = 0;

    public float FlareTime = 0;

    public override void Behavior(float delta)
    {
        AITimer += delta;

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

        if (FlareCooldown > 0)
        {
            FlareCooldown -= delta;
        }
        else
        {
           
        }


        switch (AIState)
        {
            case -1:
                BasicFlightMovement(42, 1.3f, 1f, 1.4f);
                if (AITimer > 0.1f)
                {
                    AITimer = 4.6f;
                    AIState = 3;
                }
                break;
            case 0:
                
                BasicFlightMovement(26, 1.9f, 0.8f, 1.6f, new Vector2(0, 300), false);

                if (TargetInFiringArc)
                {
                    ShootAtTarget();
                }

                if (AITimer > 15.5)
                {
                    ShootNuke();
                    AITimer = 0;
                    AIState = 2;

                }

                if (FlareCooldown == 0)
                {
                    CheckIncomingMissiles();
                }
                break;
            case 1:
                BasicFlightMovement(36, 1.4f, 1f, 1.4f);
                if (AITimer>3.5)
                {
                    ShootNuke();
                    AITimer = 0;
                    AIState = 0;

                }
                break;
            
             case 2:
                BasicFlightMovement(42, 0.0f, 1f, 1.4f);
                if (AITimer > 2.5f)
                {
                    AIState = 3;
                    AITimer = 0;
                }

                break;

             

            case 3:
                BasicFlightMovement(38, 1.2f, 0.8f, 1.4f);
                if (AITimer > 3)
                {
                    
                    AITimer = 0;
                    AIState = 1;

                }
                break;
            
        }
    }

    public void BasicFlightMovement(float cruiseSpeed = 24, float accelerationMax = 1.9f, float minSpeedMultiplier = 0.5f, float maxSpeedMultiplier = 1.5f, Vector2? rotatedOffset = null, bool matchPlayerSpeed = true)
    {
        Player player = Game.CurrentLevel.player;

        FlightSpeed = matchPlayerSpeed ? Mathf.Max(player.Velocity.Length() + 3, cruiseSpeed) : cruiseSpeed;
        Vector2 o1;

        if (rotatedOffset == null)
        {
            o1 = Vector2.Zero;
        }
        else
        {
            o1 = rotatedOffset.Value;
        }

        Vector2 toTarget2 = player.LevelRelativePosition + (o1.Rotated(player.Rotation));
        toTarget2 = toTarget2 - LevelRelativePosition;
        toTarget2 = toTarget2.Normalized();
        toTarget2 *= accelerationMax;

        Velocity += toTarget2;

        if (Velocity.Length() < FlightSpeed * minSpeedMultiplier)
        {
            Velocity = Velocity.Normalized() * FlightSpeed * minSpeedMultiplier;
        }
        else if (Velocity.Length() > FlightSpeed * maxSpeedMultiplier)
        {
            Velocity = Velocity.Normalized() * FlightSpeed * maxSpeedMultiplier;
        }

        Rotation = Velocity.Angle() + (float)Utils.Pi / 2f;
    }

    public void CheckIncomingMissiles()
    {
        for (int i = 0; i < Game.CurrentLevel.ProjectileContainer.GetChildCount(); ++i)
        {
            Node n = Game.CurrentLevel.ProjectileContainer.GetChild(i);


            if (n is MissileFriendly)
            {
                MissileFriendly missile = n as MissileFriendly;

                if (missile.target == this as Character)
                {
                    FlareTime = 2;
                    FlareCooldown = 7;
                }

            }

        }

    }

    public void SpawnFlare(Vector2 shootVel)
    {
        var scene = GD.Load<PackedScene>("res://Content/Entities/Projectiles/Flare.tscn");

        //Flare instance = scene.Instance() as Flare;
        //Game.CurrentLevel.AddChild(instance);
        Game.CurrentLevel.SpawnProjectile(scene, LevelRelativePosition, shootVel, Rotation, 0);

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

    public void SetTargetInArc(Node body)
    {
        TargetInFiringArc = true;

    }

    public void UnsetTargetInArc(Node body)
    {
        TargetInFiringArc = false;


    }


    public void ShootAtTarget(float reload = 0.3f)
    {
        if (((Timer)GetNode("ReloadTimer")).TimeLeft == 0)
        {
            (GetNode("ReloadTimer") as Timer).Start(reload);
            SpawnBullet(50, new Vector2(5, 0), 6);
            SpawnBullet(50, new Vector2(-5, 0), 6);
        }

    }

    public void ShootNuke()
    {
        string str = "res://Content/Entities/Projectiles/MissileHostileNuke.tscn";

        Game.CurrentLevel.SpawnProjectile(str, LevelRelativePosition, Velocity + new Vector2(0, -10).Rotated(Rotation), Rotation, 199);

    }

    public void SpawnBullet( float bulletSpeed = 50, Vector2? offset = null, float dmg = 10 )
    {
        string str = "res://Content/Entities/Projectiles/BulletHostile.tscn";

        if (offset == null)
        {
            offset = Vector2.Zero;
        }


        //BulletHostile instance = scene.Instance() as BulletHostile;
        Game.CurrentLevel.SpawnProjectile(str, LevelRelativePosition + offset.Value + new Vector2(0, -bulletSpeed).Rotated(Rotation), Velocity + new Vector2(0, -bulletSpeed).Rotated(Rotation), Rotation, dmg);


        //instance.set
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
