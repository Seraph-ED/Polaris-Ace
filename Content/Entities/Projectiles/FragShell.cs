using Godot;
using System;


public class FragShell : Projectile
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    [Export]
    public int Fragments = 8;

    [Export]
    public float FragmentVel = 10;

    public bool Fragment = true;

    public override void _Ready()
    {
        
    }

    public override void OnDestroyed()
    {
        if (Fragment)
        {
            var scene = GD.Load<PackedScene>("res://Content/Entities/Projectiles/BulletHostileSlow.tscn");

           

            //Explosion instance = scene2.Instance() as Explosion;
            //Game.CurrentLevel.AddChild(instance);
            //instance.Position = LevelRelativePosition;

            Game.CurrentLevel.PlaceExplosion(LevelRelativePosition, 0.6f);

            for (int i = 0; i < Fragments; i++)
            {
                float angle = Mathf.Pi * 2f / Fragments * i;

                Vector2 fragVec = new Vector2(0, FragmentVel * Mathf.Sin(angle)).Rotated(Velocity.Angle());

                Game.CurrentLevel.SpawnProjectile(scene, LevelRelativePosition, Velocity + fragVec, angle, Damage / Fragments) ;
            }
        }

       
    }

    public override void OnHit(Node body)
    {
        if (body is Character)
        {
            (body as Character).DealDamage(Damage);

            Fragment = false;

            DestroyProjectile();
        }



    }


    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
