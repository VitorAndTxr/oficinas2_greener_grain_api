using GreenerGrain.Framework.Enumerators;
using System;

namespace GreenerGrain.Framework.Exceptions
{
    /// <summary>
    /// Exception para representar um HTTP 404 - NOT FOUND
    /// </summary>
    public class NotFoundException : BaseException
    {
        /// <summary>
        /// Construtor
        /// </summary>
        public NotFoundException() : base(404) { }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="message">Mensagem descrevendo a exception.</param>
        public NotFoundException(string message) : base(404, message) { }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="message">Mensagem descrevendo a exception.</param>
        /// <param name="innerException">Exception com a causa da exception atual.</param>
        public NotFoundException(string message, Exception innerException) : base(404, message, innerException) { }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="errorCode">Codigo de erro a ser utilizado na ocorrência da Exception.</param>
        /// <param name="message">Mensagem descrevendo a exception.</param>
        public NotFoundException(string errorCode, string message) : base(errorCode, 404, message) { }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="message">Mensagem descrevendo a exception.</param>
        /// <param name="innerException">Exception com a causa da exception atual.</param>
        public NotFoundException(string errorCode, string message, Exception innerException) : base(errorCode, 404, message, innerException) { }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="error">Enumeration erro a ser utilizado na ocorrência da Exception.</param>
        public NotFoundException(Enumeration error) : base(404, error) { }
    }
}
