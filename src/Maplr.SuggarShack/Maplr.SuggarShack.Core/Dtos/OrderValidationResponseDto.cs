namespace Maplr.SuggarShack.Core.Dtos;

public class OrderValidationResponseDto
{
    public bool IsOrderValid { get; set; }

    public string[] Errors { get; set; }
}
