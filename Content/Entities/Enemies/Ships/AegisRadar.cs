using Content;
using Godot;
using System;
using System.Collections.Generic;
using System.Security.Policy;

public class AegisRadar : ShipComponent
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }

    public Character Target;

    public float antennaRotation = 0;

    public Node2D RadarHandler;

    public Area2D RadarDetection;

    public List<Vector2> radarPoints = new List<Vector2>();

    public List<Node> DetectedObjects = new List<Node>();

    public override void OnReady()
    {
        // target = null;
        //FlightSpeed = 18f;
        IsEnemy = true;
        Health = 450;
        RadarHandler = (GetNode("RadarCasts") as Node2D);
        RadarDetection = (GetNode("RadarDetection") as Area2D);
    }

    public void FindTarget()
    {

      
    }

    public void DetectObject(Node body)
    {
        if (!DetectedObjects.Contains(body))
        {
            DetectedObjects.Add(body);
        }
    }

    public void RemoveObject(Node body)
    {
        if (DetectedObjects.Contains(body))
        {
            DetectedObjects.Remove(body);
        }
    }

    public void GetRadarRaycastIntersectionPoints()
    {
       radarPoints.Clear();

        radarPoints.Add(Vector2.Zero);
        
        for(int i = 0; i < RadarHandler.GetChildCount();  ++i)
        {
            RayCast2D radarRay = RadarHandler.GetChild(i) as RayCast2D;

            radarRay.ForceRaycastUpdate();


            if (radarRay.IsColliding())
            {
                radarPoints.Add(radarRay.GetCollisionPoint() - GlobalPosition);
            }
            else
            {
                radarPoints.Add(radarRay.CastTo.Rotated(radarRay.Rotation+RadarHandler.Rotation));
            }

            

        }


    }

    public override void Behavior(float delta)
    {
        base.Behavior(delta);

        //FindTarget();

        if (DetectedObjects.Contains(Game.CurrentLevel.player))
        {
            Target = Game.CurrentLevel.player;
        }
        else
        {
            Target = null;
        }

        if (RadarHandler == null || RadarDetection == null)
        {
            RadarHandler = (GetNode("RadarCasts") as Node2D);
            RadarDetection = (GetNode("RadarDetection") as Area2D);
        }

        GetRadarRaycastIntersectionPoints();

        (RadarDetection.GetNode("RadarHitbox") as CollisionPolygon2D).Polygon = radarPoints.ToArray();


        if (Target != null)
        {
            antennaRotation = Utils.TurnTowards(antennaRotation, (Target.LevelRelativePosition - LevelRelativePosition).Angle(), Mathf.Deg2Rad(3f) * delta * 60);
        }
        else
        {
            antennaRotation += Mathf.Deg2Rad(1f) * delta * 60;
        }

        (GetNode("Antenna") as Sprite).Rotation = antennaRotation;
        (GetNode("RadarCasts") as Node2D).Rotation = antennaRotation;// + (Mathf.Pi / 2f);
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
