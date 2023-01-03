using Godot;
using System;

public class PlayerHPBar : ProgressBar
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public Player currentPlayer = null;

    public bool SearchedForPlayer = false;

    public override void _Ready()
    {
        SearchedForPlayer = false;
        Value = 0;
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if(Visible&&!SearchedForPlayer)
        {
            Node root = GetNode("/root/Game");

            Level lv = Game.CurrentLevel;
            /*
            for (int i = 0; i < root.GetChildCount(); i++)
            {
                Node n = root.GetChild(i);

                //GD.Print(i, ", Node type: ",n.GetType());

                if((n is Level))
                {
                    lv = n as Level;
                    break;
                }



                

            }*/

            if(lv != null)
            {
                currentPlayer = lv.player;
                SearchedForPlayer = true;
            }

            //SearchedForPlayer = true;

        }else if (!Visible||!(GetParent().GetParent() as Control).Visible)
        {
            SearchedForPlayer = false;
            currentPlayer = null;
        }

        if(currentPlayer != null)
        {
            Value = currentPlayer.Health;

        }
        else
        {
            Value = 69;
            SearchedForPlayer=false;
        }
        
        //Value = find  
    }
}
