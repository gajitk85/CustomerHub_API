
namespace CustomerHub.DataAccess.Utilites
{
    public class UtilitesMethos
    {
        public static int GetAge(DateTime dob)
        {
            int age = 0;
            age = DateTime.Now.Subtract(dob).Days;
            age = age / 365;
            return age;
        }
    }
}
