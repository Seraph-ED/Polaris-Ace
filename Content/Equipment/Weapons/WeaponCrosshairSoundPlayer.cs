using Godot;
using System;

public class WeaponCrosshairSoundPlayer : AudioStreamPlayer2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        base._Process(delta);
        
        if ((GetParent() as WeaponCrosshair).WeaponName == EquipmentHandler.Instance.equippedWeapon.DisplayName&& (GetParent().GetParent() as Node2D).Visible)
        {
            VolumeDb = GD.Linear2Db(1) - 20;
        }
        else
        {
            VolumeDb = GD.Linear2Db(0);
        }
    }
}
