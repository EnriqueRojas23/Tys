

using Domain.Common.Exceptions;
namespace Domain.TYS.Seguridad.Exceptions
{
    public class SistemaRolUsuarioException : DomainException
    {
        public SistemaRolUsuarioException(string message)
            : base(message)
        {
        }
    }
}
