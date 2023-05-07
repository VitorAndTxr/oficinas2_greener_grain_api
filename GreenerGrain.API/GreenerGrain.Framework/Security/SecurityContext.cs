using System.Collections.Generic;
using System.Security.Claims;

namespace GreenerGrain.Framework.Security
{
    /// <summary>
    /// Contexto de segurança
    /// </summary>
    public class SecurityContext
    {
        /// <summary>
        /// Token Jwt enviado na requisição
        /// </summary>
        public string JwtToken { get; set; }

        /// <summary>
        /// Claims do usuário logado
        /// </summary>
        public IEnumerable<Claim> Claims { get; set; }

        /// <summary>
        /// Conta do Usuario logado com profiles e roles
        /// </summary>
        public Account Account { get; set; }
    }
}
