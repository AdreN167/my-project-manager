namespace MyProjectManager.Domain.Result;

// для возврата коллекций элементов
public class CollectionResult<T> : BaseResult<IEnumerable<T>>
{
    public int Count { get; set; }
}
