using Godot;
using System;

public class TitleMusic : AudioStreamPlayer
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
       // Autoplay = true;
        //Playing = true;
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        base._Process(delta);

       // VolumeDb = Mathf.Lerp(-80, -15, Game.MusicVolume);

    }

    public void Start()
    {
        //Playing = false;
    }
}
