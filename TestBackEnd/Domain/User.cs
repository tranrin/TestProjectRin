using System.ComponentModel.DataAnnotations;

namespace TestBackEnd.Domain
{
    public class User
    {
        [Required(ErrorMessage = "User Name is required")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
    public class Register
    {
        [Required(ErrorMessage = "User Name is required")]
        public string? Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
    public static class UserRoles
    {
        public const string Admin = "Admin";
        public const string User = "User";
    }
    public static class UserPermission
    {
        public const string CanEdit = "CanEdit";
        public const string CanAdd = "CanAdd";
        public const string CanDelete = "CanDelete";

    }
    public class Response
    {
        public string? Status { get; set; }
        public string? Message { get; set; }
    }
}

