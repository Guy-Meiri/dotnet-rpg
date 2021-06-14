using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Models;

namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper;
        public CharacterService(IMapper mapper)
        {
            this._mapper = mapper;

        }
        private static List<Character> characters = new List<Character>(){

            new Character(),
            new Character{Id = 1, Name= "Sam", HitPoints=40},
            new Character{Id = 2, Name= "Mosh", HitPoints=50}
        };
        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            Character c = _mapper.Map<Character>(newCharacter);
            c.Id = characters.Max(c => c.Id) + 1;
            characters.Add(c);
            serviceResponse.Data = _mapper.Map<List<GetCharacterDto>>(characters);
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            serviceResponse.Data = _mapper.Map<List<GetCharacterDto>>(characters);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();
            GetCharacterDto c = _mapper.Map<GetCharacterDto>(characters.FirstOrDefault(c => c.Id == id));
            serviceResponse.Data = c;
            return serviceResponse;
        }

        public async Task<ServiceResponse<UpdateCharacterDto>> UpdateCaracter(UpdateCharacterDto updatedCharacter)
        {
            ServiceResponse<UpdateCharacterDto> serviceResponse = new ServiceResponse<UpdateCharacterDto>();
            try
            {

                Character c = characters.FirstOrDefault(c => c.Id == updatedCharacter.Id);
                c.Name = updatedCharacter.Name;
                c.Class = updatedCharacter.Class;
                c.Defense = updatedCharacter.Defense;
                c.Intelligence = updatedCharacter.Intelligence;
                c.HitPoints = updatedCharacter.HitPoints;
                c.Strength = updatedCharacter.Strength;

                serviceResponse.Data = _mapper.Map<UpdateCharacterDto>(c);

            }
            catch (Exception e)
            {
                serviceResponse.success = false;
                serviceResponse.Message = e.Message;
            }
            return serviceResponse;
        }
    }
}