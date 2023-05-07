using GreenerGrain.Framework.Enumerators;
using System;

namespace GreenerGrain.Framework.Exceptions
{
    /// <summary>
    /// Exception para representar um HTTP 500 - INTERNAL SERVER ERROR
    /// </summary>
    public class InternalException : BaseException
    {
        /// <summary>
        /// Construtor
        /// </summary>
        public InternalException() : base(500) { }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="message">Mensagem descrevendo a exception.</param>
        public InternalException(string message) : base(500, message) { }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="message">Mensagem descrevendo a exception.</param>
        /// <param name="innerException">Exception com a causa da exception atual.</param>
        public InternalException(string message, Exception innerException) : base(500, message, innerException) { }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="errorCode">Codigo de erro a ser utilizado na ocorrência da Exception.</param>
        /// <param name="message">Mensagem descrevendo a exception.</param>
        public InternalException(string errorCode, string message) : base(errorCode, 500, message) { }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="message">Mensagem descrevendo a exception.</param>
        /// <param name="innerException">Exception com a causa da exception atual.</param>
        public InternalException(string errorCode, string message, Exception innerException) : base(errorCode, 500, message, innerException) { }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="error">Enumeration erro a ser utilizado na ocorrência da Exception.</param>
        public InternalException(Enumeration error) : base(500, error) { }
    }
}
