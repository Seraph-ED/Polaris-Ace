using Godot;
using ShmupGame.Content.Entities;
using System;
using System.Text;

public class Character : KinematicBody2D, IActivateable
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.

    public Vector2 Velocity = Vector2.Zero;

    public Vector2 LevelRelativePosition {

        get => Game.CurrentLevel != null ? GlobalPosition - Game.CurrentLevel.GlobalPosition : Vector2.Zero;
        set {
            if(Game.CurrentLevel != null)
            {
                GlobalPosition = Game.CurrentLevel.GlobalPosition + value;
            }

        }
    }

    public float HitFlash = 0;

    public bool InHitFlash = false;

    public Color HitFlashModulateStorage = new Color(255, 255, 255);

    [Export]
    public float Health = 100;
    
    public float MaxHealth = 100;

    [Export]
    public bool IsEnemy = false;
    [Export]
    public bool IsStructure = false;
    [Export]
    public bool Active = true;
    [Export]
    public bool IsBoss = false;

    public bool MissileJamming = false;
    
    [Export]
    public bool MissionKill = false;

    public float InvinTime = 0;

    public KinematicCollision2D CollisionData = null;


    public bool HasBeenActive = false;

    public bool IsActive()
    {
        return Active;
    }

    public virtual bool DealDamage(float damage, int specialCircumstances = 0)
    {
        if (InvinTime > 0||MissionKill)
        {
            return false;
        }
        if (!InHitFlash&&HitFlash<=0)
        {
            HandleHitFlash();
        }
        
        Health -= damage;

        if(Health <= 0)
        {
            Kill();
        }
        return true;


    }


    public virtual void Behavior(float delta)
    {

    }

    public virtual void PostBehavior(float delta)
    {

    }

    public virtual void OnActivate()
    {

    }
    //public virtual void OnHit(in)

    public virtual void Kill()
    {
        //isActive = false;
        var scene = GD.Load<PackedScene>("res://Content/VFX/Explosion.tscn");

        Explosion instance = scene.Instance() as Explosion;
        Game.CurrentLevel.AddChild(instance);
        instance.GlobalPosition = GlobalPosition;
        QueueFree();
    }

    public override void _Ready()
    {
        OnReady();
        Active = true;
        MaxHealth = Health;
    }

    public virtual void OnReady()
    {
        MissileJamming = false;
    }

    public virtual void HandleHitFlash()
    {
        Node2D spriteNode = null;

        for(int i = 0; i < GetChildCount(); ++i)
        {
            Node childNode = GetChild(i);

            if(childNode is Sprite)
            {
                spriteNode = (childNode as Sprite);
                break;
            }
            else if(childNode is AnimatedSprite)
            {
                spriteNode = (childNode as AnimatedSprite);
                break;
            }
        }

        if (spriteNode != null)
        {
            HitFlashModulateStorage = spriteNode.Modulate;
            spriteNode.Modulate = new Color(255, 0, 0);
            HitFlash = 0.05f;
            InHitFlash = true;
        }
        
    }

    public virtual void HandleHitFlashEnd()
    {
        Node2D spriteNode = null;

        for (int i = 0; i < GetChildCount(); ++i)
        {
            Node childNode = GetChild(i);

            if (childNode is Sprite)
            {
                spriteNode = (childNode as Sprite);
                break;
            }
            else if (childNode is AnimatedSprite)
            {
                spriteNode = (childNode as AnimatedSprite);
                break;
            }
        }

        if (spriteNode != null)
        {
            //HitFlashModulateStorage = spriteNode.Modulate;
            spriteNode.Modulate = HitFlashModulateStorage;
            //HitFlash = 0.05f;
            InHitFlash = false;
        }
    }

    public virtual double HandleBossBarValues()
    {
        return (double)Health / (double)MaxHealth;
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);

        if (Game.CurrentLevel == null||!Active)
        {
            return;
        }


        
        Behavior(delta);
        //LevelRelativePosition = GlobalPosition - Game.CurrentLevel.GlobalPosition;
        CollisionData = MoveAndCollide(Velocity* delta * 60);

        PostBehavior(delta);

        if (InvinTime > 0)
        {
            InvinTime -= delta;
        }
    }

    public override void _Process(float delta)
    {
        
        if (GetParent() is IActivateable)
        {
            Active = (GetParent() as IActivateable).IsActive();
        }

        if (!Active)
        {
            Visible = false;
            SetPhysicsProcess(false);
            HasBeenActive = false;

            //if(child)
            //colli
            return;
        }
        else
        {
            Visible = true;
            SetPhysicsProcess(true);
            if (!HasBeenActive)
            {
                OnActivate();
                HasBeenActive = true;
            }
        }

        if (HitFlash > 0)
        {
            HitFlash -= delta;
        }
        else
        {
            if (InHitFlash)
            {
                HandleHitFlashEnd();
            }
        }
        
    }

    
    //public override po
}
