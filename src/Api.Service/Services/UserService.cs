using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.Dtos.User;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Services.User;
using AutoMapper;

namespace Api.Service.Services
{
    public class UserService : IUserService
    {
        private IRepository<UserEntity> _repository;
        private readonly IMapper _mapper;

        public UserService(IRepository<UserEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> Delete(Guid id) => await _repository.DeleteAsync(id);

        public async Task<UserDto> Get(Guid id)
        {
            var obj = await _repository.SelectAsync(id);

            return _mapper.Map<UserDto>(obj);
        }

        public async Task<IEnumerable<UserDto>> GetAll()
        {
            var objs = await _repository.SelectAsync();

            return _mapper.Map<IEnumerable<UserDto>>(objs);
        }

        public async Task<UserDto> Post(UserDtoCreate user)
        {
            var obj = _mapper.Map<UserEntity>(user);

            var result = await _repository.InsertAsync(obj);

            return _mapper.Map<UserDto>(result);
        }

        public async Task<UserDto> Put(UserDtoUpdate user)
        {
            var obj = _mapper.Map<UserEntity>(user);

            var result = await _repository.UpdateAsync(obj);

            return _mapper.Map<UserDto>(result);
        }
    }
}
