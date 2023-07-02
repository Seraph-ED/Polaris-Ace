using Godot;
using System;

public class SaveData : Node
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.

    public int HighestLevelUnlocked = 1;

    public override void _Ready()
    {
        
    }

    public Godot.Collections.Dictionary<string, object> Save()
    {
        return new Godot.Collections.Dictionary<string, object>()
     {
        { "Filename", Filename },
        { "Parent", GetParent().GetPath() },
        { "LevelProgress", HighestLevelUnlocked },
       
        };
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
