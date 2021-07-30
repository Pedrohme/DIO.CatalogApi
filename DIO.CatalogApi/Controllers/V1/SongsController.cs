using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SongViewModel>>> Get([FromQuery, Range(1, int.MaxValue)] int page = 1, [FromQuery, Range(1, 50)] int numItems = 5) {
            var result = await _songService.Get(page, numItems);

            if (result.Count == 0) {
                return NoContent();
            }

            return Ok(result);
        }

        [HttpGet("{songId:guid}")]
        public async Task<ActionResult<SongViewModel>> Get([FromRoute] Guid songId) {
            var result = await _songService.Get(songId);

            if (result == null) {
                return NoContent();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<SongViewModel>> InsertSong([FromBody] SongInputModel song) {
            try {
                var result = await _songService.Insert(song);
                return Ok(result);
            }
            catch (SongAlreadyRegisteredException e) {
                return UnprocessableEntity("This song has already been registered");
            }
        }

        [HttpPut("{songId:guid}")]
        public async Task<ActionResult> UpdateSong([FromRoute] Guid songId, [FromBody] SongInputModel song) {
            try {
                await _songService.Update(songId, song);
                return Ok();
            }
            catch (SongNotRegisteredException e) {
                return NotFound("This song doesn't exist");
            }
        }

        [HttpPatch("{songId:guid}/album/{album}")]
        public async Task<ActionResult> UpdateSong([FromRoute] Guid songId, [FromRoute] string album) {
            try {
                await _songService.Update(songId, album);
                return Ok();
            }
            catch (SongNotRegisteredException e) {
                return NotFound("This song doesn't exist");
            }
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteSong([FromRoute] Guid songId) {
            try {
                await _songService.Delete(songId);
                return Ok();
            }
            catch (SongNotRegisteredException e) {
                return NotFound("This song doesn't exist");
            }
        }
    }
}
