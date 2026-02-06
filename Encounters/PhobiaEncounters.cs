using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class PhobiaEncounters
    {
        public static void Add()
        {
            Portals.AddPortalSign("Phobia_Sign", ResourceLoader.LoadSprite("PhobiaVerticalTimeline", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
            EnemyEncounter_API phobiasMed = new EnemyEncounter_API(0, Garden.H.Phobia.Med, "Phobia_Sign")
            {
                MusicEvent = "event:/AAMusic/mudeth/Drowning",
                RoarEvent = "event:/AAEnemy/Phobias/PhobiasRoar",
            };
            phobiasMed.SimpleAddEncounter(1, "Phobia_Phobias_EN", 1, "InHisImage_EN", 1, "InHerImage_EN");
            phobiasMed.SimpleAddEncounter(2, "Phobia_Phobias_EN");
            phobiasMed.SimpleAddEncounter(1, "Phobia_Phobias_EN", 1, "InHisImage_EN");
            phobiasMed.SimpleAddEncounter(1, "Phobia_Phobias_EN", 2, "NextOfKin_EN");
            phobiasMed.SimpleAddEncounter(1, "Phobia_Phobias_EN", 2, "MachineGnomes_EN");
            if (AApocrypha.CrossMod.SaltEnemies)
            {
                phobiasMed.SimpleAddEncounter(2, "Phobia_Phobias_EN", 1, "Damocles_EN");
                phobiasMed.SimpleAddEncounter(1, "Phobia_Phobias_EN", 1, Bots.Grey);
                phobiasMed.SimpleAddEncounter(2, "Phobia_Phobias_EN", 1, "BlackStar_EN");
            }
            if (AApocrypha.CrossMod.IntoTheAbyss)
            {
                phobiasMed.SimpleAddEncounter(2, "Phobia_Phobias_EN", 1, Signs.Red);
                phobiasMed.SimpleAddEncounter(1, "Phobia_Phobias_EN", 1, Enemies.Shivering, 1, Symbols.Yellow);
                phobiasMed.SimpleAddEncounter(1, "Phobia_Phobias_EN", 1, Jumble.Entropic);
                phobiasMed.SimpleAddEncounter(1, "Phobia_Phobias_EN", 1, Jumble.Irid);
                phobiasMed.SimpleAddEncounter(1, "Phobia_Phobias_EN", 1, Spoggle.Irid);
            }
            phobiasMed.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector(Garden.H.Phobia.Med, 9, ZoneType_GameIDs.Garden_Hard, BundleDifficulty.Medium);

            EnemyEncounter_API phobiasHard = new EnemyEncounter_API(0, Garden.H.Phobia.Hard, "Phobia_Sign")
            {
                MusicEvent = "event:/AAMusic/mudeth/Drowning",
                RoarEvent = "event:/AAEnemy/Phobias/PhobiasRoar",
            };
            phobiasHard.SimpleAddEncounter(3, "Phobia_Phobias_EN");
            phobiasHard.SimpleAddEncounter(2, "Phobia_Phobias_EN", 1, Enemies.Minister);
            phobiasHard.SimpleAddEncounter(2, "Phobia_Phobias_EN", 1, "ChoirBoy_EN");
            phobiasHard.SimpleAddEncounter(1, "Phobia_Phobias_EN", 2, Enemies.Minister);
            if (AApocrypha.MoonData.Visibility >= 50f) {phobiasHard.SimpleAddEncounter(2, "Phobia_Phobias_EN", 2, "SomeoneSister_EN");}
            else {phobiasHard.SimpleAddEncounter(2, "Phobia_Phobias_EN", 2, "NooneSister_EN");}
            if (AApocrypha.CrossMod.SaltEnemies)
            {
                phobiasHard.SimpleAddEncounter(2, "Phobia_Phobias_EN", 1, "MiniReaper_EN");
                phobiasHard.SimpleAddEncounter(2, "Phobia_Phobias_EN", 1, "Grandfather_EN");
                phobiasHard.SimpleAddEncounter(2, "Phobia_Phobias_EN", 1, "ClockTower_EN");
            }
            if (AApocrypha.CrossMod.HellIslandFell)
            {
                phobiasHard.SimpleAddEncounter(2, "Phobia_Phobias_EN", 1, Noses.Red, 1, Enemies.Minister);
            }
            phobiasHard.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector(Garden.H.Phobia.Hard, 7, ZoneType_GameIDs.Garden_Hard, BundleDifficulty.Hard);
        }
    }
}
