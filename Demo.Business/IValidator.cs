using Demo.Shared.Enums;

namespace Demo.Business
{
    public interface IValidator
    {
        public DTOStatus Validate<T>(T entity);
    }
}
