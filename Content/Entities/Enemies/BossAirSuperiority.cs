using Content;
using Godot;
using System;

public class BossAirSuperiority : Character
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    public Node target;

    public float FlightSpeed = 0;

    public bool TargetInFiringArc = false;

    [Export]
    public int AIState = 0;

    public float AITimer = 0;

    public Vector2 TargetPosition = Vector2.Zero;

    public override void OnReady()
    {
        SetProcess(true);
        SetPhysicsProcess(true);
        target = null;
        FlightSpeed = 18f;
        IsEnemy = true;
        IsBoss = true;
        Health = 800;
        MaxHealth = Health;
    }

    public void Start()
    {
        //Velocity = new Vector2(0, -20);
       //IsEnemy = true;
        //SetProcess(true);
        //SetPhysicsProcess(true);
    }

    public void FindTarget()
    {

        target = Game.CurrentLevel.player;
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
            (GetNode("ReloadTimer") as Timer).Start(0.2f);
            SpawnBullet();
        }

    }

    public void DropFlares()
    {
        if (((Timer)GetNode("ReloadTimer")).TimeLeft < 0.1f)
        {
            (GetNode("ReloadTimer") as Timer).Start(0.28f);
            SpawnFlare(Velocity + new Vector2(40, 0).Rotated(Rotation));
            SpawnFlare(Velocity + new Vector2(-40, 0).Rotated(Rotation));
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

                //Player player = target as Player;


                //Vector2 toTarget = (player.Position - Position);
                //toTarget = toTarget.Normalized();
                //toTarget *= 0.4f;

                //Velocity += toTarget;

                // }

                DropFlares();


                Velocity = Velocity.Normalized() * FlightSpeed;

                Rotation = Velocity.Angle() + (float)Utils.Pi / 2f;

                MissileJamming = true;

               /* if (TargetInFiringArc)
                {
                    ShootAtTarget();
                }*/

                if (AITimer > 5.0)
                {
                    AITimer = 0;
                    AIState = 1;
                }

                break;

            case 1:
                Player player2 = target as Player;

                Vector2 behind = player2.Velocity;

                Vector2 toTarget2 = ((player2.Position - (behind.Normalized() * 500)) - Position);


                Vector2 toTarget = toTarget2.Normalized();
                toTarget *= 2.4f;

                Velocity += toTarget;

                // }
                MissileJamming = false;

                if (Velocity.Length() < FlightSpeed * 0.9f)
                {
                    Velocity = Velocity.Normalized() * FlightSpeed * 0.9f;
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

                if(AITimer % 3 < delta*2)
                {
                    SpawnMissile();
                }

                if (AITimer > 9.0)
                {
                    

                    AITimer = 0;
                    AIState = 2;
                }

                break;

            case 2:

                MissileJamming = false;

                // }
                if (AITimer % 3 < delta * 2)
                {
                    TargetPosition = (target as Character).LevelRelativePosition + (target as Character).Velocity * 40;
                }

                

                if (TargetInFiringArc|| (Position + (Velocity*10) - TargetPosition).Length() < 1000)
                
                {
                    
                    ShootAtTarget();
                }
                else{
                    Player player = target as Player;


                    Vector2 toTarget3 = (player.Position - Position);
                    toTarget = toTarget3.Normalized();
                    toTarget *= 1.3f;

                    Velocity += toTarget;

                }

                Velocity = Velocity.Normalized() * FlightSpeed;

                Rotation = Velocity.Angle() + (float)Utils.Pi / 2f;

                if (AITimer > 9.0)
                {
                    AITimer = 0;
                    AIState = 0;
                }

                break;

        }

    }

    public override void _PhysicsProcess(float delta)
    {
        // if(target == null)
        // {
        //    FindTarget();
        // }
        //else
        // {

        base._PhysicsProcess(delta);



    }
}
