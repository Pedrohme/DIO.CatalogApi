using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using DIO.CatalogApi.Exceptions;
using DIO.CatalogApi.InputModel;
using DIO.CatalogApi.Services;
using DIO.CatalogApi.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace DIO.CatalogApi.Controllers.V1 {
    [Route("api/V1/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase {
        private readonly ISongService _songService;

        public SongsController(ISongService songService) {
            _songService = songService;
        }

        /// <summary>
        /// Gets songs in pages
        /// </summary>
        /// <remarks>
        /// Its not possible to return the songs without paging
        /// </remarks>
        /// <param name="page">The page to query. min 1</param>
        /// <param name="numItems">The number of itens per page. min 1, max 50</param>
        /// <response code="200">Returns the song list</response>
        /// <response code="204">No songs to be returned</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SongViewModel>>> Get([FromQuery, Range(1, int.MaxValue)] int page = 1, [FromQuery, Range(1, 50)] int numItems = 5) {
            var result = await _songService.Get(page, numItems);

            if (result.Count == 0) {
                return NoContent();
            }

            return Ok(result);
        }

        /// <summary>
        /// Gets song from id
        /// </summary>
        /// <param name="songId">Song id to query</param>
        /// <response code="200">Returns the song</response>
        /// <response code="204">No song song found with this id</response>
        [HttpGet("{songId:guid}")]
        public async Task<ActionResult<SongViewModel>> Get([FromRoute] Guid songId) {
            var result = await _songService.Get(songId);

            if (result == null) {
                return NoContent();
            }

            return Ok(result);
        }

        /// <summary>
        /// Inserts a song in the catalog
        /// </summary>
        /// <param name="song">Song data to be inserted</param>
        /// <response code="200">Song successfully inserted</response>
        /// <response code="422">Song already registered</response>
        [HttpPost]
        public async Task<ActionResult<SongViewModel>> InsertSong([FromBody] SongInputModel song) {
            try {
                var result = await _songService.Insert(song);
                return Ok(result);
            }
            catch (SongAlreadyRegisteredException) {
                return UnprocessableEntity("This song has already been registered");
            }
        }

        /// <summary>
        /// Updates a song in the catalog
        /// </summary>
        /// <param name="songId">the registered song's id</param>
        /// <param name="song">the new song data</param>
        /// <response code="200">Song successfully updated</response>
        /// <response code="404">No song found with this id</response>
        [HttpPut("{songId:guid}")]
        public async Task<ActionResult> UpdateSong([FromRoute] Guid songId, [FromBody] SongInputModel song) {
            try {
                await _songService.Update(songId, song);
                return Ok();
            }
            catch (SongNotRegisteredException) {
                return NotFound("This song doesn't exist");
            }
        }

        /// <summary>
        /// Change a song's album
        /// </summary>
        /// <param name="songId">the registered song's id</param>
        /// <param name="album">the new album</param>
        /// <response code="200">Song successfully updated</response>
        /// <response code="404">No song found with this id</response>
        [HttpPatch("{songId:guid}/album/{album}")]
        public async Task<ActionResult> UpdateSong([FromRoute] Guid songId, [FromRoute] string album) {
            try {
                await _songService.Update(songId, album);
                return Ok();
            }
            catch (SongNotRegisteredException) {
                return NotFound("This song doesn't exist");
            }
        }

        /// <summary>
        /// Deletes a song from the catalog
        /// </summary>
        /// <param name="songId">The song's id</param>
        /// <response code="200">Song successfully deleted</response>
        /// <response code="404">No song found with this id</response>
        [HttpDelete("{songId:guid}")]
        public async Task<ActionResult> DeleteSong([FromRoute] Guid songId) {
            try {
                await _songService.Delete(songId);
                return Ok();
            }
            catch (SongNotRegisteredException) {
                return NotFound("This song doesn't exist");
            }
        }
    }
}
