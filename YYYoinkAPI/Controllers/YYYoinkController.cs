using Microsoft.AspNetCore.Mvc;

namespace YYYoinkAPI.Controllers;

public class YYYoinkController : APIController
{
    private readonly IYYYoinkService _yyyoinkService;

    public YYYoinkController(IYYYoinkService yyyoinkService)
    {
        if (yyyoinkService == null)
        {
            throw new ArgumentNullException(nameof(yyyoinkService), "yyyoinkservice cannot be null");
        }

        _yyyoinkService = yyyoinkService;
    }

    [HttpPost]
    public Task<IActionResult> CreateYYYoink(CreateYYYoinkRequest request)
    {
        YYYoink yyyoink = new YYYoink(
            Guid.NewGuid(),
            request.Title,
            request.TargetLanguage,
            request.ResolutionLanguage,
            request.Body,
            null,
            null,
            null,
            request.AccountUuid
        );
        Task<ErrorOr<Created>> createYYYoinkResult = _yyyoinkService.CreateYYYoink(yyyoink);
        return createYYYoinkResult.Match(
            created => CreatedAtGetYYYoink(yyyoink),
            errors => Problem(errors)
        );
    }

    private static YYYoinkResponse MapYYYoinkResponse(YYYoink yyyoink)
    {
        return new YYYoinkResponse(
            yyyoink.Uuid,
            yyyoink.Name,
            yyyoink.AccountUuid
            );
    }

    private CreatedAtActionResult CreatedAtGetYYYoink(YYYoink yyyoink)
    {
        return CreatedAtAction(
            actionName: nameof(CreateYYYoink),
            routeValues: new { uuid = yyyoink.Uuid },
            value: MapYYYoinkResponse(yyyoink)
        );
    }
}