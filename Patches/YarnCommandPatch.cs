using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using Tools;
using Yarn;
using Yarn.Unity;

namespace A_Apocrypha.Patches
{
    [HarmonyPatch]
    public static class YarnCommandPatch
    {
        [HarmonyPatch(typeof(RunInGameData), nameof(RunInGameData.InitializeDialogueFunctions))]
        [HarmonyPostfix]

        public static void ExtraDialogueFunctions(RunInGameData __instance, DialogueRunner_BO dialogueRunner, IRunDialogueData runData)
        {
            dialogueRunner.AddFunction("AA_GetRunStringData", 1, delegate (Value[] parameters)
            {
                Value value = parameters[0];
                return __instance.GetStringData(value.AsString);
            });
        }
        /*public static void GenerateShopItemPresent(string[] info)
        {
            OverworldManagerBG World = UnityEngine.Object.FindObjectOfType<OverworldManagerBG>();
            World.StartCoroutine(ProcessPresent(BronzoPresentType.ShopItem));
        }
        public static void GenerateTreasureItemPresent(string[] info)
        {
            OverworldManagerBG World = UnityEngine.Object.FindObjectOfType<OverworldManagerBG>();
            World.StartCoroutine(ProcessPresent(BronzoPresentType.TreasureItem));
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
        public static void InitializeCustomDialogueFunctions(Action<OverworldManagerBG, DialogueRunner_BO> orig, OverworldManagerBG self, DialogueRunner_BO dialogueRunner)
        {
            Debug.Log("CustomDialogueFunctions | hey guess what we're here somehoww");
            orig(self, dialogueRunner);

            Setup(dialogueRunner, self);
        }
        public static void Setup(DialogueRunner_BO runner, OverworldManagerBG world)
        {
            runner.AddCommandHandler("AA_GiftShopItem", GenerateShopItemPresent);
            runner.AddCommandHandler("AA_GiftTreasureItem", GenerateTreasureItemPresent);
        }*/
    }
}
