namespace dotnet_api.DTOs
{
    public class WorkShiftDTO
    {
        public int ID { get; set; }
        public string ShiftName { get; set; }
        public List<ShiftDetailDTO> ShiftDetails { get; set; }
    }

    public class WorkShiftDTOPOST
    {
        public string ShiftName { get; set; }
        public List<ShiftDetailDTO> ShiftDetails { get; set; }
    }

    public class WorkShiftDTOPUT
    {
        public int ID { get; set; }
        public string ShiftName { get; set; }
        public List<ShiftDetailDTO> ShiftDetails { get; set; }
    }
}
