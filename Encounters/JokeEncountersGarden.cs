using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class JokeEncountersGarden
    {
        //the idea behind these is mostly to help counteract the To the Word, not the Spirit of the Law spam during randomizer runs
        //most of these aren't used yet! but they may be someday... >:)
        public static void Add()
        {
            Portals.AddPortalSign("JokeGarden_Sign", ResourceLoader.LoadSprite("DuneThresherTimelineWhy", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);

            EnemyEncounter_API DiscordantJoke = new EnemyEncounter_API(0, "H_Zone03_Discordance_Joke_EnemyBundle", "JokeGarden_Sign")
            {
                MusicEvent = "event:/AAMusic/ProjectMoon/BlueStar",
                RoarEvent = "event:/AAEnemy/LogosDisco/LogosDiscoRoar",
            };
            DiscordantJoke.SimpleAddEncounter(4, Logos.Broken);
            DiscordantJoke.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector("H_Zone03_Discordance_Joke_EnemyBundle", 0, ZoneType_GameIDs.Garden_Hard, BundleDifficulty.Hard);

            EnemyEncounter_API ShadowdanceJoke = new EnemyEncounter_API(0, "H_Zone03_Shadowdance_Joke_EnemyBundle", "JokeGarden_Sign")
            {
                MusicEvent = "event:/AAMusic/mudeth/Shadowdance",
                RoarEvent = "event:/AAEnemy/Phobias/PhobiasRoar",
            };
            ShadowdanceJoke.SimpleAddEncounter(3, "Phobia_Death_EN");
            ShadowdanceJoke.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector("H_Zone03_Shadowdance_Joke_EnemyBundle", 0, ZoneType_GameIDs.Garden_Hard, BundleDifficulty.Hard);

            EnemyEncounter_API GnomesJoke = new EnemyEncounter_API(0, "H_Zone03_Gnomes_Joke_EnemyBundle", "JokeGarden_Sign")
            {
                MusicEvent = "event:/AAMusic/Everhood/YouWantGnomes",
                RoarEvent = "event:/AAEnemy/GnomesRoar",
            };
            GnomesJoke.SimpleAddEncounter(5, "MachineGnomes_EN");
            GnomesJoke.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector("H_Zone03_Gnomes_Joke_EnemyBundle", 0, ZoneType_GameIDs.Garden_Hard, BundleDifficulty.Hard);

            EnemyEncounter_API ChestJoke = new EnemyEncounter_API(0, "H_Zone03_TheChestSimulator_Joke_EnemyBundle", "JokeGarden_Sign")
            {
                MusicEvent = "event:/AAMusic/EXCELSIOR/HungarianRhapsody2",
                RoarEvent = "event:/AASFX/Nothing_SFX",
            };
            ChestJoke.SimpleAddEncounter(1, "Roids_BOSS", 1, "Scrungie_EN", 2, "Macerator_EN");
            ChestJoke.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector("H_Zone03_TheChestSimulator_Joke_EnemyBundle", 0, ZoneType_GameIDs.Garden_Hard, BundleDifficulty.Hard);
        }
    }
}
