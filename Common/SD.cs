namespace Common
{
    public static class SD
    {
        public const double PricePerMin = 0.567;

        public static string[] EvenDays = { "Tuesday", "Thursday", "Saturday" };
        public static string[] OddDays = { "Monday", "Wednesday", "Friday", "Sunday" };
        public static string[] Weekdays = { "Monday", "Tuesday", "Wendesday", "Thursday", "Friday" };

        public const string Booking = "Booking";

        public const string Status_Pending = "Pending";
        public const string Status_Paid = "Paid";
        public const string Status_Refunded = "Refunded";

        public const string Role_Admin = "Admin";
        public const string Role_Manager = "Manager";
        public const string Role_Customer = "Customer";

        public const string Local_Token = "Jwt Token";
        public const string Local_UserDetails = "User Details";
        public const string Local_BookingDetails = "LocalDetails";

        private static char[] letters = {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l'};

        public static string GetCode()
        {
            string code = "";
            Random random = new Random();

            for (int i = 0; i < 10; i++)
            {
                if (i % 2 == 0)
                {
                    code += random.Next(1, 9);
                }
                else
                {
                    code += letters[random.Next(0, 11)];
                }
            }

            return code;
        }
    }
}