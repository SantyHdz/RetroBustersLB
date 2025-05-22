using lib_dominio.Entidades;
using lib_presentaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace asp_presentacion.Pages.Ventanas
{
    public class AuditoriasModel : SecurePageModel
    {
        private readonly IAuditoriasPresentacion _auditoriasPresentacion;

        public AuditoriasModel(
            IAuditoriasPresentacion auditoriasPresentacion,
            IRolesPresentacion rolesPresentacion,
            IHttpContextAccessor httpContextAccessor)
            : base(rolesPresentacion, httpContextAccessor)
        {
            _auditoriasPresentacion = auditoriasPresentacion;
        }

        [BindProperty] public List<Auditoria>? Lista { get; set; }

        public async Task OnGetAsync()
        {
            Lista = await _auditoriasPresentacion.Listar();
        }
    }
}