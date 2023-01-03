using Godot;
using System;

public enum EquipmentType
{
    Weapon = 0,
    Defense = 1,
    Component = 2,
}

public class EquipmentSelectButton : Button
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.

    public int IndexPointedTo = 0;

    public bool Initialized = false;

    public bool PointsToEquipped = false;

    public EquipmentType type;

    public string DisplayName = "";

    public string Description = "";

    public override void _Ready()
    {
        Initialize();
    }

    public void Initialize()
    {
        Game game = (GetNode("/root/Game") as Game);

        switch (type)
        {
            case EquipmentType.Weapon:
                DisplayName = (EquipmentHandler.WeaponDirectory[IndexPointedTo]).DisplayName;
                Description = (EquipmentHandler.WeaponDirectory[IndexPointedTo]).Description;

                break;
            case EquipmentType.Defense:
                DisplayName = (EquipmentHandler.DefenseDirectory[IndexPointedTo]).DisplayName;
                Description = (EquipmentHandler.DefenseDirectory[IndexPointedTo]).Description;

                break;
            case EquipmentType.Component:
                DisplayName = (EquipmentHandler.ComponentDirectory[IndexPointedTo]).DisplayName;
                Description = (EquipmentHandler.ComponentDirectory[IndexPointedTo]).Description;
                break;
            
        }
        
        Text = DisplayName;
        HintTooltip = Description;
        Initialized = true;
    }

    public override void _Process(float delta)
    {
        base._Process(delta);

        if (!Initialized)
        {
            return;
        }

        switch (type)
        {
            case EquipmentType.Weapon:
                PointsToEquipped = EquipmentHandler.WeaponDirectory.IndexOf(EquipmentHandler.Instance.equippedWeapon) == IndexPointedTo;

                break;
            case EquipmentType.Defense:
                PointsToEquipped = EquipmentHandler.DefenseDirectory.IndexOf(EquipmentHandler.Instance.equippedDefense) == IndexPointedTo;

                break;
            case EquipmentType.Component:
                PointsToEquipped = EquipmentHandler.ComponentDirectory.IndexOf(EquipmentHandler.Instance.equippedComponent) == IndexPointedTo;
                break;
            default:
                PointsToEquipped = false;
                break;
        }


        if (PointsToEquipped)
        {
            AddColorOverride("font_color", new Color(0, 1, 0));
        }
        else
        {
            RemoveColorOverride("font_color");
        }

    }

    public override void _Pressed()
    {
        
        switch (type)
        {
            case EquipmentType.Weapon:
                EquipmentHandler.Instance.equippedWeapon = EquipmentHandler.WeaponDirectory[IndexPointedTo];

                break;
            case EquipmentType.Defense:
                EquipmentHandler.Instance.equippedDefense = EquipmentHandler.DefenseDirectory[IndexPointedTo];

                break;
            case EquipmentType.Component:
                EquipmentHandler.Instance.equippedComponent = EquipmentHandler.ComponentDirectory[IndexPointedTo];
                break;
        }

    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
