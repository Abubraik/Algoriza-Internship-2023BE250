namespace Vezeeta.Core.Enums
{
    public static class Enums
    {
        public enum Gender : short
        {
            Male = 0,
            Female = 1
        }

        public enum Days : short
        {
            Saturday = 0,
            Sunday = 1,
            Monday = 2,
            Tuesday = 3,
            Wednesday = 4,
            Thursday = 5,
            Friday = 6
        }

        public enum DiscountType : short
        {
            Percentage = 0,
            Value = 1
        }

        public enum Status : short
        {
            Canceled = -1,
            Pending = 0,
            Completed = 1
        }
    }
}
