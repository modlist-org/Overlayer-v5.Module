using System.Collections.Generic;
using Overlayer.Localization;
using Overlayer.Module.ADOFAI.IO;
using Overlayer.Module.ADOFAI.Patch;
using Overlayer.Patch.Safe;
using Overlayer.UI.Generator;
using Overlayer.UI.Objects;
using Overlayer.UI.Objects.Impl;
using Overlayer.UI.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace Overlayer.Module.ADOFAI.UI;

public class MainUI {
    public static void CreateMenu(RectTransform parent) {
        
    }
    
    private static readonly Dictionary<string, UIObject> objects = [];
    
    public static void CreatePanel(RectTransform parent) {
        GameObject pad = new("Pad");
        pad.transform.SetParent(parent, false);

        RectTransform padRect = pad.AddComponent<RectTransform>();
        padRect.anchorMin = Vector2.zero;
        padRect.anchorMax = Vector2.one;
        padRect.pivot = new Vector2(0.5f, 0.5f);
        padRect.offsetMin = new Vector2(18f, 18f);
        padRect.offsetMax = new Vector2(-18f, -18f);

        GameObject viewport = new("Viewport");
        viewport.transform.SetParent(pad.transform, false);

        RectTransform viewportRect = viewport.AddComponent<RectTransform>();
        viewportRect.anchorMin = Vector2.zero;
        viewportRect.anchorMax = Vector2.one;
        viewportRect.offsetMin = Vector2.zero;
        viewportRect.offsetMax = Vector2.zero;
        viewportRect.pivot = new Vector2(0.5f, 0.5f);

        viewport.AddComponent<EmptyGraphic>().raycastTarget = true;
        viewport.AddComponent<RectMask2D>();

        GameObject content = new("Content");
        content.transform.SetParent(viewport.transform, false);

        RectTransform contentRect = content.AddComponent<RectTransform>();
        contentRect.anchorMin = new Vector2(0f, 1f);
        contentRect.anchorMax = new Vector2(1f, 1f);
        contentRect.pivot = new Vector2(0.5f, 1f);
        contentRect.offsetMin = Vector2.zero;
        contentRect.offsetMax = Vector2.zero;

        VerticalLayoutGroup layout = content.AddComponent<VerticalLayoutGroup>();
        layout.spacing = 12f;
        layout.childControlWidth = true;
        layout.childControlHeight = true;
        layout.childForceExpandWidth = true;
        layout.childForceExpandHeight = false;

        ContentSizeFitter fitter = content.AddComponent<ContentSizeFitter>();
        fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

        pad.AddComponent<UIScrollController>().SetContent(contentRect, viewportRect);

        ADOFAISettings defSet = new();
        
        var spSaj = SafePatchController.Get<SP_ShowAutoJudgment>();
        UIToggle showAutoJudgmentToggle = GenerateUI.Toggle(
            GenerateUI.Row(content.transform),
            defSet.ShowAutoplayJudgment,
            Core.Config.ShowAutoplayJudgment,
            toggle => {
                Core.Config.ShowAutoplayJudgment = toggle;
                Core.ConfigFile.RequestSave();

                if(toggle) {
                    spSaj.Apply();
                } else {
                    spSaj.Remove();
                }
            },
            "Show Autoplay Judgment",
            "show_autoplay_judgment"
        );
        showAutoJudgmentToggle.OnlyModOn = true;
        showAutoJudgmentToggle.Label.gameObject.AddComponent<TextLocalization>().Init("SHOW_AUTOPLAY_JUDGMENT", "Show Autoplay Judgment");
        objects[showAutoJudgmentToggle.Id] = showAutoJudgmentToggle;
        showAutoJudgmentToggle.Rect.AddToolTip(
            "DESC_SHOW_AUTOPLAY_JUDGMENT",
            "Applies a patch to show the true judgment in AutoPlay on the Hit Error Meter"
        );
    }
}