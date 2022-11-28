namespace AquariumShop.Dtos
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Nazwa { get; set; } = string.Empty;
        public int Cena { get; set; }
        public string Opis { get; set; } = string.Empty;
        public string Opis_dlugi {get; set;} = string.Empty;
        public string obrazek { get; set; } = string.Empty;
        public string kategoria { get; set; } = string.Empty;
        public string file { get; set; } = string.Empty;
    }
}
