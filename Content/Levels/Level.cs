using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class Level : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.

    public Player player = null;

    [Export]
    public float TimeLeft = 600;
    [Export]
    public string LevelName = "Mission";

    [Export]
    public bool HasTerrain = false;

    [Export(PropertyHint.MultilineText)]
    public string Description = "Description";


    public Node ProjectileContainer => GetNode("ProjectileContainer");

    public Node SpawnedCharacterContainer => GetNode("SpawnedCharactersContainer");

    public List<Character> Enemies = new List<Character>();

    public List<Character> NonEnemyStructures = new List<Character>();

    public List<BossHealthBar> BossHealthBars = new List<BossHealthBar>();

    public bool Won = false;

    public bool Lost = false;

    public override void _Ready()
    {
        SetProcess(true);
        Enemies = new List<Character>();
        NonEnemyStructures = new List<Character>();
        if (HasNode("Player"))
        {
            player = GetNode("Player") as Player;
        }

        if (SpawnedCharacterContainer == null)
        {
            PlaceSpawnedCharacterContainer();
        }
        
        OnReady();
    }


    public void PlaceSpawnedCharacterContainer()
    {
        var scene = GD.Load<PackedScene>("res://Content/Levels/SpawnedCharactersContainer.tscn");

        Node n = scene.Instance();

        n.Name = "SpawnedCharactersContainer";
        (n as EntityContainer).Active = true;

        this.AddChild(n);
    }

    public virtual void OnReady()
    {

    }

    public void UpdateEnemyList(Node toCheck = null, bool clear = true)
    {
        Node basenode; 
        if(toCheck == null)
        {
            basenode = this;
        }
        else
        {
            basenode = toCheck;
        }

        if (clear)
        {
            Enemies.Clear();
        }
        //GD.Print("Initializing recursive search on node: " + basenode.Name);

        for (int i = 0; i < basenode.GetChildCount(); i++)
        {
            Node n = basenode.GetChild(i);

            if (n.IsQueuedForDeletion()||!IsInstanceValid(n))
            {
                continue;
            }

            //GD.Print(i, ", Node type: ",n.GetType());

            if ((n is Character) && (n as Character).Active)
            {
                if((n as Character).IsEnemy)
                {
                    Enemies.Add(n as Character);
                }
                UpdateEnemyList(n, false);
            }

            if(n is EntityContainer && (n as EntityContainer).Active)
            {
                UpdateEnemyList(n, false);
            }

            


        }
        //GD.Print("NUmber of enemies found: " + Enemies.Count);

    }

    public void UpdateStructureList(Node toCheck = null, bool clear = true)
    {
        Node basenode;
        if (toCheck == null)
        {
            basenode = this;
        }
        else
        {
            basenode = toCheck;
        }

        if (clear)
        {
            NonEnemyStructures.Clear();
        }
        //GD.Print("Initializing recursive search on node: " + basenode.Name);

        for (int i = 0; i < basenode.GetChildCount(); i++)
        {
            Node n = basenode.GetChild(i);

            if (n.IsQueuedForDeletion() || !IsInstanceValid(n))
            {
                continue;
            }

            //GD.Print(i, ", Node type: ",n.GetType());

            if ((n is Character) && (n as Character).IsStructure && (n as Character).Active)
            {
                NonEnemyStructures.Add(n as Character);
                UpdateStructureList(n, false);
            }

            if (n is EntityContainer && (n as EntityContainer).Active)
            {
                UpdateStructureList(n, false);
            }




        }
        //GD.Print("NUmber of enemies found: " + Enemies.Count);

    }

    public virtual bool CheckWinCondition()
    {

        bool haswon = true;

        for(int i = 0; i < Enemies.Count; ++i)
        {
            if (!Enemies[i].MissionKill)
            {
                haswon = false;
            }
        }


        
        
        
        return haswon || Enemies.Count == 0;
    }

    public virtual bool CheckLoseCondition()
    {

        return TimeLeft < 0;
    }

    public int AddBossHealthBar(Character tiedTo = null)
    {
        PackedScene hbar = GD.Load<PackedScene>("res://Content/UI/BossHealthBar.tscn");
        int previousnumHbars = BossHealthBars.Count;
        BossHealthBar healthBar = hbar.Instance() as BossHealthBar;
        GetNode("/root/Game/GameUI").AddChild(healthBar);
        
        healthBar.Index=BossHealthBars.Count;
        healthBar.AttachedBoss = tiedTo;
        BossHealthBars.Add(healthBar);

        return 0;
    }

    public Projectile SpawnProjectile(string projScenePath, Vector2 position, Vector2 velocity, float rotation, float damage)
    {
        var scene = GD.Load<PackedScene>(projScenePath);

        return SpawnProjectile(scene, position, velocity, rotation, damage);
    }

    public Projectile SpawnProjectile(PackedScene projScene, Vector2 position, Vector2 velocity, float rotation, float damage)
    {
        var scene = projScene;

        Node n = scene.Instance();

        if(!(n is Projectile))
        {
            return null;
        }

        Projectile instance = n as Projectile;


        this.ProjectileContainer.AddChild(instance);

        instance.LevelRelativePosition = position;
        instance.Velocity = velocity;
        instance.Rotation = rotation;
        instance.Damage = damage;

        return instance;
    }

    public Character SpawnCharacter(string charScenePath, Vector2 position, Vector2 velocity, float rotation)
    {
        var scene = GD.Load<PackedScene>(charScenePath);

        return SpawnCharacter(scene, position, velocity, rotation);
    }

    public Character SpawnCharacter(PackedScene charScene, Vector2 position, Vector2 velocity, float rotation)
    {
        var scene = charScene;

        Node n = scene.Instance();

        if (!(n is Character))
        {
            return null;
        }

        Character instance = n as Character;

        if (SpawnedCharacterContainer == null)
        {
            GD.PrintErr("Spawned Character Container in level is null");
            return null;
        }

        this.SpawnedCharacterContainer.AddChild(instance);

        instance.LevelRelativePosition = position;
        instance.Velocity = velocity;
        instance.Rotation = rotation;
        

        return instance;
    }


    public void UpdateBossHealthbars()
    {

       // BossHealthBars.Clear();

        for(int i = 0; i < Enemies.Count; ++i)
        {
            Character c = Enemies[i];



            if (!c.IsBoss)
            {
                continue;
            }

            if(BossHealthBars.Where(x=>x.AttachedBoss == c).Count() == 0)
            {
                AddBossHealthBar(c);
            }


        }

    }

    public void Start()
    {
        // SetProcess(true);
    }
    // / Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if (player == null)
        {
            if (HasNode("Player"))
            {
                player = GetNode("Player") as Player;
            }
        }
        
        UpdateEnemyList(null, true);
        UpdateStructureList(null, true);
        UpdateBossHealthbars();

        TimeLeft -= delta;
        

        if (CheckWinCondition())
        {
            Won = true;
            SetProcess(false);
            SetPhysicsProcess(false);
            //Visible = false;
            ((Control)GetNode("/root/Game/Screens/WinScreen")).Visible = true;
            ((Control)GetNode("/root/Game/GameUI/GameHUD")).Visible = false;
            if (HasNode("Music"))
            {
                (GetNode("Music") as AudioStreamPlayer).Playing = false;
            }

        }

        if (CheckLoseCondition())
        {
            
            if (player != null)
            {
                player.Kill();
            }
        }

    }

    public void PlaceExplosion(Vector2 position, float explScale = 1f)
    {
        var scene = GD.Load<PackedScene>("res://Content/VFX/Explosion.tscn");

        Explosion instance = scene.Instance() as Explosion;
        GetParent().AddChild(instance);
        instance.GlobalPosition = position;
        instance.Scale = Vector2.One * explScale;
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);
        UpdateLevel(delta);
    }
    public virtual void UpdateLevel(float delta)
    {

    }
}
