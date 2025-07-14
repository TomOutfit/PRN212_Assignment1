// BusinessObjects/Category.cs
using System.Collections.Generic;
using System.Text.Json.Serialization; // Thêm dòng này

namespace BusinessObjects
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public Category(int catID, string catName)
        {
            this.CategoryId = catID;
            this.CategoryName = catName ?? string.Empty; // Khởi tạo nếu null
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty; // Khởi tạo mặc định

        // THAY ĐỔI: Đánh dấu thuộc tính này để System.Text.Json bỏ qua khi serialize/deserialize
        [JsonIgnore]
        public virtual ICollection<Product> Products { get; set; }
    }
}