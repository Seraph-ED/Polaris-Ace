using Godot;
using System;

public class SparkEmitter : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.

    [Signal] public delegate void EmitParticleSignal();
    public override void _Ready()
    {
        
    }

    public void EmitParticles()
    {
        for(int i = 0; i < GetChildCount();  ++i)
        {
            if (GetChild<Particles2D>(i) != null)
            {
                GetChild<Particles2D>(i).Emitting = true;
            }
        }


    }


//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
