namespace Business_Layer.Validations
{
    public class AbsenceValidationResult
    {
        public string? Message { get; set; }
        public bool CanAdd { get; set; }



        public AbsenceValidationResult(bool CanAdd, string? Message = null)
        {
            this.CanAdd = CanAdd;
            this.Message = Message;
        }
    }
}
