using Godot;
using System;

public class MissileHostileNuke : Projectile
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
    public float FinalSpeed = 40;

    [Export]
    public float Acceleration = 20;

    [Export]
    public float Homing = 2f;

    [Export]
    public float Drag = 30;

    [Export]
    public float Drag2 = 50;

    [Export]
    public float DropTime = 1.5f;


    public int State = 0;


    public float Timer = 0;

    

    public bool EngineActive = false;
   

    public override void Behavior(float delta)
    {
        Timer += delta;

        if (Timer > DropTime)
        {
            if (State==0)
            {
                ActivateEngine();
            }

        }

        if (Lifespan < 0.5f)
        {
            if (State == 0)
            {
                
                ActivateEngine(2);
            }
            if (State != 2)
            {
                GetNode<Node2D>("ProxFuse").Visible = false;
                GetNode<Node2D>("ProxFuse2").Visible = true;
            }
            State = 2;
        }

        switch (State)
        {
            case 0:
                Velocity -= Vector2.Right.Rotated(Rotation) * Drag * delta;
                if (Velocity.Length() < InitialSpeed)
                {
                    Velocity = Velocity.Normalized() * InitialSpeed;
                }
                break;
            case 1:
                Velocity += Vector2.Right.Rotated(Rotation) * Acceleration * delta;

                Player p = Game.CurrentLevel.player;

                Velocity += (p.LevelRelativePosition - LevelRelativePosition).Normalized() * Homing;

                if (Velocity.Length() > CruiseSpeed)
                {
                    Velocity = Velocity.Normalized() * CruiseSpeed;
                }
                break;
            case 2:
                Velocity -= Vector2.Right.Rotated(Rotation) * Drag2 * delta;
                if (Velocity.Length() < FinalSpeed)
                {
                    Velocity = Velocity.Normalized() * FinalSpeed;
                }
                break;
            
        }

        Rotation = Velocity.Angle();

    }

    public void ActivateEngine(int stateset = 1)
    {
        (GetNode("Exhaust") as Particles2D).Emitting = true;
        (GetNode("ShootSound") as AudioStreamPlayer2D).Play();

        State = stateset;
    }

    public override void OnDestroyed()
    {
        base.OnDestroyed();

        string s = "res://Content/Entities/Projectiles/NukeExplosion.tscn";


        Game.CurrentLevel.SpawnProjectile(s, LevelRelativePosition, Vector2.Zero, 0, Damage);


    }

    public override void OnHit(Node hitNode)
    {
        
        //GD.Print("Collision");
        
            if (Lifespan > 1)
            {
                Lifespan = 1f;
                GetNode<Node2D>("ProxFuse").Visible = false;
                GetNode<Node2D>("ProxFuse2").Visible = true;
                State = 2;
            }


        

    }



    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
