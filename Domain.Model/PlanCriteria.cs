namespace Domain.Model
{
    public class PlanCriteria
    {
        public string Texto { get; }

        public PlanCriteria(string texto)
        {
            Texto = texto ?? string.Empty;
        }
    }
}
