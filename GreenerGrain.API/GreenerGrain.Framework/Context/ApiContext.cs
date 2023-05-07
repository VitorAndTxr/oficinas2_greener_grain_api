using API.Framework.Interfaces;
using GreenerGrain.Framework.Diagnostics;
using GreenerGrain.Framework.Field;
using GreenerGrain.Framework.Filtering;
using GreenerGrain.Framework.Paging;
using GreenerGrain.Framework.Result;
using GreenerGrain.Framework.Security;
using GreenerGrain.Framework.Sorting;
using System.Collections.Generic;

namespace GreenerGrain.Framework.Context
{
    /// <summary>
    /// Contexto de trabalho do serviço
    /// </summary>
    public class ApiContext : IApiContext
    {
        /// <summary>
        /// Contexto de instrumentação (privado)
        /// </summary>
        private InstrumentationContext instrumentationContext = null;

        /// <summary>
        /// Contexto de paginação (privado)
        /// </summary>
        private PagingContext pagingContext = null;

        /// <summary>
        /// Contexto de sort (privado)
        /// </summary>
        private SortingContext sortingContext = null;

        /// <summary>
        /// Contexto de filter
        /// </summary>
        private FilteringContext filteringContext = null;

        /// <summary>
        /// Context for field selection
        /// </summary>
        private FieldContext fieldingContext = null;

        /// <summary>
        /// Contexto de segurança (privado)
        /// </summary>
        private SecurityContext securityContext = null;

        /// <summary>
        /// HTTP status code específico a ser retornado ao fim da requisição. Opcional. 
        /// </summary>
        public int? HttpStatusCode { get; set; } = null;

        /// <summary>
        /// Lista de erros a ser retornado ao fim da requisição. Opcional.
        /// </summary>
        public List<Error> Errors { get; } = new List<Error>();

        /// <summary>
        /// Identificador único de transação. Opcional.
        /// </summary>
        public string TransactionId { get; set; } = null;

        /// <summary>
        /// Ativa a telemetria do insights
        /// </summary>
        public bool EnableTelemetry { get; set; }

        /// <summary>
        /// Contexto de instrumentação.
        /// </summary>
        public InstrumentationContext InstrumentationContext
        {
            get
            {
                if (instrumentationContext == null)
                {
                    instrumentationContext = new InstrumentationContext();
                }
                return instrumentationContext;
            }
        }

        /// <summary>
        /// Contexto de paginação.
        /// </summary>
        public PagingContext PagingContext
        {
            get
            {
                if (pagingContext == null)
                {
                    pagingContext = new PagingContext();
                }
                return pagingContext;
            }
        }

        /// <summary>
        /// Contexto de ordenação.
        /// </summary>
        public SortingContext SortingContext
        {
            get
            {
                if (sortingContext == null)
                {
                    sortingContext = new SortingContext();
                }
                return sortingContext;
            }
        }

        /// <summary>
        /// Contexto de filtragem
        /// </summary>
        public FilteringContext FilteringContext
        {
            get
            {
                if (filteringContext == null)
                {
                    filteringContext = new FilteringContext();
                }
                return filteringContext;
            }
        }

        /// <summary>
        /// Context for field selection
        /// </summary>
        public FieldContext FieldContext
        {
            get
            {
                if (fieldingContext == null)
                {
                    fieldingContext = new FieldContext();
                }
                return fieldingContext;
            }
        }

        /// <summary>
        /// Contexto de segurança.
        /// </summary>
        public SecurityContext SecurityContext
        {
            get
            {
                if (securityContext == null)
                {
                    securityContext = new SecurityContext();
                }
                return securityContext;
            }
        }

    }
}
