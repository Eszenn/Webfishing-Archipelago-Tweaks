using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;


namespace ArchipelagoTweaks;

public class LootTableGenerationPatch : IScriptMod
{

    public bool ShouldRun(string path) => path == "res://Scenes/Singletons/globals.gdc";

    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens)
    {
        
        var  lootTable = new Dictionary<string, string[]> 
        {
            // Misc
            { "lake_trash", ["wtrash_bone", "wtrash_boot", "wtrash_branch", "wtrash_drink_rings", "wtrash_plastic_bag", "wtrash_sodacan", "wtrash_weed"] },
            { "ocean_trash", ["wtrash_bone", "wtrash_boot", "wtrash_branch", "wtrash_drink_rings", "wtrash_plastic_bag", "wtrash_sodacan", "wtrash_weed"] },
            { "rain_spectral", ["fish_rain_anomalocaris", "fish_rain_horseshoe_crab", "fish_rain_heliocoprion"] },
            { "alien_spectral", ["fish_alien_dog"] },
            { "void_spectral", ["fish_void_voidfish"] }, 
            
            
            // Lake fish
            { "lake_travelers", ["fish_lake_salmon", "fish_lake_bass", "fish_lake_carp", "fish_lake_rainbowtrout", "fish_lake_bluegill", "fish_lake_perch", "fish_lake_walleye", "fish_lake_goldfish"] },
            { "lake_collectors", ["fish_lake_crayfish", "fish_lake_drum", "fish_lake_guppy", "fish_lake_snail", "fish_lake_frog", "fish_lake_crab"] },
            { "lake_shining", ["fish_lake_catfish", "fish_lake_crappie", "fish_lake_pike", "fish_lake_bowfin", "fish_lake_koi", "fish_lake_sturgeon"] },
            { "lake_opulent", ["fish_lake_leech", "fish_lake_turtle", "fish_lake_toad"] },
            { "lake_glistening", ["fish_lake_muskellunge", "fish_lake_kingsalmon", "fish_lake_gar"] },
            { "lake_radiant", ["fish_lake_pupfish", "fish_lake_axolotl", "fish_lake_mooneye"] },
            { "lake_alpha", ["fish_lake_alligator", "fish_lake_bullshark"] },
            { "lake_prosperous", ["fish_lake_goldenbass", "wtrash_diamond"] },
            { "lake_prosperous_rain", ["fish_lake_goldenbass", "wtrash_diamond", "fish_rain_leedsichthys"] },
            
            // Ocean fish
            { "ocean_travelers", ["fish_ocean_atlantic_salmon", "fish_ocean_herring", "fish_ocean_flounder", "fish_ocean_clownfish", "fish_ocean_shrimp", "fish_ocean_angelfish", "fish_ocean_grouper"] },
            { "ocean_collectors", ["fish_ocean_krill", "fish_ocean_oyster", "fish_ocean_bluefish", "fish_ocean_lobster", "fish_ocean_tuna", "fish_ocean_seahorse", "fish_ocean_sunfish", "fish_ocean_swordfish"] },
            { "ocean_shining", ["fish_ocean_marlin", "fish_ocean_octopus", "fish_ocean_stingray", "fish_ocean_eel", "fish_ocean_dogfish", "fish_ocean_lionfish"] },
            { "ocean_opulent", ["fish_ocean_sawfish", "fish_ocean_wolffish", "fish_ocean_hammerhead_shark"] },
            { "ocean_glistening", ["fish_ocean_sea_turtle", "fish_ocean_squid", "fish_ocean_manowar", "fish_ocean_manta_ray"] }, 
            { "ocean_radiant", ["fish_ocean_greatwhiteshark"] },
            { "ocean_alpha", ["fish_ocean_whale", "fish_ocean_coelacanth"] },
            { "ocean_propserous", ["fish_ocean_golden_manta_ray", "wtrash_diamond"] },
            { "ocean_propserous_rain", ["fish_ocean_golden_manta_ray", "wtrash_diamond", "fish_rain_leedsichthys"] },
        };

        var newlineConsumer = new TokenConsumer(t => t.Type is TokenType.Newline);
        var funcConsumer = new TokenConsumer(t => t.Type is TokenType.PrFunction);

        // generate_voice_bank
        var generationWaiter = new MultiTokenWaiter([
            t => t is IdentifierToken { Name: "_generate_voice_bank"},
            t => t.Type is TokenType.ParenthesisOpen,
            t => t.Type is TokenType.ParenthesisClose
        ]);

        // func _generate_loot_tables(category
        var lootTableWaiter = new MultiTokenWaiter([
            t => t.Type is TokenType.PrFunction,
            t => t is IdentifierToken { Name: "_generate_loot_tables" },
            t => t.Type is TokenType.ParenthesisOpen,
            t => t is IdentifierToken { Name: "category" }
        ]);

        var ifWaiter = new MultiTokenWaiter([
            t => t.Type is TokenType.CfIf,
            t => t is IdentifierToken { Name: "data" },
        ]);

        
        var printWaiter = new MultiTokenWaiter([
            t => t is IdentifierToken { Name: "loot_tables" },
            t => t.Type is TokenType.BracketOpen,
            t => t is IdentifierToken { Name: "table" },
            t => t.Type is TokenType.BracketClose,
            t => t.Type is TokenType.OpAssign,
            t => t is IdentifierToken { Name: "new_table" },
            t => t.Type is TokenType.Newline
        ]);

        foreach (var token in tokens)
        {
            if (newlineConsumer.Check(token)) continue;
            if (funcConsumer.Check(token)) continue;
            
            if (newlineConsumer.Ready)
            {
                yield return token;
                newlineConsumer.Reset();
            }
            
            else if (funcConsumer.Check(token))
            {
                yield return token;
                funcConsumer.Reset();
            }
            
            else if (lootTableWaiter.Check(token))
            {
                // table, entries): 
                yield return new IdentifierToken("table");
                yield return new Token(TokenType.Comma);
                yield return new IdentifierToken("entries");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 1);
                
                newlineConsumer.SetReady();
            }
            
            else if (ifWaiter.Check(token))
            {
                // entries.has(item):
                yield return new IdentifierToken("entries");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("has");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("item");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 3);
                
                newlineConsumer.SetReady();
            }
            
            else if (printWaiter.Check(token))
            {
                yield return token;
                
                newlineConsumer.SetReady();
            }
            
            else if (generationWaiter.Check(token))
            {
                yield return token;
                yield return new Token(TokenType.Newline, 1);
                
                foreach (var table in lootTable.Keys)
                {
                    // _generate_loot_tables(table, [
                    yield return new IdentifierToken("_generate_loot_tables");
                    yield return new Token(TokenType.ParenthesisOpen);
                    yield return new ConstantToken(new StringVariant(table));
                    yield return new Token(TokenType.Comma);
                    yield return new Token(TokenType.BracketOpen);

                    foreach (var entry in lootTable[table])
                    {
                        // "entry",
                        yield return new ConstantToken(new StringVariant(entry));
                        yield return new Token(TokenType.Comma);
                    }

                    // ])\n
                    yield return new Token(TokenType.BracketClose);
                    yield return new Token(TokenType.ParenthesisClose);
                    yield return new Token(TokenType.Newline, 1);
                    
                    // print(loot_tables[tableloot_tables[table]["entries"])
                    yield return new Token(TokenType.BuiltInFunc, (uint?)BuiltinFunction.TextPrint);
                    yield return new Token(TokenType.ParenthesisOpen);
                    yield return new IdentifierToken("loot_tables");
                    yield return new Token(TokenType.BracketOpen);
                    yield return new ConstantToken(new StringVariant(table));
                    yield return new Token(TokenType.BracketClose);
                    yield return new Token(TokenType.BracketOpen);
                    yield return new ConstantToken(new StringVariant("entries"));
                    yield return new Token(TokenType.BracketClose);
                    yield return new Token(TokenType.ParenthesisClose);
                    yield return new Token(TokenType.Newline, 1);

                    
                } 
                
                yield return new Token(TokenType.Newline);

                funcConsumer.SetReady();
                
            }

            else
            {
                yield return token;
            }
        }
    }
    

}