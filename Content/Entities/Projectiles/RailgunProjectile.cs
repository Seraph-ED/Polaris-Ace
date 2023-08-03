using Godot;
using System;

public class RailgunProjectile : Projectile
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public PackedScene BulletScene;

    [Export] public float ShockBulletFreq1 = 300;
    [Export] public float ShockBulletFreq2 = 0;
    [Export] public float ShockBulletSpeed1 = 20;
    [Export] public float ShockBulletSpeed2 = 300;

    [Export] public float LingeringBulletFreq = 80;
    [Export] public float DegreesPerLingeringBullet = 18;
    [Export] public float LingeringBulletAmplitude = 50;

    [Export] public float ShockwaveBulletDamage = 10;
    [Export] public float LingeringBulletDamage = 5;


    public float DistTraveled1 = 0;
    public float DistTraveled2 = 0;
    public float DistTraveled3 = 0;

    public float BulletSinAngle = 0;

    public override void _Ready()
    {
        base._Ready();
        
        
        BulletScene = GD.Load<PackedScene>("res://Content/Entities/Projectiles/BulletHostileSlow.tscn");
    }

  
    public override void Behavior(float delta)
    {
        base.Behavior(delta);
        float vel = Velocity.Length() * 60 * delta;

        if (BulletScene == null)
        {

            BulletScene = GD.Load<PackedScene>("res://Content/Entities/Projectiles/BulletHostileSlow.tscn");
        }

        DistTraveled1 += vel;
        DistTraveled2 += vel;
        DistTraveled3 += vel;

        if (ShockBulletSpeed1 > 0 && ShockBulletFreq1 > 0&&DistTraveled1>ShockBulletFreq1)
        {
            DistTraveled1 = 0;
            SpawnShockWaveBullets(ShockBulletSpeed1);
        }

        if (ShockBulletSpeed2 > 0 && ShockBulletFreq2 > 0 && DistTraveled2 > ShockBulletFreq2)
        {
            DistTraveled2 = 0;
            SpawnShockWaveBullets(ShockBulletSpeed2);
        }

        if (LingeringBulletFreq > 0&& DistTraveled3 > LingeringBulletFreq)
        {
            DistTraveled3 = 0;
            BulletSinAngle += Mathf.Deg2Rad(DegreesPerLingeringBullet);
            SpawnLingeringBullets(BulletSinAngle, LingeringBulletAmplitude, 3);
        }

        Rotation = Velocity.Angle();

    }

    public void SpawnShockWaveBullets(float speed)
    {
        if (BulletScene == null)
        {
            GD.Print("bullet null");
            return;
        }

        float angle = Mathf.Pi / 2f;

        Game.CurrentLevel.SpawnProjectile(BulletScene, LevelRelativePosition, (Vector2.Right * speed).Rotated(Rotation+angle), angle, ShockwaveBulletDamage);
        Game.CurrentLevel.SpawnProjectile(BulletScene, LevelRelativePosition, (Vector2.Right * speed).Rotated(Rotation-angle), -angle, ShockwaveBulletDamage);

    }

    public void SpawnLingeringBullets(float theta, float amplitude, int pattern = 0)//0 = 1 wave, no straight, 1 = 1 wave, straight, 2 = 2 wave, no straight, 3 = 2 wave, straight
    {
        Vector2 offset = new Vector2(0, amplitude * Mathf.Sin(theta)).Rotated(Rotation);


        Game.CurrentLevel.SpawnProjectile(BulletScene, LevelRelativePosition + offset, Vector2.Zero, 0, LingeringBulletDamage);

        if(pattern == 1 || pattern == 3)
        {
            Game.CurrentLevel.SpawnProjectile(BulletScene, LevelRelativePosition, Vector2.Zero, 0, LingeringBulletDamage);
        }
        if (pattern == 2 || pattern == 3)
        {
            Game.CurrentLevel.SpawnProjectile(BulletScene, LevelRelativePosition - offset, Vector2.Zero, 0, LingeringBulletDamage);
        }
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
