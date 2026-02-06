using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class DiscordantLogosEncounters
    {
        public static void Add()
        {
            Portals.AddPortalSign("BlackLogos_Sign", ResourceLoader.LoadSprite("LogosTimelineBlack", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
            EnemyEncounter_API blackLogosHard = new EnemyEncounter_API(0, Garden.H.Logos.Broken.Hard, "BlackLogos_Sign")
            {
                MusicEvent = "event:/AAMusic/MillieAmp/kcarTrorreT",
                RoarEvent = "event:/AAEnemy/LogosDisco/LogosDiscoRoar",
            };
            blackLogosHard.SimpleAddEncounter(1, Logos.Broken, 1, Enemies.Minister);
            blackLogosHard.SimpleAddEncounter(1, Logos.Broken, 1, Enemies.Skinning, 1, Enemies.Shivering);
            blackLogosHard.SimpleAddEncounter(1, Logos.Broken, 1, Enemies.Skinning, 2, Enemies.Shivering);
            blackLogosHard.SimpleAddEncounter(1, Logos.Broken, 1, "BellRinger_EN", 1, Enemies.Minister);
            if (AApocrypha.CrossMod.GlitchsFreaks)
            {
                blackLogosHard.SimpleAddEncounter(1, Logos.Broken, 1, "BellRinger_EN", 1, "FrowningChancellor_EN");
            }
            if (AApocrypha.MoonData.Visibility < 40f)
            {
                blackLogosHard.SimpleAddEncounter(1, Logos.Broken, 1, "ChoirBoy_EN", 1, Enemies.Minister);
            }
            blackLogosHard.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector(Garden.H.Logos.Broken.Hard, 6, ZoneType_GameIDs.Garden_Hard, BundleDifficulty.Hard); // 5
        }
    }
}
