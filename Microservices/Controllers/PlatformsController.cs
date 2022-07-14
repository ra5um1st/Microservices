using AutoMapper;
using DataUtils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Platforms.Data.DTOs;
using Services.Platforms.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Platforms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        public PlatformsController(IRepository<Platform> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        private readonly IRepository<Platform> repository;
        private readonly IMapper mapper;

        //GET api/platforms
        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDTO>> GetAll()
        {
            var items = repository.GetAll();
            return Ok(mapper.Map<IEnumerable<PlatformReadDTO>>(items));
        }

        //GET api/platforms/id
        [HttpGet("{id}", Name = nameof(Get))]
        public async Task<ActionResult<PlatformReadDTO>> Get(int id)
        {
            var item = await repository.GetAsync(id);

            if(item == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<PlatformReadDTO>(item));
        }

        //POST api/platforms/id
        [HttpPost("{id}")]
        public async Task<ActionResult<PlatformReadDTO>> Create(PlatformCreateDTO platformCreateDTO)
        {
            var itemToCreate = mapper.Map<Platform>(platformCreateDTO);
            var item = await repository.CreateAsync(itemToCreate);
            await repository.SaveChangesAsync();
            var platformReadDTO = mapper.Map<PlatformReadDTO>(item);
            return CreatedAtRoute(nameof(Get), new { platformReadDTO.Id }, platformReadDTO);
        }
    }
}
