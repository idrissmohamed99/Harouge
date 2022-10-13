namespace HarougeAPI.Models.UserManagmentModel.UserModel
{
    public class InsertUserModel : BaseUserModel
    {

        public string PasswordHash { get; set; }
        public string CreateUserId { get; set; }

    }
}
