using Microsoft.AspNetCore.Mvc;
using TranslatorApi.Models;
using TranslatorApi.Services;

namespace TranslatorApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TranslateController : ControllerBase
{
	[HttpPost("gettext")]
	public IActionResult GetText(HtmlInputModel model)
	{
		string translateText = SeleniumService.Translate(model.Html);
		if (string.IsNullOrEmpty(translateText))
			return BadRequest();

		string t = translateText;
		return Ok(new HtmlInputModel { Html = translateText });
	}
}
