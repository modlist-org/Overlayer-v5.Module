using Overlayer.Compat;
using Overlayer.Core;
using Overlayer.IO;
using Overlayer.Localization;
using Overlayer.Module.ADOFAI.IO;
using Overlayer.Module.ADOFAI.Patch;
using Overlayer.Module.ADOFAI.UI;
using Overlayer.ModuleAPI;
using Overlayer.Patch.Safe;
using Overlayer.UI;
using Overlayer.UI.Factory;
using System.IO;
using UnityEngine;

namespace Overlayer.Module.ADOFAI {
    public class Core : OverlayerModule {
        public static OverlayerLogger Logger => new(MainCore.Host.OverlayerLogger, "ADOFAI Module");
        public static SettingsFile<ADOFAISettings> ConfigFile = new(Path.Combine(MainCore.Paths.ModulePath, "ADOFAI/Settings.json"));
        public static ADOFAISettings Config => ConfigFile.Data;
        public static Translator Tr = new();

        private static void LoadTr()
            => _ = Tr.Load(new(Path.Combine(MainCore.Paths.ModulePath, "ADOFAI/Lang")));

        private void OnLanguageChanged(string lang)
            => Tr.Language = lang;

        public override void OnInitialize() {
            Tr.SetLog(Logger.Msg);

            MainCore.Tr.OnLoadStart += LoadTr;
            MainCore.Tr.OnLanguageChanged += OnLanguageChanged;

            SafePatchController.Add(new SP_ShowAutoJudgment());
            SafePatchController.ApplyAll();

            MainUI.CreateMenu(UICore.MenuContent);
            MainUI.CreatePage(PageFactory.CreatePageBase(100));
        }
        
        public override void OnDispose() {
            MainCore.Tr.OnLoadStart -= LoadTr;
            MainCore.Tr.OnLanguageChanged -= OnLanguageChanged;
        }

        public override string Name => Info.Name;
        public override string Author => Info.Author;
        public override string Version => Info.Version;
    }
}