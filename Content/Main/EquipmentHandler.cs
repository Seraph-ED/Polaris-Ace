using Godot;
using System;
using System.Collections.Generic;

public class EquipmentHandler : Node
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.

    public Weapon equippedWeapon = null;

    public static List<Weapon> WeaponDirectory = new List<Weapon>();

    public DefenseItem equippedDefense = null;

    public static List<DefenseItem> DefenseDirectory = new List<DefenseItem>();

    public ComponentItem equippedComponent = null;

    public static List<ComponentItem> ComponentDirectory = new List<ComponentItem>();

    public static EquipmentHandler Instance;//= new EquipmentHandler();


    public void InitalizeWeapons()
    {

        WeaponDirectory = new List<Weapon>();
        
        Node weaponsHolder = GetNode("Weapons");

        for(int i = 0; i < weaponsHolder.GetChildCount(); i++)
        {
            if(weaponsHolder.GetChild(i) is Weapon)
            {
                WeaponDirectory.Add(weaponsHolder.GetChild(i) as Weapon);
            }
        }



    }

    public void InitalizeDefenses()
    {

        DefenseDirectory = new List<DefenseItem>();

        Node defenseHolder = GetNode("Defenses");

        for (int i = 0; i < defenseHolder.GetChildCount(); i++)
        {
            if (defenseHolder.GetChild(i) is DefenseItem)
            {
                DefenseDirectory.Add(defenseHolder.GetChild(i) as DefenseItem);
            }
        }



    }
    public void InitalizeComponents()
    {

        ComponentDirectory = new List<ComponentItem>();

        Node defenseHolder = GetNode("Components");

        for (int i = 0; i < defenseHolder.GetChildCount(); i++)
        {
            if (defenseHolder.GetChild(i) is ComponentItem)
            {
                ComponentDirectory.Add(defenseHolder.GetChild(i) as ComponentItem);
            }
        }



    }


    public override void _Ready()
    {
        Instance = this;
        equippedWeapon = Weapon.GetWeapon("Missile");
        equippedDefense = DefenseItem.GetDefense("Flares");
        equippedComponent = ComponentItem.GetComponent("NoComponent");
        InitalizeWeapons();
        InitalizeDefenses();
        InitalizeComponents();

    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
