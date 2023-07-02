using Godot;
using System;
using Content;
using System.IO;
using System.Collections.Generic;

public class Player : Character
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.


    public float CurrentVelRotation = 0;

    //public float Tar

    public float CurrentSpeed = 12;

    public float TargetSpeed = 12;

    [Export]
    public float Acceleration = 0.4f;

    [Export]
    public float Braking = 0.8f;

    [Export]
    public float MinimumSpeed = 12;

    [Export]
    public float MaximumSpeed = 36;

    [Export]
    public float MaxRotSpeedDegrees = 4;

    [Export]
    public float BulletDamage = 12;

    [Export]
    public int CollisionDamage = 50;


    public float RWRWarning = 0;

    public bool ShowCrosshair = false;

    public float WeaponUseTimer = 0f;

    public float DefenseUseTimer = 0f;

    public float AccelMultiplier = 1.0f;

    public float BrakeMultiplier = 1.0f;

    public float BrakeTurnMultiplier = 1.0f;

    public float MinspeedMultiplier = 1.0f;

    public float MaxSpeedMultiplier = 1.0f;

    public float WeaponCooldownMultiplier = 1.0f;

    public float DefenseCooldownMultiplier = 1.0f;

    public float TurnSpeedMultiplier = 1.0f;

    public List<Projectile> IncomingProjectileQueue = new List<Projectile>();

    public List<Projectile> IncomingProjectiles = new List<Projectile>();

    public List<Vector3> RWRSources = new List<Vector3>();



    Vector2 targetVel = Vector2.Zero;

    

    public override void OnReady()
    {
        CurrentVelRotation = Rotation;

        TargetSpeed = 2;

        CurrentSpeed = 2;

        SetProcess(true);
        SetPhysicsProcess(true);
        //GD.Print(GetPath());
    }

    public void Start()
    {
       // SetProcess(true);
        //SetPhysicsProcess(true);
    }

    public override void Kill()
    {
        SetProcess(false);
        SetPhysicsProcess(false);
        Visible = false;
        if (GetParent().HasNode("Music"))
        {
            (GetParent().GetNode("Music") as AudioStreamPlayer).Playing = false;
        }

        if (Game.CurrentLevel.Won)
        {
            return;
        }

        Game.CurrentLevel.Lost = true;


        ((Control)GetNode("/root/Game/Screens/LoseScreen")).Visible = true;
        ((Control)GetNode("/root/Game/GameUI/GameHUD")).Visible = false;
        GetParent().SetProcess(false);
        GetParent().SetPhysicsProcess(false);
        MissionKill = true;



    }

    public void ResetMultipliers()
    {
        AccelMultiplier = 1.0f;
        BrakeMultiplier = 1.0f;
        WeaponCooldownMultiplier = 1.0f;
        DefenseCooldownMultiplier = 1.0f;
        MaxSpeedMultiplier = 1.0f;
        MinspeedMultiplier = 1.0f;
        BrakeTurnMultiplier = 1.0f;
        TurnSpeedMultiplier = 1.0f;
        

    }

    public void ApplyIncomingProjectiles()
    {
        IncomingProjectiles.Clear();

        for (int i = 0; i < IncomingProjectileQueue.Count; ++i)
        {
            Projectile p = IncomingProjectileQueue[i];

            if (!IsInstanceValid(p) || p.IsQueuedForDeletion())
            {
                continue;
            }
            
            //if(!IncomingProjectiles.Contains())

            IncomingProjectiles.Add(IncomingProjectileQueue[i]);
        }

        IncomingProjectileQueue.Clear();


    }

    public override void Behavior(float delta)
    {
        ApplyIncomingProjectiles();
        


        ResetMultipliers();

        float adjustedAccel = Acceleration;

        float adjustedBrake = Braking;

        float adjustedBrakeTurn = Braking * 0.5f;

        float adjustedMinSpeed = MinimumSpeed;

        float adjustedMaxSpeed = MaximumSpeed;

        float adjustedTurningSpeedDegrees = MaxRotSpeedDegrees;

        if (EquipmentHandler.Instance.equippedComponent != null)
        {
            EquipmentHandler.Instance.equippedComponent.PassiveEffect(this);
        }


        adjustedAccel *= AccelMultiplier;
        adjustedBrake *= BrakeMultiplier;
        adjustedBrakeTurn *= BrakeTurnMultiplier;
        adjustedMinSpeed *= MinspeedMultiplier;
        adjustedMaxSpeed *= MaxSpeedMultiplier;
        adjustedTurningSpeedDegrees *= TurnSpeedMultiplier;


        float proportionOfMaxSpeed = (CurrentSpeed - adjustedMinSpeed) / (adjustedMaxSpeed - adjustedMinSpeed);



        float rotationSpeed = Utils.Lerp(adjustedTurningSpeedDegrees, adjustedTurningSpeedDegrees / 2f, proportionOfMaxSpeed);
        float rotationSpeedExtra = Utils.Lerp(adjustedTurningSpeedDegrees, adjustedTurningSpeedDegrees * 1.3f, proportionOfMaxSpeed);


        if (Input.IsActionPressed("ThrottleDown")&&!Input.IsActionPressed("ThrottleUp"))
        {
            rotationSpeed = rotationSpeedExtra;
        }

        if (Input.IsActionPressed("TurnLeft"))
        {
            RotationDegrees -= rotationSpeed;
        }
        else if (Input.IsActionPressed("TurnRight"))
        {
            RotationDegrees += rotationSpeed;
        }
        else
        {

        }

        if (Input.IsActionPressed("ThrottleUp"))
        {
            TargetSpeed += adjustedAccel;
        }
        else if (Input.IsActionPressed("ThrottleDown"))
        {

            if (Input.IsActionPressed("TurnLeft") || Input.IsActionPressed("TurnRight"))
            {
                TargetSpeed -= adjustedBrakeTurn;
            }
            else
            {
                TargetSpeed -= adjustedBrake;
            }


        }
        else
        {

        }
        TargetSpeed = (float)Utils.Clamp(TargetSpeed, MinimumSpeed, MaximumSpeed);


        CurrentSpeed = Utils.Lerp(CurrentSpeed, TargetSpeed, 0.06f);


        targetVel = Vector2.Up.Rotated(Rotation) * TargetSpeed;


        //Timer t = GetChild<Timer>(0);

        if (Input.IsActionPressed("ShootGun") && (GetNode("ReloadTimer") as Timer).TimeLeft == 0)
        {
            (GetNode("ReloadTimer") as Timer).Start(0.06f);
            SpawnBullet();
        }

        if ( (GetNode("MissileTimer") as Timer).TimeLeft == 0&& EquipmentHandler.Instance.equippedWeapon != null)
        {
           
            
            
            
                bool weaponLaunchInputRecieved = false;

                switch (EquipmentHandler.Instance.equippedWeapon.UseStyle)
                {
                    default:
                        weaponLaunchInputRecieved = Input.IsActionJustPressed("ShootMissile");
                        break;
                    case EquipmentUseStyle.PressAndRelease:
                        weaponLaunchInputRecieved = Input.IsActionJustReleased("ShootMissile");
                        ShowCrosshair = Input.IsActionPressed("ShootMissile");
                        break;
                }

                if (weaponLaunchInputRecieved)
                {
                    EquipmentHandler.Instance.equippedWeapon.Shoot(this);
                    WeaponUseTimer = EquipmentHandler.Instance.equippedWeapon.UseTime;
                    (GetNode("MissileTimer") as Timer).Start((EquipmentHandler.Instance.equippedWeapon.CooldownSeconds * WeaponCooldownMultiplier) + EquipmentHandler.Instance.equippedWeapon.UseTime);
                }
        }
        else
        {
            ShowCrosshair = false;
        }

        if (Input.IsActionPressed("Flares") && (GetNode("DefenseTimer") as Timer).TimeLeft == 0)
        {
            if (EquipmentHandler.Instance.equippedDefense != null)
            {
                EquipmentHandler.Instance.equippedDefense.Activate(this);
                DefenseUseTimer = EquipmentHandler.Instance.equippedDefense.UseTime;
                (GetNode("DefenseTimer") as Timer).Start((EquipmentHandler.Instance.equippedDefense.CooldownSeconds * DefenseCooldownMultiplier)+ EquipmentHandler.Instance.equippedDefense.UseTime);
                //SpawnMissiles();


            }
        }


        Velocity = Utils.VecLerp(Velocity, targetVel, 0.25f);

        if (WeaponUseTimer > 0)
        {
            if (EquipmentHandler.Instance.equippedWeapon != null)
            {
                EquipmentHandler.Instance.equippedWeapon.PassiveEffect(this);
            }
            
            
            WeaponUseTimer -= delta;

            if (WeaponUseTimer <= 0)
            {
                if (EquipmentHandler.Instance.equippedWeapon != null)
                {
                    EquipmentHandler.Instance.equippedWeapon.EndPassiveEffect(this);
                }
            }

        }else if (WeaponUseTimer < 0)
        {
            WeaponUseTimer = 0;
        }

        if (DefenseUseTimer > 0)
        {
            if (EquipmentHandler.Instance.equippedDefense != null)
            {
                EquipmentHandler.Instance.equippedDefense.PassiveEffect(this);
            }
                
            DefenseUseTimer -= delta;

            if (DefenseUseTimer <= 0)
            {
                if (EquipmentHandler.Instance.equippedDefense != null)
                {
                    EquipmentHandler.Instance.equippedDefense.EndPassiveEffect(this);
                    //GD.Print("Passive Effect Ended");
                }
            }
        }
        else if (DefenseUseTimer < 0)
        {
            DefenseUseTimer = 0;
        }

        if (Game.DevMode)
        {
            Health = MaxHealth;
            //InvinTime = 1f;
        }

        if (RWRWarning > 0)
        {
            RWRWarning -= delta;
        }
        else
        {
            RWRWarning = 0;
        }

        

    }

    public override void PostBehavior(float delta)
    {
        //base.PostBehavior(delta);

        if (CollisionData != null)
        {
            //GD.Print("PlayerColliding");

            if(CollisionData.Collider is TerrainElement||CollisionData.Collider is CollideableCharacter)
            {
                HandleTerrainCollision(CollisionData);

            }

            for(int i = RWRSources.Count-1; i >= 0; i--)
            {
                RWRSources[i] = new Vector3(RWRSources[i].x, RWRSources[i].y, RWRSources[i].z - delta);

                if (RWRSources[i].z < 0)
                {
                    RWRSources.RemoveAt(i);
                }
            }

        }
        else
        {
            //GD.Print("PlayerNotColliding");
        }

       

    }

    public void HandleTerrainCollision(KinematicCollision2D collision)
    {
        Velocity = Velocity.Bounce(collision.Normal);
        Rotation = Velocity.Angle()+(Mathf.Pi/2f);
        if (DealDamage(CollisionDamage))
        {
            InvinTime = 0.05f;
        }
        
    }

    public void SpawnBullet()
    {
        var scene = GD.Load<PackedScene>("res://Content/Entities/Projectiles/BulletFriendly.tscn");

        Game.CurrentLevel.SpawnProjectile(scene, LevelRelativePosition + new Vector2(0, -50).Rotated(Rotation), Velocity + new Vector2(0, -80).Rotated(Rotation), Rotation, BulletDamage);

       

        //instance.set
    }

    public void SpawnMissiles()
    {
        var scene = GD.Load<PackedScene>("res://Content/Entities/Projectiles/MissileFriendly.tscn");

        for (int i = 0; i < 1; i++)
        {

            Game.CurrentLevel.SpawnProjectile(scene, LevelRelativePosition + new Vector2(0, -50).Rotated(Rotation), Velocity + new Vector2(0, -50).Rotated(Rotation), Rotation, 50);

            
            
        }

        //instance.set
    }
    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
