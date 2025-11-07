using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BabelBooksAPI.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public abstract class BaseAPIController : ControllerBase
    {
        /// <summary>
        /// Controlador base de API que usa el ISender de MediatR para manejar requests.
        /// Es mejor usar ISender para una robusta segregación de  interfaces; IMediator
        /// puede tanto enviar como publicar, así que es mejor dividir la funcionalidad.
        /// </summary>

        private ISender? _mediator;
        protected ISender Mediator
            => _mediator ??= HttpContext!.RequestServices.GetService<ISender>()!;
    }
}