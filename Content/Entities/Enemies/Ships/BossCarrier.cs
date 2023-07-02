using Godot;
using System;
using System.Collections.Generic;

public class BossCarrier : Ship
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.

    public List<CarrierCatapult> Cats = new List<CarrierCatapult>();

    public Timer JetTimer;

    [Export]
    public string FleetPath = "";

    public bool FleetProtected = true;

    public int CatIndex = 0;

    public bool Initialized = false;



    public override void OnReady()
    {
        base.OnReady();
        JetTimer = GetNode("JetLaunchTimer") as Timer;
    }

    public bool CheckFleet()
    {
        if(Game.CurrentLevel.GetNode("Fleet")==null||!(Game.CurrentLevel.GetNode("Fleet") is EntityContainer))
        {
            return false;
        }

        Node fleet = (Game.CurrentLevel.GetNode("Fleet") as EntityContainer);

        int activeShips = 0;

        for(int i = 0; i < fleet.GetChildCount(); ++i)
        {
            if(fleet.GetChild(i) is Ship)
            {
                if(!(fleet.GetChild(i) as Ship).MissionKill)
                {
                    ++activeShips;
                }
            }
        }

        return activeShips > 2;


    }


    public void FindCatapults()
    {
        if(GetNode("Parts/Catapults")!=null&& GetNode("Parts/Catapults") is EntityContainer)
        {
            Node n = GetNode("Parts/Catapults");

            for(int i = 0; i < n.GetChildCount(); ++i)
            {
                Node n2 = n.GetChild(i);
                if(n2 is CarrierCatapult)
                {
                    Cats.Add(n2 as CarrierCatapult);
                }
            }

        }

    }

    public void CheckCatapults()
    {
        for(int i = 0; i < Cats.Count; ++i)
        {
            if (Cats[i].MissionKill)
            {
                CarrierCatapult c = Cats[i];

                Cats.RemoveAt(i);

                c.QueueFree();
            }
        }
    }

    public void LaunchJets()
    {
        if (Game.CurrentLevel.SpawnedCharacterContainer != null)
        {
            int jetsActive = 0;
            
            for (int i = 0; i < Game.CurrentLevel.SpawnedCharacterContainer.GetChildCount(); ++i)
            {
                if(Game.CurrentLevel.SpawnedCharacterContainer.GetChild(i) is EnemyNavalFighter)
                {
                    jetsActive++;
                }
            }

            while(jetsActive < 4)
            {
                ++jetsActive;

                CatIndex = CatIndex % Cats.Count;

                Cats[CatIndex].LaunchJet();

                ++CatIndex;

            }

        }

    }


    public void CheckComponentListAlt()
    {
        bool allPivotalsDead = true;

        CurrentComponentWeight = 0;

        for (int i = 0; i < Components.Count; ++i)
        {
            ShipComponent comp = Components[i];

            if (!comp.MissionKill)
            {
                CurrentComponentWeight += comp.SubsystemWeight;
            }


        }

        for (int i = 0; i < Components.Count; ++i)
        {
            ShipComponent comp = Components[i];


            if (FleetProtected)
            {
                comp.InvinTime = 0.4f;
            }

            if (comp.Critical)
            {
                if (CurrentComponentWeight > MinComponentWeightForInvincibility)
                {
                    comp.InvinTime = 0.4f;
                }
                if (!comp.MissionKill)
                {
                    allPivotalsDead = false;
                }

            }

        }

        if (allPivotalsDead)
        {
            KillType = 1;
            Kill();

        }
    }

    public override void Kill()
    {
        if(Game.CurrentLevel is Level4)
        {
            (Game.CurrentLevel as Level4).CarrierDowned = true;
        }
        SpawnOnDeathExplosionPattern(KillType);

        for (int i = Components.Count - 1; i >= 0; --i)
        {


            Components[i].Kill();
        }

        if (KillType == 1)
        {
            MissionKill = true;
        }
        else
        {
            for (int i = Components.Count - 1; i >= 0; --i)
            {
                Components[i].QueueFree();
            }
            QueueFree();
            //base.Kill();
        }

    }

    public override void OnActivate()
    {
        FindCatapults();
       
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
        else
        {

            if (!Initialized)
            {
                if (Cats == null)
                {
                    FindCatapults();

                }

                Initialized = true;
            }
        }

        


        UpdateComponentList();
        CheckComponentListAlt();

        if (InitializationFrames == 0)
        {
            if (Cats == null)
            {
                FindCatapults();
            }
            else
            {
                CheckCatapults();
            }

            FleetProtected = CheckFleet();
        }
        
        

        if (JetTimer!=null&&JetTimer.TimeLeft == 0)
        {
            LaunchJets();
            JetTimer.Start(20);
        }else if (JetTimer == null)
        {
            JetTimer = GetNode("JetLaunchTimer") as Timer;
        }

    }



    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
