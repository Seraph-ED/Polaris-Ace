using Godot;
using System;
using System.Collections.Generic;

public class Projectile : Area2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public Vector2 Velocity = Vector2.Zero;

    public Vector2 LevelRelativePosition
    {

        get => GlobalPosition - Game.CurrentLevel.GlobalPosition;
        set => GlobalPosition = Game.CurrentLevel.GlobalPosition + value;
    }

    public List<Character> collidingNodes = new List<Character>();

    //[Export]
    public float Damage = 0;
    [Export]
    public float Lifespan = 3.0f;


    public override void _Ready()
    {
        //Velocity = Vector2.Zero;


    }

    public void CheckHits()
    {

        for (int i = collidingNodes.Count-1; i >= 0; --i)
        {
            Character hitNode = collidingNodes[i];

            if (hitNode.IsQueuedForDeletion() || !IsInstanceValid(hitNode))
            {
                collidingNodes.RemoveAt(i);
                
                //continue;
            }

            
        }

        for ( int i = 0; i < collidingNodes.Count; ++i)
        {
            Character hitNode = collidingNodes[i];
            
            if(hitNode.IsQueuedForDeletion()||!IsInstanceValid(hitNode))
            {
                continue;
            }
            
            if ( !(hitNode is Character) || !(hitNode as Character).Active || (hitNode as Character).InvinTime > 0)
            {
                continue;
            }

            OnHit(hitNode);
        }
    }


    public virtual void OnHit(Node hitNode)
    {

    }

    public void SetAsColliding(Node hitNode)
    {
        if (IsInstanceValid(hitNode) && !hitNode.IsQueuedForDeletion() && hitNode is Character && (hitNode as Character).Active)
        {
            collidingNodes.Add(hitNode as Character);
        }
    }

    public void UnsetAsColliding(Node hitNode)
    {
        Character a = hitNode as Character;
        
        if (!collidingNodes.Contains(a))
        {
            return;
        }

        //int ind = collidingNodes.IndexOf(hitNode as Character);



        collidingNodes.Remove(a);

    }

    public virtual void Behavior(float delta)
    {

    }

    public virtual void OnDestroyed()
    {

    }

    public void DestroyProjectile()
    {
        OnDestroyed();
        QueueFree();
    }

    // public override 

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);

        Behavior(delta);
        CheckHits();

        Position += Velocity * (delta * 60);

        Lifespan -= delta;

        if (Lifespan <= 0)
        {
            DestroyProjectile();
        }
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
