using Godot;
using System;

public class Level6 : Level
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.

    [Export]
    public int LightningRarity = 120;

    [Export]
    public float ThunderLoudness = 0.6f;

    [Export]
    public float ThunderThreshold = 0.4f;

    [Export]
    public Curve LightningStrengthCurve;

    [Export]
    public float CutsceneLength = 12;

    public float CutsceneTimer = 12;

    public bool Cutscene = true;

    [Export]
    public NodePath LightningStormPath = "";

    public int stormcheck = 10;

    public LightningStormBackground background = null;

    public override void OnReady()
    {
        base.OnReady();
        CutsceneTimer = CutsceneLength;
        Cutscene = true;
    }


    public override bool CheckWinCondition()
    {
        return false;//base.CheckWinCondition();
    }

    public override void UpdateLevel(float delta)
    {
        base.UpdateLevel(delta);


        if (stormcheck > 0)
        {
            Node n = GetNode(LightningStormPath);


            if (n != null && n is LightningStormBackground)
            {
                background = n as LightningStormBackground;
            }

            stormcheck--;
        }

        if (Cutscene)
        {
            CutsceneTimer -= delta;

            if (CutsceneTimer <= 0)
            {
                Cutscene = false;
                StartGameplayAfterCutscene();
            }

            return;
        }

        

        if (background == null) return;

        if(background.LightningStrength<=0)
        {
            if(GD.Randi() % LightningRarity == 0)
            {
                GD.Print("Attempting lightning flash");
                PlaceLightning(background, GD.Randf());
            }

        }

        
        




    }

    public void StartGameplayAfterCutscene()
    {
        (GetNode("Music") as AudioStreamPlayer).Play(0);
        player.Active = true;
        player.Rotation = 0;
        GetNode<EntityContainer>("Wave").Active = true;
        GetNode<Character>("Wave/BossPrototype").LevelRelativePosition = player.LevelRelativePosition + new Vector2(0, -10000);
        GetNode<Character>("Wave/BossPrototype").Velocity = new Vector2(0, 40);
        //GetNode<Character>("Wave/BossPrototype").Rotation = Mathf.Pi;
    }




    private void PlaceLightning(LightningStormBackground bg, float r)
    {
        float a = LightningStrengthCurve.Interpolate(r);
        bg.LightningStrength = a;
        Node n = GetNode("Thunder");
        if (n != null)
        {

            int randa = (int)(GD.Randi() % n.GetChildCount());

            Node b = n.GetChild(randa);

            if (b is AudioStreamPlayer)
            {
                AudioStreamPlayer sound = b as AudioStreamPlayer;

                sound.VolumeDb =GD.Linear2Db(ThunderLoudness*Mathf.Clamp((a - ThunderThreshold) / (1 - ThunderThreshold), 0, 1f));
                sound.Play(0);
            }


        }


    }

    public void PlaceLightningExternal(float r)
    {
        if (background == null)
        {
            return;
        }

        PlaceLightning(background, r);


    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
