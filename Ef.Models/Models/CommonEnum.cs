
namespace Ef.Models.Models
{
   public  class CommonEnum
    {
        public enum UserState
        {
            Normal = 1,
            Frozen = 2,
            Deleted = 3
        }

        public enum UserType
        {
            Admin = 1,
            User = 2,
            SuperAdmin = 4
        }

        public enum CategoryState
        {
            Normal = 0,
            Frozen = 1,
            Deleted = 2
        }
    }
}
