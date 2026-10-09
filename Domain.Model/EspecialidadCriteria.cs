namespace Domain.Model
{
    public class EspecialidadCriteria
    {
        public string Texto { get; set; } = string.Empty;

        public EspecialidadCriteria(string texto)
        {
            Texto = texto ?? string.Empty;
        }
    }
}
