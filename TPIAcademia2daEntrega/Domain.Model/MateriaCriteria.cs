namespace Domain.Model
{
    public class MateriaCriteria
    {
        public string Texto { get; private set; }

        public MateriaCriteria(string texto)
        {
            Texto = (texto ?? string.Empty).Trim();
        }
    }
}
