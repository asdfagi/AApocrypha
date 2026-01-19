using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class WinterLanternEncounters
    {
        public static void Add()
        {
            Portals.AddPortalSign("WinterLantern_Sign", ResourceLoader.LoadSprite("WinterLanternTimeline", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
            EnemyEncounter_API winterLanternMedium = new EnemyEncounter_API(0, Siren.H.WinterLantern.Med, "WinterLantern_Sign")
            {
                MusicEvent = "event:/AAMusic/EXCELSIOR/MartyrsTribunal",
                RoarEvent = LoadedAssetsHandler.GetEnemy("SilverSuckle_EN").deathSound,
            };
            winterLanternMedium.SimpleAddEncounter(1, "WinterLantern_EN", 1, "BirdBath_EN", 1, "Boiler_EN");
            winterLanternMedium.SimpleAddEncounter(1, "WinterLantern_EN", 2, "BirdBath_EN");
            winterLanternMedium.SimpleAddEncounter(1, "WinterLantern_EN", 1, "Tumult_EN", 1, "TumultShell_EN");
            winterLanternMedium.SimpleAddEncounter(1, "WinterLantern_EN", 1, "Olmic_EN");
            winterLanternMedium.SimpleAddEncounter(1, "WinterLantern_EN", 1, "BirdBath_EN", 1, "Tassnn_EN");
            winterLanternMedium.SimpleAddEncounter(1, "WinterLantern_EN", 1, "Boiler_EN", 1, "Tassnn_EN");
            winterLanternMedium.SimpleAddEncounter(1, "WinterLantern_EN", 1, "BirdBath_EN", 2, "PetrifiedPuker_EN");
            if (AApocrypha.CrossMod.StewSpecimens)
            {
                winterLanternMedium.SimpleAddEncounter(1, "WinterLantern_EN", 1, "BirdBath_EN", 1, "Euryale_EN");
            }
            if (AApocrypha.CrossMod.IntoTheAbyss)
            {
                winterLanternMedium.SimpleAddEncounter(1, "WinterLantern_EN", 2, "Orphan_EN");
            }
            if (AApocrypha.CrossMod.SaltEnemies)
            {
                winterLanternMedium.SimpleAddEncounter(1, "WinterLantern_EN", 1, Ecstasy.Random, 1, "Boiler_EN");
                winterLanternMedium.SimpleAddEncounter(1, "WinterLantern_EN", 1, Ecstasy.Random, 1, "Tassnn_EN");
            }
            winterLanternMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToCustomZoneSelector(Siren.H.WinterLantern.Med, 8, "TheSiren_Zone1", BundleDifficulty.Medium);
        }
    }
}
