using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class SistersEncounters
    {
        public static void Add()
        {
            bool bright = false;
            double moonVisibility = AApocrypha.MoonData.Visibility;
            if (moonVisibility > 50) { bright = true; }
            string primarySister = bright ? "SomeoneSister_EN" : "NooneSister_EN";
            string secondarySister = bright ? "NooneSister_EN" : "SomeoneSister_EN";
            Portals.AddPortalSign("Sisters_Sign", ResourceLoader.LoadSprite((bright ? "SomeoneSisterOverworld" : "NooneSisterOverworld"), new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
            EnemyEncounter_API sistersMedium = new EnemyEncounter_API(0, Garden.H.Sisters.Med, "Sisters_Sign")
            {
                MusicEvent = "event:/AAMusic/EXCELSIOR/BelowZion",
                RoarEvent = "event:/AAEnemy/SistersRoar",
            };
            sistersMedium.SimpleAddEncounter(1, primarySister, 2, "NextOfKin_EN");
            sistersMedium.SimpleAddEncounter(1, primarySister, 1, "NextOfKin_EN", 1, "InHisImage_EN");
            sistersMedium.SimpleAddEncounter(1, primarySister, 1, "NextOfKin_EN", 1, "InHerImage_EN");
            sistersMedium.SimpleAddEncounter(1, primarySister, 2, "MachineGnomes_EN");
            sistersMedium.SimpleAddEncounter(1, primarySister, 1, secondarySister);
            if (AApocrypha.CrossMod.IntoTheAbyss)
            {
                sistersMedium.SimpleAddEncounter(1, primarySister, 1, secondarySister, 1, "Monad_EN");
                sistersMedium.SimpleAddEncounter(2, primarySister, 1, "NextOfKin_EN", 1, Signs.Blue);
                sistersMedium.SimpleAddEncounter(2, primarySister, 1, "NextOfKin_EN", 1, Signs.Grey);
            }
            if (AApocrypha.CrossMod.SaltEnemies)
            {
                sistersMedium.SimpleAddEncounter(1, primarySister, 1, secondarySister, 1, "Damocles_EN");
                sistersMedium.SimpleAddEncounter(2, primarySister, 1, Flower.Blue, 1, "PawnA_EN");
            }
            if (AApocrypha.CrossMod.StewSpecimens)
            {
                sistersMedium.SimpleAddEncounter(1, primarySister, 1, secondarySister, 1, "KapteynAbductor_EN");
                sistersMedium.SimpleAddEncounter(1, primarySister, 1, "AloofEnvoy_EN", 1, secondarySister);
                sistersMedium.SimpleAddEncounter(1, primarySister, 2, "KapteynAbductor_EN");
                sistersMedium.SimpleAddEncounter(1, primarySister, 1, "TravellingBard_G_EN");
                sistersMedium.SimpleAddEncounter(1, primarySister, 1, secondarySister, 1, "MonumentOfEnmity_EN");
                sistersMedium.SimpleAddEncounter(1, primarySister, 2, "ShiveringHomunculus_EN", 1, "Key_EN");
                if (AApocrypha.CrossMod.UndivineComedy)
                {
                    sistersMedium.SimpleAddEncounter(1, primarySister, 1, secondarySister, 1, "Key_EN", 1, "BellRinger_EN");
                    sistersMedium.SimpleAddEncounter(1, primarySister, 1, "AloofEnvoy_EN", 1, "BellRinger_EN");
                }
            }
            sistersMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector(Garden.H.Sisters.Med, 11, ZoneType_GameIDs.Garden_Hard, BundleDifficulty.Medium);

            EnemyEncounter_API sistersHard = new EnemyEncounter_API(0, Garden.H.Sisters.Hard, "Sisters_Sign")
            {
                MusicEvent = "event:/AAMusic/EXCELSIOR/BelowZion",
                RoarEvent = "event:/AAEnemy/SistersRoar",
            };
            sistersHard.SimpleAddEncounter(1, primarySister, 2, "GigglingMinister_EN");
            sistersHard.SimpleAddEncounter(1, primarySister, 2, "InHisImage_EN", 1, "NextOfKin_EN");
            sistersHard.SimpleAddEncounter(1, primarySister, 2, "InHerImage_EN", 1, "NextOfKin_EN");
            sistersHard.SimpleAddEncounter(2, primarySister, 2, "MachineGnomes_EN");
            sistersHard.SimpleAddEncounter(1, primarySister, 1, secondarySister);
            if (AApocrypha.CrossMod.IntoTheAbyss)
            {
                sistersHard.SimpleAddEncounter(1, primarySister, 1, secondarySister, 1, "Monad_EN");
                sistersHard.SimpleAddEncounter(2, primarySister, 1, "SkinningHomunculus_EN", 1, Signs.Blue);
                sistersHard.SimpleAddEncounter(2, primarySister, 1, "SkinningHomunculus_EN", 1, Signs.Grey);
            }
            if (AApocrypha.CrossMod.SaltEnemies)
            {
                sistersHard.SimpleAddEncounter(1, primarySister, 1, secondarySister, 1, "MiniReaper_EN");
                sistersHard.SimpleAddEncounter(2, primarySister, 1, Flower.Blue, 1, Flower.Red);
            }
            if (AApocrypha.CrossMod.MarmoEnemies)
            {
                sistersHard.SimpleAddEncounter(1, primarySister, 1, secondarySister, 1, "Bonsai_EN");
                sistersHard.SimpleAddEncounter(2, primarySister, 1, "Git_EN", 1, "NextOfKin_EN");
            }
            sistersHard.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector(Garden.H.Sisters.Hard, 6, ZoneType_GameIDs.Garden_Hard, BundleDifficulty.Hard);
        }
    }
}
