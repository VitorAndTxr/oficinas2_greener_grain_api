using System.Collections.Generic;

namespace GreenerGrain.Framework.Result
{
    /// <summary>
    /// Envelope de resposta dos métodos das APIs
    /// </summary>
    /// <typeparam name="T">Tipo a ser envelopado.</typeparam>
    public class RestResponse<T>
    {
        /// <summary>
        /// Indica se ocorreu sucesso na execução do método solicitado.
        /// </summary>
        public bool Success { get; set; } = false;

        /// <summary>
        /// Resultado da operação (se houver).
        /// </summary>
        public T Result { get; set; } = default(T);

        /// <summary>
        /// Lista de erros ocorridos na execução do método solicitado.
        /// </summary>
        public Error Error { get; set; } = null;

        /// <summary>
        /// Construtor
        /// </summary>
        public RestResponse()
        {
        }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="success">Indica se ocorreu sucesso na execução do método solicitado</param>
        public RestResponse(bool success)
        {
            this.Success = success;
        }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="success">Indica se ocorreu sucesso na execução do método solicitado</param>
        /// <param name="result">Resultado da operação</param>
        public RestResponse(bool success, T result)
        {
            this.Success = success;
            this.Result = result;
        }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="error">Erro ocorrido na execução do método solicitado.</param>
        public RestResponse(Error error)
        {
            this.Success = false;
            this.Error = error;
        }
    }
}
