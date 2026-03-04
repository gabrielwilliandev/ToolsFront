using Tools.Application.Interfaces;
using Tools.Domain.Entities;

namespace Tools.Application.UseCases.Queries
{
    public class ToolQueryService
    {
        private readonly IToolRepository _toolRepository;

        public ToolQueryService(IToolRepository toolRepository)
        {
            _toolRepository = toolRepository;
        }

        public async Task<List<Tool>> GetAllAsync()
        {
            return await _toolRepository.GetAllAsync();
        }
        public async Task<Tool?> GetToolByIdAsync(Guid id)
        {
            return await _toolRepository.GetToolByIdAsync(id);

        }
        public async Task<IEnumerable<Tool>> SearchAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return Enumerable.Empty<Tool>();
            }
            return await _toolRepository.SearchAsync(query);
        }
    }
    }
