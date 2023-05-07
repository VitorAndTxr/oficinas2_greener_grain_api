namespace GreenerGrain.Framework.Diagnostics
{
    /// <summary>
    /// Contexto de instrumentação do serviço
    /// </summary>
    public class InstrumentationContext
    {
        /// <summary>
        /// Referencia interna aos checkpoints.
        /// </summary>
        private Checkpoints checkpoints = null;

        /// <summary>
        /// Referencia interna às mensagens de depuração.
        /// </summary>
        private DebugMessages debugMessages = null;

        /// <summary>
        /// Referência interna às métricas.
        /// </summary>
        private Metrics metrics = null;

        /// <summary>
        /// Checkpoints desse contexto de instrumentação
        /// </summary>
        public Checkpoints Checkpoints
        {
            get
            {
                if (checkpoints == null)
                {
                    checkpoints = new Checkpoints();
                }

                return checkpoints;
            }
        }

        /// <summary>
        /// Mensagens de depuração desse contexto de instrumentação.
        /// </summary>
        public DebugMessages DebugMessages
        {
            get
            {
                if (debugMessages == null)
                {
                    debugMessages = new DebugMessages();
                }

                return debugMessages;
            }
        }

        /// <summary>
        /// Métricas desse contexto de instrumentação.
        /// </summary>
        public Metrics Metrics
        {
            get
            {
                if (metrics == null)
                {
                    metrics = new Metrics();
                }

                return metrics;
            }
        }

        /// <summary>
        /// Ativa/desativa a utilização das mensagens de depuração
        /// </summary>
        public bool DebugMessagesEnabled { get; set; } = true;
    }
}
