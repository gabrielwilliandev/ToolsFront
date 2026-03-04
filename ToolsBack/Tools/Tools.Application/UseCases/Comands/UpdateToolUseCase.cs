using Tools.Application.Interfaces;

namespace Tools.Application.UseCases.Comands
{
    public class UpdateToolUseCase
    {
        private readonly IToolRepository _toolRepository;
        private readonly ITagRepository _tagRepository;

        public UpdateToolUseCase(IToolRepository toolRepository, ITagRepository tagRepository)
        {
            _toolRepository = toolRepository;
            _tagRepository = tagRepository;
        }

        public async Task<bool> ExecuteAsync(Guid id, string name, string description, List<string> tags)
        {
            var tool = await _toolRepository.GetToolByIdAsync(id);
            if (tool == null)
            {
                return false;
            }
            tool.Update(name, description);
            if (tags != null)
            {
                tool.Tags.Clear();
                foreach (var tagName in tags.Where(t => !string.IsNullOrWhiteSpace(t)))
                {
                    var normalizedTags = tags
            .Where(t => !string.IsNullOrWhiteSpace(t))
            .Select(t => t.Trim().ToLower())
            .Distinct();
                    foreach (var tag in normalizedTags)
                    {
                        var existingTag = await _tagRepository.GetTagByNameAsync(tagName);
                        if (existingTag != null)
                        {
                            tool.Tags.Add(existingTag);
                        }
                        else
                        {
                            var newTag = new Tools.Domain.Entities.Tag(tagName);
                            tool.Tags.Add(newTag);
                        }
                    }
                }
            }
            await _toolRepository.UpdateToolAsync(tool);
            return true;

        }
    }
}
