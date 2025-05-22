using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class IndexModel : PageModel
{
    private readonly IUsuariosPresentacion iPresentacion = null;

    public IndexModel(IUsuariosPresentacion iPresentacion)
    {
        this.iPresentacion = iPresentacion;
    }

    public bool EstaLogueado = false;
    [BindProperty] public string? Email { get; set; }
    [BindProperty] public string? Contrasena { get; set; }
    [BindProperty] public Usuarios? Usuarios { get; set; }
    [BindProperty] public List<Usuarios>? Usuarios_list { get; set; }
    public string? ErrorMessage { get; set; }

    public void OnGet()
    {
        var variable_session = HttpContext.Session.GetString("Usuario");
        if (!string.IsNullOrEmpty(variable_session))
        {
            EstaLogueado = true;
            Email = variable_session;
        }
    }

    public void OnPostBtClean()
    {
        Email = string.Empty;
        Contrasena = string.Empty;
        ErrorMessage = null;
    }

    public void OnPostBtEnter()
    {
        if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Contrasena))
        {
            ErrorMessage = "Debe ingresar email y contraseña.";
            return;
        }

        var hashContrasena = HashUtil.ComputeSha256Hash(Contrasena);

        var resultado = this.iPresentacion!.PorCorreo(Usuarios);
        resultado.Wait();
        Usuarios_list = resultado.Result;

        Usuarios = Usuarios_list.FirstOrDefault(u => u.Correo == Email);

        if (Usuarios == null || Usuarios.ContrasenaHash != hashContrasena)
        {
            ErrorMessage = "Email o contraseña incorrectos.";
            return;
        }

        HttpContext.Session.SetString("Usuario", JsonConversor.ConvertirAString(Usuarios));
        EstaLogueado = true;

        Email = null;
        Contrasena = null;
        ErrorMessage = null;
    }


    public void OnPostBtClose()
    {
        HttpContext.Session.Clear();
        EstaLogueado = false;
        Email = null;
        Contrasena = null;
        ErrorMessage = null;
    }
}