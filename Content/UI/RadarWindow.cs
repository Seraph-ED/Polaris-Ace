using Godot;
using System;
using System.Collections.Generic;

public class RadarWindow : Panel
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    //private Texture _texture;
    public Texture BlipTexture;
    public Texture BlipTextureStructure;
    public Texture MissileWarnTexture;

    public Level currentLevel;

    public Player currentPlayer;

    public List<Vector3> RelativeEnemyPositions;

    public List<Vector2> RelativeProjectilePositions;

    public override void _Ready()
    {
        BlipTexture = GD.Load<Texture>("res://Assets/Sprites/RadarBlip.png");
        BlipTextureStructure = GD.Load<Texture>("res://Assets/Sprites/RadarBlipSquare.png");
        MissileWarnTexture = GD.Load<Texture>("res://Assets/Sprites/MissileWarning.png");
        RelativeEnemyPositions = new List<Vector3>();
        RelativeProjectilePositions = new List<Vector2>();
    }

    public void GetCurrentLevel()
    {
        Node root = GetNode("/root/Game");

        Level lv = null;

        for (int i = 0; i < root.GetChildCount(); i++)
        {
            Node n = root.GetChild(i);

            //GD.Print(i, ", Node type: ",n.GetType());

            if ((n is Level))
            {
                lv = n as Level;
                break;
            }

        }

        currentLevel = lv;
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

            if ((n is Character)&&(n as Character).Active&&((n as Character).IsEnemy || (n as Character).IsStructure))
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

            if ((n is Character) && (n as Character).Active && ((n as Character).IsEnemy || (n as Character).IsStructure))
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

        for(int i = 0; i < currentPlayer.IncomingProjectiles.Count; i++)
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
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
        

        GetPlayerAndEnemyPositions();

        Update();
    }

    public override void _Draw()
    {

       // GD.Print(" Current amount of enemies with positions detected: ", RelativeEnemyPositions.Count);
        for(int i = 0; i < RelativeEnemyPositions.Count; ++i)
        {
            Vector3 a = RelativeEnemyPositions[i];

            Vector2 relative = new Vector2(a.x, a.y);

            float maxRadarDist = 10000;

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




    }
}
