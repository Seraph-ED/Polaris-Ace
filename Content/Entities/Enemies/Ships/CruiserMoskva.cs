using Godot;
using System;
using System.Collections.Generic;

public class CruiserMoskva : Ship
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.


    //public Character SearchRadar1;

    //public Character SearchRadar2;

    //public Character ShortRangeVLS;

    public Character Bridge;

    public List<VLS> VLSes = null;

    public ShipRadar TrackRadar;
    public ShipRadar SearchRadar1;
    public ShipRadar SearchRadar2;

    public bool RadarLock = false;

    public bool CloseRangeRadarLock = false;

    public override void _Ready()
    {
        
    }

    public override void OnActivate()
    {
        base.OnActivate();
        TrackRadar = GetNode("Parts/MoskvaTrackingRadar") as ShipRadar;
        SearchRadar1 = GetNode("Parts/MoskvaSearchRadarSingle") as ShipRadar;
        SearchRadar1 = GetNode("Parts/MoskvaSearchRadarDouble") as ShipRadar;
        FindVLS();
    }

    public void FindVLS()
    {
        VLSes = new List<VLS>();
        Node partHolder = GetNode("Parts");

        for(int i = 0; i < partHolder.GetChildCount(); ++i)
        {
            Node n = partHolder.GetChild(i);
            if (n.GetType() == typeof(VLS))
            {
                VLSes.Add(n as VLS);
            }
        }

    }

    public override void Behavior(float delta)
    {
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

        if (TrackRadar!=null&&!TrackRadar.MissionKill && TrackRadar.Target != null)
        {
            FireVLSes(0, 2, 2);
        }

        if (SearchRadar1 != null && !SearchRadar1.MissionKill && SearchRadar1.Target != null)
        {
            FireVLSes(6, 2, 2);
        }

        if (SearchRadar2 != null && !SearchRadar2.MissionKill && SearchRadar2.Target != null)
        {
            FireVLSes(2, 4, 2);
        }
    }

    public void FireVLSes(int start, int numVLSes = 1, int numMissiles = 1)
    {
        if (VLSes == null || VLSes.Count == 0)
        {
            return;
        }
        
        for(int i = start; i < start+numVLSes; ++i)
        {
            VLS a = VLSes[i % VLSes.Count];

            a.QueueShootMissiles(numMissiles);

        }
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
