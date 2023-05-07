using GreenerGrain.Framework.Enumerators;
using System;

namespace GreenerGrain.Framework.Result
{
    /// <summary>
    /// Indicador de erro na execução de um método da API.
    /// </summary>
    public class Error
    {
        /// <summary>
        /// Código do erro (se houver).
        /// </summary>
        public string Code { get; set; } = null;

        /// <summary>
        /// Mensagem de erro (se houver).
        /// </summary>
        public string Message { get; set; } = null;

        /// <summary>
        /// Construtor
        /// </summary>
        public Error()
        {
        }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="code">Código do erro</param>
        public Error(string code)
        {
            this.Code = code;
        }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="code">Código do erro</param>
        /// <param name="message">Mensagem de erro</param>
        public Error(string code, string message)
        {
            this.Code = code;
            this.Message = message;
        }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="code">Código do erro</param>
        /// <param name="exception">Exception que originou o erro. Será armazenada a mensagem da exception.</param>
        public Error(string code, Exception exception)
        {
            this.Code = code;
            this.Message = exception.Message;
        }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="error">Enumeration erro a ser utilizado na ocorrência da Exception.</param>
        public Error(Enumeration error)
        {
            this.Code = error.Code;
            this.Message = error.Name;
        }

    }
}
