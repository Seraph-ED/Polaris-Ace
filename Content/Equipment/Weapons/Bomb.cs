using Godot;
using System;

public class Bomb : Weapon
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.

    public override string DisplayName => "Bomb";

    public override string Description => "A projectile that explodes in a large radius and deals more damage on direct hits, can only hit grounded targets. 50 Damage, 50 Explosion Damage, 3 second cooldown.";

    public override EquipmentUseStyle UseStyle => EquipmentUseStyle.PressAndRelease;
    public override void _Ready()
    {
        
    }

    public override void Shoot(Character shooter)
    {
        var scene = GD.Load<PackedScene>("res://Content/Entities/Projectiles/BombFriendly.tscn");



        Game.CurrentLevel.SpawnProjectile(scene, shooter.LevelRelativePosition, shooter.Velocity + (Vector2.Up.Rotated(shooter.Rotation) * 6), shooter.Rotation - (Mathf.Pi/2f), 50);
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
