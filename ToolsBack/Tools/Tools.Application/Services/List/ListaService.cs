using Tools.Application.DTOs.Tools;
using Tools.Application.Interfaces;
using Tools.Application.Notifications;
using Tools.Domain.Entities;

namespace Tools.Application.Services.List
{
    public class ListaService : IListaService
    {
        private readonly IListaRepository _listaRepository;
        private readonly NotificationContext _notification;
        public ListaService(IListaRepository listaRepository, NotificationContext notification)
        {
            _listaRepository = listaRepository;
            _notification = notification;
        }
        public async Task<ListResponse?> CreateListaAsync(CreateListRequest request, Guid userId)
        {
            if(string.IsNullOrEmpty(request.Name))
            {
                _notification.AddErrors("list.name", "Nome é obrigatório");
                return null;
            }

            var list = new Lista(request.Name, userId);

            await _listaRepository.CreateAsync(list);

            return new ListResponse
            {
                Id = list.Id,
                Name = list.Name
            };
        }

        public async Task<bool> DeleteListaAsync(Guid id, Guid userId)
        {
            var lista = await _listaRepository.GetByIdAsync(id, userId);

            if(lista == null)
            {
                _notification.AddErrors("list.id", "Lista não encontrada");
                return false;
            }
            await _listaRepository.DeleteAsync(lista);
            return true;
        }

        public async Task<List<ListResponse>> GetAllListasAsync(Guid userId)
        {
            var listas = await _listaRepository.GetAllByUserAsync(userId);

            return listas.Select(l => new ListResponse
            {
                Id = l.Id,
                Name = l.Name
            }).ToList();
        }

        public async Task<ListResponse?> GetListaByIdAsync(Guid id, Guid userId)
        {
            var lista = await _listaRepository.GetByIdAsync(id, userId);

            if (lista == null)
            {
                _notification.AddErrors("list.id", "Lista não encontrada");
                return null;
            };

            return new ListResponse
            {
                Id = lista.Id,
                Name = lista.Name,
                Tools = lista.Tools.Select(t => new ToolResponse
                {
                    Id = t.Id,
                    ListaId = t.ListaId,
                    ListaNome = lista.Name,
                    Name = t.Name,
                    Description = t.Description,
                    Tags = t.Tags.Select(tag => tag.Name).ToList()
                }).ToList()
            };
        }

        public async Task<bool> UpdateListaAsync(Guid id, UpdateListRequest request, Guid userId)
        {
            var lista = await _listaRepository.GetByIdAsync(id, userId);

            if (lista == null)
            {
                _notification.AddErrors("list.id", "Lista não encontrada");
                return false;
            }
            lista.Tools.Clear();

            if (request.Tools != null)
            {
                lista.UpdateName(request.Name);
                foreach (var tool in request.Tools)
                {
                    var newTool = new Tool(tool.Name, tool.Description, lista.Id);
                    if (tool.Tags != null)
                    {
                        foreach (var tagName in tool.Tags)
                        {
                            var tag = new Tag(tagName);
                            newTool.Tags.Add(tag);
                        }
                    }
                    lista.Tools.Add(newTool);
                }
            }
            await _listaRepository.UpdateAsync(lista);

            return true;
        }
    }
}
