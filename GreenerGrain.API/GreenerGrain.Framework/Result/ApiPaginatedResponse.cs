using System.Collections.Generic;

namespace GreenerGrain.Framework.Result
{
    /// <summary>
    /// Envelope de resposta paginada dos métodos das APIs
    /// </summary>
    /// <typeparam name="T">Tipo a ser envelopado.</typeparam>
    public class ApiPaginatedResponse<T> : ApiResponse<T>
    {
        /// <summary>
        /// Construtor
        /// </summary>
        public ApiPaginatedResponse() : base()
        {
        }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="success">Indica se ocorreu sucesso na execução do método solicitado</param>
        public ApiPaginatedResponse(bool success) : base(success)
        {
        }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="success">Indica se ocorreu sucesso na execução do método solicitado</param>
        /// <param name="result">Resultado da operação</param>
        public ApiPaginatedResponse(bool success, T result) : base(success, result)
        {
        }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="errors">Lista de erros ocorridos na execução do método solicitado.</param>
        public ApiPaginatedResponse(List<Error> errors) : base(errors)
        {
        }

        /// <summary>
        /// Indica os detalhes de paginação da resposta atual.
        /// </summary>
        public Pagination Paging { get; set; }
    }
}
