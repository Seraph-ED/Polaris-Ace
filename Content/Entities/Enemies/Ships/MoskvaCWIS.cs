using Content;
using Godot;
using System;

public class MoskvaCWIS : ShipComponent
{
   

    

    public Node target;

    public bool TargetInFiringArc = false;

    public int ShootState = 0;

    [Export]
    public float BulletDamage = 1;

    [Export]
    public float StartTimeOffset = 0;

    public override void OnReady()
    {

    }

    public override void OnActivate()
    {
        (GetNode("ReloadTimer") as Timer).Start(4f+StartTimeOffset);
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

        if (((target as Character).LevelRelativePosition - LevelRelativePosition).Length() < 1200 && (ShootState >= 1f || ((Timer)GetNode("ReloadTimer")).TimeLeft < 2.5f) && ((Timer)GetNode("ReloadTimer")).TimeLeft > 0.6f)
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
        



        ShootAtTarget();

        if (HasNode("FiringCrosshair"))
        {

            (GetNode("FiringCrosshair") as FiringCrosshair).Visible = ShootState == 2 || ((Timer)GetNode("ReloadTimer")).TimeLeft < 2.5f;
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

        float offset = Mathf.Deg2Rad(45);

       //Game.CurrentLevel.SpawnProjectile(scene, LevelRelativePosition + new Vector2(15, 0).Rotated(Rotation), vel + new Vector2(20, 0).Rotated(Rotation-offset), Rotation-offset + (Mathf.Pi / 2f), BulletDamage);
        Game.CurrentLevel.SpawnProjectile(scene, LevelRelativePosition + new Vector2(12, 0).Rotated(Rotation), vel + new Vector2(20, 0).Rotated(Rotation), Rotation + (Mathf.Pi / 2f), BulletDamage);
        //Game.CurrentLevel.SpawnProjectile(scene, LevelRelativePosition + new Vector2(15, 0).Rotated(Rotation), vel + new Vector2(20, 0).Rotated(Rotation+offset), Rotation+offset + (Mathf.Pi / 2f), BulletDamage);

        //Game.CurrentLevel.SpawnProjectile(scene, LevelRelativePosition + new Vector2(0, -13).Rotated(turretRotation), Velocity + new Vector2(45, 0).Rotated(turretRotation), turretRotation + (Mathf.Pi / 2f), 5);


        //instance.set
    }

    public void ShootAtTarget()
    {
        if (((Timer)GetNode("ReloadTimer")).TimeLeft <= 0.05f)
        {
            if (((target as Character).LevelRelativePosition - LevelRelativePosition).Length() < 3000 && ShootState == 0)
            {
                (GetNode("ReloadTimer") as Timer).Start(4f);
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

        }

        else if (((Timer)GetNode("ReloadTimer")).TimeLeft < 0.6f && ShootState == 2)
        {
            //ShootState = 2;
            if (((Timer)GetNode("ShootTimer")).TimeLeft < 0.1f)
            {

                Node grandparent = GetParent().GetParent();
                
                (GetNode("ShootTimer") as Timer).Start(0.12f+0.05f);
                

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
