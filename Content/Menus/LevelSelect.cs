using Godot;
using System;

public class LevelSelect : Control
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.

    public static int LevelIndexSelected = -1;

    public static PackedScene LvButton = GD.Load<PackedScene>("res://Content/Menus/LevelButton.tscn");

    public override void _Ready()
    {
        
    }



    public void Initialize()
    {
        for(int i = 0; i < GetNode("ButtonPanelContainer/LevelButtonContainer").GetChildCount(); i++)
        {
            GetNode("ButtonPanelContainer/LevelButtonContainer").GetChild(i).QueueFree();
        }
        
        Game game = (GetNode("/root/Game") as Game);

        for(int i = 0; i < game.Levels.Count; i++)
        {
            LevelButton button = (LvButton.Instance() as LevelButton);
            button.LevelIndexPointedTo = i;
            //button.Initialize();

            GetNode("ButtonPanelContainer/LevelButtonContainer").AddChild(button);


        }


    }

    //public override rea

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
