using Godot;
using System;

public class ComponentItem : EquipmentItem
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public virtual float CooldownSeconds => 0f;

    public virtual float UseTime => 0f;

    public override void _Ready()
    {

    }

    public virtual void ActiveAbility(Character activated)
    {

    }

    public static ComponentItem GetComponent(string name)
    {
        Node n = EquipmentHandler.Instance.GetNode("Components/" + name);

        if (n is ComponentItem)
        {
            return (ComponentItem)n;
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
