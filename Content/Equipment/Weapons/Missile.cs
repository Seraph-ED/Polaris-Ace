using Godot;
using System;

public class Missile : Weapon
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.

    public override string DisplayName => "Missile";

    public override string Description => "A high speed explosive projectile that homes in on enemy units. 50 Damage, 3 second cooldown.";

    public override EquipmentUseStyle UseStyle => EquipmentUseStyle.PressAndRelease;
    public override void _Ready()
    {
        
    }

    public override void Shoot(Character shooter)
    {
        var scene = GD.Load<PackedScene>("res://Content/Entities/Projectiles/MissileFriendly.tscn");

        Game.CurrentLevel.SpawnProjectile(scene, shooter.LevelRelativePosition + new Vector2(0, -50).Rotated(shooter.Rotation), shooter.Velocity + new Vector2(0, -50).Rotated(shooter.Rotation), shooter.Rotation, 50);
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
