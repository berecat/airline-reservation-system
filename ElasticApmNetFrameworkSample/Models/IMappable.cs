namespace ElasticApmNetFrameworkSample.Models
{
    public interface IMappable<T>
    {
        T Convert();
    }
}