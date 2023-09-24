using Mc2.CrudTest.Application.Features.Customers.Get;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Mc2.CrudTest.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly IMediator mediator;

    public CustomerController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet(Name = "Get/{id}")]
    public async Task<ActionResult> Get(Guid id)
    {
        var result = await this.mediator.Send(new GetCustomerQuery() { Id = id });
        return Ok(result);
    }

    [HttpPost(Name = "Add")]
    public async Task<ActionResult<CreateCustomerCommandResponse>> Create(
        [FromBody] CreateCustomerCommand createCommand)
    {
        var response = await this.mediator.Send(createCommand);
        return Ok(response);
    }

    [HttpPut(Name = "Update")]
    public async Task<ActionResult<UpdateCustomerCommandResponse>> Update(
        [FromBody] UpdateCustomerCommand updateCommand)
    {
        var response = await this.mediator.Send(updateCommand);
        return Ok(response);
    }

    [HttpDelete(Name = "Delete")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await this.mediator.Send(new DeleteCustomerCommand { Id = id});
        return NoContent();
    }
}