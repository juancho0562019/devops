namespace Bext.Reps.Domain.Commons.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, object key)
            : base($"No se encontro la entidad {name} con la llave {key}")
        {
        }

        public NotFoundException(string message)
            : base(message)
        {
        }
    }
}
