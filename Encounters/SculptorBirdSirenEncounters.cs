using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class SculptorBirdSirenEncounters
    {
        public static void Add()
        {
            Portals.AddPortalSign("SirenBird_Sign", ResourceLoader.LoadSprite("SirenBirdTimeline", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
            EnemyEncounter_API sirenBirdMedium = new EnemyEncounter_API(0, Siren.H.SculptorBird.Med, "SirenBird_Sign")
            {
                MusicEvent = "event:/AAMusic/mudeth/DepressionShop",
                RoarEvent = LoadedAssetsHandler.GetEnemyBundle(Orph.Scrungie.Hard)._roarReference.roarEvent,
            };
            sirenBirdMedium.SimpleAddEncounter(1, "SculptorBirdSiren_EN", 1, "BirdBath_EN", 1, "Boiler_EN");
            sirenBirdMedium.SimpleAddEncounter(1, "SculptorBirdSiren_EN", 1, "BirdBath_EN", 2, "Boiler_EN");
            sirenBirdMedium.SimpleAddEncounter(1, "SculptorBirdSiren_EN", 1, "BirdBath_EN", 1, "Tumult_EN");
            sirenBirdMedium.SimpleAddEncounter(1, "SculptorBirdSiren_EN", 2, "BirdBath_EN", 1, "Tumult_EN");
            sirenBirdMedium.SimpleAddEncounter(1, "SculptorBirdSiren_EN", 1, "BirdBath_EN", 1, "Tumult_EN", 1, "TumultShell_EN");
            sirenBirdMedium.SimpleAddEncounter(1, "SculptorBirdSiren_EN", 1, "BirdBath_EN", 1, Jumble.Blue);
            sirenBirdMedium.SimpleAddEncounter(1, "SculptorBirdSiren_EN", 1, "BirdBath_EN", 1, "PetrifiedPuker_EN");
            sirenBirdMedium.SimpleAddEncounter(1, "SculptorBirdSiren_EN", 2, "BirdBath_EN", 1, "Tassnn_EN");
            sirenBirdMedium.SimpleAddEncounter(1, "SculptorBirdSiren_EN", 2, "BirdBath_EN", 1, "Tassnn_EN", 1, "WinterLantern_EN");
            if (AApocrypha.CrossMod.IntoTheAbyss)
            {
                sirenBirdMedium.SimpleAddEncounter(1, "SculptorBirdSiren_EN", 1, "BirdBath_EN", 1, Jumble.BlueGrey);
            }
            if (AApocrypha.CrossMod.HellIslandFell)
            {
                sirenBirdMedium.SimpleAddEncounter(1, "SculptorBirdSiren_EN", 1, "BirdBath_EN", 1, "Boiler_EN", 1, "OneShooter_EN");
                sirenBirdMedium.SimpleAddEncounter(1, "SculptorBirdSiren_EN", 1, "BirdBath_EN", 1, "Tassnn_EN", 1, "OneShooter_EN");
            }
            if (AApocrypha.CrossMod.StewSpecimens)
            {
                sirenBirdMedium.SimpleAddEncounter(1, "SculptorBirdSiren_EN", 1, "BirdBath_EN", 1, "Euryale_EN");
            }
            if (AApocrypha.CrossMod.Mythos)
            {
                sirenBirdMedium.SimpleAddEncounter(1, "SculptorBirdSiren_EN", 1, "BirdBath_EN", 1, "StarVampire_EN");
                sirenBirdMedium.SimpleAddEncounter(1, "SculptorBirdSiren_EN", 1, "Boiler_EN", 1, "BirdBath_EN", 1, "StarVampire_EN");
                sirenBirdMedium.SimpleAddEncounter(1, "SculptorBirdSiren_EN", 1, "BirdBath_EN", 1, "Lloigor_EN");
            }
            if (AApocrypha.CrossMod.SaltEnemies)
            {
                sirenBirdMedium.SimpleAddEncounter(1, "SculptorBirdSiren_EN", 1, "Boiler_EN", 1, "BirdBath_EN", 1, Ecstasy.Random);
            }
            sirenBirdMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToCustomZoneSelector(Siren.H.SculptorBird.Med, 8, "TheSiren_Zone1", BundleDifficulty.Medium);
        }
    }
}
