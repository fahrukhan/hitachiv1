using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace hitachiv1.Services.AssetServices
{
    public class AreaService : IAreaService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AreaService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor){
            _mapper = mapper;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Response<T>> Add<T>(object newObject)
        {
            var response = new Response<T>();
            var ar = newObject as AddAreaDto;
            var area = _mapper.Map<Area>(ar);
            var exist = await _context.Areas
                .Where(e => e.Code == ar!.Code || e.Name.ToLower() == ar!.Name.ToLower())
                .FirstOrDefaultAsync();

            if(exist is not null){
                response.Success = false;
                response.Message = "Areas code or name already exists!";
            }else{
                try
                {
                    _context.Areas.Add(area);
                    await _context.SaveChangesAsync();

                    var res = _mapper.Map<T>(area);
                    response.Data = res;
                }
                catch (Exception e)
                {
                    response.Success = false;
                    response.Message = e.Message;
                }
            }
            
            return response;
        }

        public async Task<Response<List<T>>> All<T>()
        {
            var response = new Response<List<T>>();
            var areas = await _context.Areas.ToListAsync();
            response.Data = areas.Select(a => _mapper.Map<T>(a)).ToList();
            return response;

        }

        public Task<Response<string>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<T>> GetById<T>(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<T>> Update<T>(object updateDepartment)
        {
            throw new NotImplementedException();
        }
    }
}