namespace Domain.Models
{
    public class ErrorsMessages
    {
        public const string INVALID_PAGE = "Chosen page does not exist or its number is invalid";
        public const string RECORD_NOT_FOUND = "Record with this id not found";
        public const string UPDATE_ERROR = "Error while updating the record";
        public const string DELETION_ERROR = "Error while deleting the record";
        public const string EMPTY_ARGUMENT = "Request argument is empty";
    }
}
