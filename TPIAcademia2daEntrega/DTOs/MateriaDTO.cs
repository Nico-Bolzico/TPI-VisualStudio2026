namespace DTOs
{
    public class MateriaDTO
    {
        public int Id { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public int HsSemanales { get; set; }
        public int HsTotales { get; set; }
        public int IdPlan { get; set; }
    }
}
