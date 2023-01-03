using Godot;
using System;

public class FlareCooldownBar : ProgressBar
{
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
        if (Visible && (!SearchedForPlayer || !IsInstanceValid(currentPlayer) || currentPlayer == null))
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

            if (lv != null)
            {
                currentPlayer = lv.player;
                SearchedForPlayer = true;
            }

            //SearchedForPlayer = true;

        }
        else if (!Visible || !(GetParent().GetParent() as Control).Visible || Game.CurrentLevel == null)
        {
            SearchedForPlayer = false;
            currentPlayer = null;
        }
        

        if (IsInstanceValid(currentPlayer) && currentPlayer != null)
        {
            Timer t = (currentPlayer.GetNode("DefenseTimer") as Timer);


            Value = 100 * (1f-(t.TimeLeft / EquipmentHandler.Instance.equippedDefense.CooldownSeconds));

        }
        else
        {
            Value = 69;
           /// currentPlayer = null;
            SearchedForPlayer = false;
        }

        //Value = find  
    }
}
