using Godot;
using System;

public class ImprovedCooling : ComponentItem
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override string DisplayName => "Enhanced Cooling";
    public override string Description => "Better heat management allows for quicker reloading of secondary weapons";
    public override void _Ready()
    {
        
    }

    public override void PassiveEffect(Character c)
    {
        if (!(c is Player))
        {
            return;
        }

        Player player = (Player)c;

        player.WeaponCooldownMultiplier = 0.5f;
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
