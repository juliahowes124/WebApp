using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AnimalShelter.API.Data;
using AnimalShelter.API.DTOs;
using AnimalShelter.API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnimalShelter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private readonly IAnimalRepository _animal_repo;
        private readonly IMapper _mapper;
        private readonly IUserRepository _user_repo;

        public AnimalsController(IAnimalRepository animal_repo, IMapper mapper, IUserRepository user_repo)
        {
            _user_repo = user_repo;
            _animal_repo = animal_repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAnimals()
        {
            var animals = await _animal_repo.GetAnimals();
            var animalsToReturn = _mapper.Map<IEnumerable<AnimalForListDto>>(animals);
            return Ok(animalsToReturn);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAnimal(int id)
        {
            var animal = await _animal_repo.GetAnimal(id);
            var user = await _user_repo.GetUser(animal.UserId);
            var firstMap= _mapper.Map<AnimalForDetailDto>(user);
            var animalToReturn = _mapper.Map(animal, firstMap);
            // for(like in animalToReturn.Likes)
            // {

            // }
            return Ok(animalToReturn);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(AnimalForRegisterDto animalForRegisterDto)
        {
            var animalToCreate = _mapper.Map<Animal>(animalForRegisterDto);
            var createdAnimal = await _animal_repo.Register(animalToCreate);
            return StatusCode(201);
        }

        [HttpPut("{id}", Name="GetAnimal")]
        public async Task<IActionResult> UpdateAnimal(int id, AnimalForUpdateDto animalForUpdateDto) 
        {
            // if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            //     return Unauthorized();
            
            var animalFromRepo = await _animal_repo.GetAnimal(id);

            _mapper.Map(animalForUpdateDto, animalFromRepo);

            if (await _animal_repo.SaveAll())
                return NoContent();
            throw new System.Exception($"Updating user {id} failed on save");
        }

    }

}