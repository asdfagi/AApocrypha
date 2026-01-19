using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class AcolyteFarShoreEncounters
    {
        public static void Add()
        {
            Portals.AddPortalSign("Acolyte_Sign", ResourceLoader.LoadSprite("AcolyteTimeline", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);

            EnemyEncounter_API acolyteEasy = new EnemyEncounter_API(EncounterType.Random, Shore.H.Acolyte.Easy, "Acolyte_Sign")
            {
                MusicEvent = "event:/AAMusic/FinalFantasy/DarkTower",
                RoarEvent = "event:/Characters/Enemies/InHisImage/CHR_ENM_InHisImage_Roar",
            };
            acolyteEasy.SimpleAddEncounter(1, "Acolyte_EN");
            acolyteEasy.SimpleAddEncounter(1, "Acolyte_EN", 1, "Mung_EN");
            acolyteEasy.SimpleAddEncounter(1, "Acolyte_EN", 2, "Mung_EN");
            acolyteEasy.SimpleAddEncounter(1, "Acolyte_EN", 1, "MudLung_EN", 1, "Mung_EN");
            acolyteEasy.SimpleAddEncounter(1, "Acolyte_EN", 1, "Keko_EN");
            if (AApocrypha.CrossMod.GlitchsFreaks)
            {
                acolyteEasy.SimpleAddEncounter(1, "Acolyte_EN", 1, "Flakkid_EN");
                acolyteEasy.SimpleAddEncounter(1, "Acolyte_EN", 1, "Enno_EN");
            };
            if (AApocrypha.CrossMod.GlitchsFreaks)
            {
                acolyteEasy.SimpleAddEncounter(1, "Acolyte_EN", 1, "Minana_EN");
                acolyteEasy.SimpleAddEncounter(1, "Acolyte_EN", 1, "Pinano_EN");
                acolyteEasy.SimpleAddEncounter(1, "Acolyte_EN", 1, "Wall_EN");
            };
            acolyteEasy.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector(Shore.H.Acolyte.Easy, 6, ZoneType_GameIDs.FarShore_Hard, BundleDifficulty.Easy);

            EnemyEncounter_API acolyteMedium = new EnemyEncounter_API(EncounterType.Random, Shore.H.Acolyte.Med, "Acolyte_Sign")
            {
                MusicEvent = "event:/AAMusic/FinalFantasy/DarkTower",
                RoarEvent = "event:/Characters/Enemies/InHisImage/CHR_ENM_InHisImage_Roar",
            };
            acolyteMedium.SimpleAddEncounter(1, "Acolyte_EN", 2, "MudLung_EN");
            acolyteMedium.SimpleAddEncounter(1, "Acolyte_EN", 1, "MudLung_EN", 1, "MunglingMudLung_EN");
            acolyteMedium.SimpleAddEncounter(1, "Acolyte_EN", 1, "Wringle_EN", 1, "Mung_EN");
            acolyteMedium.SimpleAddEncounter(1, "Acolyte_EN", 1, "TearDrinker_EN", 1, "Mung_EN");
            acolyteMedium.SimpleAddEncounter(1, "Acolyte_EN", 1, Spoggle.Blue);
            acolyteMedium.SimpleAddEncounter(2, "Acolyte_EN", 1, "Mung_EN");
            if (AApocrypha.CrossMod.Colophons)
            {
                acolyteMedium.SimpleAddEncounter(1, "Acolyte_EN", 1, Colophon.Blue);
                acolyteMedium.SimpleAddEncounter(1, "Acolyte_EN", 1, Colophon.Red);
                acolyteMedium.SimpleAddEncounter(1, "Acolyte_EN", 1, Colophon.RedBlueSplit);
            }
            if (AApocrypha.CrossMod.SaltEnemies)
            {
                acolyteMedium.SimpleAddEncounter(1, "Acolyte_EN", 1, "Chiito_EN");
                acolyteMedium.SimpleAddEncounter(1, "Acolyte_EN", 1, "Arceles_EN");
            }
            if (AApocrypha.CrossMod.Mythos)
            {
                acolyteMedium.SimpleAddEncounter(1, "Acolyte_EN", 1, "MudLung_EN", 1, "Madman_EN");
                acolyteMedium.SimpleAddEncounter(1, "Acolyte_EN", 1, "MudLung_EN", 1, "RatThing_EN");
                acolyteMedium.SimpleAddEncounter(2, "Acolyte_EN", 1, "Madman_EN");
            }
            acolyteMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector(Shore.H.Acolyte.Med, 20, ZoneType_GameIDs.FarShore_Hard, BundleDifficulty.Medium); //default: 20
        }
    }
}
