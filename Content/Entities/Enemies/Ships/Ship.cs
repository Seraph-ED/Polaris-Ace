using Godot;
using ShmupGame.Content.Entities;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

public class Ship : Character
{
    


    [Export]
    public float MinComponentWeightForInvincibility = 1.0f;

    public float CurrentComponentWeight = 0.0f;

    public List<ShipComponent> Components = new List<ShipComponent>();

    public int KillType = 0;

    public void UpdateComponentList()
    {
        Components.Clear();
        CurrentComponentWeight = 0;
        ComponentRecursiveTraversal(this);
    }

    public void ComponentRecursiveTraversal(Node n)
    {
        if (n is ShipComponent)
        {
            Components.Add(n as ShipComponent);
            CurrentComponentWeight += (n as ShipComponent).SubsystemWeight;
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
        
        for(int i = 0; i < Components.Count; ++i)
        {
            ShipComponent comp = Components[i];

            if (comp.Pivotal)
            {
                if (!comp.MissionKill)
                {
                    allPivotalsDead = false;
                }
                if(CurrentComponentWeight > MinComponentWeightForInvincibility)
                {
                    comp.InvinTime = 0.1f;
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
        
        UpdateComponentList();
        CheckComponentList();
    }

    //public override 

    public virtual void SpawnOnDeathExplosionPattern(int killType)
    {

    }

   

    public override void _Ready()
    {
        
    }

    public override void Kill()
    {
        SpawnOnDeathExplosionPattern(KillType);
        
        for (int i = Components.Count-1; i >= 0; --i)
        {
            if(KillType == 0)
            {

            }
            else
            {

            }

        }

        if(KillType== 1)
        {
            MissionKill = true;
        }
        else
        {

        }
        base.Kill();
    }



    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
