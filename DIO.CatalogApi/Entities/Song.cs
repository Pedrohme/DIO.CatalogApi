using System;
namespace DIO.CatalogApi.Entities {
    public class Song {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Album { get; set; }
        public string Author { get; set; }
    }
}
