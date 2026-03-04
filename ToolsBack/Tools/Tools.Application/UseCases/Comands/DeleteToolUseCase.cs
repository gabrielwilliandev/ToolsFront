using Tools.Application.Interfaces;

namespace Tools.Application.UseCases.Comands
{
    public class DeleteToolUseCase
    {
        private readonly IToolRepository _toolRepository;
        public DeleteToolUseCase(IToolRepository toolRepository)
        {
            _toolRepository = toolRepository;
        }
        public async Task<bool> ExecuteAsync(Guid id)
        {
            var tool = await _toolRepository.GetToolByIdAsync(id);
            if (tool == null)
            {
                return false;
            }
            await _toolRepository.DeleteToolAsync(id);
            return true;
        }
    }
}
