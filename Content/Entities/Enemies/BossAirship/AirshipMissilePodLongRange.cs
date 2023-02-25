using Content;
using Godot;
using System;

public class AirshipMissilePodLongRange : Character
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public Node target;

    public bool TargetInFiringArc = false;
    [Export]
    public int ShootState = 0;

    public override void OnReady()
    {

    }

    public void FindTarget()
    {

        target = Game.CurrentLevel.GetNode("Player");
    }

    public override void OnActivate()
    {
        (GetNode("ReloadTimer") as Timer).Start(0.1f);
    }

    public override void Behavior(float delta)
    {
        //base.Behavior(delta);

        FindTarget();
        // MovementBehavior();

        

        ShootAtTarget();

        if (HasNode("FiringCrosshair"))
        {
            (GetNode("FiringCrosshair") as FiringCrosshair).TimeProportion = Mathf.Clamp(((Timer)GetNode("ReloadTimer")).TimeLeft - 1.2f, 0f, 1f);
        }

        if (TargetInFiringArc)
        {

        }

    }

    public void SpawnMissile()
    {
        var scene = GD.Load<PackedScene>("res://Content/Entities/Projectiles/MissileHostileLongRange.tscn");
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


        Game.CurrentLevel.SpawnProjectile(scene, LevelRelativePosition + new Vector2(0, 0).Rotated(Rotation), vel, Rotation, 15);
        //Game.CurrentLevel.SpawnProjectile(scene, LevelRelativePosition + new Vector2(0, -13).Rotated(turretRotation), Velocity + new Vector2(45, 0).Rotated(turretRotation), turretRotation + (Mathf.Pi / 2f), 5);


        //instance.set
    }

    public void ShootAtTarget()
    {
        if (((Timer)GetNode("ReloadTimer")).TimeLeft <= 0.05f)
        {
            (GetNode("Sprite") as AnimatedSprite).Frame = 0;
            if ( ShootState == 0)
            {
                (GetNode("ReloadTimer") as Timer).Start(15f);
                ShootState = 1;
            }
            else if (ShootState > 0)
            {
                ShootState = 0;
            }



        }

        if (((Timer)GetNode("ReloadTimer")).TimeLeft < 2.2f && ShootState == 1)
        {
            ShootState = 2;
            (GetNode("Sprite") as AnimatedSprite).Frame = 1;

        }

        else if (((Timer)GetNode("ReloadTimer")).TimeLeft < 1 && ShootState == 2)
        {
            //ShootState = 2;
            if (((Timer)GetNode("ShootTimer")).TimeLeft < 0.1f)
            {
                (GetNode("ShootTimer") as Timer).Start(0.5f);
                SpawnMissile();
            }
        }






    }
    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
