using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace ArchipelagoTweaks;

public class BaitQualityPatch : IScriptMod
{
    public bool ShouldRun(string path) => path == "res://Scenes/Entities/Player/player.gdc";

    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens)
    {
        var constConsumer = new TokenConsumer(t => t.Type is TokenType.PrConst);
            
        // wait for const BAIT_DATA = {\n
        var tokenWaiter = new MultiTokenWaiter([
            t => t.Type is TokenType.PrConst,
            t => t is IdentifierToken { Name: "BAIT_DATA"},
            t => t.Type is TokenType.OpAssign,
            t => t.Type is TokenType.CurlyBracketOpen,
        ]);
        
        foreach (var token in tokens)
        {
            if (constConsumer.Check(token)) continue;

            if (constConsumer.Ready)
            {
                yield return token;
                constConsumer.Reset();
            }

            else if (tokenWaiter.Check(token))
            {
                yield return token;

                yield return new Token(TokenType.Newline, 1);

                // "": {"catch": 0.0, "max_tier": 0, "quality": []},
                yield return new ConstantToken(new StringVariant(""));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CurlyBracketOpen);
                yield return new ConstantToken(new StringVariant("catch"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new RealVariant(0.0));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("max_tier"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("quality"));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.BracketOpen);
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.CurlyBracketClose);
                yield return new Token(TokenType.Comma);
                yield return new Token(TokenType.Newline, 1);
                
                // "worms": {"catch": 0.06, "max_tier": 1, "quality": [1.0]},
                yield return new ConstantToken(new StringVariant("worms"));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CurlyBracketOpen);
                yield return new ConstantToken(new StringVariant("catch"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new RealVariant(0.06));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("max_tier"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(2));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("quality"));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new RealVariant(1.0));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.CurlyBracketClose);
                yield return new Token(TokenType.Comma);
                yield return new Token(TokenType.Newline, 1);
                
                // "cricket": {"catch": 0.06, "max_tier": 2, "quality": [0.0, 1.0]},
                yield return new ConstantToken(new StringVariant("cricket"));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CurlyBracketOpen);
                yield return new ConstantToken(new StringVariant("catch"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new RealVariant(0.06));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("max_tier"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(2));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("quality"));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new RealVariant(0.0));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new RealVariant(1.0));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.CurlyBracketClose);
                yield return new Token(TokenType.Comma);
                yield return new Token(TokenType.Newline, 1);
                
                // "leech": {"catch": 0.06, "max_tier": 2, "quality": [0.0, 0.0, 1.0]},
                yield return new ConstantToken(new StringVariant("leech"));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CurlyBracketOpen);
                yield return new ConstantToken(new StringVariant("catch"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new RealVariant(0.06));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("max_tier"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(2));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("quality"));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new RealVariant(0.0));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new RealVariant(0.0));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new RealVariant(1.0));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.CurlyBracketClose);
                yield return new Token(TokenType.Comma);
                yield return new Token(TokenType.Newline, 1);

                // "minnow": {"catch": 0.06, "max_tier": 2, "quality": [0.0, 0.0, 0.0, 1.0]},
                yield return new ConstantToken(new StringVariant("minnow"));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CurlyBracketOpen);
                yield return new ConstantToken(new StringVariant("catch"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new RealVariant(0.06));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("max_tier"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(2));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("quality"));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new RealVariant(0.0));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new RealVariant(0.0));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new RealVariant(0.0));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new RealVariant(1.0));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.CurlyBracketClose);
                yield return new Token(TokenType.Comma);
                yield return new Token(TokenType.Newline, 1);
                
                // "squid": {"catch": 0.06, "max_tier": 2, "quality": [0.0, 0.0, 0.0, 0.0, 1.0]},
                yield return new ConstantToken(new StringVariant("squid"));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CurlyBracketOpen);
                yield return new ConstantToken(new StringVariant("catch"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new RealVariant(0.06));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("max_tier"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(2));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("quality"));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new RealVariant(0.0));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new RealVariant(0.0));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new RealVariant(0.0));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new RealVariant(0.0));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new RealVariant(1.0));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.CurlyBracketClose);
                yield return new Token(TokenType.Comma);
                yield return new Token(TokenType.Newline, 1);
                
                // "nautilus": {"catch": 0.06, "max_tier": 2, "quality": [0.0, 0.0, 0.0, 0.0, 0.0, 1.0]},
                yield return new ConstantToken(new StringVariant("nautilus"));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CurlyBracketOpen);
                yield return new ConstantToken(new StringVariant("catch"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new RealVariant(0.06));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("max_tier"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(2));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("quality"));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new RealVariant(0.0));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new RealVariant(0.0));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new RealVariant(0.0));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new RealVariant(0.0));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new RealVariant(0.0));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new RealVariant(1.0));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.CurlyBracketClose);
                yield return new Token(TokenType.Comma);
                yield return new Token(TokenType.Newline);
                
                // "gildedworm": {"catch": 0.06, "max_tier": 2, "quality": [1.0, 0.99, 0.85, 0.75, 0.55, 0.12]},
                yield return new ConstantToken(new StringVariant("gildedworm"));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CurlyBracketOpen);
                yield return new ConstantToken(new StringVariant("catch"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new RealVariant(0.06));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("max_tier"));
                yield return new Token(TokenType.Colon);
                yield return new ConstantToken(new IntVariant(2));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("quality"));
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new RealVariant(1.0));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new RealVariant(0.99));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new RealVariant(0.85));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new RealVariant(0.75));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new RealVariant(0.55));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new RealVariant(0.12));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.CurlyBracketClose);
                yield return new Token(TokenType.Comma);
                yield return new Token(TokenType.Newline);
                
                yield return new Token(TokenType.CurlyBracketClose);
                yield return new Token (TokenType.Newline);
                
                constConsumer.SetReady();
            }
            
            else
            {
                yield return token;
            }
        }
    }
}