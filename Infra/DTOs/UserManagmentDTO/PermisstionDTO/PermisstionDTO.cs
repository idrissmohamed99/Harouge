namespace Infra.DTOs
{
    public class PermisstionDTO : ActivePermisstionDTO
    {
        public string Name { get; set; }
        public string ModuleName { get; set; }
        public string ModuleId { get; set; }
        public bool IsActive { get; set; }
    }
}
