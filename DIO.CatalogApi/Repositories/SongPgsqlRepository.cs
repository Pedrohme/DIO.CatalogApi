using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DIO.CatalogApi.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace DIO.CatalogApi.Repositories {
    public class SongPgsqlRepository : ISongRepository {
        private readonly NpgsqlConnection conn;

        public SongPgsqlRepository(IConfiguration configuration) {
            conn = new NpgsqlConnection(configuration.GetConnectionString("Default"));
        }

        public async Task Delete(Guid id) {
            await conn.OpenAsync();

            await using (var cmd = new NpgsqlCommand("DELETE FROM song WHERE id = (@p)", conn)) {
                cmd.Parameters.AddWithValue("p", id);
                await cmd.ExecuteNonQueryAsync();
            }

            await conn.CloseAsync();
        }

        public async Task<List<Song>> Get(int page, int numItems) {
            throw new NotImplementedException();
        }

        public async Task<Song> Get(Guid id) {
            Song song = null;

            await conn.OpenAsync();

            await using (var cmd = new NpgsqlCommand("SELECT * FROM song WHERE id = (@p)", conn)) {
                cmd.Parameters.AddWithValue("p", id);
                await using (var reader = await cmd.ExecuteReaderAsync()) {
                    while (await reader.ReadAsync()) {
                        song = new Song() {
                            Id = (Guid)reader["id"],
                            Name = (string)reader["name"],
                            Album = (string)reader["album"],
                            Author = (string)reader["author"]
                        };
                    }
                }
            }

            await conn.CloseAsync();

            return song;
        }

        public async Task<List<Song>> Get(string name, string author) {
            var songs = new List<Song>();

            await conn.OpenAsync();

            await using (var cmd = new NpgsqlCommand("SELECT * FROM song WHERE name = (@p) AND author = (@q)", conn)) {
                cmd.Parameters.AddWithValue("p", name);
                cmd.Parameters.AddWithValue("q", author);
                await using (var reader = await cmd.ExecuteReaderAsync()) {
                    while (await reader.ReadAsync()) {
                        songs.Add(new Song() {
                            Id = (Guid)reader["id"],
                            Name = (string)reader["name"],
                            Album = (string)reader["album"],
                            Author = (string)reader["author"]
                        });
                    }
                }
            }

            await conn.CloseAsync();

            return songs;
        }

        public async Task Insert(Song song) {
            await conn.OpenAsync();

            await using (var cmd = new NpgsqlCommand("INSERT INTO song (id, name, album, author) VALUES (@m, @n, @o, @p)", conn)) {
                cmd.Parameters.AddWithValue("m", song.Id);
                cmd.Parameters.AddWithValue("n", song.Name);
                cmd.Parameters.AddWithValue("o", song.Album);
                cmd.Parameters.AddWithValue("p", song.Author);
                await cmd.ExecuteNonQueryAsync();
            }

            await conn.CloseAsync();
        }

        public async Task Update(Song song) {
            throw new NotImplementedException();
        }

        public void Dispose() {
            conn?.Close();
            conn?.Dispose();
        }
    }
}
