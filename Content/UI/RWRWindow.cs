using Godot;
using System;

public class RWRWindow : Panel
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
        Visible = Game.CurrentLevel != null && Game.CurrentLevel.player != null && Game.CurrentLevel.player.RWRWarning>0;   
  }
}
