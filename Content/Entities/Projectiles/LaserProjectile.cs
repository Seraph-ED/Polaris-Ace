using Godot;
using System;

public class LaserProjectile : Projectile
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public float beamLength = 5000;

    public Character attachedChar = null;

    public bool PlayerHeld = true;

    

    public override void _Ready()
    {
        Lifespan = 5.0f;
    }

    public override void Behavior(float delta)
    {
        base.Behavior(delta);

        if (attachedChar.IsQueuedForDeletion() || !IsInstanceValid(attachedChar))
        {
            attachedChar = null;
            DestroyProjectile();
        }


        ((GetNode("Hitbox") as CollisionShape2D).Shape as RayShape2D).Length = beamLength + 10;

        if (attachedChar != null)
        {
            LevelRelativePosition = attachedChar.LevelRelativePosition;
        }

        if (PlayerHeld)
        {
            Vector2 relativeMousePosition = GetGlobalMousePosition() - GlobalPosition;

            Rotation = relativeMousePosition.Angle();
        }
    }

    public override void OnHit(Node body)
    {
        if (body is Character)
        {
            if ((body as Character).DealDamage(Damage))
            {
                (body as Character).InvinTime += 0.2f;
            }

           

            //DestroyProjectile();
        }



    }
    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
