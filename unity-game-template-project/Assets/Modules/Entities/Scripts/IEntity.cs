namespace Modules.Entities
{
    public interface IEntity
    {
        public T Get<T>() where T : class;

        public bool TryGet<T>(out T component) where T : class;
    }
}