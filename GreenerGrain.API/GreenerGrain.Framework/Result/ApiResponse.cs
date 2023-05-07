using System.Collections.Generic;

namespace GreenerGrain.Framework.Result
{
    /// <summary>
    /// Envelope de resposta dos métodos das APIs
    /// </summary>
    /// <typeparam name="T">Tipo a ser envelopado.</typeparam>
    public class ApiResponse<T>
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
        public List<Error> Errors { get; set; } = null;

        /// <summary>
        /// Construtor
        /// </summary>
        public ApiResponse()
        {
        }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="success">Indica se ocorreu sucesso na execução do método solicitado</param>
        public ApiResponse(bool success)
        {
            this.Success = success;
        }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="success">Indica se ocorreu sucesso na execução do método solicitado</param>
        /// <param name="result">Resultado da operação</param>
        public ApiResponse(bool success, T result)
        {
            this.Success = success;
            this.Result = result;
        }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="errors">Lista de erros ocorridos na execução do método solicitado.</param>
        public ApiResponse(List<Error> errors)
        {
            this.Success = false;
            this.Errors = errors;
        }

        /// <summary>
        /// Adiciona um novo erro ao envelope de resposta.
        /// </summary>
        /// <param name="error">Erro a ser adicionado</param>
        public void AddError(Error error)
        {
            if (this.Errors == null)
            {
                this.Errors = new List<Error>();
            }
            this.Errors.Add(error);
        }
    }
}
