using Godot;
using System;

public class NoComponent : ComponentItem
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.

    public override string DisplayName => "Unequip Component";

    public override string Description => "Remove the equipped component from your airframe.\nThe removal of weight penalties associated with installed components slightly increases acceleration";
    public override void _Ready()
    {
        
    }

    public override void PassiveEffect(Character c)
    {
        if(!(c is Player))
        {
            return;
        }
        
        Player player = (Player)c;

        player.AccelMultiplier += 0.1f;

        
        base.PassiveEffect(c);
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
