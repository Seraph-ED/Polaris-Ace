using Godot;
using System;

public class SaveSettingsButton : Button
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    public override void _Pressed()
    {
        Range musicSlider = (GetParent().GetNode("SoundSliderContainer/MuteButtonContainer/MusicVolumeContainer/MusicVolumeSlider") as Range);
        Range soundSlider = (GetParent().GetNode("SoundSliderContainer/MuteButtonContainer/SFXVolumeContainer/SFXVolumeSlider") as Range);


        Game.MusicVolume = (float)(musicSlider.Value / musicSlider.MaxValue );
        Game.SoundVolume = (float)(soundSlider.Value / soundSlider.MaxValue);

        

        (GetNode("/root/Game") as Game).UpdateVolumes();

        GD.Print("Music Volume Scalar: " + Game.MusicVolume);
        GD.Print("Sound Volume Scalar: " + Game.SoundVolume);
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
