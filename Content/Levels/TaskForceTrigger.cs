using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class TaskForceTrigger : Area2D
{

    [Export]
    public bool Triggered = false;

    public void OnEntered(Node body)
    {
        Triggered = true;
    }

}
