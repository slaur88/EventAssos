using System;
using System.Collections.Generic;
using System.Text;

namespace EventAssos.Core.Objects
{
    public class ResultPattern<T>
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public string? ErrorMessage { get; }
        public T Data { get; }

        
        private ResultPattern(bool isSuccess, T data, string? errorMessage = null)
        {
            IsSuccess = isSuccess;
            Data = data;
            ErrorMessage = errorMessage;
        }

        
        public static ResultPattern<T> Success(T data) => new(true, data);

        
        public static ResultPattern<T> Success() => new(true, default!);

        
        public static ResultPattern<T> Failure(string errorMessage)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(errorMessage);
            return new(false, default!, errorMessage);
        }

        
        public static implicit operator ResultPattern<T>(string error) => Failure(error);

       
        public static implicit operator bool(ResultPattern<T> result) => result.IsSuccess;
    }

}

