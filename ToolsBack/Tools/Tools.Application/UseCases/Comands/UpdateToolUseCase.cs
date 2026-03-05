using Tools.Application.Interfaces;
using Tools.Domain.Entities;

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

            if (tool is null)
                return false;

            tool.Update(name, description);

            if (tags is not null)
            {
                tool.Tags.Clear();

                var normalizedTags = tags
                    .Where(t => !string.IsNullOrWhiteSpace(t))
                    .Select(t => t.Trim().ToLower())
                    .Distinct()
                    .ToList();

                foreach (var tagName in normalizedTags)
                {
                    var existingTag = await _tagRepository.GetTagByNameAsync(tagName);

                    if (existingTag is not null)
                    {
                        tool.Tags.Add(existingTag);
                    }
                    else
                    {
                        var newTag = new Tag(tagName);
                        await _tagRepository.AddAsync(newTag);
                        tool.Tags.Add(newTag);
                    }
                }
            }

            await _toolRepository.UpdateToolAsync(tool);

            return true;
        }
    }
}