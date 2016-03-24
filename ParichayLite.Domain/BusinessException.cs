using System;

namespace ParichayLite.Domain.Entities
{
    internal class BusinessException : Exception
    {
        private string _exception;
        private string _innerException;

        public BusinessException()
        {
        }

        public BusinessException(string message) : base(message)
        {
        }

        public BusinessException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public BusinessException(string exception, string innerException)
        {
            this._exception = exception;
            this._innerException = innerException;
        }
    }
}