﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.DTO;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using PokemonReviewApp.Repository;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : Controller
    {
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IMapper _mapper;

        public PokemonController(IPokemonRepository pokemonRepository,IMapper mapper)
        {
            _pokemonRepository = pokemonRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200,Type=typeof(IEnumerable<Pokemon>))]
        public IActionResult GetPokemons()
        {
            var pokemons =_mapper.Map<List<PokemonDto>>( _pokemonRepository.GetPokemons());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(pokemons);
        }
        [HttpGet("{pokeid}")]
        [ProducesResponseType(200, Type = typeof(Pokemon))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemon(int pokeid)
        {
            if (!_pokemonRepository.PokemonExists(pokeid))
                return NotFound();

            var pokemon =_mapper.Map<Pokemon>( _pokemonRepository.GetPokemon(pokeid));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(pokemon);
        }

        [HttpGet("{pokeId}")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonRating(int pokeId)
        {
            if (!_pokemonRepository.PokemonExists(pokeId))
                return NotFound();
            var rating = _pokemonRepository.GetPokemonRating(pokeId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(rating);
        }
        // Create method
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePokemon([FromQuery] int ownerId, [FromQuery] int catId, [FromBody] PokemonDto pokemonCreate)
        {
            if (pokemonCreate == null)
                return BadRequest(ModelState);

            var pokemons = _pokemonRepository.GetPokemonTrimToUpper(pokemonCreate);

            if (pokemons != null)
            {
                ModelState.AddModelError("", "Owner already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var pokemonMap = _mapper.Map<Pokemon>(pokemonCreate);


            if (!_pokemonRepository.CreatePokemon(ownerId, catId, pokemonMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }
    }
}
