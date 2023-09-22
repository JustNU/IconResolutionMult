using BepInEx;
using BepInEx.Configuration;
using System;
using System.IO;
using UnityEngine;


namespace IconResolutionMult
{
    [BepInPlugin("com.JustNU.IconResolutionMult", "JustNU-IconResolutionMult", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        // create config ent
        public static ConfigEntry<int> ConfItemIconResMult;
        public static ConfigEntry<int> ConfClothingIconResMult;
        public static ConfigEntry<bool> ConfCleanIconCache;
        public static ConfigEntry<bool> ConfModFuncEnabled;

        private void Awake()
        {
            // set up config
            ConfCleanIconCache = Config.Bind("Core", "Clean Icon Cache on start-up", false, "Cleans icon cache");
            ConfItemIconResMult = Config.Bind("Values", "Item Icon Resolution Multiplier", 1, new ConfigDescription("Set multiplier for item icon's png resolution", new AcceptableValueRange<int>(1, 8)));
            ConfClothingIconResMult = Config.Bind("Values", "Clothing Resolution Multiplier", 1, new ConfigDescription("Set multiplier for clothing icon's png resolution", new AcceptableValueRange<int>(1, 20)));

            // Plugin startup logic
            Logger.LogInfo($"Plugin com.JustNU.IconResolutionMult is loading");

            // clean icon cache
            if (Plugin.ConfCleanIconCache.Value == true) 
            {
                System.IO.DirectoryInfo IconCachePath = new DirectoryInfo(Path.Combine(Application.temporaryCachePath, "Icon Cache\\live"));
                System.IO.DirectoryInfo ClothingCachePath = new DirectoryInfo(Path.Combine(Application.temporaryCachePath, "Icon Cache\\live\\Clothing"));

                foreach (FileInfo file in IconCachePath.GetFiles())
                {
                    file.Delete();
                }

                foreach (FileInfo file in ClothingCachePath.GetFiles())
                {
                    file.Delete();
                }
            }

            new itemIconPatch().Enable();
            //new ClothingIconPatch().Enable();
        }
    }
}
