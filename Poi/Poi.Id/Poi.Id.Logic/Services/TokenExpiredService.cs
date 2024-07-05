using Poi.Id.InfraModel.DataAccess;
using Poi.Id.Logic.Dtos;
using Poi.Id.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Id.Logic.Services
{
    public class TokenExpiredService : ITokenExpiredService
    {
        private readonly IdDbContext _context;
        public TokenExpiredService(IdDbContext context)
        {
            _context = context;
        }

        public async Task<CudResponseDto> AddTokenExpired(string token)
        {
            var model = new TokenExpired
            {
                Token = token
            };

            _context.TokenExpired.Add(model);

            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                IsSucceeded = true
            };
        }
    }
}
