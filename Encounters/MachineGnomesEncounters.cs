using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class MachineGnomesEncounters
    {
        public static void Add()
        {
            Portals.AddPortalSign("MachineGnomes_Sign", ResourceLoader.LoadSprite("GnomesTimeline", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
            EnemyEncounter_API gnomesMedium = new EnemyEncounter_API(0, Garden.H.MachineGnomes.Med, "MachineGnomes_Sign")
            {
                MusicEvent = "event:/AAMusic/Everhood/DoYouHearGnomes",
                RoarEvent = "event:/AAEnemy/GnomesRoar",
            };
            gnomesMedium.SimpleAddEncounter(1, "MachineGnomes_EN", 2, "NextOfKin_EN");
            gnomesMedium.SimpleAddEncounter(1, "MachineGnomes_EN", 1, Enemies.Shivering, 1, "InHisImage_EN");
            gnomesMedium.SimpleAddEncounter(1, "MachineGnomes_EN", 2, "InHisImage_EN");
            gnomesMedium.SimpleAddEncounter(1, "MachineGnomes_EN", 2, "InHerImage_EN", 1, "NextOfKin_EN");
            gnomesMedium.SimpleAddEncounter(2, "MachineGnomes_EN");
            if (AApocrypha.CrossMod.IntoTheAbyss)
            {
                gnomesMedium.SimpleAddEncounter(1, "MachineGnomes_EN", 1, "Streetlight_EN");
                gnomesMedium.SimpleAddEncounter(1, "MachineGnomes_EN", 1, Enemies.Shivering, 1, Signs.Yellow);
                gnomesMedium.SimpleAddEncounter(1, "MachineGnomes_EN", 1, "InHisImage_EN", 1, Signs.Gray);
            }
            if (AApocrypha.CrossMod.SaltEnemies)
            {
                gnomesMedium.SimpleAddEncounter(1, "MachineGnomes_EN", 2, "TortureMeNot_EN");
            }
            gnomesMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector(Garden.H.MachineGnomes.Med, 10, ZoneType_GameIDs.Garden_Hard, BundleDifficulty.Medium);

            EnemyEncounter_API gnomesHard = new EnemyEncounter_API(0, Garden.H.MachineGnomes.Hard, "MachineGnomes_Sign")
            {
                MusicEvent = "event:/AAMusic/Everhood/DoYouHearGnomes",
                RoarEvent = "event:/AAEnemy/GnomesRoar",
            };
            gnomesHard.SimpleAddEncounter(1, "MachineGnomes_EN", 1, Enemies.Shivering, 1, Enemies.Skinning);
            gnomesHard.SimpleAddEncounter(1, "MachineGnomes_EN", 1, "InHisImage_EN", 2, "InHerImage_EN");
            gnomesHard.SimpleAddEncounter(1, "MachineGnomes_EN", 2, "InHisImage_EN", 1, "InHerImage_EN");
            gnomesHard.SimpleAddEncounter(2, "MachineGnomes_EN", 1, Enemies.Minister);
            gnomesHard.SimpleAddEncounter(2, "MachineGnomes_EN", 1, "SomeoneSister_EN");
            if (AApocrypha.CrossMod.GlitchsFreaks)
            {
                gnomesHard.SimpleAddEncounter(1, "MachineGnomes_EN", 1, "Vagabond_EN", 1, "NextOfKin_EN");
            }
            if (AApocrypha.CrossMod.StewSpecimens)
            {
                gnomesHard.SimpleAddEncounter(1, "MachineGnomes_EN", 1, "MonumentOfEnmity_EN");
                gnomesHard.SimpleAddEncounter(2, "MachineGnomes_EN", 1, "AloofEnvoy_EN");
                gnomesHard.SimpleAddEncounter(3, "MachineGnomes_EN", 1, "Marut_EN");
            }
            if (AApocrypha.CrossMod.IntoTheAbyss)
            {
                gnomesHard.SimpleAddEncounter(1, "MachineGnomes_EN", 1, "Fleet_EN");
                gnomesHard.SimpleAddEncounter(1, "MachineGnomes_EN", 2, "Streetlight_EN");
                gnomesHard.SimpleAddEncounter(2, "MachineGnomes_EN", 1, "Eater_Invis_EN");
                gnomesHard.SimpleAddEncounter(2, "MachineGnomes_EN", 1, "Monad_EN");
            }
            gnomesHard.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector(Garden.H.MachineGnomes.Hard, 4, ZoneType_GameIDs.Garden_Hard, BundleDifficulty.Hard);
        }
    }
}
