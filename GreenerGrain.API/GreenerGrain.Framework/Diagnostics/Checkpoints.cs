using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace GreenerGrain.Framework.Diagnostics
{
    /// <summary>
    /// Checkpoints de instrumentação para medição de tempo entre seções de código.
    /// </summary>
    public class Checkpoints
    {
        /// <summary>
        /// Representação interna de uma seção de código,
        /// </summary>
        internal class TSection
        {
            /// <summary>
            /// Nome da seção.
            /// </summary>
            public string SectionName = "";

            /// <summary>
            /// Timestamp de entrada na seção.
            /// </summary>
            public long TimeMs = 0;

            /// <summary>
            /// Timestamps internos registrados para seção.
            /// </summary>
            public List<TItem> items = new List<TItem>();

            /// <summary>
            /// Construtor
            /// </summary>
            /// <param name="sectionName">Nome da seção.</param>
            /// <param name="timeMs">Timestamp de entrada na seção.</param>
            public TSection(string sectionName, long timeMs)
            {
                SectionName = sectionName;
                TimeMs = timeMs;
            }

            /// <summary>
            /// Tempo total consumido dentro da seção (em milisegundos).
            /// </summary>
            public long TotalTimeMs
            {
                get
                {
                    if (items.Count == 0) return 0;
                    return items.Last().TimeMs - this.TimeMs;
                }
            }
        }

        /// <summary>
        /// Representação interna de um timestamp 
        /// </summary>
        internal class TItem
        {
            /// <summary>
            /// Mensagem associado a esse timestamp.
            /// </summary>
            public string Message = "";

            /// <summary>
            /// Timestamp em milisegundos.
            /// </summary>
            public long TimeMs = 0;

            /// <summary>
            /// Construtor.
            /// </summary>
            /// <param name="message">Mensagem associado a esse timestamp.</param>
            /// <param name="timeMs">Timestamp em milisegundos.</param>
            public TItem(string message, long timeMs)
            {
                Message = message;
                TimeMs = timeMs;
            }
        }

        /// <summary>
        /// Lista interna de seções.
        /// </summary>
        private List<TSection> _sections = new List<TSection>();

        /// <summary>
        /// Referencia para a seção atual.
        /// </summary>
        private TSection _currentSection = null;

        /// <summary>
        /// Stopwatch para geração dos timestamps.
        /// </summary>
        private Stopwatch stopwatch = new Stopwatch();

        /// <summary>
        /// Registra a entrada em uma nova seção de código.
        /// </summary>
        /// <param name="sectionName">Nome da seção</param>
        public void EnterSection(string sectionName)
        {
            if (!stopwatch.IsRunning)
            {
                stopwatch.Start();
            }

            _currentSection = new TSection(sectionName, stopwatch.ElapsedMilliseconds);
            _sections.Add(_currentSection);
        }

        /// <summary>
        /// Registra a saída da seção atual do código.
        /// </summary>
        public void ExitSection()
        {
            _currentSection = null;
        }

        /// <summary>
        /// Adiciona uma nova mensagem (e consequentemente o timestamp) à seção atual de código.
        /// </summary>
        /// <param name="message">Mensagem a ser adicionada.</param>
        public void Add(string message)
        {
            if (_currentSection == null)
            {
                EnterSection("New Section");
            }


            _currentSection.items.Add(new TItem(message, stopwatch.ElapsedMilliseconds));
        }

        /// <summary>
        /// Retorna a listagem de medição de tempo para todas seções registradas.
        /// </summary>
        /// <returns>Listagem de medição de tempo para todas seções registradas</returns>
        public List<string> GetMeasurements()
        {
            List<string> list = new List<string>();

            foreach (var section in _sections)
            {
                list.Add("Enter Section [" + section.SectionName + "]: " + section.TimeMs);
                long lastTime = section.TimeMs;

                foreach (var item in section.items)
                {
                    list.Add(item.Message + ":" + (item.TimeMs - lastTime));
                    lastTime = item.TimeMs;
                }

                list.Add("Exit Section [" + section.SectionName + "]. Total time: " + section.TotalTimeMs);
            }

            return list;
        }

        /// <summary>
        /// Retorna a listagem de medição de tempo para todas seções registradas.
        /// </summary>
        /// <returns>Listagem de medição de tempo para todas seções registradas.</returns>
        public string GetMeasurementsString()
        {
            StringBuilder sb = new StringBuilder();

            List<string> list = this.GetMeasurements();
            foreach (var line in list)
            {
                sb.AppendLine(line);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Retorna o tempo consumido até o momento dentro da seção atual de código
        /// </summary>
        public long CurrentSectionTimeInMs
        {
            get
            {
                if (_currentSection == null) return 0;
                return _currentSection.TotalTimeMs;
            }
        }
    }
}
