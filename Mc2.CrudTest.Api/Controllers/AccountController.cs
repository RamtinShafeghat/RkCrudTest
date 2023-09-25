using Microsoft.AspNetCore.Mvc;

namespace Mc2.CrudTest.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAuthenticationService authenticationService;

    public AccountController(IAuthenticationService authenticationService)
    {
        this.authenticationService = authenticationService;
    }

    [HttpPost("Authenticate")]
    public async Task<ActionResult<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request)
    {
        return Ok(await authenticationService.AuthenticateAsync(request));
    }

    [HttpPost("Register")]
    public async Task<ActionResult<RegistrationResponse>> RegisterAsync(RegistrationRequest request)
    {
        return Ok(await authenticationService.RegisterAsync(request));
    }
}
