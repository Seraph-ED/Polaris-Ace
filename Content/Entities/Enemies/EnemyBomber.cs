using Content;
using Godot;
using System;

public class EnemyBomber : Character
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public EntityContainer CirclePoint;

    public float FlightSpeed = 12;

    public float CircleDist = 0;

    public float TurretAngle = 0;

    public override void OnReady()
    {
        Health = 350;
        IsEnemy = true;
        CirclePoint = null;
        //IsBoss = true;
        CircleDist = Position.Length();
    }

   
    public void SpawnMissile(float direction)
    {
        var scene = GD.Load<PackedScene>("res://Content/Entities/Projectiles/MissileHostileBomber.tscn");

        Game.CurrentLevel.SpawnProjectile(scene, LevelRelativePosition, new Vector2(0, -5).Rotated(direction), direction + (Mathf.Pi / 2), 30);

        

        //instance.set
    }

    public void GetWaypoint()
    {
        if(!(GetParent() is EntityContainer))
        {
            return;
        }

        CirclePoint = (GetParent() as EntityContainer);
    
    }


    

    public override void Behavior(float delta)
    {

        if (CirclePoint == null)
        {
            GetWaypoint();
        }

        Velocity = new Vector2(FlightSpeed, 0).Rotated(Rotation - (Mathf.Pi / 2f));

        if (CirclePoint != null)
        {

            Vector2 futureTarget = (Position + (Velocity * 4)).Normalized() * CircleDist;

            Rotation = (futureTarget - Position).Angle() + (Mathf.Pi / 2f);



        }

        if (Game.CurrentLevel == null)
        {
            return;
        }
        
            
            
        
        if (IsInstanceValid(Game.CurrentLevel.player) && Game.CurrentLevel.player != null)
        {



            TurretAngle = (Game.CurrentLevel.player.LevelRelativePosition - (LevelRelativePosition + (new Vector2(0, 22).Rotated(Rotation)))).Angle();

            if ((Game.CurrentLevel.player.LevelRelativePosition - LevelRelativePosition).Length() < 800)
            {
               /* if (((Timer)GetNode("ReloadTimer")).TimeLeft < 0.1f)
                {
                    (GetNode("ReloadTimer") as Timer).Start(0.4f);
                    SpawnBullet(TurretAngle);
                }*/
            }


        }



        if (((Timer)GetNode("MissileTimer")).TimeLeft < 0.1f)
        {
            (GetNode("MissileTimer") as Timer).Start(5f);
            int numMissiles = 4;
            float a = Mathf.Deg2Rad(120);
            for (int i = 0; i < numMissiles; ++i)
            {


                SpawnMissile(Rotation + (a / numMissiles * i) - (a / 2));
            }
        }

        (GetNode("TurretSprite") as Node2D).Rotation = (TurretAngle + (Mathf.Pi * 0.5f)) - Rotation;
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
