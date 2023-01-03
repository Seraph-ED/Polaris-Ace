using Content;
using Godot;
using System;

public class DroneFriendly : Character
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public int FollowIndex = 0;

    [Export]
    public float FlightTime = 30;


    public int AIState = 0;

    public float FlightSpeed = 38;

    public override void OnReady()
    {
        
    }


    public override void Behavior(float delta)
    {
        base.Behavior(delta);

        if (Game.CurrentLevel.player == null)
        {
            QueueFree();
            return;
        }


        switch (AIState)
        {
            default:

                int lol = ((FollowIndex % 2) * 2) - 1;
                int lol2 = ((int)(FollowIndex / 2)) + 1;

                Vector2 holdingPatternPosition = new Vector2(50 * lol, 50) * lol2;

                Vector2 velocity2 = (Game.CurrentLevel.player.LevelRelativePosition +(holdingPatternPosition).Rotated(Game.CurrentLevel.player.Rotation)) - LevelRelativePosition;

                velocity2 = velocity2.Normalized() * FlightSpeed;

                Velocity = Utils.VecLerp(Velocity, velocity2, 0.06f);

                if (((Game.CurrentLevel.player.LevelRelativePosition + (holdingPatternPosition).Rotated(Game.CurrentLevel.player.Rotation)) - LevelRelativePosition).Length() < 60)
                {
                    Rotation = Game.CurrentLevel.player.Rotation;
                }
                else
                {
                    Rotation = Velocity.Angle() + (Mathf.Pi / 2f);
                }

                FlightTime -= delta;
                if (FlightTime <= 0)
                {
                    FlightTime = 0;
                    AIState = 1;
                }

                if (Input.IsActionPressed("ShootGun") && (GetNode("ReloadTimer") as Timer).TimeLeft == 0)
                {
                    (GetNode("ReloadTimer") as Timer).Start(0.06f);
                    SpawnBullets();
                }


                break;
            case 1:
                Rotation = Velocity.Angle() + (Mathf.Pi / 2f);

                Velocity = Utils.VecLerp(Velocity, Velocity.Normalized() * FlightSpeed, 0.03f);

                if ((Game.CurrentLevel.player.LevelRelativePosition - LevelRelativePosition).Length()>3000)
                {
                    QueueFree();
                }

                break;

        }
    }

    public void SpawnBullets()
    {
        var scene = GD.Load<PackedScene>("res://Content/Entities/Projectiles/BulletFriendly.tscn");

        Game.CurrentLevel.SpawnProjectile(scene, LevelRelativePosition + new Vector2(20, 0).Rotated(Rotation), Velocity + new Vector2(0, -80).Rotated(Rotation), Rotation, (7f/24f)*Game.CurrentLevel.player.BulletDamage);
        Game.CurrentLevel.SpawnProjectile(scene, LevelRelativePosition + new Vector2(-20, 0).Rotated(Rotation), Velocity + new Vector2(0, -80).Rotated(Rotation), Rotation, (7f / 24f) *Game.CurrentLevel.player.BulletDamage);


        //instance.set
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
