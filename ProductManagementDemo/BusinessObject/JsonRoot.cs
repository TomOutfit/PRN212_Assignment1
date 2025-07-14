// BusinessObjects/JsonRoot.cs
using System.Collections.Generic;
// using System.Text.Json.Serialization; // Cần thiết nếu dùng [JsonPropertyName]

namespace BusinessObjects
{
    public class JsonRoot
    {
        // Nếu bạn muốn tên thuộc tính C# khác với tên JSON, bạn sẽ dùng [JsonPropertyName("json_key_name")]
        // Tuy nhiên, với PropertyNameCaseInsensitive = true trong JsonSerializerOptions,
        // bạn chỉ cần đảm bảo tên là duy nhất và có cùng ý nghĩa.
        // Ví dụ: "accounts" trong JSON sẽ khớp với thuộc tính "Accounts"
        public List<AccountMember>? Accounts { get; set; }
        public List<Category>? Categories { get; set; }
        public List<Product>? Products { get; set; }

        public JsonRoot()
        {
            Accounts = new List<AccountMember>();
            Categories = new List<Category>();
            Products = new List<Product>();
        }
    }
}