using Godot;
using Content;
using System;

public class MissileHostileAgile : Projectile
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.

    public Character target;

    public float SeekerAngleDegrees = 500;

    public float SeekerRange = 1000;

    public int State = 0;

    public override void _Ready()
    {
        target = null;
        Lifespan = 7.0f;
    }

    public void FindTarget()
    {

        if (Game.CurrentLevel == null || Game.CurrentLevel.player == null )
        {
            return;
        }
        
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
        instance.Position = LevelRelativePosition;
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
            if (target.MissionKill)
            {
                DestroyProjectile();
            }

            State = Lifespan < 4 && Lifespan > 3.5f ? 1 : 0;


            //Vector2 targetPosition = ();
            Vector2 toTarget = (target.LevelRelativePosition - LevelRelativePosition);
            toTarget += (target.Velocity * 10 * (toTarget.Length() / SeekerRange));

            toTarget = toTarget.Normalized();

            switch (State)
            {
                default:             
                    toTarget *= 2.5f;
                    if ((target as Character).MissileJamming)
                    {
                        toTarget *= -1;
                    }
                   
                    break;

                case 1:
                    toTarget *= 9.5f;
                    
                    break;
            }
            Velocity += toTarget;

            Vector2 relative = (target.Position - Position);

            if ((relative.Length() > SeekerRange) || Mathf.Abs(relative.Angle() + (Mathf.Pi / 2f) - (Rotation)) > Mathf.Deg2Rad(SeekerAngleDegrees))
            {
                target = null;

            }


        }

        Velocity = Velocity.Normalized() * 41f;
        /*
        if (Velocity.Length() > 34f)
        {
            Velocity = Velocity.Normalized() * 34;
        }else if (Velocity.Length() < 20)
        {
            Velocity = Velocity.Normalized() * 20;
        }
        */

        Rotation = Velocity.Angle() + (float)Utils.Pi / 2f;
    }
}
