using Godot;
using System;

public class WeaponCrosshair : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public Player player;
    [Export]
    public string WeaponName = "";

    public override void _Ready()
    {
        
    }

    public override void _Process(float delta)
    {
        base._Process(delta);
        if (GetParent() is WeaponCrosshairContainer)
        {
            Visible = EquipmentHandler.Instance.equippedWeapon.DisplayName == WeaponName;
            //GD.Print("Visibility check for weapon: " + WeaponName +", " + (EquipmentHandler.Instance.equippedWeapon.DisplayName == WeaponName));

        }
        else
        {
            Visible = false;
        }


    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
