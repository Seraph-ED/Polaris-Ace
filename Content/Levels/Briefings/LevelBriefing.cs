using Godot;
using System;

public class LevelBriefing : Control
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.

    public int PageIndex = 0;

    public int LevelIndex = 0;

    public bool LastPage = false;

    public void UpdatePage()
    {
        if (!HasNode("Pages"))
        {
            return;
        }
        
        Node pageHolder = GetNode("Pages");

        for(int i = 0; i < pageHolder.GetChildCount(); ++i)
        {
            Node node = pageHolder.GetChild(i);
            if(!(node is Control))
            {
                continue;
            }
            Control ctl = node as Control;
            if (i == PageIndex)
            {
                ctl.Visible = true;
            }
            else
            {
                ctl.Visible = false;
            }

        }

    }

    public void AdvancePage()
    {
        PageIndex++;
        
        if (!HasNode("Pages"))
        {
            (GetNode("/root/Game") as Game).LoadLevel(LevelIndex);
            QueueFree();
            
            return;
        }

        Node pageHolder = GetNode("Pages");

        if(PageIndex >= pageHolder.GetChildCount())
        {
            (GetNode("/root/Game") as Game).LoadLevel(LevelIndex);
            QueueFree();

            return;
        }
        else
        {
            UpdatePage();
            if(PageIndex == pageHolder.GetChildCount() - 1)
            {
                LastPage = true;
            }
        }



    }

    public override void _Ready()
    {
        UpdatePage();
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
