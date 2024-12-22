using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace ArchipelagoTweaks;

public class ShopUnlockPatch : IScriptMod
{
    public bool ShouldRun(string path) => path == "res://Scenes/HUD/Shop/ShopButtons/shop_button.gdc";

    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens)
    {
        var unlockConsumer = new TokenConsumer(t => t is IdentifierToken { Name: "unlocked" });

        var lockWaiter = new MultiTokenWaiter([
            t => t.Type is TokenType.Dollar,
            t => t is IdentifierToken { Name: "lock" },
            t => t.Type is TokenType.Period,
            t => t is IdentifierToken { Name: "visible" },
            t => t.Type is TokenType.OpAssign,
            t => t is ConstantToken { Value: BoolVariant { Value: false} },
            t => t.Type is TokenType.Newline
        ]);
        

        foreach (var token in tokens)
        {
            if (unlockConsumer.Check(token)) continue;
            
            if (unlockConsumer.Ready)
            {
                yield return token;
                unlockConsumer.Reset();
            }
            
            else if (lockWaiter.Check(token))
            {
                yield return token;
                unlockConsumer.SetReady();
            }
            
            else
            {
                yield return token;
            }
        }
    }
}