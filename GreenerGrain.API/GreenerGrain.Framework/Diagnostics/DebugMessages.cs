using System.Collections.Generic;

namespace GreenerGrain.Framework.Diagnostics
{
    /// <summary>
    /// Mensagens de depuração úteis para instrumentação do código
    /// </summary>
    public class DebugMessages
    {
        /// <summary>
        /// Lista interna de mensagens.
        /// </summary>
        private List<string> list = new List<string>();

        /// <summary>
        /// Adiciona uma nova string à lista de mensagens.
        /// </summary>
        /// <param name="message">Mensagem a ser adicionada.</param>
        public void Add(string message)
        {
            list.Add(message);
        }

        /// <summary>
        /// Limpa a lista de mensagens.
        /// </summary>
        public void Clear()
        {
            list.Clear();
        }

        /// <summary>
        /// Retorna uma cópia da lista de mensagens.
        /// </summary>
        /// <returns></returns>
        public List<string> ToList()
        {
            return new List<string>(list);
        }
    }
}
