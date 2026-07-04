using AmongUs.Data;
using HarmonyLib;

namespace LocalizeUs.Patches;

[HarmonyPatch]
public static class BackendEndpointsPatch
{
    [HarmonyPatch(typeof(BackendEndpoints), "get_Announcements")]
    [HarmonyPostfix]
    public static bool Postfix(ref string __result)
    {
        if (!HelperUtils.IsCustomLanguage((int)DataManager.Settings.Language.CurrentLanguage))
        {
            return true;
        }

        __result = GetAnnouncementUrl(DataManager.Settings.Language.CurrentLanguage);
        return false;
    }

    public static string GetAnnouncementUrl(SupportedLangs lang)
    {
        CustomLocale.LangCodesList.TryGetValue(HelperUtils.ToCustom(lang),out string langName);
        Info($"Current Language = {HelperUtils.ToCustom(lang)}, Url=https://raw.githubusercontent.com/HayashiUme/LocalizeUs/refs/heads/main/LocalizeUs/Resources/Announcements/{langName}.json");
        return $"https://raw.githubusercontent.com/HayashiUme/LocalizeUs/refs/heads/main/LocalizeUs/Resources/Announcements/{langName}.json";
    }
}
