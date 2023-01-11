﻿namespace AquariumShop.Dtos
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public string LongDescription {get; set;} = string.Empty;
        public string Picture { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
    }
}
