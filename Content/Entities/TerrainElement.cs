using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
public class TerrainElement : StaticBody2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public List<Vector2> AbsoluteCoordsCollisionVectors = null;




    public override void _Ready()
    {
        AbsoluteCoordsCollisionVectors = new List<Vector2>();
        if (HasNode("Hitbox") && GetNode("Hitbox") is CollisionPolygon2D)
        {
            Vector2[] pointsrelative = (GetNode("Hitbox") as CollisionPolygon2D).Polygon;
            Vector2 globalPos = (GetNode("Hitbox") as CollisionPolygon2D).GlobalPosition;
            for (int i = 0; i < pointsrelative.Length; ++i)
            {
                AbsoluteCoordsCollisionVectors.Add(pointsrelative[i] + GlobalPosition);
            }
        }
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
