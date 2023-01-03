using Godot;
using System;

public class AirshipEnginePod : Character
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public bool Destroyed = false;

    public override void _Ready()
    {
        
    }

    public override void Kill()
    {
        if (Destroyed)
        {
            return;
        }
        
        var scene = GD.Load<PackedScene>("res://Content/VFX/Explosion.tscn");
        Explosion instance = scene.Instance() as Explosion;
        GetParent().AddChild(instance);
        instance.Position = Position;
        Destroyed = true;
        (GetNode("Sprite") as AnimatedSprite).Frame = 1;
        //Colli
        Health = 1;
        (GetNode("Hitbox") as CollisionPolygon2D).SetDeferred("disabled", true); ;
        (GetNode("DamageFlames") as Node2D).Visible = true;
        (GetNode("Exhausts") as Node2D).Visible = false;
        //SetCollisionLayerBit(3, false);
    }

    public override void Behavior(float delta)
    {
       // base.Behavior(delta);

        IsEnemy = !Destroyed;
        if (Destroyed)
        {
           // (GetNode("Sprite") as AnimatedSprite).Frame = 1;
            //Colli
           // SetCollisionLayerBit(3, false);
           // (GetNode("Hitbox") as CollisionPolygon2D).Disabled = true;
        }

    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
