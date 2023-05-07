using GreenerGrain.Framework.Enumerators;
using System;

namespace GreenerGrain.Framework.Exceptions
{
    /// <summary>
    /// Exception para representar um HTTP 501 - NOT IMPLEMENTED
    /// </summary>
    public class NotImplementedException : BaseException
    {
        /// <summary>
        /// Construtor
        /// </summary>
        public NotImplementedException() : base(501) { }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="message">Mensagem descrevendo a exception.</param>
        public NotImplementedException(string message) : base(501, message) { }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="message">Mensagem descrevendo a exception.</param>
        /// <param name="innerException">Exception com a causa da exception atual.</param>
        public NotImplementedException(string message, Exception innerException) : base(501, message, innerException) { }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="errorCode">Codigo de erro a ser utilizado na ocorrência da Exception.</param>
        /// <param name="message">Mensagem descrevendo a exception.</param>
        public NotImplementedException(string errorCode, string message) : base(errorCode, 501, message) { }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="message">Mensagem descrevendo a exception.</param>
        /// <param name="innerException">Exception com a causa da exception atual.</param>
        public NotImplementedException(string errorCode, string message, Exception innerException) : base(errorCode, 501, message, innerException) { }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="error">Enumeration erro a ser utilizado na ocorrência da Exception.</param>
        public NotImplementedException(Enumeration error) : base(501, error) { }
    }
}
