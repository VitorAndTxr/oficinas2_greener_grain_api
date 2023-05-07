using GreenerGrain.Framework.Enumerators;

namespace GreenerGrain.Domain.Enumerators
{
    public class AccountErrors : Enumeration
    {
        public AccountErrors(int id, string code, string name) : base(id, code, name) { }

        public static AccountErrors PayloadIsNull = new AccountErrors(1, "AC001", "Payload não pode ser nulo.");
        public static AccountErrors LoginNullOrEmpty = new AccountErrors(2, "AC002", "Login nulo ou não enviado.");
        public static AccountErrors PasswordNullOrEmpty = new AccountErrors(3, "AC003", "Senha nula ou não enviada.");
        public static AccountErrors UnableToAuthorize = new AccountErrors(4, "AC004", "Não foi possível autorizar com as credenciais fornecidas.");
        public static AccountErrors UnableToDecodePassword = new AccountErrors(5, "AC005", "Não foi possível realizar o decode da senha.");
        public static AccountErrors WithoutPermissions = new AccountErrors(6, "AC006", "Usuário não possuí permissões para acessar esse sistema.");
        public static AccountErrors GraphTokenEmpty = new AccountErrors(7, "AC007", "Microsoft Token esta ausente.");
        public static AccountErrors AccountNotFound = new AccountErrors(8, "AC008", "Conta do cliente não encontrada.");
        public static AccountErrors ApplicationUnableToAuthorize = new AccountErrors(9, "AC009", "Conta de usuário não autorizada para esta aplicação.");
        public static AccountErrors AuthorizationNotImplemented = new AccountErrors(10, "AC0010", "Tipo de autenticação não implementada.");
        public static AccountErrors GoogleTokenEmpty = new AccountErrors(11, "AC011", "Google Token esta ausente.");
        public static AccountErrors GoogleUserDoestMachToken = new AccountErrors(12, "AC012", "Email que solicitou login não bate com o token enviado.");
        public static AccountErrors UsersPerApplicationNull = new AccountErrors(13, "AC013", "Não foi possível retornar o total de usuarios para esta aplicação.");
    }
}