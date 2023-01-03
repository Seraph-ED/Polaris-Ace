using Godot;
using System;

public class AirshipGunPodConstraints : Character
{
    
    [Export]
    public int ShootState = 0;

    public int phase = 0;

    [Export]
    public int TurnOffsetMultiplier = 1;

    public float TurningOffset = 0;

    public override void OnReady()
    {

    }

    public void FindTarget()
    {

       
    }

    public override void OnActivate()
    {
        (GetNode("ReloadTimer") as Timer).Start(15f);
    }

    public override void Behavior(float delta)
    {
        //base.Behavior(delta);

        //FindTarget();
        // MovementBehavior();



        ShootAtTarget();

        if (HasNode("FiringCrosshair"))
        {
            (GetNode("FiringCrosshair") as FiringCrosshair).TimeProportion = Mathf.Clamp(((Timer)GetNode("ReloadTimer")).TimeLeft - 1.2f, 0f, 1f);
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

        

        if (grandparent is BossAirship && (grandparent as BossAirship).Vulnerable)
        {
            for (int i = 0; i < 4; i++)
            {
                float angle = Mathf.Pi * 2f / 4 * i;
                float angle2 = angle + (TurningOffset*TurnOffsetMultiplier);// + (Mathf.Pi / 4f);
                Projectile proj = Game.CurrentLevel.SpawnProjectile(scene, LevelRelativePosition, vel + (Vector2.Right * 12).Rotated(angle2), angle2, 2);
                proj.Lifespan = 3.6f;

                //proj.Rotation = angle;
            }
            TurningOffset += Mathf.Deg2Rad(1f);
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                float angle = Mathf.Pi * 2f / 4 * i;
                float angle2 = angle;// + (Mathf.Pi / 4f);
                Projectile proj = Game.CurrentLevel.SpawnProjectile(scene, LevelRelativePosition, vel + (Vector2.Right * 12).Rotated(angle2), angle2, 2);
                proj.Lifespan = 3.6f;

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
                TurningOffset = 0;
            }
            else if (ShootState > 0)
            {
                ShootState = 0;
                
            }



        }

        if (((Timer)GetNode("ReloadTimer")).TimeLeft < 4.2f && ShootState == 1)
        {
            ShootState = 2;
            (GetNode("Sprite") as AnimatedSprite).Frame = 1;

        }

        else if (((Timer)GetNode("ReloadTimer")).TimeLeft < 4 && ShootState == 2)
        {
            //ShootState = 2;
            if (((Timer)GetNode("ShootTimer")).TimeLeft < 0.05f)
            {
                (GetNode("ShootTimer") as Timer).Start(0.13f);
                SpawnBullets();
            }
        }






    }
    //  // 
}
