namespace AcadEvalSys.Application.Common;

public class PagedResult<T>
{
    public PagedResult(IEnumerable<T> items, int totalCount, int? pageSize, int? pageNumber)
    {
        Items = items;
        TotalItemsCount = totalCount;

        var currentPageNumber = pageNumber ?? 1;
        var currentPageSize = pageSize ?? (totalCount > 0 ? totalCount : 10); // Default to totalCount or 10 if totalCount is 0

        TotalPages =
            (int)Math.Ceiling(totalCount / (double)currentPageSize); // calculo la cantidad de pag redondeando para arriba
        ItemsFrom = (currentPageSize * (currentPageNumber - 1) + 1); //el primer elemento de la pagina
        ItemsTo = Math.Min((ItemsFrom + currentPageSize - 1),
            TotalItemsCount); // ultimo elemento de la pagina. el mat min es en el caso de que en la ultima pagina haya menos elementos que el size
    }

    public IEnumerable<T> Items { get; set; }
    public int TotalPages { get; set; }
    public int TotalItemsCount { get; set; }
    public int ItemsFrom { get; set; }
    public int ItemsTo { get; set; }
}