using Godot;

using System;

public class DestroyerDaring : Ship
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.

	//public bool MissionKill = false;

	public Character LongRangeRadar;

	public Character LongRangeVLS;

	public Character ShortRangeVLS;

	public Character Bridge;

	public bool RadarLock = false;

	

	public override void _Ready()
	{
		LongRangeRadar = GetNode("Parts/AegisRadar") as Character;
		LongRangeVLS = GetNode("Parts/VLSLongRange") as Character;
		ShortRangeVLS = GetNode("Parts/VLSShortRange") as Character;
		Bridge = GetNode("Parts/DaringBridge") as Character;
	}

	public override void OnActivate()
	{
		base.OnActivate();



	}

	public void CheckRadarLock()
	{
		RadarLock = LongRangeRadar!= null && !LongRangeRadar.MissionKill && (LongRangeRadar as AegisRadar).Target!=null;
	}

	public override void Behavior(float delta)
	{
		CheckRadarLock();

		if(RadarLock)
		{
			(LongRangeVLS as VLSLongRange).QueueShootMissiles(5);

		}
	}


	//public override 
	//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	//  public override void _Process(float delta)
	//  {
	//      
	//  }
}
