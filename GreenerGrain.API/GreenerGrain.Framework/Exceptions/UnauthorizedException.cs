using GreenerGrain.Framework.Enumerators;
using System;

namespace GreenerGrain.Framework.Exceptions
{
    /// <summary>
    /// Exception para representar um HTTP 401 - UNAUTHORIZED
    /// </summary>
    public class UnauthorizedException : BaseException
    {
        /// <summary>
        /// Construtor
        /// </summary>
        public UnauthorizedException() : base(401) { }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="message">Mensagem descrevendo a exception.</param>
        public UnauthorizedException(string message) : base(401, message) { }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="message">Mensagem descrevendo a exception.</param>
        /// <param name="innerException">Exception com a causa da exception atual.</param>
        public UnauthorizedException(string message, Exception innerException) : base(401, message, innerException) { }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="errorCode">Codigo de erro a ser utilizado na ocorrência da Exception.</param>
        /// <param name="message">Mensagem descrevendo a exception.</param>
        public UnauthorizedException(string errorCode, string message) : base(errorCode, 401, message) { }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="message">Mensagem descrevendo a exception.</param>
        /// <param name="innerException">Exception com a causa da exception atual.</param>
        public UnauthorizedException(string errorCode, string message, Exception innerException) : base(errorCode, 401, message, innerException) { }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="error">Enumeration erro a ser utilizado na ocorrência da Exception.</param>
        public UnauthorizedException(Enumeration error) : base(401, error) { }
    }
}
