using AutoMapper;
using PokemonReviewApp.DTO;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Helpers
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Pokemon, PokemonDto>();
            CreateMap<PokemonDto,Pokemon >();
            CreateMap<Owner, OwnerDto>();
            CreateMap<OwnerDto,Owner >();
            CreateMap<Review,ReviewDto >();
            CreateMap<ReviewDto,Review >();
            CreateMap<Reviewer, ReviewerDto>();
            CreateMap<ReviewerDto,Reviewer >();
            CreateMap<Country, CountryDto>();
            CreateMap<CountryDto,Country >();
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto,Category >();


        }
    }
}
