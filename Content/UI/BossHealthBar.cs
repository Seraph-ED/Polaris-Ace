using Godot;
using System;

public class BossHealthBar : Control
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public Character AttachedBoss;

    public int Index;

    public int BaseY = 65;

    public int a = 32;
    public override void _Ready()
    {

    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        
        
        
        if (Game.CurrentLevel==null||!IsInstanceValid(AttachedBoss) || AttachedBoss.IsQueuedForDeletion() || AttachedBoss == null||Index>=Game.CurrentLevel.BossHealthBars.Count)
        {
            if (AttachedBoss != null)
            {
                AttachedBoss = null;
            }

            QueueFree();
            return;
        }

        ProgressBar bar = (GetNode("Healthbar") as ProgressBar);//.Value

        bar.RectPosition = new Vector2((1920 - bar.RectSize.x) / 2, BaseY + (Index * a));
        bar.Value = AttachedBoss.HandleBossBarValues();



    }
}
