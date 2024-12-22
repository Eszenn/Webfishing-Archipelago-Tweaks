using System.Text.Json.Serialization;

namespace ArchipelagoTweaks;

public class Config {
    [JsonInclude] public bool Enabled = true;
}
