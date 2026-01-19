using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class HazardHaulerSirenEncounters
    {
        public static void Add()
        {
            Portals.AddPortalSign("HazardHauler_Sign", ResourceLoader.LoadSprite("HazardHaulerTimeline", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
            EnemyEncounter_API hazardHaulerSirenMedium = new EnemyEncounter_API(0, Siren.H.HazardHauler.Med, "HazardHauler_Sign")
            {
                MusicEvent = "event:/AAMusic/AnAxe/HarmfulIfInhaled",
                RoarEvent = "event:/AAEnemy/SandSifterRoar",
            };
            hazardHaulerSirenMedium.SimpleAddEncounter(1, "HazardHauler_Siren_EN", 1, "SandSifter_EN", 2, "BirdBath_EN");
            hazardHaulerSirenMedium.SimpleAddEncounter(1, "HazardHauler_Siren_EN", 1, "Boiler_EN", 1, "BirdBath_EN");
            hazardHaulerSirenMedium.SimpleAddEncounter(1, "HazardHauler_Siren_EN", 1, "Boiler_EN", 1, "BirdBath_EN", 1, "Tassnn_EN");
            hazardHaulerSirenMedium.SimpleAddEncounter(2, "HazardHauler_Siren_EN", 2, "BirdBath_EN");
            if (AApocrypha.CrossMod.SaltEnemies)
            {
                hazardHaulerSirenMedium.SimpleAddEncounter(1, "HazardHauler_Siren_EN", 1, "Boiler_EN", 1, "BirdBath_EN", 1, Ecstasy.Random);
            }
            if (AApocrypha.CrossMod.IntoTheAbyss)
            {
                hazardHaulerSirenMedium.SimpleAddEncounter(1, "HazardHauler_Siren_EN", 1, "Tassnn_EN", 1, "Orphan_EN");
                if (AApocrypha.CrossMod.EnemyPack)
                {
                    hazardHaulerSirenMedium.SimpleAddEncounter(1, "HazardHauler_Siren_EN", 1, "Erelim_EN", 1, "BirdBath_EN");
                }
            }
            if (AApocrypha.CrossMod.Mythos)
            {
                hazardHaulerSirenMedium.SimpleAddEncounter(1, "HazardHauler_Siren_EN", 1, "Boiler_EN", 1, "StarVampire_EN");
            }
            hazardHaulerSirenMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToCustomZoneSelector(Siren.H.HazardHauler.Med, 6, "TheSiren_Zone1", BundleDifficulty.Medium); //6
        }
    }
}
