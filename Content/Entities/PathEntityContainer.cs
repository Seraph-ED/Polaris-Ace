using Godot;
using ShmupGame.Content.Entities;
using System;

public class PathEntityContainer : EntityContainer
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    [Export]
    public double Proportion = 0;

    [Export]
    public double CompletionTime = 0;

    [Export]
    public int LoopType = 0;


    private float ActualProportion = 0;

    public Vector2 LevelRelativePosition
    {

        get => GlobalPosition - Game.CurrentLevel.GlobalPosition;
        set => GlobalPosition = Game.CurrentLevel.GlobalPosition + value;
    }

    public Vector2 LevelPositionOnpath = Vector2.Zero;
    public float RotationOnPath = 0;


    public override void _Ready()
    {
        Proportion = 0;
    }

    public override void _PhysicsProcess(float delta)
    {
        if (GetParent() is IActivateable)
        {
            Active = (GetParent() as IActivateable).IsActive();
        }

        if (Active)
        {
            Proportion += 1.0/CompletionTime*delta;

            switch (LoopType)
            {
                default:
                    
                    Proportion = Mathf.Clamp((float)Proportion, 0, 1f);
                    ActualProportion = (float)Proportion;
                    break;
                case 1:
                    Proportion = Proportion % 1;
                    ActualProportion = (float)Proportion;
                    break;
                case 2:
                    Proportion = Proportion % 1;
                    ActualProportion = 2 * Mathf.Abs((((float)Proportion +0.5f) % 1)-0.5f);
                    break;

            }
            ActualProportion = Mathf.Clamp(ActualProportion, 0, 1);

        }
        else
        {
            ActualProportion = (float)Proportion;
        }

        if (HasNode("Path") && HasNode("Path/PathFollow")&&Game.CurrentLevel!=null)
        {
            if(IsInstanceValid(GetNode("Path"))&& IsInstanceValid(GetNode("Path/PathFollow")) && !GetNode("Path").IsQueuedForDeletion() && !GetNode("Path/PathFollow").IsQueuedForDeletion())
            {
                if (GetNode("Path") is Path2D && GetNode("Path/PathFollow") is PathFollow2D)
                {

                    Path2D path = (GetNode("Path") as Path2D);
                    PathFollow2D pathFollow = (GetNode("Path/PathFollow") as PathFollow2D);

                    if(IsInstanceValid(GetNode("Path")) && IsInstanceValid(GetNode("Path/PathFollow")) && !GetNode("Path").IsQueuedForDeletion() && !GetNode("Path/PathFollow").IsQueuedForDeletion())
                    {
                        pathFollow.UnitOffset = ActualProportion;

                        LevelPositionOnpath = pathFollow.GlobalPosition - Game.CurrentLevel.GlobalPosition;
                        RotationOnPath = pathFollow.Rotation;
                    }

                   


                }
            }
            
            
            
            
            


        }


    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
