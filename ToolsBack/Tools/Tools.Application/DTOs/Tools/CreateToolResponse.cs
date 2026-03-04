/// </summary>
public sealed class CreateToolResponse
{
    /// <summary>
    /// Identificador da ferramenta criada.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Nome da ferramenta (opcional).
    /// </summary>
    public string? Name { get; init; }

    /// <summary>
    /// Descrição da ferramenta (opcional).
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// Tags associadas (opcional).
    /// </summary>
    public IReadOnlyList<string>? Tags { get; init; }
}

