using Godot;
using ShmupGame.Content.Entities;
using System;

public class CharacterParticles : Particles2D
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
        if(GetParent() is IActivateable)
        {
            Emitting = (GetParent() as IActivateable).IsActive();
        }else if(GetParent().GetParentOrNull<Node>()!=null&& GetParent().GetParent() is IActivateable)
        {
            Emitting = (GetParent().GetParent() as IActivateable).IsActive();
        }
    }
}
