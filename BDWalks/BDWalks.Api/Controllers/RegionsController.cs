using BDWalks.Api.Data;
using BDWalks.Api.Models.Domain;
using BDWalks.Api.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace BDWalks.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly BdWalksDbContext dbContext;
        public RegionsController(BdWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        //Get All Region
        [HttpGet]
        public IActionResult GetAll()
        {
            // Get Data From Database - Domain models
            var regionsDomain = dbContext.Regions.ToList();

            //Map Domain Model to Dto
            var regionsDto = new List<RegionDto>();
            foreach (var regionDomain in regionsDomain)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = regionDomain.Id,
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    RegionImageUrl = regionDomain.RegionImageUrl,
                });
            }
            //Return Dto
            return Ok(regionsDto);
        }

        //Get Single Region By id
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            // var region = dbContext.Regions.Find(id);
            // Get Region Domain Model From Database
            var regionDomain = dbContext.Regions.FirstOrDefault(r => r.Id == id);
            if (regionDomain == null)
            {
                return NotFound();
            }
            //Map/ Convert Region Domain Model to Region Dto

            var regionDto = new RegionDto()
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl,
            };
            //return DTO Back to Client
            return Ok(regionDto);
        }

        // Post to Create New Region
        [HttpPost]
        public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            //Map or Convert Dto to Domain Model
            var regionDomainModel = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl =addRegionRequestDto.RegionImageUrl

            };

            //Use Domain Model to Create Region
             dbContext.Regions.Add(regionDomainModel); 
             dbContext.SaveChanges();

            //Map Domain Model Back to Dto
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            return CreatedAtAction(nameof(GetById), new {id = regionDto.Id }, regionDto);

        }

        // Update Region
        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            var regionDomainModel = dbContext.Regions.FirstOrDefault(x => x.Id == id);
            if(regionDomainModel == null)
            {
                return NotFound();
            }
    
            regionDomainModel.Code = updateRegionRequestDto.Code;
            regionDomainModel.Name = updateRegionRequestDto.Name;
            regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;
            dbContext.SaveChanges();

            //Convert Domain Model to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            return Ok(regionDto);
        }

        //Delete Region
        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
           var regionDomainModel = dbContext.Regions.FirstOrDefault(x => x.Id == id); 
            if( regionDomainModel == null)
            {
                return NotFound();
            }
            //Delete Region
            dbContext.Regions.Remove(regionDomainModel);
            dbContext.SaveChanges();
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            return Ok(regionDto);
        }
    
    }
}


