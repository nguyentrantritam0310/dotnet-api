namespace dotnet_api.DTOs
{
    public class AttendanceMachineDTO
    {
        public int ID { get; set; }
        public string AttendanceMachineName { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string AllowedRadius { get; set; }
    }
}
