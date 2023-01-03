using Content;
using Godot;
using System;
using System.Collections.Generic;

public class MissileFriendly : Projectile
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.

    public List<Character> potentialTargets = new List<Character>();

    public Character target;

    public float SeekerAngleDegrees = 140;

    public float SeekerRange = 1400;

    public override void _Ready()
    {
        target = null;
        Lifespan = 10.0f;
    }

    public void AddTarget(Node body)
    {
        if(body is Character && (body as Character).IsEnemy && (body as Character).InvinTime<=0 && (body as Character).Active)
        {
            potentialTargets.Add(body as Character);
        }

       
    }

    public void RemoveTarget(Node body)
    {
        if(potentialTargets.Contains(body as Character))
        {
            potentialTargets.Remove(body as Character);
        }
    }



    public void FindTarget()
    {

        if (Game.CurrentLevel == null || Game.CurrentLevel.Enemies == null)
        {
            return;
        }

        Character closestTarget = null;
        Vector2 currentDist = new Vector2(SeekerRange, SeekerRange);

        for(int i = 0; i < potentialTargets.Count; ++i)
        {

            Character c = potentialTargets[i];
            if (!IsInstanceValid(c)||c.IsQueuedForDeletion())
            {
                potentialTargets.Remove(c);
                return;
            }

            Vector2 relative = (c.LevelRelativePosition - LevelRelativePosition);

            if ((closestTarget==null||relative.Length() < currentDist.Length()))
            {
                closestTarget = c;
                currentDist = (c.LevelRelativePosition - LevelRelativePosition);

            }


        }

        if(closestTarget != null)
        {
            target = closestTarget;
        }
    }

    public override void OnHit(Node body)
    {
        if (body is Character)
        {
            if ((body as Character).DealDamage(Damage))
            {
                DestroyProjectile();
            }


        }



    }
    public override void OnDestroyed()
    {
        var scene = GD.Load<PackedScene>("res://Content/VFX/Explosion.tscn");

        Explosion instance = scene.Instance() as Explosion;
        Game.CurrentLevel.AddChild(instance);
        instance.Position = Position;
    }

    public override void Behavior(float delta)
    {
        //base.AI(delta);
        FindTarget();
        if (target == null||!IsInstanceValid(target))
        {
            //FindTarget();
        }
        else
        {
            Vector2 toTarget = (target.LevelRelativePosition - LevelRelativePosition);
            toTarget += (target.Velocity * 30 * (toTarget.Length() / SeekerRange));

            toTarget = toTarget.Normalized();
            toTarget *= 5.5f;

            if((target as Character).MissileJamming)
            {
                toTarget *= -1;
            }

            Velocity += toTarget;
            


            // }

            Vector2 relative = (target.LevelRelativePosition - LevelRelativePosition);

            if ( (relative.Length() > SeekerRange)||!potentialTargets.Contains(target))
            {
                target = null;

            }




        }

        if (Velocity.Length() > 43f)
        {
            Velocity = Velocity.Normalized() * 43;
        }else if (Velocity.Length() < 28)
        {
            Velocity = Velocity.Normalized() * 28;
        }


        Rotation = Velocity.Angle() + (float)Utils.Pi / 2f;
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
