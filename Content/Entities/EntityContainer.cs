using Godot;
using ShmupGame.Content.Entities;
using System;

public class EntityContainer : Node2D, IActivateable
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    //public Vector2 levelPosition

    [Export]
    public bool Active;

    public virtual bool IsActive()
    {
        return Active;
    }

    public override void _Ready()
    {

    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if (GetParent() is IActivateable)
        {
            Active = (GetParent() as IActivateable).IsActive();
        }
    }
}
