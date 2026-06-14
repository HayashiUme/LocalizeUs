using HarmonyLib;
using TMPro;
using UnityEngine;

namespace LocalizeUs.Patches;

[HarmonyPatch]
public static class TmpAwakePatch
{
    private static bool _sharedMaterialAdjusted;

    [HarmonyPatch(typeof(TextMeshPro), nameof(TextMeshPro.Awake))]
    [HarmonyPostfix]
    public static void TmpAwakePostfix(TextMeshPro __instance)
    {
        if (__instance.font.name == "LiberationSans SDF")
        {
            // The custom LiberationSans Extended font may use different SDF
            // material parameters (e.g. _FaceDilate) than the original, causing
            // text to render bolder/darker. Copy the original font's key SDF
            // parameters to the replacement font's shared material once.
            if (!_sharedMaterialAdjusted)
            {
                CopySdfParams(__instance.font.material, LocaleUsAssets.LibSansRegTmp.material);
                _sharedMaterialAdjusted = true;
            }

            __instance.font = LocaleUsAssets.LibSansRegTmp;
        }
        /*else if (component.font.name == "Brook SDF" && CustomLocale.LangsWithCustomFont.Contains(auLang))
        {
            ogBrookTmp = component.font;
            component.font = LocaleUsAssets.AmaticScBoldTmp;
        }*/
        __instance.ForceMeshUpdate(false, false);
    }

    private static void CopySdfParams(Material from, Material to)
    {
        CopyProperty("_FaceDilate");
        CopyProperty("_OutlineWidth");
        CopyProperty("_OutlineSoftness");
        CopyProperty("_ScaleRatioA");
        CopyProperty("_ScaleRatioB");
        CopyProperty("_ScaleRatioC");
        return;

        void CopyProperty(string name)
        {
            if (from.HasProperty(name) && to.HasProperty(name))
            {
                to.SetFloat(name, from.GetFloat(name));
            }
        }
    }
}