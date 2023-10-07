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
            //var GetClass = PatchConstants.EftTypes.Single(x => x.GetMethod("GetIcon", BindingFlags.Public | BindingFlags.Instance, null, new [] { typeof(GClass2821), typeof(GStruct23).MakeByRefType() }, null) != null );
            var GetClass = typeof(GClass847);

            return GetClass.GetMethod("GetIcon", BindingFlags.Public | BindingFlags.Instance);
        }

        [PatchPrefix]
        private static bool PatchPrefix(GClass2821 clothing, ref GStruct23 textureSize)
        {
            textureSize = textureSize * Plugin.ConfClothingIconResMult.Value;

            return true;
        }
    }
}
