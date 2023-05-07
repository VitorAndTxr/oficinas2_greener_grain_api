using System;

namespace GreenerGrain.Framework.Result
{
    /// <summary>
    /// Informações de paginação de um resultado
    /// </summary>
    public class Pagination
    {
        /// <summary>
        /// Indice do registro atual
        /// </summary>
        public int CurrentRecord { get; set; }

        /// <summary>
        /// Indice da pagina atual
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Quantidade de registros por pagina que foram solicitados
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Quantidade de registros na pagina atual
        /// </summary>
        public int RecordsOnPage { get; set; }

        /// <summary>
        /// Total de registros existentes para serem retornados
        /// </summary>
        public int TotalRecords { get; set; }

        /// <summary>
        /// Total de páginas existentes para serem retornadas
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Pagination()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="currentRecord">Current record index of this result</param>
        /// <param name="totalRecords">Total records available to be returned</param>
        /// <param name="pageSize">Page size for this result</param>
        public Pagination(int currentRecord, int totalRecords, int pageSize)
        {
            if (currentRecord < 0)
            {
                throw new ArgumentException("Value should not be lesser than zero.", "currentRecord");
            }
            if (totalRecords < 0)
            {
                throw new ArgumentException("Value should not be lesser than zero.", "totalRecords");
            }
            if (pageSize <= 0)
            {
                this.CurrentRecord = currentRecord;
                this.TotalRecords = totalRecords;
                this.PageSize = totalRecords;

                this.TotalPages = 0;
                this.CurrentPage = 0;

                this.RecordsOnPage = totalRecords;

                return;
            }

            this.CurrentRecord = currentRecord;
            this.TotalRecords = totalRecords;
            this.PageSize = pageSize;

            this.TotalPages = (int)Math.Ceiling((double)totalRecords / (double)pageSize);
            this.CurrentPage = (int)Math.Ceiling((double)currentRecord / (double)pageSize);

            if (this.CurrentPage == 0)
            {
                this.RecordsOnPage = this.PageSize;
            }
            else if ((int)Math.Max(0, (totalRecords - (this.CurrentPage * pageSize))) > this.PageSize)
            {
                this.RecordsOnPage = this.PageSize;
            }
            else
            {
                this.RecordsOnPage = (int)Math.Max(0, (totalRecords - (this.CurrentPage * pageSize)));
            }


            this.CurrentPage++;
        }
    }
}
