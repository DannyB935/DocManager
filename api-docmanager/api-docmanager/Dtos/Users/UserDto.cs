using System.Text.Json.Serialization;

namespace api_docmanager.Dtos.Users;

public class UserDto
{
    public required string Code { get; set; }
    [JsonPropertyName("name")]
    public required string NameUsr { get; set; }
    [JsonPropertyName("last_name")]
    public required string LName { get; set; }
    [JsonPropertyName("full_name")]
    public required string FullName { get; set; }
    public required string Email { get; set; }
    [JsonPropertyName("unit_belong")]
    public required int UnitBelong { get; set; }
    [JsonPropertyName("unit_belong_name")]
    public required string UnitBelongName { get; set; }
}