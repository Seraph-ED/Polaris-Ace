using Godot;
using System;

public class Flares : DefenseItem
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.

    public override string DisplayName => "Countermeasures";

    public override string Description => "Launches flares and chaff bundles which confuse enemy missiles. Effect lasts for 2 seconds. 10 second cooldown.";

    public override float UseTime => 2.0f;

    public override float CooldownSeconds => 10.0f;
    public override void _Ready()
    {
        
    }

    public void SpawnFlare(Vector2 position, Vector2 shootVel)
    {
        var scene = GD.Load<PackedScene>("res://Content/Entities/Projectiles/Flare.tscn");

        Flare instance = scene.Instance() as Flare;
        Game.CurrentLevel.AddChild(instance);
        instance.LevelRelativePosition =position;// + new Vector2(0, -50).Rotated(Rotation);
        instance.Velocity = shootVel;
        //instance.Rotation = c.Rotation;
        instance.Damage = 0;

        //instance.set
    }

    public override void Activate(Character c)
    {
        for(int i = 0; i < 1; i++)
        {
            SpawnFlare(c.LevelRelativePosition, c.Velocity + new Vector2(40, 0).Rotated(c.Rotation));
            SpawnFlare(c.LevelRelativePosition, c.Velocity + new Vector2(-40, 0).Rotated(c.Rotation));
        }
    }


    public override void PassiveEffect(Character c)
    {
        //base.PassiveEffect(c);

        c.MissileJamming = true;

        if(c is Player)
        {
            
            
            
            int roundedTime = ((int)((c as Player).DefenseUseTimer * 60));

           // GD.Print("Timers: " + ((c as Player).DefenseUseTimer) + ", " + roundedTime) ;

            if (roundedTime % 10 == 0)
            {
                SpawnFlare(c.LevelRelativePosition, c.Velocity + new Vector2(40, 0).Rotated(c.Rotation));
                SpawnFlare(c.LevelRelativePosition, c.Velocity + new Vector2(-40, 0).Rotated(c.Rotation));
            }


        }
    }

    public override void EndPassiveEffect(Character c)
    {
        //base.EndPassiveEffect(c);
        c.MissileJamming = false;
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
