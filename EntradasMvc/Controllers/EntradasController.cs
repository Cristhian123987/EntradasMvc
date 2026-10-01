using EntradasMvc.Models;
using EntradasMvc.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EntradasMvc.Controllers;

public class EntradasController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        var viewModel = new CotizacionInputViewModel();

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Calcular(CotizacionInputViewModel viewModel)
    {
        // 1. ENTRADA: el ViewModel llega por Model Binding y se valida.
        if (!ModelState.IsValid)
        {
            return View("Index", viewModel);
        }

        // 2. NEGOCIO: ViewModel -> Model (reglas de cálculo en Cotizacion).
        var cotizacion = new Cotizacion
        {
            Cliente = viewModel.Cliente,
            Cantidad = viewModel.Cantidad,
            TipoEntrada = viewModel.TipoEntrada
        };

        // 3. PRESENTACIÓN: datos preparados para la vista Resultado.
        var resultadoViewModel = new ResultadoCotizacionViewModel
        {
            Cotizacion = cotizacion,
            Evento = "Concierto Web III",
            FechaEvento = new DateTime(2026, 11, 15),
            Mensaje = "Gracias por realizar su cotización."
        };

        return View("Resultado", resultadoViewModel);
    }
}
