namespace Business_Layer.Validations
{
    public class AttendanceValidationResult
    {
        public string? Message { get; set; }
        public bool CanAdd { get; set; }

        public AttendanceValidationResult()
        {

        }

        public AttendanceValidationResult(bool CanAdd, string? Message)
        {
            this.CanAdd = CanAdd;
            this.Message = Message;
        }
    }
}
