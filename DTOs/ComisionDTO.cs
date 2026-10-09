namespace DTOs
{
    public class ComisionDTO
    {
        public int Id { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public int AnioEspecialidad { get; set; }
        public int IdPlan { get; set; }
    }
}
