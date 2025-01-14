using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace ArchipelagoTweaks;

public class RodLootTablePatch : IScriptMod
{
    public bool ShouldRun(string path) => path == "res://Scenes/Entities/Player/player.gdc";

    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens)
    {
        var newlineConsumer = new TokenConsumer(t => t.Type is TokenType.Newline);
        var rollsConsumer = new TokenConsumer(t => t is IdentifierToken { Name: "rolls" });

        // Wait for item_data = PlayerData.FALLBACK_ITEM
        var heldItemWaiter = new MultiTokenWaiter([
            t => t is IdentifierToken { Name: "item_data" },
            t => t.Type is TokenType.OpAssign,
            t => t is IdentifierToken { Name: "PlayerData" },
            t => t.Type is TokenType.Period,
            t => t is IdentifierToken { Name: "FALLBACK_ITEM" },
            t => t.Type is TokenType.Newline
        ], allowPartialMatch: true);

        var fishTypeWaiter = new MultiTokenWaiter([
            t => t is IdentifierToken { Name:"treasure_mult"},
            t => t.Type is TokenType.OpAssign,
            t => t is ConstantToken { Value: RealVariant { Value: 2.0 } }
        ]);

        var zoneWaiter = new MultiTokenWaiter([
            t => t is IdentifierToken { Name: "alt_chance"},
            t => t.Type is TokenType.OpGreater,
            t => t is ConstantToken { Value: RealVariant { Value: 0.0 } },
        ]);

        foreach (var token in tokens)
        {
            if (newlineConsumer.Check(token)) continue;
            if (rollsConsumer.Check(token)) continue;


            if (newlineConsumer.Ready)
            {
                yield return token;
                newlineConsumer.Reset();
            }
            
            else if (rollsConsumer.Ready)
            {
                yield return new Token(TokenType.PrVar);
                yield return token;
                
                rollsConsumer.Reset();
            }
            
            else if (heldItemWaiter.Check(token))
            {
                yield return token;
                
                // print("Equipped item with id: " + item_data["id"])
                yield return new Token(TokenType.BuiltInFunc, (uint?) BuiltinFunction.TextPrint);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("Equipped item with id: "));
                yield return new Token(TokenType.OpAdd);
                yield return new IdentifierToken("item_data");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("id"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Newline, 1);
                
                // match item_data["id"]:
                yield return new Token(TokenType.CfMatch);
                yield return new IdentifierToken("item_data");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("id"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 2);
                
                // "fishing_rod_simple": equipped_rod = PlayerData.ROD.SIMPLE
                yield return new ConstantToken(new StringVariant("fishing_rod_simple"));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("equipped_rod");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("ROD");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("SIMPLE");
                yield return new Token(TokenType.Newline, 2);
                
                // "fishing_rod_travelers": equipped_rod = PlayerData.ROD.TRAVELERS
                yield return new ConstantToken(new StringVariant("fishing_rod_travelers"));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("equipped_rod");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("ROD");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("TRAVELERS");
                yield return new Token(TokenType.Newline, 2);
                
                // "fishing_rod_collectors": equipped_rod = PlayerData.ROD.COLLECTORS
                yield return new ConstantToken(new StringVariant("fishing_rod_collectors"));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("equipped_rod");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("ROD");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("COLLECTORS");
                yield return new Token(TokenType.Newline, 2);
                
                // "fishing_rod_collectors_shining": equipped_rod = PlayerData.ROD.SHINING
                yield return new ConstantToken(new StringVariant("fishing_rod_collectors_shining"));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("equipped_rod");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("ROD");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("SHINING");
                yield return new Token(TokenType.Newline, 2);
                
                // "fishing_rod_collectors_opulent": equipped_rod = PlayerData.ROD.OPULENT
                yield return new ConstantToken(new StringVariant("fishing_rod_collectors_opulent"));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("equipped_rod");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("ROD");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("OPULENT");
                yield return new Token(TokenType.Newline, 2);
                
                // "fishing_rod_collectors_glistening": equipped_rod = PlayerData.ROD.GLISTENING
                yield return new ConstantToken(new StringVariant("fishing_rod_collectors_glistening"));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("equipped_rod");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("ROD");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("GLISTENING");
                yield return new Token(TokenType.Newline, 2);
                
                // "fishing_rod_collectors_radiant": equipped_rod = PlayerData.ROD.RADIANT
                yield return new ConstantToken(new StringVariant("fishing_rod_collectors_radiant"));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("equipped_rod");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("ROD");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("RADIANT");
                yield return new Token(TokenType.Newline, 2);
                
                // "fishing_rod_collectors_alpha": equipped_rod = PlayerData.ROD.ALPHA
                yield return new ConstantToken(new StringVariant("fishing_rod_collectors_alpha"));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("equipped_rod");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("ROD");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("ALPHA");
                yield return new Token(TokenType.Newline, 2);
                
                // "fishing_rod_prosperous": equipped_rod = PlayerData.ROD.PROSPEROUS
                yield return new ConstantToken(new StringVariant("fishing_rod_prosperous"));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("equipped_rod");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("ROD");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("PROSPEROUS");
                yield return new Token(TokenType.Newline, 2);
                
                
                // "fishing_rod_skeleton": equipped_rod = PlayerData.ROD.SPECTRAL
                yield return new ConstantToken(new StringVariant("fishing_rod_skeleton"));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("equipped_rod");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("ROD");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("SPECTRAL");
                yield return new Token(TokenType.Newline, 1);
                
                // print("Equipped Rod: ", PlayerData.equipped_rod)
                yield return new Token(TokenType.BuiltInFunc, (uint?)BuiltinFunction.TextPrint);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("Equipped Rod: "));
                yield return new Token(TokenType.Comma);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("equipped_rod");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Newline, 1);
            }
            
            else if (fishTypeWaiter.Check(token))
            {
                yield return token;
                yield return new Token(TokenType.Newline, 1);
                
                // var zone = fish_type
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("zone");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("fish_type");
                yield return new Token(TokenType.Newline, 1);
                
                // var table = "trash"
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("table");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("trash"));
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
                
                // if rod_cast_data == "salty" and not type_lock: zone = "ocean"
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("rod_cast_data");
                yield return new Token(TokenType.OpEqual);
                yield return new ConstantToken(new StringVariant("salty"));
                yield return new Token(TokenType.OpAnd);
                yield return new Token(TokenType.OpNot);
                yield return new IdentifierToken("type_lock");
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("zone");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("ocean"));
                yield return new Token(TokenType.Newline, 1);
                
                // if rod_cast_data == "fresh" and not type_lock: zone = "lake"
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("rod_cast_data");
                yield return new Token(TokenType.OpEqual);
                yield return new ConstantToken(new StringVariant("fresh"));
                yield return new Token(TokenType.OpAnd);
                yield return new Token(TokenType.OpNot);
                yield return new IdentifierToken("type_lock");
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("zone");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("lake"));
                yield return new Token(TokenType.Newline, 1);
                
                // if table == "spectral" and zone != "alien" and zone != "void":
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("table");
                yield return new Token(TokenType.OpEqual);
                yield return new ConstantToken(new StringVariant("spectral"));
                yield return new Token(TokenType.OpAnd);
                yield return new IdentifierToken("zone");
                yield return new Token(TokenType.OpNotEqual);
                yield return new ConstantToken(new StringVariant("alien"));
                yield return new Token(TokenType.OpAnd);
                yield return new IdentifierToken("zone");
                yield return new Token(TokenType.OpNotEqual);
                yield return new ConstantToken(new StringVariant("void"));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 2);
                
                // if in_rain: 
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("in_rain");
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 3);
                
                // zone = "rain"
                yield return new IdentifierToken("zone");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("rain"));
                yield return new Token(TokenType.Newline, 2);
                
                // else:
                yield return new Token(TokenType.CfElse);
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 3);
                
                // table = "trash"
                yield return new IdentifierToken("table");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("trash"));
                yield return new Token(TokenType.Newline, 1);
                
                // if table != "spectral" and zone == "alien":
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("table");
                yield return new Token(TokenType.OpNotEqual);
                yield return new ConstantToken(new StringVariant("spectral"));
                yield return new Token(TokenType.OpAnd);
                yield return new IdentifierToken("zone");
                yield return new Token(TokenType.OpEqual);
                yield return new ConstantToken(new StringVariant("alien"));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 2);
                
                // zone = "lake"
                yield return new IdentifierToken("zone");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("lake"));
                yield return new Token(TokenType.Newline, 2);
                
                // table = "trash"
                yield return new IdentifierToken("table");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("trash"));
                yield return new Token(TokenType.Newline, 1);
                
                // var force_av_size = false
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("force_av_size");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(false));
                yield return new Token(TokenType.Newline, 1);
                
                // if table == "trash": force_av_size = true
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("table");
                yield return new Token(TokenType.OpEqual);
                yield return new ConstantToken(new StringVariant("trash"));
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("force_av_size");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new BoolVariant(true));
                yield return new Token(TokenType.Newline, 1);
                
                // if in_rain and table == "prosperous":
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("in_rain");
                yield return new Token(TokenType.OpAnd);
                yield return new IdentifierToken("table");
                yield return new Token(TokenType.OpEqual);
                yield return new ConstantToken(new StringVariant("prosperous"));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 2);
                
                // table = str(table, "_", "rain")
                yield return new IdentifierToken("table");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.BuiltInFunc, (uint?)BuiltinFunction.TextStr);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("table");
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("_rain"));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Newline, 1);
                
                // fish_type = str(zone, "_", table)
                yield return new IdentifierToken("fish_type");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.BuiltInFunc, (uint?) BuiltinFunction.TextStr);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("zone");
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
                
                rollsConsumer.SetReady();
            }
            
            else if (zoneWaiter.Check(token))
            {
                yield return token;
                yield return new Token(TokenType.Colon);
                
                newlineConsumer.SetReady();
            }
            
            else
            {
                yield return token;
            }
        }
    }
}