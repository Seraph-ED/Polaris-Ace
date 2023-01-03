using Godot;
using System;

public class DefenseItem : EquipmentItem
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public virtual float CooldownSeconds => 3.0f;

    public virtual float UseTime => 0f;

    public override void _Ready()
    {
        
    }

    public virtual void Activate(Character activated)
    {

    }

    public static DefenseItem GetDefense(string name)
    {
        Node n = EquipmentHandler.Instance.GetNode("Defenses/" + name);

        if (n is DefenseItem)
        {
            return (DefenseItem)n;
        }
        else
        {
            return null;
        }


    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
