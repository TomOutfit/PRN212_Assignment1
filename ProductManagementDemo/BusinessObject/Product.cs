namespace BusinessObjects
{
    public partial class Product
    {
        public Product() { }
        public Product(int id, string name, int catID, short unitInStock, decimal price)
        {
            this.ProductID = id;
            this.ProductName = name;
            this.CategoryId = catID;
            this.UnitsInStock = unitInStock;
            this.UnitPrice = price;
        }

        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int? CategoryId { get; set; }
        public short? UnitsInStock { get; set; }
        public decimal? UnitPrice { get; set; }
        public virtual Category Category { get; set; }
    }
}