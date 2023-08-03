using Content;
using Godot;
using System;

public class BaseRailgunTurret : Character {



    public Node target;

    public bool TargetInFiringArc = false;

    //public int ShootState = 0;

    [Export]
    public float BulletDamage = 1;

    [Export]
    public float TurnSpeedDegreesPerSecond = 1;

    [Export]
    public float TargetLead = 1;

    [Export]
    public float ProjectileSpeed = 70;

    [Export]
    public float ReloadTime = 3;

    [Export]
    public float ShootRange = 1500;

    public override void OnReady()
    {

    }

    public override void OnActivate()
    {
        (GetNode("ReloadTimer") as Timer).Start(ReloadTime);
    }

    public void FindTarget()
    {

        if (Game.CurrentLevel.player != null)
        {
            target = Game.CurrentLevel.player;
        }
        else
        {
            target = null;
        }
    }

    public override void Behavior(float delta)
    {
        //base.Behavior(delta);
        if (MissionKill)
        {
            return;
        }

        FindTarget();

        if (target == null)
        {
            return;
        }

        GetNode<FiringCrosshair>("FiringCrosshair").TimeProportion = ((GetNode("ReloadTimer") as Timer).TimeLeft / ReloadTime);
        // MovementBehavior();

        if (((target as Character).LevelRelativePosition - LevelRelativePosition).Length() < ShootRange)
        {

            Vector2 vel = Velocity;

            Node grandparent = GetParent().GetParent();

            if (GetParent() is Character)
            {
                vel = (GetParent() as Character).Velocity;
            }


            if (grandparent is Character)
            {
                vel = (grandparent as Character).Velocity;
            }

            Vector2 relativeVelocity = (target as Character).Velocity - vel;

            // GD.Print(relativeVelocity);


            Vector2 targetLead = (target as Character).LevelRelativePosition + (TargetLead * relativeVelocity);


            Rotation = Utils.TurnTowards(Rotation, (targetLead - LevelRelativePosition).Angle() - (GetParent() as Node2D).Rotation, Mathf.Deg2Rad(TurnSpeedDegreesPerSecond * delta));

            ShootAtTarget();
        }







    }

    public virtual void SpawnBullets()
    {
        var scene = GD.Load<PackedScene>("res://Content/Entities/Projectiles/RailgunProjectile.tscn");
        Vector2 vel = Velocity;


        float offset = Mathf.Deg2Rad(45);

        // Game.CurrentLevel.SpawnProjectile(scene, LevelRelativePosition + new Vector2(15, 0).Rotated(Rotation), vel + new Vector2(20, 0).Rotated(Rotation-offset), Rotation-offset + (Mathf.Pi / 2f), BulletDamage);
        Game.CurrentLevel.SpawnProjectile(scene, LevelRelativePosition + new Vector2(15, 0).Rotated(Rotation), vel + new Vector2(ProjectileSpeed, 0).Rotated(Rotation), Rotation, BulletDamage);
        // Game.CurrentLevel.SpawnProjectile(scene, LevelRelativePosition + new Vector2(15, 0).Rotated(Rotation), vel + new Vector2(20, 0).Rotated(Rotation+offset), Rotation+offset + (Mathf.Pi / 2f), BulletDamage);

        //Game.CurrentLevel.SpawnProjectile(scene, LevelRelativePosition + new Vector2(0, -13).Rotated(turretRotation), Velocity + new Vector2(45, 0).Rotated(turretRotation), turretRotation + (Mathf.Pi / 2f), 5);


        //instance.set
    }

    public virtual void ShootAtTarget()
    {
        if (((Timer)GetNode("ReloadTimer")).TimeLeft <= 0.05)
        {

            (GetNode("ReloadTimer") as Timer).Start(ReloadTime+0.05f);

            GetNode<SparkEmitter>("LeftEmitter").EmitParticles();
            GetNode<SparkEmitter>("RightEmitter").EmitParticles();
            GetNode<SparkEmitter>("MuzzleFlash").EmitParticles();

            SpawnBullets();


        }








    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
