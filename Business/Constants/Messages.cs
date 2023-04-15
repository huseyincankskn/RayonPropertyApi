namespace Business.Constants
{
    public static class Messages
    {
        public const string UserNotFound = "The user associated with this email could not be found.";
        public const string PasswordError = "The password is incorrect.";
        public const string SuccessfulLogin = "Logged in to the system.";
        public const string AccessTokenCreated = "Access token created successfully.";
        public const string UserRegistered = "User saved.";
        public const string UserExist = "A user associated with this email already exists.";
        public const string EmptyError = "Please check the empty fields.";
        public const string PasswordFormatError = "Your password must be at least 8 characters long and contain at least one lowercase letter, one uppercase letter, and one digit.";

        public const string EntityAdded = "The registration has been successfully added.";
        public const string EntityUpdated = "The registration has been successfully updated.";
        public const string EntityDeleted = "The registration has been successfully deleted.";
        public const string EntityAlreadyExist = "This record already exists.";
        public const string EntityNotFound = "The record could not be found.";
        public const string SameEntity = "You cannot add a record that already exists.";

        public const string BlogCategoryNotFound = "The Blog Category could not be found";
        public const string RoleNotFound = "Role not found.";

    }
}