using Content;
using Godot;
using System;

public class AirshipGunTurretSmall : Character
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.

    public Node target;

    public bool TargetInFiringArc = false;

    public int ShootState = 0;

    public override void OnReady()
    {

    }

    public override void OnActivate()
    {
        (GetNode("ReloadTimer") as Timer).Start(15f);
    }

    public void FindTarget()
    {

        target = Game.CurrentLevel.GetNode("Player");
    }

    public override void Behavior(float delta)
    {
        //base.Behavior(delta);

        FindTarget();
        // MovementBehavior();

        if (((target as Character).LevelRelativePosition - LevelRelativePosition).Length() < 1200 && (ShootState > 1f || ((Timer)GetNode("ReloadTimer")).TimeLeft < 4.5f))
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


            Vector2 targetLead = (target as Character).LevelRelativePosition;

            Vector2 currentLead = LevelRelativePosition + (Velocity * 20);
            Rotation = Utils.TurnTowards(Rotation, (targetLead - LevelRelativePosition).Angle() - (GetParent() as Node2D).Rotation, Mathf.Deg2Rad(2.6f * delta * 60f));
        }else if (((target as Character).LevelRelativePosition - LevelRelativePosition).Length() < 1200)
        {
            Rotation = Utils.TurnTowards(Rotation, (Mathf.Pi/2) - (GetParent() as Node2D).Rotation, Mathf.Deg2Rad(2.6f * delta * 60f));
        }



        ShootAtTarget();

        if (HasNode("FiringCrosshair"))
        {

            (GetNode("FiringCrosshair") as FiringCrosshair).Visible = ShootState == 2 || ((Timer)GetNode("ReloadTimer")).TimeLeft < 4.5f;
            (GetNode("FiringCrosshair") as FiringCrosshair).TimeProportion = Mathf.Clamp(((Timer)GetNode("ShootTimer")).TimeLeft / 1.2f, 0f, 1f);
        }

        if (TargetInFiringArc)
        {

        }

    }

    public void SpawnBullets()
    {
        var scene = GD.Load<PackedScene>("res://Content/Entities/Projectiles/BulletHostileSlow.tscn");
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


        Game.CurrentLevel.SpawnProjectile(scene, LevelRelativePosition + new Vector2(0, 0).Rotated(Rotation), vel + new Vector2(20, 0).Rotated(Rotation), Rotation + (Mathf.Pi / 2f), 5);
        //Game.CurrentLevel.SpawnProjectile(scene, LevelRelativePosition + new Vector2(0, -13).Rotated(turretRotation), Velocity + new Vector2(45, 0).Rotated(turretRotation), turretRotation + (Mathf.Pi / 2f), 5);


        //instance.set
    }

    public void ShootAtTarget()
    {
        if (((Timer)GetNode("ReloadTimer")).TimeLeft <= 0.05f)
        {
            if (((target as Character).LevelRelativePosition - LevelRelativePosition).Length() < 3000 && ShootState == 0)
            {
                (GetNode("ReloadTimer") as Timer).Start(15f);
                ShootState = 1;
            }
            else if (ShootState > 0)
            {
                ShootState = 0;
            }



        }

        if (((Timer)GetNode("ReloadTimer")).TimeLeft < 3.2f && ShootState == 1)
        {
            ShootState = 2;

        }

        else if (((Timer)GetNode("ReloadTimer")).TimeLeft < 3 && ShootState == 2)
        {
            //ShootState = 2;
            if (((Timer)GetNode("ShootTimer")).TimeLeft < 0.1f)
            {
                
                Node grandparent = GetParent().GetParent();
                if (grandparent!=null&&grandparent is BossAirship && (grandparent as BossAirship).Vulnerable)
                {
                    (GetNode("ShootTimer") as Timer).Start(0.5f);
                }
                else
                {
                    (GetNode("ShootTimer") as Timer).Start(1.2f);
                }

                SpawnBullets();
            }
        }






    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
