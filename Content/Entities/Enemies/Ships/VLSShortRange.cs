using Godot;
using System;

public class VLSShortRange : VLS
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.

   
    public override void ShootMissile()
    {
        if (FiringState <= 0)
        {

        }
        else
        {
            FiringState--;

            if (MissileScene == null)
            {
                MissileScene = GD.Load<PackedScene>(MissileScenePath);
            }
            else
            {
                Game.CurrentLevel.SpawnProjectile(MissileScene, LevelRelativePosition, (Vector2.Right * 20).Rotated((Mathf.Pi / 3 * ((float)FiringState / QueuedMissileNum))-(Mathf.Pi/6)+Rotation), 0, MissileDamage);
            }




           

        }



        if (FiringState <= 0)
        {
            FiringState = 0;
            Cooldown.Start(MissileReloadTime);
        }
        else
        {

            Cooldown.Start(MissileSalvoCooldown);
        }

    }

    
    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
