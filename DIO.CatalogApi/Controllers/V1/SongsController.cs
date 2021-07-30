using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DIO.CatalogApi.InputModel;
using DIO.CatalogApi.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace DIO.CatalogApi.Controllers.V1 {
    [Route("api/V1/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase {

        [HttpGet]
        public async Task<ActionResult<List<SongViewModel>>> Get() {
            return Ok();
        }

        [HttpGet("{songId:guid}")]
        public async Task<ActionResult<SongViewModel>> Get(Guid songId) {
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<SongViewModel>> InsertSong(SongInputModel song) {
            return Ok();
        }

        [HttpPut("{songId:guid}")]
        public async Task<ActionResult> UpdateSong(Guid songId, SongInputModel song) {
            return Ok();
        }

        [HttpPatch("{songId:guid}/album/{album:string}")]
        public async Task<ActionResult> UpdateSong(Guid songId, string album) {
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteSong(Guid songId) {
            return Ok();
        }
    }
}
