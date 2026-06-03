using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using Tools;
using UnityEngine.SceneManagement;
using Yarn;
using Yarn.Unity;

namespace A_Apocrypha.Patches
{
    [HarmonyPatch]
    public static class YarnCommandPatch
    {
        public static OverworldManagerBG World;
        public static void GenerateShopPresent(string[] info)
        {
            World = UnityEngine.Object.FindObjectOfType<OverworldManagerBG>();
            World.StartCoroutine(ProcessPresent(BronzoPresentType.ShopItem));
        }
        public static void GenerateTreasurePresent(string[] info)
        {
            World = UnityEngine.Object.FindObjectOfType<OverworldManagerBG>();
            World.StartCoroutine(ProcessPresent(BronzoPresentType.TreasureItem));
        }
        public static void GenerateCustomPresent(string[] info)
        {
            World = UnityEngine.Object.FindObjectOfType<OverworldManagerBG>();
            World.StartCoroutine(ProcessCustomPresent(info));
        }
        public static void KillThatGuy(string[] info)
        {
            World = UnityEngine.Object.FindObjectOfType<OverworldManagerBG>();
            World.StartCoroutine(ProcessThatGuyKilling());
        }
        public static void EatenDisposition(string[] info)
        {// parameter 1: PLUS is up, MINUS will decrease only the currency, anything else will decrease both | parameter 2: amount to increase/decrease
            World = UnityEngine.Object.FindObjectOfType<OverworldManagerBG>();
            if (!World._informationHolder._game.m_Data.intGameData.ContainsKey("AA_EatenDispositionValue")) { World._informationHolder._game.m_Data.intGameData.Add("AA_EatenDispositionValue", 0); }
            if (!World._informationHolder._game.m_Data.intGameData.ContainsKey("AA_EatenDispositionCurrency")) { World._informationHolder._game.m_Data.intGameData.Add("AA_EatenDispositionCurrency", 0); }
            if (info.Length < 2) { return; }
            bool valueIsInt = Int32.TryParse(info[1], out int value);
            bool increase = (info[0] == "PLUS" ? true : false);
            bool decreaseTotal = (info[0] == "MINUS" ? false : true);
            if (valueIsInt)
            {
                if (increase)
                {
                    World._informationHolder._game.m_Data.intGameData["AA_EatenDispositionValue"] += value;
                    World._informationHolder._game.m_Data.intGameData["AA_EatenDispositionCurrency"] += value;
                }
                else
                {
                    if (decreaseTotal) { World._informationHolder._game.m_Data.intGameData["AA_EatenDispositionValue"] = Mathf.Max(0, World._informationHolder._game.m_Data.intGameData["AA_EatenDispositionValue"] - value); }
                    World._informationHolder._game.m_Data.intGameData["AA_EatenDispositionCurrency"] = Mathf.Max(0, World._informationHolder._game.m_Data.intGameData["AA_EatenDispositionCurrency"] - value);
                }
            }
            Debug.Log("Mr Eaten Disposition Handler | " + (increase ? "increased" : "decreased") + "! new values: " + World._informationHolder._game.m_Data.intGameData["AA_EatenDispositionValue"] + " (" + World._informationHolder._game.m_Data.intGameData["AA_EatenDispositionCurrency"] + ")");
            World._informationHolder._game.m_Data.NeedsToBeSaved = true;
        }

        [HarmonyPatch(typeof(RunInGameData), nameof(RunInGameData.InitializeDialogueFunctions))]
        [HarmonyPostfix]

        public static void ExtraDialogueFunctions(RunInGameData __instance, DialogueRunner_BO dialogueRunner, IRunDialogueData runData)
        {
            dialogueRunner.AddFunction("AA_GetRunStringData", 1, delegate (Value[] parameters)
            {
                Value value = parameters[0];
                return __instance.GetStringData(value.AsString);
            });
            dialogueRunner.AddFunction("AA_GetMainCharacter", 0, delegate (Value[] parameters)
            {
                return __instance.GetStringData("AA_MainCharacter");
            });
        }

        [HarmonyPatch(typeof(OverworldManagerBG), nameof(OverworldManagerBG.InitializeDialogueFunctions))]
        [HarmonyPostfix]

        public static void ExtraCommandHandlers(OverworldManagerBG __instance, DialogueRunner_BO dialogueRunner)
        {
            dialogueRunner.AddCommandHandler("AA_GiveShopItem", GenerateShopPresent);
            dialogueRunner.AddCommandHandler("AA_GiveTreasureItem", GenerateTreasurePresent);
            dialogueRunner.AddCommandHandler("AA_GiveItemByID", GenerateCustomPresent);
            dialogueRunner.AddCommandHandler("AA_Die", KillThatGuy);
            dialogueRunner.AddCommandHandler("AA_MrEatenDisposition", EatenDisposition);
        }
        public static IEnumerator ProcessPresent(BronzoPresentType type)
        {
            OverworldManagerBG World = UnityEngine.Object.FindObjectOfType<OverworldManagerBG>();
            yield return null;
            World.SaveProgress(saveRunToo: false);
            BaseWearableSO item = null;
            RunDataSO run = World._informationHolder.Run;
            ItemPoolDataBase itemPoolDB = LoadedDBsHandler.ItemPoolDB;
            switch (type)
            {
                case BronzoPresentType.TreasureItem:
                    item = itemPoolDB.TryGetPrizeItem(run.PrizesInRun, World._informationHolder.Game);
                    break;
                case BronzoPresentType.ShopItem:
                    item = itemPoolDB.TryGetShopItem(run.ShopItemsInRun, World._informationHolder.Game);
                    break;
            }
            if (item != null)
            {
                bool hasItemSpace = run.playerData.HasItemSpace;
                StringTrioData itemLocData = item.GetItemLocData();
                string text = string.Format(LocUtils.GameLoc.GetUIData(UILocID.PrizeGetLabel), itemLocData.text);
                if (!hasItemSpace)
                {
                    text = text + "\n" + LocUtils.GameLoc.GetUIData(UILocID.ItemNotEnoughSpace);
                }
                string uIData3 = LocUtils.GameLoc.GetUIData(UILocID.ContinueButton);
                ConfirmDialogReference dialogReference = new ConfirmDialogReference(text, uIData3, "", item.wearableImage, itemLocData.description);
                NtfUtils.notifications.PostNotification(Utils.showConfirmDialogNtf, null, dialogReference);
                while (dialogReference.result == DialogResult.Abort)
                {
                    yield return null;
                    NtfUtils.notifications.PostNotification(Utils.showConfirmDialogNtf, null, dialogReference);
                }
                World._soundManager.PlayOneshotSound(World._soundManager.itemGet);
                while (dialogReference.result == DialogResult.None)
                {
                    yield return null;
                }
                if (hasItemSpace)
                {
                    run.playerData.AddNewItem(item);
                }
                else
                {
                    World._extraItemMenuIsOpen = true;
                    World._extraUIHandler.OpenItemExchangeMenu(new BaseWearableSO[1] { item });
                    while (World._extraItemMenuIsOpen)
                    {
                        yield return null;
                    }
                }
            }
            World._informationHolder.Run.inGameData.SetBoolData(DataUtils.bronzoTimeTravelVar, variable: false);
            World.SaveProgress(saveRunToo: true);
        }
        public static IEnumerator ProcessCustomPresent(string[] itemIDs)
        {
            OverworldManagerBG World = UnityEngine.Object.FindObjectOfType<OverworldManagerBG>();
            yield return null;
            World.SaveProgress(saveRunToo: false);
            BaseWearableSO item = null;
            RunDataSO run = World._informationHolder.Run;
            ItemPoolDataBase itemPoolDB = LoadedDBsHandler.ItemPoolDB;
            if (itemIDs.Length == 1) { item = LoadedAssetsHandler.GetWearable(itemIDs[0]); }
            else if (itemIDs.Length >= 2)
            {
                int randomIndex = UnityEngine.Random.Range(0, itemIDs.Length);
                item = LoadedAssetsHandler.GetWearable(itemIDs[randomIndex]);
            }
            if (item != null)
            {
                bool hasItemSpace = run.playerData.HasItemSpace;
                StringTrioData itemLocData = item.GetItemLocData();
                string text = string.Format(LocUtils.GameLoc.GetUIData(UILocID.PrizeGetLabel), itemLocData.text);
                if (!hasItemSpace)
                {
                    text = text + "\n" + LocUtils.GameLoc.GetUIData(UILocID.ItemNotEnoughSpace);
                }
                string uIData3 = LocUtils.GameLoc.GetUIData(UILocID.ContinueButton);
                ConfirmDialogReference dialogReference = new ConfirmDialogReference(text, uIData3, "", item.wearableImage, itemLocData.description);
                NtfUtils.notifications.PostNotification(Utils.showConfirmDialogNtf, null, dialogReference);
                while (dialogReference.result == DialogResult.Abort)
                {
                    yield return null;
                    NtfUtils.notifications.PostNotification(Utils.showConfirmDialogNtf, null, dialogReference);
                }
                World._soundManager.PlayOneshotSound(World._soundManager.itemGet);
                while (dialogReference.result == DialogResult.None)
                {
                    yield return null;
                }
                if (hasItemSpace)
                {
                    run.playerData.AddNewItem(item);
                }
                else
                {
                    World._extraItemMenuIsOpen = true;
                    World._extraUIHandler.OpenItemExchangeMenu(new BaseWearableSO[1] { item });
                    while (World._extraItemMenuIsOpen)
                    {
                        yield return null;
                    }
                }
            }
            World._informationHolder.Run.inGameData.SetBoolData(DataUtils.bronzoTimeTravelVar, variable: false);
            World.SaveProgress(saveRunToo: true);
        }
        public static IEnumerator ProcessThatGuyKilling()
        {
            OverworldManagerBG World = UnityEngine.Object.FindObjectOfType<OverworldManagerBG>();
            yield return null;
            Debug.Log("You dare bring light to my lair? You must die!");
            SaveDataManager_2024.Delete_Run();
            SceneManager.LoadScene(World._mainMenuSceneName);
        }
    }
}
