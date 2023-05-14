using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public class Game : Node2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.

	public static bool DevMode = false;

	public static float SoundVolume = 0f;

	public static float MusicVolume = 0f;

	public static List<string> Levelnames = new List<string> { "Level1", "Level2", "Level3", "Level4", "Level5", "Level6", "LevelTestBonus" };

    public List<PackedScene> Levels = new List<PackedScene>();

	public List<PackedScene> Briefings = new List<PackedScene>();

	


	public static int CurrentLevelNumber = 1;

	public static Level CurrentLevel = null;

	public static int ActiveBriefing = -1;

	public int MaxLevel = 0;

	public bool Initialized = false;

	public static Game instance;


	[Export]
	public string MusicBusName = "Music";

	[Export]
	public string SoundBusName = "SFX";

	[Export]
	public bool DeveloperMode;


	public int MusicAudioBusIndex = -1;

	public int SoundAudioBusIndex = -1;


	/*public static Node GetInstance()
	{
		return GD.no
	}*/

	//public static Node Instance => ;

	public void InitializeLevels()
	{
		Levels = new List<PackedScene>();
		Briefings = new List<PackedScene>();
		foreach (string str in Levelnames)
		{
			Levels.Add(GD.Load<PackedScene>("res://Content/Levels/" + str + ".tscn"));
			var dir = new Directory();

			PackedScene briefScene = GD.Load<PackedScene>("res://Content/Levels/Briefings/" + str + "Briefing.tscn");

			if (briefScene!=null)
			{
				GD.Print("Loaded Briefing for: " + str);
				Briefings.Add(briefScene);
			}
			else
			{
				GD.Print("Could not load Briefing for: " + str);
				Briefings.Add(null);
			}
		}


		MaxLevel = Levels.Count;
		Initialized = true;
	}



	public void QuitToTitle()
	{
		if (!Initialized)
		{
			InitializeLevels();
		}





		if (CurrentLevel != null)
		{
			(CurrentLevel.GetNode("GameCamera") as Camera2D).Current = false;

            CurrentLevel.GetNode("ProjectileContainer").QueueFree();
            CurrentLevel.QueueFree();
		}

		((Control)GetNode("Menus/TitleScreen")).Visible = true;
		((AudioStreamPlayer)GetNode("Menus/TitleScreen/TitleMusic")).Play(0);//.Visible
		UpdateCurrentLevel();
	}

	public void QuitToMissionScreen()
	{
		if (!Initialized)
		{
			InitializeLevels();
		}





		if (CurrentLevel != null)
		{
			(CurrentLevel.GetNode("GameCamera") as Camera2D).Current = false;
			if (CurrentLevel.HasNode("ProjectileContainer"))
			{
				CurrentLevel.GetNode("ProjectileContainer").QueueFree();

			}
			for(int i = 0; i < CurrentLevel.GetChildCount(); ++i)
			{
				if(CurrentLevel.GetChild(i) is EntityContainer) { CurrentLevel.GetChild(i).QueueFree(); }
			}

			CurrentLevel.QueueFree();
		}

		((Control)GetNode("Menus/LevelSelect")).Visible = true;
		((AudioStreamPlayer)GetNode("Menus/TitleScreen/TitleMusic")).Play(0);//.Visible
		UpdateCurrentLevel();
	}

	public void UpdateCurrentLevel()
	{
		Level lv = null;

		for (int i = 0; i < GetChildCount(); i++)
		{
			Node n = GetChild(i);

			if (!IsInstanceValid(n))
			{
				continue;
			}
			//GD.Print(i, ", Node type: ",n.GetType());

			if ((n is Level) && !n.IsQueuedForDeletion())
			{
				lv = n as Level;
				break;
			}

		}


		CurrentLevel = lv;
	}

	public void LoadLevel(int levelNum)
	{
		if (!Initialized)
		{
			InitializeLevels();
		}

		int trueInd = (levelNum - 1) % MaxLevel;



		if (CurrentLevel != null)
		{
			(CurrentLevel.GetNode("GameCamera") as Camera2D).Current = false;
			CurrentLevel.QueueFree();
		}
		(GetNode("Menus/TitleScreen") as Control).Visible = false;
		(GetNode("Menus/LevelSelect") as Control).Visible = false;
		Node instance = Levels[trueInd].Instance();
		this.AddChild(instance);
		((AudioStreamPlayer)GetNode("Menus/TitleScreen/TitleMusic")).Playing = false;
		(GetNode("GameUI/GameHUD") as Control).Visible = true;
        (GetNode("GameUI/BossWarning") as Control).Visible = true;
        UpdateCurrentLevel();



	}

	public void LoadLevelWithBriefing(int levelNum)
	{
		if (!Initialized)
		{
			InitializeLevels();
		}

		int trueInd = (levelNum - 1) % MaxLevel;



		if (CurrentLevel != null)
		{
			(CurrentLevel.GetNode("GameCamera") as Camera2D).Current = false;
			CurrentLevel.ProjectileContainer.QueueFree();
			CurrentLevel.QueueFree();
		}
	   (GetNode("Menus/TitleScreen") as Control).Visible = false;
		(GetNode("Menus/LevelSelect") as Control).Visible = false;
		Node instance = Briefings[trueInd].Instance();
		(instance as LevelBriefing).LevelIndex = levelNum;
		GetNode("LevelBriefings").AddChild(instance);
		
		((AudioStreamPlayer)GetNode("Menus/TitleScreen/TitleMusic")).Playing = false;
        //(GetNode("GameUI/GameHUD") as Control).Visible = true;
        (GetNode("GameUI/BossWarning") as Control).Visible = false;
        ActiveBriefing = levelNum;
		UpdateCurrentLevel();



	}

	public void InitializeAudioBuses()
	{
		MusicAudioBusIndex = AudioServer.GetBusIndex(MusicBusName);
		SoundAudioBusIndex = AudioServer.GetBusIndex(SoundBusName);
	}


	public void UpdateVolumes()
	{

		if (MusicAudioBusIndex < 0 || SoundAudioBusIndex < 0)
		{
			InitializeAudioBuses();
		}
		
		
		if (MusicAudioBusIndex >= 0)
		{
			AudioServer.SetBusVolumeDb(MusicAudioBusIndex, GD.Linear2Db(MusicVolume));
		}

		if (SoundAudioBusIndex >= 0)
		{
			AudioServer.SetBusVolumeDb(SoundAudioBusIndex, GD.Linear2Db(SoundVolume));
		}



	}

	public void SetMenu(string menuname)
	{
		Node menuContainer = GetNode("/root/Game/Menus");

		for (int i = 0; i < menuContainer.GetChildCount(); ++i)
		{
			if (menuContainer.GetChild(i) is Control)
			{
				(menuContainer.GetChild(i) as Control).Visible = false;
			}
		}

		if (menuContainer.HasNode(menuname)&&menuContainer.GetNode(menuname) is Control)
		{
			(menuContainer.GetNode(menuname) as Control).Visible = true;
		}
		else
		{
			(menuContainer.GetNode("TitleScreen") as Control).Visible = true;
		}
	}

	public override void _Ready()
	{
		CurrentLevelNumber = 1;
		instance = this;
		InitializeLevels();
		InitializeAudioBuses();
		UpdateCurrentLevel();
		UpdateVolumes();
		(GetNode("Menus/TitleScreen") as TitleScreen).PlayTitleMusic();
	}

	public void Start()
	{
		LoadLevel(CurrentLevelNumber);

	}

	//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		UpdateCurrentLevel();
		DevMode = DeveloperMode;
		//GD.Print("Devmode:" + DevMode);

		if (CurrentLevel != null && !(CurrentLevel.Won||CurrentLevel.Lost) && Input.IsActionJustPressed("Pause"))
		{
			if (GetTree().Paused)
			{
				GetTree().Paused = false;
			}
			else
			{
				GetTree().Paused = true;
			}
		}
	}
}
