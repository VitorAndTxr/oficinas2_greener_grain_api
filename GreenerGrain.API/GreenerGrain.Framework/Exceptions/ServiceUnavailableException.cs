using GreenerGrain.Framework.Enumerators;
using System;

namespace GreenerGrain.Framework.Exceptions
{
    /// <summary>
    /// Exception para representar um HTTP 503 - SERVICE UNAVAILABLE
    /// </summary>
    public class ServiceUnavailableException : BaseException
    {
        /// <summary>
        /// Construtor
        /// </summary>
        public ServiceUnavailableException() : base(503) { }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="message">Mensagem descrevendo a exception.</param>
        public ServiceUnavailableException(string message) : base(503, message) { }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="message">Mensagem descrevendo a exception.</param>
        /// <param name="innerException">Exception com a causa da exception atual.</param>
        public ServiceUnavailableException(string message, Exception innerException) : base(503, message, innerException) { }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="errorCode">Codigo de erro a ser utilizado na ocorrência da Exception.</param>
        /// <param name="message">Mensagem descrevendo a exception.</param>
        public ServiceUnavailableException(string errorCode, string message) : base(errorCode, 503, message) { }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="message">Mensagem descrevendo a exception.</param>
        /// <param name="innerException">Exception com a causa da exception atual.</param>
        public ServiceUnavailableException(string errorCode, string message, Exception innerException) : base(errorCode, 503, message, innerException) { }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="error">Enumeration erro a ser utilizado na ocorrência da Exception.</param>
        public ServiceUnavailableException(Enumeration error) : base(503, error) { }
    }
}
