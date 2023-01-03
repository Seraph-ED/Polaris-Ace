using Content;
using Godot;
using System;

public class EnemyHelicopterGunship : Character
{

    public Node target;

    public bool TargetInFiringArc = false;

    public int ShootState = 0;

    public Vector2 MovementTarget = Vector2.Zero;

    public float MovementSpeed = 8;

    public override void OnReady()
    {

    }

    public void MovementBehavior()
    {

        if (GetParent() is PathEntityContainer)
        {
            PathEntityContainer p = (GetParent() as PathEntityContainer);

            MovementTarget = p.LevelPositionOnpath;


            Velocity = (MovementTarget - LevelRelativePosition).Normalized() * MovementSpeed;
            //Rotation = Velocity.Angle();

        }

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
        }
        else if (((target as Character).LevelRelativePosition - LevelRelativePosition).Length() < 1200)
        {
            Rotation = Utils.TurnTowards(Rotation, (Mathf.Pi / 2) - (GetParent() as Node2D).Rotation, Mathf.Deg2Rad(2.6f * delta * 60f));
        }



        ShootAtTarget();

        if (HasNode("FiringCrosshair"))
        {

            (GetNode("FiringCrosshair") as FiringCrosshair).Visible = ShootState == 0;// || ((Timer)GetNode("ReloadTimer")).TimeLeft < 4.5f;
             (GetNode("FiringCrosshair") as FiringCrosshair).TimeProportion = Mathf.Clamp(((Timer)GetNode("ShootTimer")).TimeLeft / 3f, 0f, 1f);
        }

        if (TargetInFiringArc)
        {

        }

    }

    public void SpawnBullets()
    {
        var scene = GD.Load<PackedScene>("res://Content/Entities/Projectiles/BulletHostile.tscn");
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


        Game.CurrentLevel.SpawnProjectile(scene, LevelRelativePosition + new Vector2(0, 0).Rotated(Rotation), vel + new Vector2(50, 0).Rotated(Rotation), Rotation + (Mathf.Pi / 2f), 3);
        //Game.CurrentLevel.SpawnProjectile(scene, LevelRelativePosition + new Vector2(0, -13).Rotated(turretRotation), Velocity + new Vector2(45, 0).Rotated(turretRotation), turretRotation + (Mathf.Pi / 2f), 5);


        //instance.set
    }


    public void SpawnSlowBullets()
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


        Game.CurrentLevel.SpawnProjectile(scene, LevelRelativePosition + new Vector2(0, 0).Rotated(Rotation), new Vector2(16, 0).Rotated(Rotation), Rotation + (Mathf.Pi / 2f), 8);
        //Game.CurrentLevel.SpawnProjectile(scene, LevelRelativePosition + new Vector2(0, -13).Rotated(turretRotation), Velocity + new Vector2(45, 0).Rotated(turretRotation), turretRotation + (Mathf.Pi / 2f), 5);


        //instance.set
    }

    public void SpawnFragMissiles()
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
        vel /= 10f;

        float angleAlt = Mathf.Deg2Rad(30f);


        Projectile proj = Game.CurrentLevel.SpawnProjectile(scene, LevelRelativePosition + new Vector2(0, 0).Rotated(Rotation), vel + new Vector2(4, 0).Rotated(Rotation+angleAlt), (vel + new Vector2(4, 0).Rotated(Rotation + angleAlt)).Angle() + (Mathf.Pi / 2f), 10);

        proj.Lifespan = 0.8f;
        (proj as MissileHostileFragmentation).Fragments = 8;
        (proj as MissileHostileFragmentation).FragmentVel = 8;

        angleAlt *= -1;


        proj = Game.CurrentLevel.SpawnProjectile(scene, LevelRelativePosition + new Vector2(0, 0).Rotated(Rotation), vel + new Vector2(4, 0).Rotated(Rotation + angleAlt), (vel + new Vector2(4, 0).Rotated(Rotation + angleAlt)).Angle() + (Mathf.Pi / 2f), 10);

        proj.Lifespan = 0.8f;
        (proj as MissileHostileFragmentation).Fragments = 8;
        (proj as MissileHostileFragmentation).FragmentVel = 8;
        //Game.CurrentLevel.SpawnProjectile(scene, LevelRelativePosition + new Vector2(0, -13).Rotated(turretRotation), Velocity + new Vector2(45, 0).Rotated(turretRotation), turretRotation + (Mathf.Pi / 2f), 5);


        //instance.set
    }

    public void ShootAtTarget()
    {
        Timer reload = (Timer)GetNode("ReloadTimer");


        if (((Timer)GetNode("ReloadTimer")).TimeLeft <= 0.05f)
        {
            ++ShootState;

            if (ShootState > 3)
            {
                ShootState = 0;
            }

            switch (ShootState)
            {
                case 0:
                    reload.Start(3);

                    break;
                case 1:
                    reload.Start(1.5f);

                    break;
                case 2:
                    reload.Start(4.8f);

                    break;
                case 3:
                    reload.Start(1);

                    break;





            }





        }

        switch (ShootState)
        {
            case 1:
                if (((Timer)GetNode("ShootTimer")).TimeLeft < 0.1f)
                {

                    SpawnBullets();
                     (GetNode("ShootTimer") as Timer).Start(0.2f);
                       


                }
                break;
            case 2:
                if (((Timer)GetNode("ShootTimer")).TimeLeft < 0.1f)
                {

                    SpawnSlowBullets();
                    (GetNode("ShootTimer") as Timer).Start(0.8f);



                }
                break;

            case 3:
                if (((Timer)GetNode("ShootTimer")).TimeLeft < 0.1f)
                {

                    SpawnFragMissiles();
                    (GetNode("ShootTimer") as Timer).Start(0.6f);



                }
                break;


        }

        





    }
}
