using System.IO;
using Overlayer.Core;
using Overlayer.IO;
using Overlayer.Module.ADOFAI.IO;
using Overlayer.ModuleAPI;
using UnityEngine;

namespace Overlayer.Module.ADOFAI {
    public class Core : OverlayerModule {
        public static SettingsFile<ADOFAISettings> ConfigFile = new(Path.Combine(MainCore.Paths.ModulePath, "ADOAFI/Settings.json"));
        public static ADOFAISettings Config => ConfigFile.Data;

        public override void OnInitialize() {
            
        }
        public override void OnModEnabledChanged(bool enabled, bool isDispose) {
            
        }

        public override void OnDispose() {
            
        }

        public override string Name => Info.Name;
        public override string Author => Info.Author;
        public override string Version => Info.Version;
    }
}