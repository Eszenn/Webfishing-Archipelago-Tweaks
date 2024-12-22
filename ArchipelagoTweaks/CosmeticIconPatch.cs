using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace ArchipelagoTweaks;

public class CosmeticIconPatch : IScriptMod
{
    public bool ShouldRun(string path) => path == "res://Scenes/HUD/Shop/ShopButtons/button_cosmetic_unlock.gdc";

    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens)
    {
        var funcConsumer = new TokenConsumer(t => t.Type is TokenType.PrFunction);
        var ifWaiter = new TokenWaiter(t => t.Type is TokenType.CfIf);

        foreach (var token in tokens)
        {
            if (funcConsumer.Check(token)) continue;

            if (funcConsumer.Ready)
            {
                yield return new Token(TokenType.Newline);
                yield return token;
                funcConsumer.Reset();
            }
            
            else if (ifWaiter.Check(token))
            {
                funcConsumer.SetReady();
            }

            else
            {
                yield return token;
            }
        }
    }
}