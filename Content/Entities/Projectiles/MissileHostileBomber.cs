using Content;
using Godot;
using System;

public class MissileHostileBomber : Projectile
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.

    public Character target;

    public float SeekerAngleDegrees = 500;

    public float SeekerRange = 1000;

    public override void _Ready()
    {
        target = null;
        Lifespan = 6.0f;
    }

    public void FindTarget()
    {

        if (Game.CurrentLevel == null || Game.CurrentLevel.player == null)
        {
            return;
        }
        /*
        Character closestTarget = null;
        Vector2 currentDist = Vector2.Inf;

        for(int i = 0; i < Game.CurrentLevel.Enemies.Count; ++i)
        {

            Character c = Game.CurrentLevel.Enemies[i];
            if (!IsInstanceValid(c))
            {
                return;
            }

            Vector2 relative = (c.Position - Position);

            if ((closestTarget==null||relative.Length() < currentDist.Length())&&(relative.Length()<SeekerRange)&& Mathf.Abs(relative.Angle()-(Rotation - (Mathf.Pi / 2f)))<Mathf.Deg2Rad(SeekerAngleDegrees))
            {
                closestTarget = c;
                currentDist = (c.Position - Position);

            }


        }*/

        
           target = Game.CurrentLevel.player;
        (target as Player).IncomingProjectileQueue.Add(this);

    }

    public override void OnHit(Node body)
    {
        if (body is Character)
        {
            (body as Character).DealDamage(Damage);

            DestroyProjectile();
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

        if (target == null||!IsInstanceValid(target))
        {
            FindTarget();
        }
        else
        {
            if(target == null || !IsInstanceValid(target))
            {
                return;
            }
            
            Vector2 toTarget = (target.LevelRelativePosition - LevelRelativePosition);
            toTarget += (target.Velocity * 3 * (toTarget.Length() / SeekerRange));

            toTarget = toTarget.Normalized();
            toTarget *= 0.5f;
            if ((target as Character).MissileJamming)
            {
                toTarget *= -1;
            }
            Velocity += toTarget;
            


            // }

            Vector2 relative = (target.Position - Position);

            if ( (relative.Length() > SeekerRange) || Mathf.Abs(relative.Angle() + (Mathf.Pi/2f) - (Rotation)) > Mathf.Deg2Rad(SeekerAngleDegrees))
            {
                target = null;

            }




        }

        float minspeed = Utils.Lerp(5, 30, 1f - ((Lifespan - 4) / 2f));
        float maxspeed = Utils.Lerp(10, 36, 1f - ((Lifespan - 2) / 4f));
        if(minspeed > maxspeed)
        {
            minspeed = maxspeed;
        }
        //Velocity = Velocity.Normalized() * 41f;

        if (Velocity.Length() > 34f)
        {
            Velocity = Velocity.Normalized() * 34;
        }else if (Velocity.Length() < minspeed)
        {
            Velocity = Velocity.Normalized() * minspeed;
        }
        

        Rotation = Velocity.Angle() + (float)Utils.Pi / 2f;
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
