namespace Domain.Model
{
    public class ComisionCriteria
    {
        public string Texto { get; set; } = string.Empty;

        public ComisionCriteria(string texto)
        {
            Texto = texto ?? string.Empty;
        }
    }
}
