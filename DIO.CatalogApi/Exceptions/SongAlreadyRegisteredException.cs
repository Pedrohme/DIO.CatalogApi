using System;
namespace DIO.CatalogApi.Exceptions {
    public class SongAlreadyRegisteredException : Exception {
        public SongAlreadyRegisteredException() : base("This song has already been registered") { }
    }
}
