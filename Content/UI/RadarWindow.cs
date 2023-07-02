using Godot;
using System;
using System.Collections.Generic;
using System.Security.Policy;

public class RadarWindow : Panel
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    //private Texture _texture;

    public List<List<Vector2>> PolygonPoints;


    public List<List<Vector2>> RelativePolygonPoints;

    [Export]
    public float RadarRange = 10000;

    public Texture BlipTexture;
    public Texture BlipTextureStructure;
    public Texture MissileWarnTexture;
    public Texture RWRWarnTexture;

    public Level currentLevel;

    public Player currentPlayer;

    public List<Vector3> RelativeEnemyPositions;

    public List<Vector2> RelativeProjectilePositions;

    public Color[] radarPolyColors = new Color[] { new Color(1, 1, 1) };


    public bool ConstructedTerrain = false;

    public override void _Ready()
    {
        BlipTexture = GD.Load<Texture>("res://Assets/Sprites/RadarBlip.png");
        BlipTextureStructure = GD.Load<Texture>("res://Assets/Sprites/RadarBlipSquare.png");
        MissileWarnTexture = GD.Load<Texture>("res://Assets/Sprites/MissileWarning.png");
        RWRWarnTexture = GD.Load<Texture>("res://Assets/Sprites/RWRDirectionIndicator.png");
        RelativeEnemyPositions = new List<Vector3>();
        RelativeProjectilePositions = new List<Vector2>();
    }




    public void GetPlayerAndEnemyPositions()
    {
        RelativeEnemyPositions.Clear();
        RelativeProjectilePositions.Clear();
        //Node root = GetNode("/root/Game");
        currentLevel = Game.CurrentLevel;
        if (currentLevel == null)
        {
            //GetCurrentLevel();

            return;
        }

        //if()

        currentPlayer = currentLevel.player;




        Level lv = currentLevel;

        for (int i = 0; i < lv.Enemies.Count; i++)
        {


            Node n = lv.Enemies[i];
            if (!IsInstanceValid(n))
            {
                continue;
            }

            //GD.Print(i, ", Node type: ",n.GetType());

            if ((n is Character) && (n as Character).Active && ((n as Character).IsEnemy || (n as Character).IsStructure))
            {
                //Vector2 enemylevelRelativePosition = (n as Character).r - lv.GlobalPosition;
                Vector2 rel = (n as Character).LevelRelativePosition - currentPlayer.LevelRelativePosition;
                Vector3 code = new Vector3(rel.x, rel.y, (n as Character).IsStructure ? 12 : 0);

                RelativeEnemyPositions.Add(code);
            }






        }

        for (int i = 0; i < lv.NonEnemyStructures.Count; i++)
        {


            Node n = lv.NonEnemyStructures[i];
            if (!IsInstanceValid(n))
            {
                continue;
            }

            //GD.Print(i, ", Node type: ",n.GetType());

            if ((n is Character) && (n as Character).Active && !(n as Character).MissionKill && ((n as Character).IsEnemy || (n as Character).IsStructure))
            {
                //Vector2 enemylevelRelativePosition = (n as Character).r - lv.GlobalPosition;
                Vector2 rel = (n as Character).LevelRelativePosition - currentPlayer.LevelRelativePosition;
                Vector3 code = new Vector3(rel.x, rel.y, (n as Character).IsStructure ? 12 : 0);

                RelativeEnemyPositions.Add(code);
            }






        }

        if (currentPlayer == null)
        {
            return;
        }

        for (int i = 0; i < currentPlayer.IncomingProjectiles.Count; i++)
        {
            Projectile p = currentPlayer.IncomingProjectiles[i];

            if (!(IsInstanceValid(p)) || p.IsQueuedForDeletion())
            {
                continue;
            }


            RelativeProjectilePositions.Add(currentPlayer.IncomingProjectiles[i].LevelRelativePosition - currentPlayer.LevelRelativePosition);
        }

        if (lv != null)
        {

        }
    }


    public void FindTerrainElements()
    {
        if (Game.CurrentLevel == null || !Game.CurrentLevel.HasTerrain)
        {
            return;
        }

        PolygonPoints = new List<List<Vector2>>();

        //bool LevelHasTerrain = false;

        TerrainContainer terrain = null;


        for (int i = 0; i < Game.CurrentLevel.GetChildCount(); ++i)
        {
            if (Game.CurrentLevel.GetChild(i) is TerrainContainer)
            {
                terrain = Game.CurrentLevel.GetChild(i) as TerrainContainer;
            }

        }

        if (terrain == null)
        {
            return;
        }

        for (int i = 0; i < terrain.GetChildCount(); ++i)
        {
            Node n = terrain.GetChild(i);

            if (!(n is TerrainElement))
            {
                continue;
            }

            TerrainElement t = n as TerrainElement;

            if (t.AbsoluteCoordsCollisionVectors != null && t.AbsoluteCoordsCollisionVectors.Count > 0)
            {
                PolygonPoints.Add(t.AbsoluteCoordsCollisionVectors);
            }
        }

        GD.Print("Constructed Absolute Polygon Point List of length: " + PolygonPoints.Count);

        if (PolygonPoints.Count > 0)
        {
            GD.Print("Number of points in first object in list: " + PolygonPoints[0].Count);
        }

        ConstructedTerrain = true;

    }

    public void GetTerrainPositions()
    {

        if (Game.CurrentLevel == null||!Game.CurrentLevel.HasTerrain)
        {
            return;
        }

        currentPlayer = Game.CurrentLevel.player;
        
        if (PolygonPoints == null || !ConstructedTerrain )
        {
            //GD.Print("Requesting new terrain constructions");
            ConstructedTerrain = false;
            return;
        }

        if (PolygonPoints.Count == 0)
        {
           // GD.Print("cannot draw radar terrain bc there is no terraiin to draw");
            return;
        }

        if (currentPlayer == null)
        {
           // GD.Print("cannot draw radar terrain bc the player is not defined");
            return;
        }

        //GD.Print("trying terrain pos update");

        if (RelativePolygonPoints == null)
        {
            RelativePolygonPoints = new List<List<Vector2>>();
        }
        else
        {
            RelativePolygonPoints.Clear();
        }

        for(int j = 0; j < PolygonPoints.Count; ++j)
        {
            List<Vector2> poly = PolygonPoints[j];

            List<Vector2> poly2 = new List<Vector2>();

            for (int i = 0; i < poly.Count; ++i)
            {

                //GD.Print("Attempting addition of point [" + j + "][" + i + "]");

                Vector2 relativeV = ((poly[i] - currentPlayer.GlobalPosition) * 120f / (float)RadarRange);


               // relativeV.x = Mathf.Clamp(relativeV.x, -120f, 120f);
                //relativeV.y = Mathf.Clamp(relativeV.y, -120f, 120f);




                poly2.Add( relativeV + new Vector2(120f, 120f) );
            }
            RelativePolygonPoints.Add(poly2);

        }



    }
    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
  {
        if (Game.CurrentLevel == null)
        {
            ConstructedTerrain = false;
        }
        else
        {
            if (!ConstructedTerrain)
            {
                FindTerrainElements();
            }
            else
            {

            }

        }






        GetPlayerAndEnemyPositions();

        
        GetTerrainPositions();
        

       


        Update();
    }


    public void DrawTerrainOnWindow()
    {
        if (Game.CurrentLevel==null||!Game.CurrentLevel.HasTerrain||RelativePolygonPoints == null||RelativePolygonPoints.Count==0)
        {
            //GD.Print("Polygon list not valid");
            return;
        }

        for (int i = 0; i < RelativePolygonPoints.Count; ++i)
        {

           

            
            Vector2 MeanPosition = Vector2.Zero;

            for(int j = 0; j < RelativePolygonPoints[i].Count; ++j)
            {
                MeanPosition += ((RelativePolygonPoints[i][j])-new Vector2(120f, 120f));
            }

            if ((MeanPosition / (RelativePolygonPoints[i].Count)).Length()< 12000)
            {
                DrawPolygon(RelativePolygonPoints[i].ToArray(), radarPolyColors);
            }
        }


    }

    public void DrawPlayerRWRBlips()
    {
        if (currentPlayer == null)
        {
            return;
        }

        for(int i = 0; i < currentPlayer.RWRSources.Count; ++i)
        {
            Vector3 a = currentPlayer.RWRSources[i];
            Vector2 relativeRadarPos = new Vector2(a.x - currentPlayer.LevelRelativePosition.x, a.y - currentPlayer.LevelRelativePosition.y);

            float relativeRadarDirection = relativeRadarPos.Angle();



            DrawSetTransform(new Vector2(120, 120), relativeRadarDirection, Vector2.One * 0.3f);

            DrawTexture(RWRWarnTexture, new Vector2(60, -16), new Color(1, 1, 1, a.z/3f));

        }
    }

    public override void _Draw()
    {
        DrawTerrainOnWindow();
       // GD.Print(" Current amount of enemies with positions detected: ", RelativeEnemyPositions.Count);
        for(int i = 0; i < RelativeEnemyPositions.Count; ++i)
        {
            Vector3 a = RelativeEnemyPositions[i];

            Vector2 relative = new Vector2(a.x, a.y);

            float maxRadarDist = RadarRange;

            if (Math.Abs(relative.x) > maxRadarDist)
            {

                relative.x = maxRadarDist * (relative.x < 0 ? -1 : 1);
            }

            if (Math.Abs(relative.y) > maxRadarDist)
            {

                relative.y = maxRadarDist * (relative.y < 0 ? -1 : 1); ;
            }



            float rectSize = 8;

            if (a.z > 0)
            {
                rectSize = 10;
            }

            

            Vector2 drawPosition = new Vector2(120, 120) - (Vector2.One * rectSize / 2)+ new Vector2(relative.x * (RectSize.x / 2f) / maxRadarDist , relative.y * (RectSize.y / 2f) / maxRadarDist);

            if (a.z == 0)
            {
                DrawTextureRect(BlipTexture, new Rect2(drawPosition, Vector2.One * rectSize), false);
            }
            else
            {
                DrawTextureRect(BlipTextureStructure, new Rect2(drawPosition, Vector2.One * rectSize), false);
            }
            

        }

        if (currentPlayer == null)
        {
            return;
        }

        for (int i = 0; i < RelativeProjectilePositions.Count; ++i)
        {
            Vector2 a = RelativeProjectilePositions[i];

            Vector2 relative = new Vector2(a.x, a.y);

            float maxRadarDist = 10000;

            if (Math.Abs(relative.x) > maxRadarDist || Math.Abs(relative.y) > maxRadarDist)
            {
                return;
            }



            float rectSize = 4;



            Vector2 drawPosition = new Vector2(120, 120) - (Vector2.One * rectSize / 2) + new Vector2(relative.x * (RectSize.x / 2f) / maxRadarDist, relative.y * (RectSize.y / 2f) / maxRadarDist);

           // DrawSetTransform(drawPosition, relative.Angle(), Vector2.One);

            DrawTextureRect(MissileWarnTexture, new Rect2(drawPosition, Vector2.One*rectSize), false);

            

        }

        DrawPlayerRWRBlips();



    }
}
