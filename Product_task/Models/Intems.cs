﻿namespace Products.Models
{
    public class Items
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public Category Category {get; set;}

    }
}
