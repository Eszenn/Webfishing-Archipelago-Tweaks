using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace ArchipelagoTweaks;

public class BuddyLootTablePatch : IScriptMod
{
    public bool ShouldRun(string path) => path == "res://Scenes/Entities/Props/fish_trap.gdc";

    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens)
    {
        var newlineConsumer = new TokenConsumer(t => t.Type is TokenType.Newline);
        
        // $catch_ring.play()
        var ringWaiter = new MultiTokenWaiter([
            t => t.Type is TokenType.Dollar,
            t => t is IdentifierToken { Name: "catch_ring"}, 
            t => t.Type is TokenType.Period,
            t => t is IdentifierToken { Name: "play" },
            t => t.Type is TokenType.ParenthesisOpen,
            t => t.Type is TokenType.ParenthesisClose,
            t => t.Type is TokenType.Newline
        ]);
    
        // var roll = Globals._roll_loot_table(
        var rollWaiter = new MultiTokenWaiter([
            t => t.Type is TokenType.PrVar,
            t => t is IdentifierToken { Name: "roll" },
            t => t.Type is TokenType.OpAssign,
            t => t is IdentifierToken { Name: "Globals" },
            t => t.Type is TokenType.Period,
            t => t is IdentifierToken { Name: "_roll_loot_table" },
            t => t.Type is TokenType.ParenthesisOpen,
        ]);

        foreach (var token in tokens)
        {

            if (newlineConsumer.Check(token)) continue;

            if (newlineConsumer.Ready)
            {
                yield return token;
                newlineConsumer.Reset();
            }
            
            else if (ringWaiter.Check(token))
            {
                yield return token;
                
                // var table = ""
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("table");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant(""));
                yield return new Token(TokenType.Newline, 1);
                
                // match PlayerData.equipped_rod:
                yield return new Token(TokenType.CfMatch);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("equipped_rod");
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 2);
                
                // PlayerData.ROD.SIMPLE: table = "trash"
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("ROD");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("SIMPLE");
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("table");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("trash"));
                yield return new Token(TokenType.Newline, 2);
                
                // PlayerData.ROD.TRAVELERS: table = "travelers"
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("ROD");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("TRAVELERS");
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("table");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("travelers"));
                yield return new Token(TokenType.Newline, 2);
                
                // PlayerData.ROD.COLLECTORS: table = "collectors"
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("ROD");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("COLLECTORS");
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("table");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("collectors"));
                yield return new Token(TokenType.Newline, 2);
                
                // PlayerData.ROD.SHINING: table = "shining"
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("ROD");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("SHINING");
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("table");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("shining"));
                yield return new Token(TokenType.Newline, 2);
                
                // PlayerData.ROD.OPULENT: table = "opulent"
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("ROD");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("OPULENT");
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("table");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("opulent"));
                yield return new Token(TokenType.Newline, 2);
                
                // PlayerData.ROD.GLISTENING: table = "glistening"
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("ROD");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("GLISTENING");
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("table");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("glistening"));
                yield return new Token(TokenType.Newline, 2);
                
                // PlayerData.ROD.RADIANT: table = "radiant"
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("ROD");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("RADIANT");
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("table");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("radiant"));
                yield return new Token(TokenType.Newline, 2);
                
                // PlayerData.ROD.ALPHA: table = "alpha"
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("ROD");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("ALPHA");
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("table");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("alpha"));
                yield return new Token(TokenType.Newline, 2);
                
                // PlayerData.ROD.SPECTRAL: table = "spectral"
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("ROD");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("SPECTRAL");
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("table");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("spectral"));
                yield return new Token(TokenType.Newline, 2);
                
                // PlayerData.ROD.PROSPEROUS: table = "prosperous"
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("ROD");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("PROSPEROUS");
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("table");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("prosperous"));
                yield return new Token(TokenType.Newline, 1);
                
                // var loot_table = str(fish_type, "_", table)\n
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("loot_table");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.BuiltInFunc, (uint?)BuiltinFunction.TextStr);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("fish_type");
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("_"));
                yield return new Token(TokenType.Comma);
                yield return new IdentifierToken("table");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Newline, 1);
                
                // print(fish_type)
                yield return new Token(TokenType.BuiltInFunc, (uint?)BuiltinFunction.TextPrint);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("fish_type");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Newline, 1);
            }
            
            else if (rollWaiter.Check(token))
            {
                yield return token;
                
                // loot_table, 2)\n
                yield return new IdentifierToken("loot_table");
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new IntVariant(2));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Newline, 1);
                
                newlineConsumer.SetReady();
            }

            else
            {
                yield return token;
            }
        }
    }
}