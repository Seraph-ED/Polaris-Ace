using Godot;
using System;

public class CarrierCatapult : ShipComponent
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    [Export]
    public float LaunchAnimLength = 1.0f;

    [Export]
    public float RetractAnimLength = 1.0f;

    public float LaunchTimer = 0f;

    public int LaunchState = 0; //0 = ready, 1 = launching, 2 = recalling and prep;

    public static string JetPath = "res://Content/Entities/Enemies/EnemyNavalFighter.tscn";

    public override void _Ready()
    {
       
    }


    

    public void LaunchJet()
    {
        if (LaunchState == 0)
        {
            LaunchTimer = LaunchAnimLength * 2;
            LaunchState = 1;
        }
    }

    public void SpawnAircraft()
    {

    }


    public override void Behavior(float delta)
    {

        base.Behavior(delta);


        if (LaunchTimer > 0)
        {
            LaunchTimer-=delta;
        }
        else
        {
            if (LaunchState == 1)
            {

                float speed = (1000f / LaunchAnimLength) / 60f;

                Game.CurrentLevel.SpawnCharacter(JetPath, LevelRelativePosition + new Vector2(1000, 0).Rotated(Rotation), new Vector2(speed, 0).Rotated(Rotation), Rotation + Mathf.Pi / 2);
                LaunchState = 2;
                LaunchTimer = RetractAnimLength;
            }else if(LaunchState == 2)
            {
                LaunchState = 0;
            }


        }



    }


    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
