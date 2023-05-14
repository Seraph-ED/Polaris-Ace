using Godot;
using System;

public class VLS : ShipComponent
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    [Export]
    public string MissileScenePath = "";

    public PackedScene MissileScene = null;

    public Timer Cooldown;

    public int FiringState = 0;

    public bool CanShoot = false;

    protected int QueuedMissileNum = 0;

    [Export]
    public float MissileDamage = 20;

    [Export]
    public float MissileSalvoCooldown = 0.1f;

    [Export]
    public float MissileReloadTime = 6f;


    public override void _Ready()
    {
        Cooldown = GetNode("Cooldown") as Timer;
         MissileScene = GD.Load<PackedScene>(MissileScenePath);
    }

    public void QueueShootMissiles(int num)
    {
        if (!CanShoot||MissionKill)
        {
            return;
        }

        FiringState = num;
        QueuedMissileNum = num;


    }

    public virtual void ShootMissile()
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
                Game.CurrentLevel.SpawnProjectile(MissileScene, LevelRelativePosition, Vector2.Zero, 0, MissileDamage);
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

    public override void Behavior(float delta)
    {

        if (MissionKill)
        {
            
            return;
        }
        CanShoot = FiringState == 0 && Cooldown.TimeLeft > 0;

        if (Cooldown.TimeLeft <= 0)
        {
            ShootMissile();
        }


    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
