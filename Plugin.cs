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
        public static ConfigEntry<int> ConfPlayerIconResMult;

        private void Awake()
        {
            // set up config
            ConfItemIconResMult = Config.Bind("Values", "Item Icon Resolution Multiplier", 1, new ConfigDescription("Set multiplier for item icon png resolution", new AcceptableValueRange<int>(1, 8)));
            ConfClothingIconResMult = Config.Bind("Values", "Clothing Resolution Multiplier", 1, new ConfigDescription("Set multiplier for clothing icon png resolution", new AcceptableValueRange<int>(1, 20)));
            ConfPlayerIconResMult = Config.Bind("Values", "Player Icon Resolution Multiplier", 1, new ConfigDescription("Set multiplier for player icon png resolution", new AcceptableValueRange<int>(1, 4)));

            // Plugin startup logic
            Logger.LogInfo($"Plugin com.JustNU.IconResolutionMult is loading");

            new itemIconPatch().Enable();
            new ClothingIconPatch().Enable();
            new PlayerIconPatch().Enable();
        }
    }
}
