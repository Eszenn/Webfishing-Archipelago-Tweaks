using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace ArchipelagoTweaks;

public class ItemPricePatch : IScriptMod
{
    public bool ShouldRun(string path) => path is "res://Scenes/HUD/Shop/ShopButtons/button_bait_unlock.gdc"
        or "res://Scenes/HUD/Shop/ShopButtons/button_cosmetic_unlock.gdc"
        or "res://Scenes/HUD/Shop/ShopButtons/button_lure_unlock.gdc"
        or "res://Scenes/HUD/Shop/ShopButtons/button_upgrade.gdc"
        or "res://Scenes/HUD/Shop/ShopButtons/button_item.gdc";

    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens)
    {
        var setupWaiter = new MultiTokenWaiter([
            t => t.Type is TokenType.PrFunction,
            t => t is IdentifierToken { Name: "_setup" },
            t => t.Type is TokenType.ParenthesisOpen,
            t => t.Type is TokenType.ParenthesisClose,
            t => t.Type is TokenType.Colon,
            t => t.Type is TokenType.Newline
        ]);

        foreach (var token in tokens)
        {
            if (setupWaiter.Check(token))
            {
                yield return token;
            
                // cost = 0
                yield return new IdentifierToken("cost");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Newline, 1);
            }

            else
            {
                yield return token;
            }
        }
    }
}