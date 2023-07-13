using AutoMapper;
using BookStore.DbOperations;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public abstract class ApiController:ControllerBase
    {
        protected readonly IBookStoreDbContext _dbContext;
        protected readonly IMapper _mapper;
        public ApiController(IBookStoreDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }
    }
}
