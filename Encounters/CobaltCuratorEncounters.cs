using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class CobaltCuratorEncounters
    {
        public static void Add()
        {
            Portals.AddPortalSign("CobaltCurator_Sign", ResourceLoader.LoadSprite("CobaltCuratorTimeline", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
            EnemyEncounter_API curatorMedium = new EnemyEncounter_API(0, Orph.H.CobaltCurator.Med, "CobaltCurator_Sign")
            {
                MusicEvent = "event:/AAMusic/AnAxe/HarmfulIfInhaled",
                RoarEvent = "event:/AAEnemy/SandSifterRoar",
            };
            curatorMedium.SimpleAddEncounter(1, "CobaltCurator_EN", 2, "MusicMan_EN");
            curatorMedium.SimpleAddEncounter(1, "CobaltCurator_EN", 1, "MusicMan_EN", 1, "Scrungie_EN");
            curatorMedium.SimpleAddEncounter(1, "CobaltCurator_EN", 1, "MusicMan_EN", 1, Aggregates.Yellow);
            curatorMedium.SimpleAddEncounter(1, "CobaltCurator_EN", 1, "MusicMan_EN", 3, "Blemmigan_EN");
            curatorMedium.SimpleAddEncounter(1, "CobaltCurator_EN", 1, "MusicMan_EN", 1, Jumble.Blue);
            curatorMedium.SimpleAddEncounter(1, "CobaltCurator_EN", 1, "MusicMan_EN", 1, "SingingStone_EN", 1, Jumble.Purple);
            if (AApocrypha.CrossMod.pigmentRainbow)
            {
                curatorMedium.SimpleAddEncounter(1, "CobaltCurator_EN", 1, "MusicMan_EN", 1, Jumble.Rainbow);
            }
            if (AApocrypha.CrossMod.GlitchsFreaks)
            {
                curatorMedium.SimpleAddEncounter(1, "CobaltCurator_EN", 2, Frostbites.Normal);
                curatorMedium.SimpleAddEncounter(1, "CobaltCurator_EN", 1, "MusicMan_EN", 1, "BackupDancer_EN");
            }
            if (AApocrypha.CrossMod.EnemyPack)
            {
                curatorMedium.SimpleAddEncounter(1, "CobaltCurator_EN", 2, "Chapman_EN");
                curatorMedium.SimpleAddEncounter(1, "CobaltCurator_EN", 1, "MusicMan_EN", 1, "Gizo_EN");
                curatorMedium.SimpleAddEncounter(1, "CobaltCurator_EN", 1, "MusicMan_EN", 1, "Neoplasm_EN");
            }
            if (AApocrypha.CrossMod.BismuthBoiler)
            {
                curatorMedium.SimpleAddEncounter(1, "CobaltCurator_EN", 2, "FerrousFeaster_EN");
                curatorMedium.SimpleAddEncounter(1, "CobaltCurator_EN", 1, "AluminumAlchemist_EN", 2, Enemies.Suckle);
            }
            if (AApocrypha.CrossMod.IntoTheAbyss)
            {
                curatorMedium.SimpleAddEncounter(1, "CobaltCurator_EN", 1, "Mistaken_EN", 1, "Mistake_EN");
                curatorMedium.SimpleAddEncounter(1, "CobaltCurator_EN", 1, "Scrungie_EN", 1, "Fanatic_EN");
            }
            curatorMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector(Orph.H.CobaltCurator.Med, 9, ZoneType_GameIDs.Orpheum_Hard, BundleDifficulty.Medium);
        }
    }
}
