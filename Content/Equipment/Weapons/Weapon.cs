using Godot;
using System;

public class Weapon : EquipmentItem
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public virtual float CooldownSeconds => 3.0f;

    public virtual float UseTime => 0f;

    
    public virtual EquipmentUseStyle UseStyle => EquipmentUseStyle.FirstPress;

    public static Weapon GetWeapon(string name)
    {
        Node n = EquipmentHandler.Instance.GetNode("Weapons/" + name);

        if(n is Weapon)
        {
            return (Weapon)n;
        }
        else
        {
            return null;
        }
            

    }

    public virtual void Shoot(Character shooter)
    {

    }


    public override void _Ready()
    {
        
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
