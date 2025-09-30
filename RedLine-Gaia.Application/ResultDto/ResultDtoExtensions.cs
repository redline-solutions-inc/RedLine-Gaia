using FluentResults;
using Mapster;
using RedLine_Gaia.Domain.Models;

namespace RedLine_Gaia.Application.ResultDto;

/// <summary>
/// Extensions for the FluentResults to map to internal REsultDto object.
/// </summary>
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

    public static IEnumerable<ErrorDto> TransformErrors(IEnumerable<IError> errors)
    {
        return errors.Select(TransformError);
    }

    public static ErrorDto TransformError(IError error)
    {
        var key = TransformErrorKey(error);

        return new ErrorDto(error.Message, key ?? string.Empty);
    }

    /// <summary>
    /// Converts a PaginationResult to a ResultDto with paginated data.
    /// </summary>
    /// <typeparam name="TEntity">The source entity type</typeparam>
    /// <typeparam name="TDto">The destination DTO type</typeparam>
    /// <param name="paginationResult">The pagination result to convert</param>
    /// <returns>A ResultDto containing the paginated data</returns>
    public static ResultDto<PaginationResult<TDto>> ToPaginatedResultDto<TEntity, TDto>(
        this PaginationResult<TEntity> paginationResult
    )
    {
        var mappedResults = paginationResult.Results.Adapt<IEnumerable<TDto>>();

        var paginatedDto = new PaginationResult<TDto>
        {
            Page = paginationResult.Page,
            PageSize = paginationResult.PageSize,
            TotalPages = paginationResult.TotalPages,
            TotalSize = paginationResult.TotalSize,
            Results = mappedResults,
        };

        return new ResultDto<PaginationResult<TDto>>(
            true,
            Enumerable.Empty<ErrorDto>(),
            paginatedDto
        );
    }

    /// <summary>
    /// Converts a Result containing PaginationResult to a ResultDto with paginated data.
    /// </summary>
    /// <typeparam name="TEntity">The source entity type</typeparam>
    /// <typeparam name="TDto">The destination DTO type</typeparam>
    /// <param name="result">The result containing pagination data to convert</param>
    /// <returns>A ResultDto containing the paginated data</returns>
    public static ResultDto<PaginationResult<TDto>> ToPaginatedResultDto<TEntity, TDto>(
        this Result<PaginationResult<TEntity>> result
    )
    {
        if (result.IsFailed)
        {
            return new ResultDto<PaginationResult<TDto>>(
                false,
                TransformErrors(result.Errors),
                new PaginationResult<TDto>()
            );
        }

        var paginationResult = result.Value;
        var mappedResults = paginationResult.Results.Adapt<IEnumerable<TDto>>();

        var paginatedDto = new PaginationResult<TDto>
        {
            Page = paginationResult.Page,
            PageSize = paginationResult.PageSize,
            TotalPages = paginationResult.TotalPages,
            TotalSize = paginationResult.TotalSize,
            Results = mappedResults,
        };

        return new ResultDto<PaginationResult<TDto>>(
            true,
            Enumerable.Empty<ErrorDto>(),
            paginatedDto
        );
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
