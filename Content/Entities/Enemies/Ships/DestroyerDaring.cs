using Godot;

using System;

public class DestroyerDaring : Ship
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.

	//public bool MissionKill = false;
	[Export]
	public float CloseRadarRange = 2000;

	public Character LongRangeRadar;

	public Character LongRangeVLS;

	public Character ShortRangeVLS;

	public Character Bridge;

	public bool RadarLock = false;

    public bool CloseRangeRadarLock = false;


    public override void _Ready()
	{
		
	}

	public override void OnActivate()
	{
		base.OnActivate();
        LongRangeRadar = GetNode("Parts/AegisRadar") as Character;
        LongRangeVLS = GetNode("Parts/VLSLongRange") as Character;
        ShortRangeVLS = GetNode("Parts/VLSShortRange") as Character;
        Bridge = GetNode("Parts/DaringBridge") as Character;


    }

	public void CheckRadarLock()
	{
		RadarLock = LongRangeRadar!= null && !LongRangeRadar.MissionKill && (LongRangeRadar as AegisRadar).Target!=null;
		//GD.Print("Ship radar lock: " + RadarLock);
		if (Game.CurrentLevel != null && Game.CurrentLevel.player != null)
        {
            CloseRangeRadarLock = (Game.CurrentLevel.player.LevelRelativePosition - LevelRelativePosition).Length() < CloseRadarRange;
		}
		else
		{
			CloseRangeRadarLock = false;
		}

	}

	public override void Behavior(float delta)
	{
		//GD.Print("Mission Kill: " + MissionKill);

        if (MissionKill && InitializationFrames == 0)
        {
            return;
        }

        if (InitializationFrames > 0)
        {
            --InitializationFrames;
            MissionKill = false;
        }


        UpdateComponentList();
        CheckComponentList();

        CheckRadarLock();

		if(RadarLock)
		{
			(LongRangeVLS as VLS).QueueShootMissiles(3);

		}

        if (CloseRangeRadarLock)
        {
            (ShortRangeVLS as VLSShortRange).QueueShootMissiles(6);

        }

		//GD.Print("Weight of active components: " + CurrentComponentWeight + ", Critical components protected:" + (CurrentComponentWeight>MinComponentWeightForInvincibility));
    }


	//public override 
	//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	//  public override void _Process(float delta)
	//  {
	//      
	//  }
}
