namespace Domain.Model
{
    public class PersonaCriteria
    {
        public string Texto { get; private set; }

        public PersonaCriteria(string texto)
        {
            Texto = (texto ?? string.Empty).Trim();
        }
    }
}
