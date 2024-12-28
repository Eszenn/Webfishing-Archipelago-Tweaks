using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace ArchipelagoTweaks;

public class JournalRequirementPatch : IScriptMod
{
    public bool ShouldRun(string path) => path == "res://Scenes/HUD/Shop/ShopButtons/shop_button.gdc";

    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens)
    {
        var ifConsumer = new TokenConsumer(t => t.Type == TokenType.CfIf);

        var ownedWaiter = new MultiTokenWaiter([
            t => t is ConstantToken { Value: StringVariant { Value: "\nAlready Owned." } },
            t => t.Type is TokenType.Newline,
            t => t.Type is TokenType.Newline,
            t => t.Type is TokenType.CfIf
        ]);

        foreach (var token in tokens)
        {
            if (ifConsumer.Check(token)) continue;

            if (ifConsumer.Ready)
            {
                yield return token;
                ifConsumer.Reset();
            }
            
            else if (ownedWaiter.Check(token))
            {
                ifConsumer.SetReady();
            }
            
            else
            {
                yield return token;
            }
        }
    }
}