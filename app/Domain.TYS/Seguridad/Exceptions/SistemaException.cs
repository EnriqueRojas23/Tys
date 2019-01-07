

using Domain.Common.Exceptions;
namespace Domain.TYS.Seguridad.Exceptions
{
    public class SistemaException : DomainException
    {
        public SistemaException(string message)
            : base(message)
        {
        }
    }
}
