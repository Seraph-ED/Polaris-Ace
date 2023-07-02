using Godot;
using System;

public class NukeExplosion : Projectile
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public Timer t;

    [Export]
    public float FlashDissipationTime = 1.0f;

    public float DirectionOfBlast = 0;

    public float Alpha1 = 1.0f;

    public override void _Ready()
    {
        t = GetNode<Timer>("BlastTimer");
    }

    public override void Behavior(float delta)
    {
        if(t != null)
        {
            if (t.TimeLeft == 0)
            {
                t.Start(0.05f);

                RadialExplosion(13, 26, DirectionOfBlast);
                DirectionOfBlast += Mathf.Deg2Rad(4.3f);
            }


        }

        if (Alpha1 > 0)
        {
            Alpha1 -= delta * (1f/FlashDissipationTime);
        }

        if (GetNode<Node2D>("Sprite") != null)
        {
            GetNode<Node2D>("Sprite").SelfModulate = new Color(1, 1, 1, Alpha1);
        }


    }

    public void RadialExplosion(int numFragments, float speed, float angleOffset = 0, float dmg = 10)
    {
          var scene = GD.Load<PackedScene>("res://Content/Entities/Projectiles/BulletHostileSlow.tscn");

           // var scene2 = GD.Load<PackedScene>("res://Content/VFX/Explosion.tscn");

            //Explosion instance = scene2.Instance() as Explosion;
            //Game.CurrentLevel.AddChild(instance);
            //instance.Position = LevelRelativePosition;

            for (int i = 0; i < numFragments; i++)
            {
                float angle = Mathf.Pi * 2f / ((float)numFragments) * i;
                angle += angleOffset;
                Game.CurrentLevel.SpawnProjectile(scene, LevelRelativePosition, Velocity + (Vector2.Right * speed).Rotated(angle), angle, dmg);
            }
        
    }

    public override void OnHit(Node body)
    {
        if (body is Character)
        {
            (body as Character).DealDamage(Damage);

            
        }
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
