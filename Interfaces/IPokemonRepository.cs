﻿using PokemonReviewApp.DTO;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface IPokemonRepository
    {
        ICollection<Pokemon> GetPokemons();
        Pokemon GetPokemon(int id);
        Pokemon GetPokemon(string name);
        Decimal GetPokemonRating(int pokeId);
        bool PokemonExists(int pokeId); 
        bool CreatePokemon(int ownerId, int categoryId, Pokemon pokemon);
        bool UpdatePokemon(int ownerId, int categoryId, Pokemon pokemon);
        bool DeletePokemon(Pokemon pokemon);
        Pokemon GetPokemonTrimToUpper(PokemonDto pokemonCreate);
        bool Save();
    }
}
