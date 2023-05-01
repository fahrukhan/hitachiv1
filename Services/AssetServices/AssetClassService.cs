using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hitachiv1.Dtos.AssetDetailDto;
using Microsoft.EntityFrameworkCore;

namespace hitachiv1.Services.AssetServices
{
    public class AssetClassService : IAssetClassService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AssetClassService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor){
            _mapper = mapper;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Response<T>> Add<T>(object newObject)
        {
            var response = new Response<T>();
            var ac = newObject as AddAssetClassDto;
            var assetClass = _mapper.Map<AssetClass>(ac);
            var exist = await _context.AssetClasses
                .Where(e => e.Code == ac!.Code || e.Name == ac!.Name)
                .FirstOrDefaultAsync();

            if(exist is not null){
                response.Success = false;
                response.Message = "AssetClass code or name already exists!";
            }else{
                try
                {
                    _context.AssetClasses.Add(assetClass);
                    await _context.SaveChangesAsync();

                    var res = _mapper.Map<T>(assetClass);
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
            
            var assetClass = await _context.AssetClasses.ToListAsync();
            response.Data = assetClass.Select(a => _mapper.Map<T>(a)).ToList();
            return response;
        }

        public async Task<Response<string>> Delete(int id)
        {
            var response = new Response<String>();

            try{
                var assetClass = await _context.AssetClasses.FirstOrDefaultAsync(c => c.Id == id);
                if(assetClass is null) {
                    response.Success = false;
                    response.Message = $"Asset Class not found";
                }else{
                    _context.AssetClasses.Remove(assetClass);
                    await _context.SaveChangesAsync();

                    response.Data = $"'{assetClass.Name}' deleted successfully.";
                    response.Message = "deleted successfully.";
                }
            } catch(Exception e) {
                response.Success = false;
                response.Message = e.Message;
            }

            return response;
        }

        public async Task<Response<T>> GetById<T>(int id)
        {
            var response = new Response<T>();
            var assetClass = await _context.AssetClasses.FindAsync(id);
            if(assetClass is null) {
                response.Success = false;
                response.Message = "Not found";
            }

            response.Data = _mapper.Map<T>(assetClass);
            return response;
        }

        public async Task<Response<T>> Update<T>(object updateAssetClass)
        {
            var response = new Response<T>();
            var ud = updateAssetClass as GetAssetClassDto;

            try
            {   
                var assetClass = await _context.AssetClasses
                    .FirstOrDefaultAsync(a => a.Id == ud!.Id);

                if(assetClass is null){
                    response.Success = false;
                    response.Message = "Not Found";
                }else{
                    assetClass.Name = ud!.Name;
                    assetClass.Code = ud.Code;
                    await _context.SaveChangesAsync();

                    response.Data = _mapper.Map<T>(assetClass);
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}