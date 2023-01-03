using Godot;
using System;

public class BombExplosion : Projectile
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        // Lifespan = 3.0f;
    }

    public override void OnHit(Node body)
    {
        if (body is Character)
        {
            if ((body as Character).DealDamage(Damage))
            {
               // DestroyProjectile();
            }


        }



    }

    public void PlaceExplosion(Vector2 position, Vector2? scale = null)
    {
        Vector2 scalereal = Vector2.One;

        if (scale != null)
        {
            scalereal = scale.Value;
        }


        var scene = GD.Load<PackedScene>("res://Content/VFX/Explosion.tscn");

        Explosion instance = scene.Instance() as Explosion;
        GetParent().AddChild(instance);
        instance.GlobalPosition = position;
        instance.Scale = Scale;
    }


    public override void OnDestroyed()
    {

        /*
        for (int i = 0; i < 9; i++)
        {
            float angle = Mathf.Pi * 2f / 9f * i;
            Vector2 offset = Vector2.Right.Rotated(angle) * 60;
            PlaceExplosion(GlobalPosition, Vector2.One * 0.9f);
        }
        PlaceExplosion(GlobalPosition, Vector2.One * 3);*/
    }

    public override void Behavior(float delta)
    {
        Velocity = Vector2.Zero;
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
