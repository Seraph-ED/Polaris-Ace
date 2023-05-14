using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Content;
using Godot;

namespace ShmupGame.Content.Entities.Enemies.Ships
{
    public class MoskvaDeckGun : BasicShipDeckGun
    {

        public int Barrel = 0;

        public PackedScene shellScene = GD.Load<PackedScene>("res://Content/Entities/Projectiles/FragShell.tscn");

        public override void SpawnBullets()
        {
            Barrel++;
            Barrel = Barrel % 2;

           if(HasNode("Barrels")&&GetNode("Barrels").GetChild(Barrel) is MoskvaGunBarrelAnim)
            {
                (GetNode("Barrels").GetChild(Barrel) as MoskvaGunBarrelAnim).Recoil = 1f;
            }

            Vector2 vel = Velocity;


            float offset = Mathf.Deg2Rad(45);

            // Game.CurrentLevel.SpawnProjectile(scene, LevelRelativePosition + new Vector2(15, 0).Rotated(Rotation), vel + new Vector2(20, 0).Rotated(Rotation-offset), Rotation-offset + (Mathf.Pi / 2f), BulletDamage);
            Game.CurrentLevel.SpawnProjectile(shellScene, LevelRelativePosition + new Vector2(15, 0).Rotated(Rotation), vel + new Vector2(20, 0).Rotated(Rotation), Rotation, BulletDamage);
            // Game.CurrentLevel.SpawnProjectile(scene, LevelRelativePosition + new Vector2(15, 0).Rotated(Rotation), vel + new Vector2(20, 0).Rotated(Rotation+offset), Rotation+offset + (Mathf.Pi / 2f), BulletDamage);
            

            if (HasNode("Gunfire")&&GetNode("Gunfire") is Particles2D)
            {
                (GetNode("Gunfire") as Particles2D).Emitting = true;
            }



        }

       
    }

}
