using Godot;
using System;

public class TargetHangar : Character
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    [Export]
    public bool SpawnedEnemies = false;

    

    public void SpawnEnemies()
    {
        SpawnedEnemies = true;
        for(int i = 0; i < GetChildCount(); ++i)
        {
            Node n = GetChild(i);

            if(!(n is Character || n is EntityContainer))
            {
                continue;
            }

            if(n is Character)
            {
                (n as Character).Active = true;
            }else if (n is EntityContainer)
            {
                (n as EntityContainer).Active = true;
            }


        }


    }

    public override void OnReady()
    {
        IsStructure = true;
        for (int i = 0; i < GetChildCount(); ++i)
        {
            Node n = GetChild(i);

            if (!(n is Character))
            {
                continue;
            }

            Character c = (n as Character);

            c.Active = SpawnedEnemies;
        }
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
