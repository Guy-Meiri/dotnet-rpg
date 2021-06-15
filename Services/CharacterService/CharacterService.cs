using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_rpg.Data;
using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        public CharacterService(IMapper mapper, DataContext context)
        {
            this._context = context;
            this._mapper = mapper;

        }
        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();

            Character c = _mapper.Map<Character>(newCharacter);
            await _context.Characters.AddAsync(c);
            await _context.SaveChangesAsync();

            serviceResponse.Data = _context.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            List<Character> dbCharacters = await _context.Characters.ToListAsync();
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            serviceResponse.Data = _mapper.Map<List<GetCharacterDto>>(dbCharacters);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();
            Character dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
            GetCharacterDto c = _mapper.Map<GetCharacterDto>(dbCharacter);
            serviceResponse.Data = c;
            return serviceResponse;
        }

        public async Task<ServiceResponse<DeleteCharacterDto>> DeleteCharacter(DeleteCharacterDto deleteCharacterDto)
        {

            ServiceResponse<DeleteCharacterDto> serviceResponse = new ServiceResponse<DeleteCharacterDto>();
            Character dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == deleteCharacterDto.Id);
            _context.Characters.Remove(dbCharacter);
            await _context.SaveChangesAsync();
            serviceResponse.Data = _mapper.Map<DeleteCharacterDto>(dbCharacter);

            return serviceResponse;
        }
        public async Task<ServiceResponse<UpdateCharacterDto>> UpdateCaracter(UpdateCharacterDto updatedCharacter)
        {
            ServiceResponse<UpdateCharacterDto> serviceResponse = new ServiceResponse<UpdateCharacterDto>();
            try
            {
                Character dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);

                dbCharacter.Name = updatedCharacter.Name;
                dbCharacter.Class = updatedCharacter.Class;
                dbCharacter.Defense = updatedCharacter.Defense;
                dbCharacter.Intelligence = updatedCharacter.Intelligence;
                dbCharacter.HitPoints = updatedCharacter.HitPoints;
                dbCharacter.Strength = updatedCharacter.Strength;
                _context.Characters.Update(dbCharacter);
                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<UpdateCharacterDto>(dbCharacter);

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