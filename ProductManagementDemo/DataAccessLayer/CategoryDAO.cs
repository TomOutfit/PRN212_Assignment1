using System.Collections.Generic;
using System.Linq;
using BusinessObjects;
using Services; // Cần thiết để truy cập DataLoader
using System; // Cần thiết cho Exception

namespace DataAccessLayer
{
    public static class CategoryDAO // Nên là static class
    {
        public static List<Category> GetCategories()
        {
            try
            {
                // Tải dữ liệu mới nhất từ JSON mỗi lần gọi
                var data = DataLoader.LoadFromJson();
                return data.Categories ?? new List<Category>();
            }
            catch (Exception e)
            {
                throw new Exception($"Lỗi khi lấy danh sách danh mục từ JSON: {e.Message}", e);
            }
        }

        public static Category? GetCategoryById(int categoryId) // Thay đổi sang nullable Category?
        {
            try
            {
                // Tải dữ liệu mới nhất từ JSON mỗi lần gọi
                var data = DataLoader.LoadFromJson();
                return data.Categories?.FirstOrDefault(c => c.CategoryId == categoryId);
            }
            catch (Exception e)
            {
                throw new Exception($"Lỗi khi lấy danh mục theo ID từ JSON: {e.Message}", e);
            }
        }

        public static Category? GetCategoryById(string categoryIdString) // Thay đổi sang nullable Category?
        {
            try
            {
                if (string.IsNullOrEmpty(categoryIdString) || !int.TryParse(categoryIdString, out int id))
                {
                    return null;
                }
                return GetCategoryById(id);
            }
            catch (Exception e)
            {
                throw new Exception($"Lỗi khi lấy danh mục theo ID (string) từ JSON: {e.Message}", e);
            }
        }
    }
}