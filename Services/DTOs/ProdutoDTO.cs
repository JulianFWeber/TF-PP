namespace TF_PP.Services.DTOs
{
    // Exemplo de entrada para ProductDTO:
    // {
    //     "Description": "Coca Cola Lait,
    //     "Barcode": "01234567890123",
    //     "Barcodetype": "EAN-14",
    //     "Price": 19.99,
    //     "Costprice": 15.99,
    //     "Stock": 100
    // }

    // Exemplo de saída para TbProduct:
    // {
    //     "Id": 1,
    //     "Description": "Coca Cola Lait,
    //     "Barcode": "01234567890123",
    //     "Barcodetype": "EAN-14",
    //     "Price": 19.99,
    //     "Costprice": 15.99,
    //     "Stock": 100
    // }

    public class ProdutoDTO
    {
        public string Description { get; set; }
        public string Barcode { get; set; }
        public string Barcodetype { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public decimal Costprice { get; set; }
    }
}