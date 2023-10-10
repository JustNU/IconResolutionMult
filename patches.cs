using Aki.Reflection.Patching;
using Aki.Reflection.Utils;
using System.Reflection;
using System.Linq;
using UnityEngine;
using Comfort.Common;
using EFT.InventoryLogic;
using EFT.UI.DragAndDrop;
using EFT;
using EFT.UI;
using System;


namespace IconResolutionMult
{
    public class itemIconPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            Logger.LogInfo("itemIconPatch");
            
            var GetClass = PatchConstants.EftTypes.Single(x => x.GetMethod("LoadItemIcon", BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Static) != null);

            return GetClass.GetMethod("LoadItemIcon", BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Static);
        }

        [PatchPrefix]
        private static bool PatchPrefix(Item item, ref int scaleFactor, bool forcedGeneration)
        {
            scaleFactor = scaleFactor * Plugin.ConfItemIconResMult.Value;

            return true;
        }
    }

    public class ClothingIconPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            Logger.LogInfo("ClothingIconPatch");

            var GetClass = typeof(GClass736);

            return GetClass.GetMethod("GetIcon", BindingFlags.Public | BindingFlags.Instance);
        }

        [PatchPrefix]
        private static bool PatchPrefix(GClass2736 clothing, ref GStruct23 textureSize)
        {

            textureSize = textureSize * Plugin.ConfClothingIconResMult.Value;

            return true;
        }
    }

    public class PlayerIconPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            Logger.LogInfo("PlayerIconPatch");

            var GetClass = typeof(GClass738);

            return GetClass.GetMethod("method_11", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        [PatchPrefix]
        private static bool PatchPrefix(GClass743 iconRequest, ref GStruct23 textureSize)
        {
            textureSize = textureSize * Plugin.ConfPlayerIconResMult.Value;

            return true;
        }
    }
}
