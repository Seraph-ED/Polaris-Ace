using Godot;
using System;

public class TaskForceContainer : EntityContainer
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.

    [Export]
    public NodePath activationCheckPath;

    public bool HasEnteredZone = false;

    public override void _Ready()
    {
        
    }

    public override bool IsActive()
    {
        return base.IsActive() && HasEnteredZone;
    }

    public override void _Process(float delta)
    {
        base._Process(delta);

        if (GetNode(activationCheckPath) != null&&GetNode(activationCheckPath) is TaskForceTrigger)
        {
            TaskForceTrigger n = (GetNode(activationCheckPath) as TaskForceTrigger);

            if (n.Triggered)
            {
                HasEnteredZone = true;
            }

            
        }
        

    }



    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
