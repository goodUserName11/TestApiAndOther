using System.ComponentModel.DataAnnotations;

namespace TestApi.Model
{
    public class ChangeRoleModel
    {
        public ChangeRoleModel()
        {

        }

        public ChangeRoleModel(string userId, string changingId)
        {
            UserId = userId;
            ChangingId = changingId;
        }

        [Required]
        public string UserId { get; set; }
        [Required]
        public string ChangingId { get; set; }
    }
}
