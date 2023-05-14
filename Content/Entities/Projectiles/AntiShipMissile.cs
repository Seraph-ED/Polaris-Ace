using Godot;
using System;

public class AntiShipMissile : Projectile
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.

    [Export]
    public float CruiseSpeed = 60;

    [Export]
    public float InitialSpeed = 30;

    [Export]
    public float Acceleration = 20;

    [Export]
    public float Drag = 30;

    [Export]
    public float DropTime = 1.5f;



    public float Timer = 0;

    public bool EngineActive = false;
    public override void _Ready()
    {
        
    }

    public override void Behavior(float delta)
    {
        Timer += delta;

        if (Timer > DropTime)
        {
            if (!EngineActive)
            {
                ActivateEngine();
            }

            Velocity += Vector2.Right.Rotated(Rotation) * Acceleration * delta;
            if (Velocity.Length() > CruiseSpeed)
            {
                Velocity = Velocity.Normalized() * CruiseSpeed;
            }
        }
        else
        {
            Velocity -= Vector2.Right.Rotated(Rotation) * Drag * delta;
            if (Velocity.Length() < InitialSpeed)
            {
                Velocity = Velocity.Normalized() * InitialSpeed;
            }
        }

    }

    public void ActivateEngine()
    {
        (GetNode("Exhaust") as Particles2D).Emitting = true;
        (GetNode("ShootSound") as AudioStreamPlayer2D).Play();

        EngineActive = true;
    }

    public override void OnDestroyed()
    {
        base.OnDestroyed();
        Game.CurrentLevel.PlaceExplosion(LevelRelativePosition, 6);
    }

    public override void OnHit(Node hitNode)
    {
        if (hitNode is Character)
        {
            if ((hitNode as Character).DealDamage(Damage))
            {
                     DestroyProjectile();
            }


        }

    }
    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
