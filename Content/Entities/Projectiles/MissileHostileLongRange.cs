using Content;
using Godot;
using System;

public class MissileHostileLongRange : Projectile
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public Character target = null;

    public Character LaunchPlatform = null;

    public float SeekerAngleDegrees = 180;

    public int State = 0;

    public float Timer = 0;

    public float SeekerRange = 1000000;


    public override void _Ready()
    {

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


        //  // Called every frame. 'delta' is the elapsed time since the previous frame.
        //  public override void _Process(float delta)
        //  {
        //      
        //  }
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

        if (target == null || !IsInstanceValid(target))
        {
            FindTarget();
        }else if (State == 0)
        {
            if (target == null || !IsInstanceValid(target))
            {
                return;
            }
             (target as Player).IncomingProjectileQueue.Add(this);
            Velocity *= 0.97f;
            (GetNode("Exhaust") as Particles2D).Visible = false;
            

            if (Timer > 1.1f)
            {
                State = 1;
                Timer = 0;
                Velocity *= 0f;
            }


        }
        else if (State>0)
        {
            if (target == null || !IsInstanceValid(target) || Game.CurrentLevel == null)
            {
                return;
            }
             (target as Player).IncomingProjectileQueue.Add(this);
            Vector2 relative = (target.LevelRelativePosition - LevelRelativePosition);

            float ProgressiveSpeedValue = Mathf.Clamp(Timer / 8f, 0f, 1f);

            float minspeed = Utils.Lerp(38, 46, ProgressiveSpeedValue);
            float maxspeed = Utils.Lerp(38, 56, ProgressiveSpeedValue);

            float adjustedSeekerAngle = Utils.Lerp(120, 30, Mathf.Pow(ProgressiveSpeedValue, 0.4f));


            if ((relative.Length() < SeekerRange) && Mathf.Abs(Velocity.AngleTo(relative)) < Mathf.Deg2Rad(adjustedSeekerAngle))
            {

               

                Vector2 toTarget = relative;
                toTarget += (target.Velocity * 3 * (toTarget.Length() / SeekerRange));

                toTarget = toTarget.Normalized();
                toTarget *= maxspeed;
                if (!(target as Character).MissileJamming)
                {
                    Velocity = Utils.VecLerp(Velocity, toTarget, 0.005f + (Mathf.Pow(ProgressiveSpeedValue, 4f) * 0.35f));
                }
            }


             (GetNode("Exhaust") as Particles2D).Visible = true;


            if (minspeed > maxspeed)
            {
                minspeed = maxspeed;
            }
            //Velocity = Velocity.Normalized() * 41f;

            if (Velocity.Length() > maxspeed)
            {
                Velocity = Velocity.Normalized() * maxspeed;
            }
            else if (Velocity.Length() < minspeed)
            {
                Velocity = Velocity.Normalized() * minspeed;
            }


        }

        
       


        Rotation = Velocity.Angle();

        Timer += delta;
    }


}
