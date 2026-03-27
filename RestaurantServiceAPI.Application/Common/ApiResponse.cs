using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantServiceAPI.Application.Common;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
    public IEnumerable<string>? Errors { get; set; }

    public static ApiResponse<T> SuccessResponse(
        T data,
        string message = "Operation executed successfully") =>
        new()
        {
            Success = true,
            Message = message,
            Data = data
        };

    public static ApiResponse<T> SuccessResponse(
        string message = "Operation executed successfully") =>
        new()
        {
            Success = true,
            Message = message
        };

    public static ApiResponse<T> ErrorResponse(
        string message) =>
        new()
        {
            Success = false,
            Message = message,
            Errors = new[] { message }
        };

    public static ApiResponse<T> ErrorResponse(
        IEnumerable<string> errors,
        string message = "One or more errors occurred") =>
        new()
        {
            Success = false,
            Message = message,
            Errors = errors
        };

    public static ApiResponse<T> ValidationErrorResponse(
        IEnumerable<string> errors) =>
        new()
        {
            Success = false,
            Message = "Validation failed",
            Errors = errors
        };
}