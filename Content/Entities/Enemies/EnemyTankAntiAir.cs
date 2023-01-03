using Godot;
using System;
using Content;

public class EnemyTankAntiAir : Character
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.

    public Node target;

    public bool TargetInFiringArc = false;

    public float turretRotation = 0;

    public int ShootState = 0;

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
        if (((Timer)GetNode("ReloadTimer")).TimeLeft <= 0.05f )
        {
            if (((target as Character).LevelRelativePosition - LevelRelativePosition).Length() < 1000 && ShootState == 0)
            {
                (GetNode("ReloadTimer") as Timer).Start(3f);
                ShootState = 1;
            }else if (ShootState > 0)
            {
                ShootState = 0;
            }
            
            

        }

        if (((Timer)GetNode("ReloadTimer")).TimeLeft < 1.2f && ShootState == 1)
        {
            ShootState = 2;
            
        }

        else if (((Timer)GetNode("ReloadTimer")).TimeLeft < 1&&ShootState == 2)
        {
            //ShootState = 2;
            if (((Timer)GetNode("ShootTimer")).TimeLeft < 0.1f)
            {
                (GetNode("ShootTimer") as Timer).Start(0.15f);
                SpawnBullets();
            }
        }






    }

    public void SpawnBullets()
    {
        var scene = GD.Load<PackedScene>("res://Content/Entities/Projectiles/BulletHostile.tscn");

       

        Game.CurrentLevel.SpawnProjectile(scene, LevelRelativePosition + new Vector2(0, 13).Rotated(turretRotation), Velocity + new Vector2(45,0).Rotated(turretRotation), turretRotation + (Mathf.Pi / 2f), 5);
        Game.CurrentLevel.SpawnProjectile(scene, LevelRelativePosition + new Vector2(0, -13).Rotated(turretRotation), Velocity + new Vector2(45, 0).Rotated(turretRotation), turretRotation + (Mathf.Pi / 2f), 5);
        

        //instance.set
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

        if (((target as Character).LevelRelativePosition - LevelRelativePosition).Length() < 1000&&ShootState <= 1f)
        {
            Vector2 targetLead = (target as Character).LevelRelativePosition + (target as Character).Velocity * 20;


            turretRotation = Utils.TurnTowards(turretRotation, (targetLead - LevelRelativePosition).Angle(), Mathf.Deg2Rad(2.6f * delta * 60f));
        }

        (GetNode("Turret") as Sprite).Rotation = turretRotation + (Mathf.Pi / 2f) - Rotation;

        ShootAtTarget();

        if (HasNode("Turret/FiringCrosshair"))
        {
            (GetNode("Turret/FiringCrosshair") as FiringCrosshair).TimeProportion = Mathf.Clamp(((Timer)GetNode("ReloadTimer")).TimeLeft - 1.2f, 0f, 1f);
        }

        if (TargetInFiringArc)
        {
            
        }

    }
    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
