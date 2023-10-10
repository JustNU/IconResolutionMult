using BepInEx;
using BepInEx.Configuration;
using System;
using System.IO;
using UnityEngine;
using Aki.Reflection.Patching;
using Aki.Reflection.Utils;
using System.Reflection;
using System.Linq;
using Comfort.Common;
using EFT.InventoryLogic;
using EFT.UI.DragAndDrop;
using EFT;
using EFT.UI;


namespace IconResolutionMult
{
    [BepInPlugin("com.JustNU.IconResolutionMult", "JustNU-IconResolutionMult", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        // create config ent
        public static ConfigEntry<int> ConfItemIconResMult;
        public static ConfigEntry<int> ConfClothingIconResMult;
        public static ConfigEntry<int> ConfPlayerIconResMult;
        public static ConfigEntry<KeyboardShortcut> ConfResetIconScaleKeybind;

        private void Awake()
        {
            // set up config
            ConfItemIconResMult = Config.Bind("Values", "Item Icon Resolution Multiplier", 1, new ConfigDescription("Set multiplier for item icon png resolution", new AcceptableValueRange<int>(1, 8)));
            ConfClothingIconResMult = Config.Bind("Values", "Clothing Resolution Multiplier", 1, new ConfigDescription("Set multiplier for clothing icon png resolution", new AcceptableValueRange<int>(1, 20)));
            ConfPlayerIconResMult = Config.Bind("Values", "Player Icon Resolution Multiplier", 1, new ConfigDescription("Set multiplier for player icon png resolution", new AcceptableValueRange<int>(1, 4)));
            ConfResetIconScaleKeybind = Config.Bind("Keybind", "Reset Icon Scale Key", new KeyboardShortcut(KeyCode.Keypad4), "Resets scale of an item icon");

            // Plugin startup logic
            Logger.LogInfo($"Plugin com.JustNU.IconResolutionMult is loading");

            new itemIconPatch().Enable();
            new ClothingIconPatch().Enable();
            new PlayerIconPatch().Enable();
        }

        public void Update()
        {
            if (Plugin.ConfItemIconResMult.Value > 1)
            {
                if (Plugin.ConfResetIconScaleKeybind.Value.IsPressed())
                {
                    Logger.LogInfo("IsPressed");

                    var objects = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "grid_layout(Clone)(Clone)" && obj.activeInHierarchy == true
                                                                                         || obj.name == "trading_layout(Clone)(Clone)" && obj.activeInHierarchy == true
                                                                                         || obj.name == "trading_player_layout(Clone)(Clone)" && obj.activeInHierarchy == true);
                    foreach (var iconGameObject in objects)
                    {
                        var imageChild = iconGameObject.transform.Find("Image");

                        imageChild.transform.localScale = new Vector3(1f / (float)Plugin.ConfItemIconResMult.Value, 1f / (float)Plugin.ConfItemIconResMult.Value, 1f);
                    }
                }

                if (Plugin.ConfResetIconScaleKeybind.Value.IsDown())
                {
                    Logger.LogInfo("IsDown");

                    var objects = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "grid_layout(Clone)(Clone)" && obj.activeInHierarchy == true
                                                                                         /*|| obj.name == "something else" && obj.activeInHierarchy == true*/);
                    foreach (var iconGameObject in objects)
                    {
                        var imageChild = iconGameObject.transform.Find("Image");

                        imageChild.transform.localScale = new Vector3(1f / (float)Plugin.ConfItemIconResMult.Value, 1f / (float)Plugin.ConfItemIconResMult.Value, 1f);
                    }
                }
            }
        }
    }
}
