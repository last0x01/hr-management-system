namespace HR_MS.Utilities
{
    public class clsOperationResultEventArgs : EventArgs
    {
        public bool IsSeccuss { get; }
        public string Message { get; }
        public clsOperationResultEventArgs(bool IsSeccuss, string Message)
        {
            this.IsSeccuss = IsSeccuss;
            this.Message = Message;
        }
    }
}
