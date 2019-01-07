
using Domain.Common.Exceptions;
namespace Domain.TYS.Seguridad.Exceptions
{
    public class UsuarioException : DomainException
    {
        public UsuarioException(string message)
            : base(message)
        {
        }
    }
}
