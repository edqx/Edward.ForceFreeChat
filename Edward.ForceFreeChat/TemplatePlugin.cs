using BepInEx;
using BepInEx.Configuration;
using BepInEx.IL2CPP;
using HarmonyLib;
using InnerNet;
using Reactor;

namespace Edward.ForceFreeChat
{
    [BepInPlugin(Id)]
    [BepInProcess("Among Us.exe")]
    [BepInDependency(ReactorPlugin.Id)]
    public class TemplatePlugin : BasePlugin
    {
        public const string Id = "dev.weakeyes.forcefreechat";

        public Harmony Harmony { get; } = new Harmony(Id);

        public ConfigEntry<string> Name { get; private set; }

        public override void Load()
        {
            Name = Config.Bind("Fake", "Name", ":>");

            Harmony.PatchAll();
        }

        [HarmonyPatch(typeof(SaveManager), nameof(SaveManager.ChatModeType), MethodType.Setter)]
        public static class ChatModePatch
        {
            public static void Prefix()
            {
                SaveManager.LoadPlayerPrefs(false);
                SaveManager.chatModeType = (int)QuickChatModes.FreeChatOrQuickChat;
                SaveManager.SavePlayerPrefs(false);
                return;
            }
        }
    }
}
