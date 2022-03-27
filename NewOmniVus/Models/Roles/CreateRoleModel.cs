using System.ComponentModel.DataAnnotations;

namespace KristofferNowen_OmniVus.Models.Roles
{
    public class CreateRoleModel
    {
        [Microsoft.Build.Framework.Required]
        [RegularExpression(@"^([a-zA-Z]+?)$")]
        public string RoleName { get; set; }
        public string UserEmail { get; set; }
        
    }
}
