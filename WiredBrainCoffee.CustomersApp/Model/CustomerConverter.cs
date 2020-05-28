namespace WiredBrainCoffee.CustomersApp.Model
{
    public class CustomerConverter
    {
        public static Customer CreateCustomerFromString(string inputString)
        {
            string[] values = inputString.Split(',');
            return new Customer
            {
                FirstName = values[0],
                LastName = values[1],
                IsDeveloper = bool.Parse(values[2])
            };
        }
    }
}