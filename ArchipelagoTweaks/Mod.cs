using GDWeave;

namespace ArchipelagoTweaks;

public class Mod : IMod {
    public Config Config;

    public Mod(IModInterface modInterface) {
        this.Config = modInterface.ReadConfig<Config>();
        if (!this.Config.Enabled) return;
        
        modInterface.RegisterScriptMod(new BaitQualityPatch());
        modInterface.RegisterScriptMod(new RodLootTablePatch());
        modInterface.RegisterScriptMod(new LootTableGenerationPatch());
        modInterface.RegisterScriptMod(new UpgradePricePatch());
        modInterface.RegisterScriptMod(new ShopUnlockPatch());
        modInterface.RegisterScriptMod(new ItemPricePatch());
        modInterface.RegisterScriptMod(new CosmeticIconPatch());
    }

    public void Dispose() {
        // Cleanup anything you do here
    }
}
