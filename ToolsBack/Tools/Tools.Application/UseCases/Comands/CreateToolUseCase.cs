using Tools.Application.Interfaces;
using Tools.Domain.Entities;

namespace Tools.Application.UseCases.CreateTool
{
    public class CreateToolUseCase
    {
        private readonly IToolRepository _toolRepository;
        private readonly ITagRepository _tagRepository;
        public CreateToolUseCase(IToolRepository toolRepository, ITagRepository tagRepository)
        {
            _toolRepository = toolRepository;
            _tagRepository = tagRepository;
        }
        public async Task<Tool> ExecuteAsync(string name, string description, List<string> tags)
        {
            var tool = new Tool(name, description);
            if (tags != null) {
                foreach (var tagName in tags.Where(t => !string.IsNullOrWhiteSpace(t)))
                {
                    var normalizedTagName = tagName.Trim();
                    var existingTag = await _tagRepository.GetTagByNameAsync(tagName.Trim().ToLower());

                    if (existingTag != null)
                    {
                        tool.Tags.Add(existingTag);
                    }
                    else
                    {
                        var newTag = new Tag(normalizedTagName);
                        tool.Tags.Add(newTag);
                    }
                }
            }


            await _toolRepository.AddToolAsync(tool);
            return tool;
        }
    }
}
