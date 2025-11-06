namespace BabelBooks.Core.Application.ReadModels.ProductReadModels
{
    public class ProductReadModel
    {

        public Guid Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal ProductPrice { get; set; }
    }
}
//Esta es la clase 'denormalizada' que se utiliza para las consultas de productos
//Marten la guardará como un documento JSON y la API la utilizará para devolver datos al cliente
