using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class SculptorBirdEncounters
    {   
        public static void Add()
        {
            Portals.AddPortalSign("SculptorBird_Sign", ResourceLoader.LoadSprite("SculptorBirdTimeline", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
            EnemyEncounter_API sculptorBirdMedium = new EnemyEncounter_API(0, "H_Zone02_SculptorBird_Medium_EnemyBundle", "SculptorBird_Sign")
            {
                MusicEvent = "event:/AAMusic/DepressionShop",
                RoarEvent = "event:/Characters/Enemies/DLC_01/Scrungie/CHR_ENM_Scrungie_Roar",
            };
            sculptorBirdMedium.CreateNewEnemyEncounterData(
                [
                    "SculptorBird_EN",
                    "SculptorBirdSculpture_EN",
                    "MusicMan_EN",
                ], null);
            sculptorBirdMedium.CreateNewEnemyEncounterData(
                [
                    "SculptorBird_EN",
                    "SculptorBirdSculpture_EN",
                    "MusicMan_EN",
                    "MusicMan_EN",
                ], null);
            sculptorBirdMedium.CreateNewEnemyEncounterData(
                [
                    "SculptorBird_EN",
                    "SculptorBirdSculpture_EN",
                    "Spoggle_Ruminating_EN",
                    "Jumbleguts_Clotted_EN",
                ], null);
            sculptorBirdMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector("H_Zone02_SculptorBird_Medium_EnemyBundle", 10, ZoneType_GameIDs.Orpheum_Hard, BundleDifficulty.Medium);
        }
    }
}
