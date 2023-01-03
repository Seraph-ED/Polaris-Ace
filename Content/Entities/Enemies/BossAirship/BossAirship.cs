using Content;
using Godot;
using ShmupGame.Content.Entities;
using System;

public class BossAirship : Character
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.

    public float FlightSpeed = 28f;

    public bool Vulnerable = false;

    public bool Dead = false;

    public float DeathTimer = 0f;

    Character target = null;

    public bool initialized = false;

    public override void OnReady()
    {
       
    }

    public override void OnActivate()
    {
        target = Game.CurrentLevel.GetNode("Player") as Character;

        LevelRelativePosition = target.LevelRelativePosition + new Vector2(0, 2800);

       // GD.Print("Activaitng boss");

       // GD.Print("target global pos:" + target.GlobalPosition);

        GlobalPosition = target.GlobalPosition;// + new Vector2(0, -2800);

        //GD.Print("global pos:" + GlobalPosition);


        Velocity = new Vector2(0, -28);

    }

    public void FindTarget()
    {

      
    }

    public void CheckVulnerable()
    {
        Node engineContainer = GetNode("Engines");

        bool refreshVulnerability = false;

        for (int i = 0; i < engineContainer.GetChildCount(); i++)
        {
            AirshipEnginePod engine = engineContainer.GetChildOrNull<AirshipEnginePod>(i);

            if (engine != null && !engine.Destroyed)
            {
                refreshVulnerability = true;
            }

        }

        if (refreshVulnerability)
        {
            InvinTime = 0.5f;
            Vulnerable = false;
        }
        else
        {
            Vulnerable = true;
        }
    }


    public override void Kill()
    {
        Dead = true;
        Node turretContainer = GetNode("Turrets");
        for (int i = 0; i < turretContainer.GetChildCount(); i++)
        {
            Node turret = turretContainer.GetChild(i);

            if(!(turret is Node2D))
            {
                turret.QueueFree();
            }
            else
            {
                KillNode(turret as Node2D);
            }

            

        }

    }

    public void KillNode(Node2D nd, float explScale = 1f)
    {
        //isActive = false;
        var scene = GD.Load<PackedScene>("res://Content/VFX/Explosion.tscn");

        Explosion instance = scene.Instance() as Explosion;
        GetParent().AddChild(instance);
        instance.GlobalPosition = nd.GlobalPosition;
        instance.Scale = Vector2.One*explScale;
        nd.QueueFree();
    }

    public void PlaceExplosion(Vector2 position, float explScale = 1f)
    {
        var scene = GD.Load<PackedScene>("res://Content/VFX/Explosion.tscn");

        Explosion instance = scene.Instance() as Explosion;
        GetParent().AddChild(instance);
        instance.GlobalPosition = position;
        instance.Scale = Vector2.One * explScale;
    }

    public override void Behavior(float delta)
    {

        if (!initialized&&target!=null)
        {
            GlobalPosition = target.GlobalPosition + new Vector2(0, 2800);

            initialized = true;
        }
        
        Velocity = Velocity.Normalized() * FlightSpeed;

        Rotation = Velocity.Angle() + (float)Utils.Pi / 2f;

        if (Dead)
        {

            DeathTimer += delta;

            if(DeathTimer%0.16f < delta * 2)
            {
                for(int i = 0; i < ((int)DeathTimer)+1; i++)
                {
                    Vector2 offset = new Vector2((float)GD.RandRange(-500f, 500f),(float)GD.RandRange(-150f, 150f));
                    PlaceExplosion(GlobalPosition + offset);
                }

            }

            if (DeathTimer > 5.3)
            {
                KillNode(this, 20);
            }

            return;
        }

        CheckVulnerable();

        if (target != null)
        {
           // GD.Print("Boss global pos diff:" + (GlobalPosition - target.GlobalPosition));
        }
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
