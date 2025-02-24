using System;
using FluentResults;
using Mapster;

namespace RedLine_Gaia.Application.ResultDto;

public static class ResultDtoExtensions
{
    public static ResultDto ToResultDto(this Result result)
    {
        if (result.IsSuccess)
            return new ResultDto(true, Enumerable.Empty<ErrorDto>());

        return new ResultDto(false, TransformErrors(result.Errors));
    }

    /// <summary>
    /// Convert base Fluent Result to custom ResultDTO.
    /// </summary>
    /// <typeparam name="T">The source Entity type</typeparam>
    /// <typeparam name="M">The destination DTO type</typeparam>
    /// <param name="result">ResultDto that uses Mapster.Adapt to map <T> to <M></param>
    /// <returns></returns>
    public static ResultDto<M> ToResultDto<T, M>(this Result<T> result)
    {
        if (result.IsSuccess)
            return new ResultDto<M>(
                true,
                Enumerable.Empty<ErrorDto>(),
                result.ValueOrDefault.Adapt<M>()
            );

        return new ResultDto<M>(
            false,
            TransformErrors(result.Errors),
            result.ValueOrDefault.Adapt<M>()
        );
    }

    private static IEnumerable<ErrorDto> TransformErrors(List<IError> errors)
    {
        return errors.Select(TransformError);
    }

    private static ErrorDto TransformError(IError error)
    {
        var key = TransformErrorKey(error);

        return new ErrorDto(error.Message, key);
    }

    private static string? TransformErrorKey(IError error)
    {
        if (error.Metadata.TryGetValue("Code", out var Code))
            return Code.ToString();
        else if (error.Metadata.TryGetValue("code", out var code))
            return code.ToString();

        return "";
    }
}
