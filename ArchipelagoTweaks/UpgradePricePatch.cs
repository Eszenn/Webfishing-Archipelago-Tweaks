using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace ArchipelagoTweaks;

public class UpgradePricePatch : IScriptMod
{
    public bool ShouldRun(string path) => path == "res://Scenes/HUD/Shop/ShopButtons/button_rod_upgrade.gdc";

    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens) 
    {
        var newlineConsumer = new TokenConsumer(t => t.Type is TokenType.Newline);
        
        // Wait for var new_cost = 
        var waiter = new MultiTokenWaiter([
            t => t is IdentifierToken { Name: "new_cost"},
            t => t.Type is TokenType.OpAssign
        ]);

        foreach (var token in tokens)
        {
            if (newlineConsumer.Check(token)) continue;

            if (newlineConsumer.Ready)
            {
                yield return token;
                newlineConsumer.Reset();
                waiter.Reset();
            }
            
            else if (waiter.Check(token))
            {
                yield return token;
                
                // 0
                yield return new ConstantToken(new IntVariant(0));
                
                newlineConsumer.SetReady();
            }

            else
            {
                yield return token;
            }
        }
    }
}