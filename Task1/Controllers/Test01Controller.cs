using Microsoft.AspNetCore.Mvc;
using Task1.Models;
using Task1.Services;

namespace Task1.Controllers;

[ApiController]
[Route("/api/test-01")]
public class Test01Controller : ControllerBase
{
    private readonly ITest01Service _service;

    public Test01Controller(ITest01Service service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult Get([FromQuery] Test01Parameters test01Parameters)
    {
        var test01 = _service.Get(test01Parameters);
        var metadata = new
        {
            test01.TotalCount,
            test01.PageSize,
            test01.CurrentPage,
            test01.TotalPages,
            test01.HasNext,
            test01.HasPrevious
        };

        var result = new
        {
            metadata,
            data = test01
        };

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var test01 = _service.GetById(id);
        return Ok(test01);
    }

    [HttpPost]
    public IActionResult Add([FromBody] Test01 model)
    {
        _service.Add(model);
        return Ok();
    }


    [HttpPut]
    public IActionResult Update(int id, [FromBody] Test01 model)
    {
        _service.Update(id, model);
        return Ok();
    }

    [HttpDelete]
    public IActionResult Remove(int id)
    {
        _service.Remove(id);
        return Ok();
    }
}