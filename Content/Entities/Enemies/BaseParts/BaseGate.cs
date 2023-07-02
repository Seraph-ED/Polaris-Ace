using Godot;
using ShmupGame.Content.Entities;
using System;
using System.Collections.Generic;

public class BaseGate : CollideableCharacter
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.

    //public List<Character> TaskForce = new List<Character>();

    

    [Export]
    public float ProportionateOpening = 0;

    [Export]
    public float SecondsToOpen = 2;

    public bool Died = false;

    public bool TaskForceExists = true;

    public float GracePeriod = 1.0f;


    public void CheckList(Node n, bool reset = true)
    {
        if (reset)
        {
            TaskForceExists = false;
        }   
        
        for(int i = 0; i < n.GetChildCount(); i++)
        {
            Node c = n.GetChild(i);

            if(c == this)
            {
                continue;
            }

            if(c is IActivateable)
            {
                if(c is Character && !(c as Character).MissionKill && !(c is BaseGate))
                {
                    TaskForceExists = true;
                }

                CheckList(c, false);
            }
        }


    }


   
    public override void Behavior(float delta)
    {

        

        if (GracePeriod > 0)
        {
            GracePeriod -= delta;
            TaskForceExists = true;
        }
        else
        {
            
            CheckList(GetParent());
       // GD.Print(TaskForceExists);
            
            if (!TaskForceExists)
            {
                if (!MissionKill)
                {
                    GetNode<AudioStreamPlayer2D>("OpenSound").Play(0);
                }
                MissionKill = true;
               
            }
        }

        if (MissionKill && !Died)
        {
            
                (GetNode("Hitbox") as CollisionShape2D).SetDeferred("disabled", true);
                Died = true;
            
        }

        if (MissionKill && ProportionateOpening < 1)
        {
           
            ProportionateOpening += (1f / SecondsToOpen) * delta;
        }else if (!MissionKill)
        {
            ProportionateOpening = 0;
        }


    }

    

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
