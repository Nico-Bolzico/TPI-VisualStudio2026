namespace DTOs
{
    public class PlanDTO
    {
        public int Id { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public int IdEspecialidad { get; set; }
        public List<MateriaDTO> Materias { get; set; } = new();
    }
}
