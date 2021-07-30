using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DIO.CatalogApi.Entities;
using DIO.CatalogApi.Exceptions;
using DIO.CatalogApi.InputModel;
using DIO.CatalogApi.Repositories;
using DIO.CatalogApi.ViewModel;

namespace DIO.CatalogApi.Services {
    public class SongService : ISongService {
        private readonly ISongRepository _songRepository;

        public SongService(ISongRepository songRepository) {
            _songRepository = songRepository;
        }

        public async Task Delete(Guid id) {
            var song = await _songRepository.Get(id);

            if (song == null)
                throw new SongNotRegisteredException();

            await _songRepository.Delete(id);
        }

        public async Task<List<SongViewModel>> Get(int page, int numItems) {
            var songs = await _songRepository.Get(page, numItems);

            return songs.Select(song => new SongViewModel {
                Id = song.Id,
                Name = song.Name,
                Album = song.Album,
                Author = song.Author
            }).ToList();
        }

        public async Task<SongViewModel> Get(Guid id) {
            var song = await _songRepository.Get(id);

            if (song == null) {
                return null;
            }

            return new SongViewModel {
                Id = song.Id,
                Name = song.Name,
                Album = song.Album,
                Author = song.Author
            };
        }

        public async Task<SongViewModel> Insert(SongInputModel song) {
            var songEntity = await _songRepository.Get(song.Name, song.Author);

            if (songEntity.Count > 0) {
                throw new SongAlreadyRegisteredException();
            }

            var insertSong = new Song {
                Id = Guid.NewGuid(),
                Name = song.Name,
                Album = song.Album,
                Author = song.Author
            };

            await _songRepository.Insert(insertSong);

            return new SongViewModel {
                Id = insertSong.Id,
                Name = song.Name,
                Album = song.Album,
                Author = song.Author
            };
        }

        public async Task Update(Guid id, SongInputModel song) {
            var songEntity = await _songRepository.Get(id);

            if (songEntity == null) {
                throw new SongNotRegisteredException();
            }

            songEntity.Name = song.Name;
            songEntity.Album = song.Album;
            songEntity.Author = song.Author;

            await _songRepository.Update(songEntity);
        }

        public async Task Update(Guid id, string album) {
            var songEntity = await _songRepository.Get(id);

            if (songEntity == null) {
                throw new SongNotRegisteredException();
            }

            songEntity.Album = album;

            await _songRepository.Update(songEntity);
        }

        public void Dispose() {
            _songRepository?.Dispose();
        }
    }
}
