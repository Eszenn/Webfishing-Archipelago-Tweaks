using System.Text.Json.Serialization;

namespace apManual;

public class Config {
    [JsonInclude] public bool SomeSetting = true;
}
