using Godot;
using System;

public class BulletHostileSlow : Projectile
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
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

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
