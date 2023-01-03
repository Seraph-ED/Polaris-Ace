using Godot;
using System;

public class AirshipMissilePodFragmentation : Character
{
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
        (GetNode("ReloadTimer") as Timer).Start(6f);
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

    public void SpawnMissiles()
    {
        var scene = GD.Load<PackedScene>("res://Content/Entities/Projectiles/MissileHostileFragmentation.tscn");
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

        
        if (grandparent is BossAirship && (grandparent as BossAirship).Vulnerable)
        {
            for (int i = 0; i < 4; i++)
            {
                float angle = Mathf.Pi * 2f / 4 * i;
                float angle2 = angle + (Mathf.Pi / 4f);
                Projectile proj = Game.CurrentLevel.SpawnProjectile(scene, LevelRelativePosition, vel + (Vector2.Right * 3).Rotated(angle2), angle2, 20);
                proj.Lifespan = 0.6f;
                (proj as MissileHostileFragmentation).Fragments = 16;
                (proj as MissileHostileFragmentation).FragmentVel = 8;
                //proj.Rotation = angle;
            }
        }
        else
        {
            for (int i = 0; i < 2; i++)
            {
                float angle = Mathf.Pi * 2f / 2 * i;
                float angle2 = angle + Rotation;
                Projectile proj = Game.CurrentLevel.SpawnProjectile(scene, LevelRelativePosition, vel + (Vector2.Right * 3).Rotated(angle2), angle2, 10);
                proj.Lifespan = 0.6f;
                (proj as MissileHostileFragmentation).Fragments = 12;
                (proj as MissileHostileFragmentation).FragmentVel = 8;
                //proj.Rotation = angle;
            }
        }


        //Game.CurrentLevel.SpawnProjectile(scene, LevelRelativePosition + new Vector2(0, 0).Rotated(Rotation), vel, Rotation, 10);
        //Game.CurrentLevel.SpawnProjectile(scene, LevelRelativePosition + new Vector2(0, -13).Rotated(turretRotation), Velocity + new Vector2(45, 0).Rotated(turretRotation), turretRotation + (Mathf.Pi / 2f), 5);


        //instance.set
    }

    public void ShootAtTarget()
    {
        if (((Timer)GetNode("ReloadTimer")).TimeLeft <= 0.05f)
        {
            (GetNode("Sprite") as AnimatedSprite).Frame = 0;
            if (ShootState == 0)
            {
                (GetNode("ReloadTimer") as Timer).Start(15f);
                ShootState = 1;
            }
            else if (ShootState > 0)
            {
                ShootState = 0;
                SpawnMissiles();
            }



        }

        if (((Timer)GetNode("ReloadTimer")).TimeLeft < 1.2f && ShootState == 1)
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
               
            }
        }






    }
    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
