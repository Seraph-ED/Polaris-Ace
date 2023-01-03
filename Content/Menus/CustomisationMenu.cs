using Godot;
using System;

public class CustomisationMenu : Control
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.

    public static PackedScene weapButton = GD.Load<PackedScene>("res://Content/Menus/EquipmentSelectButton.tscn");

    public static string WeaponsContainerPath = "WeaponsPanel/WeaponsContainer";
    public static string DefenseContainerPath = "DefensePanel/DefenseContainer";
    public static string ComponentContainerPath = "ComponentPanel/ComponentContainer";

    public override void _Ready()
    {
        
    }

    public void Initialize()
    {
        InitializeWeaponList();
        InitializeDefenseList();
        InitializeComponentList();
    }


    public void InitializeWeaponList()
    {
        for(int i = 0; i < GetNode(WeaponsContainerPath).GetChildCount(); i++)
        {
            GetNode(WeaponsContainerPath).GetChild(i).QueueFree();
        }
        
        

        for(int i = 0; i < EquipmentHandler.WeaponDirectory.Count; i++)
        {
            EquipmentSelectButton button = (weapButton.Instance() as EquipmentSelectButton);
            button.IndexPointedTo = i;
            button.type = EquipmentType.Weapon;
            //button.Initialize();

            GetNode(WeaponsContainerPath).AddChild(button);


        }


    }

    public void InitializeDefenseList()
    {
        for (int i = 0; i < GetNode(DefenseContainerPath).GetChildCount(); i++)
        {
            GetNode(DefenseContainerPath).GetChild(i).QueueFree();
        }



        for (int i = 0; i < EquipmentHandler.DefenseDirectory.Count; i++)
        {
            EquipmentSelectButton button = (weapButton.Instance() as EquipmentSelectButton);
            button.IndexPointedTo = i;
            button.type = EquipmentType.Defense;
            //button.Initialize();

            GetNode(DefenseContainerPath).AddChild(button);


        }


    }

    public void InitializeComponentList()
    {
        for (int i = 0; i < GetNode(ComponentContainerPath).GetChildCount(); i++)
        {
            GetNode(ComponentContainerPath).GetChild(i).QueueFree();
        }



        for (int i = 0; i < EquipmentHandler.ComponentDirectory.Count; i++)
        {
            EquipmentSelectButton button = (weapButton.Instance() as EquipmentSelectButton);
            button.IndexPointedTo = i;
            button.type = EquipmentType.Component;
            //button.Initialize();

            GetNode(ComponentContainerPath).AddChild(button);


        }


    }

    //public override rea

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
