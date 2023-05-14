using Godot;
using System;

public class AntiShipMissileEquipment : Weapon
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.

    public override string DisplayName => "Anti Ship Missile";

    public override string Description => "A large, high speed projectile. Only effective against ships, but deals extreme damage. 10 second cooldown.";

    public override float CooldownSeconds => 10;

    public override EquipmentUseStyle UseStyle => EquipmentUseStyle.PressAndRelease;
    public override void _Ready()
    {
        
    }

    

    public override void Shoot(Character shooter)
    {
        var scene = GD.Load<PackedScene>("res://Content/Entities/Projectiles/AntiShipMissile.tscn");



        Game.CurrentLevel.SpawnProjectile(scene, shooter.LevelRelativePosition, shooter.Velocity + (Vector2.Up.Rotated(shooter.Rotation) * 6), shooter.Rotation - (Mathf.Pi/2f), 1500);
    }




    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
