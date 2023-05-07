using GreenerGrain.Framework.Enumerators;

namespace GreenerGrain.Domain.Enumerators
{
    public class AppointmentError : Enumeration
    {
        public AppointmentError(int id, string code, string name) : base(id, code, name) { }

        public static AppointmentError NotFoundInstitutionId = new(1, "APP001", "A instituicao não pode ser nulo.");

        public static AppointmentError NotFoundCalendarName = new(2, "APP002", "O Nome do calendario nao pode ser nulo.");

        public static AppointmentError InvalidPayload = new(3, "APP003", "Verifique os campos informados.");

        public static AppointmentError PageMaximumExceeded = new(4, "APP004", "Página solicitada é maior que o total de páginas geradas.");
        
        public static AppointmentError ServiceDeskNotExist = new(5, "APP005", "O Balcão de Serviço escolhido não existe.");
        
        public static AppointmentError ServiceDeskTypeDoesNotExist = new(6, "APP006", "O Tipo de Balcão de Serviço escolhido não existe.");
        
        public static AppointmentError ProtocolNumberDoesNotExist = new(7, "APP007", "O número de protocolo é necessário");

        public static AppointmentError InsertError = new(8, "APP008", "Erro ao tentar inserir novo appointment");

        public static AppointmentError NotFoundEntity = new(9, "APP009", "O id não representa um registro salvo em banco");

        public static AppointmentError WrongServiceDesk = new(10, "APP010", "O Atendente não esta ligado a essa mesa de serviço");
    
        public static AppointmentError WrongOfficer = new(11, "APP011", "O Atendente não esta ligado a esse attendimento");

        public static AppointmentError AppointmentDoesNotExist = new(12, "APP012", "O Atendimento não existe.");

        public static AppointmentError AppointmentCuncurrentScheduledDate = new(13, "APP013", "O horário da Mesa de Serviço já está ocupado.");


    }

}