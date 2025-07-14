// Services/DataLoader.cs (hoặc nơi bạn đặt DataLoader)
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq; // Cần cho FirstOrDefault
using System.Text.Json;
using BusinessObjects; // Đảm bảo đã import namespace chứa JsonRoot, Product, Category, AccountMember

namespace Services
{
    public static class DataLoader
    {
        // Đường dẫn đến file data.json, đảm bảo đúng với cấu trúc dự án của bạn
        // Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "data.json")
        // AppDomain.CurrentDomain.BaseDirectory là thư mục chứa file .exe của ứng dụng (ví dụ: bin/Debug/net8.0/)
        private static readonly string DataDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
        private static readonly string DataFilePath = Path.Combine(DataDirectory, "data.json");

        // Cấu hình JsonSerializerOptions để xử lý tên thuộc tính không phân biệt chữ hoa/thường
        private static readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true, // Cực kỳ quan trọng để khớp "accounts" với "Accounts", v.v.
            WriteIndented = true,               // Để JSON được định dạng đẹp khi lưu
            AllowTrailingCommas = true,         // Có thể hữu ích nếu JSON của bạn có dấu phẩy thừa
            ReadCommentHandling = JsonCommentHandling.Skip // Bỏ qua comment trong JSON nếu có
        };

        public static JsonRoot LoadFromJson()
        {
            if (!File.Exists(DataFilePath))
            {
                // Nếu file không tồn tại, tạo một JsonRoot rỗng và lưu lại
                // Điều này giúp tránh lỗi NullReferenceException khi ứng dụng mới chạy lần đầu
                var emptyData = new JsonRoot();
                SaveToJson(emptyData); // Tạo file rỗng
                return emptyData;
            }

            try
            {
                string jsonString = File.ReadAllText(DataFilePath);
                var data = JsonSerializer.Deserialize<JsonRoot>(jsonString, _options);

                // Quan trọng: Kiểm tra null sau khi deserialize
                if (data == null)
                {
                    // Nếu deserialization trả về null (ví dụ: file JSON trống hoặc không hợp lệ)
                    // Bạn có thể chọn trả về một JsonRoot rỗng hoặc throw lỗi tùy vào logic ứng dụng
                    throw new Exception("Dữ liệu JSON không hợp lệ hoặc trống rỗng.");
                }

                // Liên kết các Product với Category tương ứng (nếu cần cho hiển thị)
                if (data.Products != null && data.Categories != null)
                {
                    foreach (var product in data.Products)
                    {
                        product.Category = data.Categories.FirstOrDefault(c => c.CategoryId == product.CategoryId);
                    }
                }

                return data;
            }
            catch (JsonException ex)
            {
                // Xử lý lỗi khi phân tích cú pháp JSON
                Console.WriteLine($"JSON Parsing Error: {ex.Message}");
                throw new Exception($"Lỗi cú pháp JSON: {ex.Message}. Vui lòng kiểm tra lại file data.json của bạn.", ex);
            }
            catch (Exception ex)
            {
                // Xử lý các lỗi khác khi đọc file
                Console.WriteLine($"File Reading Error: {ex.Message}");
                throw new Exception($"Lỗi khi tải dữ liệu từ file JSON: {ex.Message}", ex);
            }
        }

        public static void SaveToJson(JsonRoot data)
        {
            try
            {
                // Đảm bảo thư mục "Data" tồn tại trước khi ghi file
                if (!Directory.Exists(DataDirectory))
                {
                    Directory.CreateDirectory(DataDirectory);
                }

                string jsonString = JsonSerializer.Serialize(data, _options);
                File.WriteAllText(DataFilePath, jsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"File Writing Error: {ex.Message}");
                throw new Exception($"Lỗi khi lưu dữ liệu vào file JSON: {ex.Message}", ex);
            }
        }
    }
}