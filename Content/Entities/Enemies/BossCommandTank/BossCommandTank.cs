using Content;
using Godot;
using System;

public class BossCommandTank : Character
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public Node target;

    public bool TargetInFiringArc = false;

    public int ShootState = 0;

    public Vector2 MovementTarget = Vector2.Zero;

    public float MovementSpeed = 15;

    public float turretRotation = 0;

    public override void OnReady()
    {
        
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

    public void FindTarget()
    {

        target = Game.CurrentLevel.GetNode("Player");
    }

    public void ShootAtTarget(float delta)
    {
        Timer reload = (Timer)GetNode("CycleTimer");

        (GetNode("Turret") as Node2D).Rotation = turretRotation + (Mathf.Pi / 2f) - Rotation;

        if (reload.TimeLeft <= 0.05f)
        {
            ++ShootState;

            if (ShootState > 4)
            {
                ShootState = 0;
            }

            switch (ShootState)
            {
                case 0:
                    reload.Start(5);

                    break;
                case 1:
                    reload.Start(5f);

                    break;
                case 2:
                    reload.Start(3.8f);

                    break;
                case 3:
                    reload.Start(0.8f);

                    break;
                case 4:
                    reload.Start(2);

                    break;






            }





        }

        switch (ShootState)
        {
            case 1:

                float turretTargetRot = Rotation - (Mathf.Pi / 2f) - (Mathf.Pi * 2f * (reload.TimeLeft / 5f));

                turretRotation = Utils.TurnTowards(turretRotation, turretTargetRot, Mathf.Deg2Rad(0.5f/delta));



                if (((Timer)GetNode("ShootTimer")).TimeLeft <= 0f)
                {

                   // SpawnBullets();
                    (GetNode("ShootTimer") as Timer).Start(0.1f);
                    SpawnRapidBullets();



                }
                break;
            case 2:

                turretRotation = Utils.TurnTowards(turretRotation, ((target as Character).LevelRelativePosition - LevelRelativePosition).Angle(), Mathf.Deg2Rad(3f * delta * 60f));

                if (((Timer)GetNode("ShootTimer")).TimeLeft <= 0f)
                {

                  //  SpawnSlowBullets();
                    (GetNode("ShootTimer") as Timer).Start(0.8f);



                }
                break;

            case 3:
                turretRotation = Utils.TurnTowards(turretRotation, ((target as Character).LevelRelativePosition - LevelRelativePosition).Angle(), Mathf.Deg2Rad(3f * delta * 60f));
                if (((Timer)GetNode("ShootTimer")).TimeLeft <= 0f)
                {

                   SpawnAgileMissiles();
                    (GetNode("ShootTimer") as Timer).Start(0.2f);



                }
                break;
            case 4:

                float turretTargetRot2 = Rotation - (Mathf.Pi / 2f);

                turretRotation = Utils.TurnTowards(turretRotation, turretTargetRot2, Mathf.Deg2Rad(0.5f / delta));

                if (((Timer)GetNode("ShootTimer")).TimeLeft <= 0f)
                {

                    // SpawnFragMissiles();
                    (GetNode("ShootTimer") as Timer).Start(0.6f);



                }
                break;


        }







    }

    public void SpawnRapidBullets()
    {
        var scene = GD.Load<PackedScene>("res://Content/Entities/Projectiles/BulletHostile.tscn");
        Vector2 vel = Velocity;
        Node grandparent = GetParent().GetParent();

       /* if (GetParent() is Character)
        {
            vel = (GetParent() as Character).Velocity;
        }


        if (grandparent is Character)
        {
            vel = (grandparent as Character).Velocity;
        }*/


        Game.CurrentLevel.SpawnProjectile(scene, LevelRelativePosition + new Vector2(0, 13).Rotated(turretRotation), vel + new Vector2(50, 0).Rotated(turretRotation), turretRotation + (Mathf.Pi / 2f), 5);
        Game.CurrentLevel.SpawnProjectile(scene, LevelRelativePosition + new Vector2(0, -13).Rotated(turretRotation), vel + new Vector2(50, 0).Rotated(turretRotation), turretRotation + (Mathf.Pi / 2f), 5);


        //instance.set
    }

    public void SpawnAgileMissiles()
    {
        var scene = GD.Load<PackedScene>("res://Content/Entities/Projectiles/MissileHostileAgile.tscn");
        Vector2 vel = Velocity;
        Node grandparent = GetParent().GetParent();

        /* if (GetParent() is Character)
         {
             vel = (GetParent() as Character).Velocity;
         }


         if (grandparent is Character)
         {
             vel = (grandparent as Character).Velocity;
         }*/


        Game.CurrentLevel.SpawnProjectile(scene, LevelRelativePosition + new Vector2(0, 36).Rotated(turretRotation), vel + new Vector2(40, 0).Rotated(turretRotation), turretRotation + (Mathf.Pi / 2f), 5);
        Game.CurrentLevel.SpawnProjectile(scene, LevelRelativePosition + new Vector2(0, -36).Rotated(turretRotation), vel + new Vector2(40, 0).Rotated(turretRotation), turretRotation + (Mathf.Pi / 2f), 5);


        //instance.set
    }

    public override void Behavior(float delta)
    {
        FindTarget();
        MovementBehavior();

        ShootAtTarget(delta);

        if (HasNode("FiringCrosshair"))
        {

            (GetNode("FiringCrosshair") as FiringCrosshair).Visible = ShootState == 0||ShootState == 2;// || ((Timer)GetNode("ReloadTimer")).TimeLeft < 4.5f;
            (GetNode("FiringCrosshair") as FiringCrosshair).TimeProportion = Mathf.Clamp(((Timer)GetNode("ShootTimer")).TimeLeft / 3f, 0f, 1f);
        }
    }
    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
