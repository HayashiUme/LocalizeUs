using AmongUs.Data;
using HarmonyLib;

namespace LocalizeUs.Patches;

[HarmonyPatch]
public static class BackendEndpointsPatch
{
    [HarmonyPatch(typeof(BackendEndpoints), "get_Announcements")]
    [HarmonyPrefix]
    public static void Prefix(ref string __result)
    {
        if (!HelperUtils.IsCustomLanguage((int)DataManager.Settings.Language.CurrentLanguage))
        {
            Info("Checked to vanilla language, Skip");
            return;
        }

        __result = GetAnnouncementUrl(DataManager.Settings.Language.CurrentLanguage);
    }

    public static string GetAnnouncementUrl(SupportedLangs lang)
    {
        var langId = (int)HelperUtils.ToCustom(lang);
        var url = "";
        Info($"Current Language = {HelperUtils.ToCustom(lang)}, Url=https://raw.githubusercontent.com/HayashiUme/LocalizeUs/refs/heads/main/LocalizeUs/Resources/Announcements/{langId}.json");
        return $"https://raw.githubusercontent.com/HayashiUme/LocalizeUs/refs/heads/main/LocalizeUs/Resources/Announcements/{langId}.json";
    }
}
