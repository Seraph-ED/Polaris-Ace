using Godot;
using ShmupGame.Content.Entities;
using System;
using System.Collections.Generic;

public class Ship : Character
{
    


    [Export]
    public float MinComponentWeightForInvincibility = 1.0f;

    public float CurrentComponentWeight = 0.0f;

    public List<ShipComponent> Components = new List<ShipComponent>();

    public int KillType = 0;

    [Export]
    public float LengthForDeathAnim = 0;

    public int InitializationFrames = 10;

    public void UpdateComponentList()
    {
        Components.Clear();
       
        ComponentRecursiveTraversal(this);
    }

    public void ComponentRecursiveTraversal(Node n)
    {
        if (n is ShipComponent)
        {
            Components.Add(n as ShipComponent);
           
        }

        if (n is IActivateable && (n as IActivateable).IsActive())
        {
            if(n is Character || n is EntityContainer)
            {
                for (int i = 0; i < n.GetChildCount(); ++i)
                {
                    ComponentRecursiveTraversal(n.GetChild(i));
            }
            }
        }


    }


    public void CheckComponentList()
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

        if(allPivotalsDead)
        {
            KillType = 1;
            Kill();

        }
    }

    public override void Behavior(float delta)
    {
        if (MissionKill&&InitializationFrames==0)
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
    }

    //public override 

    public virtual void SpawnOnDeathExplosionPattern(int killType)
    {

        float b = killType == 0 ? 50 : 200;

        for(float i = -LengthForDeathAnim/2; i < LengthForDeathAnim/2; i += b)
        {
            Vector2 explpos = LevelRelativePosition + (Vector2.Right.Rotated(Rotation) * i);

            Game.CurrentLevel.PlaceExplosion(explpos, b/100);


        }


    }

   

    public override void _Ready()
    {
        
    }

    public override bool DealDamage(float damage, int specialCircumstances = 0)
    {
        if (InvinTime > 0 || MissionKill)
        {
            return false;
        }
        if (!InHitFlash && HitFlash <= 0)
        {
            HandleHitFlash();
        }

        Health -= damage;

        if (Health <= 0)
        {
            KillType = 0;
            Kill();
        }
        return true;


    }

    public override void Kill()
    {
        SpawnOnDeathExplosionPattern(KillType);
        
        for (int i = Components.Count-1; i >= 0; --i)
        {
            

            Components[i].Kill();
        }

        if(KillType== 1)
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



    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
