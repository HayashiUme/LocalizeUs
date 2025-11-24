using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using Reactor;
using Reactor.Utilities;

namespace LocalizeUs;

[BepInAutoPlugin("auavengers.localizeus", "Localize Us!")]
[BepInDependency(ReactorPlugin.Id)]
public sealed partial class LocalizeUsPlugin : BasePlugin
{
    public Harmony Harmony { get; } = new(Id);

    public override void Load()
    {
        ReactorCredits.Register<LocalizeUsPlugin>(location =>
            location == ReactorCredits.Location.MainMenu || location == ReactorCredits.Location.PingTracker);

        Harmony.PatchAll();

        //LocalizationManager.Register(new SubmergedLocalizationProvider());
    }
}
