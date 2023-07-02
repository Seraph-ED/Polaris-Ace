using Godot;
using System;

public class Laser : Weapon 
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override string DisplayName => "Laser";

    public override string Description => "A concentrated beam of light which deals damage over time, can be aimed towards the cursor. 60 damage per second (total: 300), 6 second cooldown.";

    public override float UseTime => 5.0f;

    public override float CooldownSeconds => 6.0f;
    public override void _Ready()
    {
        
    }

    

    public override void Shoot(Character shooter)
    {
        base.Shoot(shooter);
        var scene = GD.Load<PackedScene>("res://Content/Entities/Projectiles/LaserProjectile.tscn");

        for (int i = 0; i < 1; i++)
        {
            //LaserProjectile instance = scene.Instance() as LaserProjectile;
            Projectile instance = Game.CurrentLevel.SpawnProjectile(scene, shooter.LevelRelativePosition, Vector2.Zero, shooter.Rotation, 12);
            Game.CurrentLevel.AddChild(instance);
            
            (instance as LaserProjectile).attachedChar = shooter;
            (instance as LaserProjectile).PlayerHeld = true;

        }
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
