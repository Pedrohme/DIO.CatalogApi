using System;
namespace DIO.CatalogApi.Exceptions {
    public class SongNotRegisteredException : Exception {
        public SongNotRegisteredException() : base("This song is not registered") { }
    }
}
