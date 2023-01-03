using Godot;
using System;

public class Drones : Weapon
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.

    public override string DisplayName => "Drones";

    public override string Description => "Summons a pair of drones which follow you in formation and add their firepower to yours, 30 second duration, 45 second cooldown";

    public override float UseTime => 30.0f;

    public override float CooldownSeconds => 45.0f;

    public override EquipmentUseStyle UseStyle => EquipmentUseStyle.FirstPress;
    public override void _Ready()
    {
        
    }

    public override void Shoot(Character shooter)
    {
        var scene = GD.Load<PackedScene>("res://Content/Entities/DroneFriendly.tscn");
        Node n = scene.Instance();

        DroneFriendly n2 = n as DroneFriendly;

        Game.CurrentLevel.AddChild(n);
        n2.FollowIndex = 0;
        n2.LevelRelativePosition = shooter.LevelRelativePosition + (new Vector2(0, 2000).Rotated(shooter.Rotation));

        n = scene.Instance();
        
        n2 = n as DroneFriendly;

        Game.CurrentLevel.AddChild(n);
        n2.FollowIndex = 1;
        n2.LevelRelativePosition = shooter.LevelRelativePosition + (new Vector2(0, 2000).Rotated(shooter.Rotation));
        //Game.CurrentLevel.SpawnProjectile(scene, shooter.LevelRelativePosition, shooter.Velocity + (Vector2.Up.Rotated(shooter.Rotation) * 6), shooter.Rotation - (Mathf.Pi/2f), 50);
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
