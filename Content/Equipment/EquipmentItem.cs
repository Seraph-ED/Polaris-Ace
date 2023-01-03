using Godot;
using System;


public enum EquipmentUseStyle : int
{
    FirstPress = 0,
    PressAndRelease = 1,

}

public class EquipmentItem : Node
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    public virtual string DisplayName => "EquipmentComponent";

    public virtual string Description => "DefaultDescription";




    public virtual void PassiveEffect(Character c)
    {

    }

    public virtual void EndPassiveEffect(Character c)
    {

    }

    // Called when the node enters the scene tree for the first time.


    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
