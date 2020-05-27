using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Newtonsoft.Json;
using WiredBrainCoffee.CustomersApp.Model;

namespace WiredBrainCoffee.CustomersApp.DataProvider
{
    public class CustomerDataProvider
    {
        private static readonly string _customersFileName = "customers.json";
        private static readonly StorageFolder LocalFolder = ApplicationData.Current.LocalFolder;

        public async Task<IEnumerable<Customer>> LoadCustomersAsync()
        {
            var storageFile = await LocalFolder.TryGetItemAsync(_customersFileName) as StorageFile;
            List<Customer> customerList = null;

            if (storageFile == null)
                customerList = new List<Customer>
                {
                    new Customer {FirstName = "Thomas", LastName = "Huber", IsDeveloper = true},
                    new Customer {FirstName = "Anna", LastName = "Rockstar", IsDeveloper = true},
                    new Customer {FirstName = "Julia", LastName = "Master"},
                    new Customer {FirstName = "Urs", LastName = "Meier", IsDeveloper = true},
                    new Customer {FirstName = "Sara", LastName = "Ramone"},
                    new Customer {FirstName = "Elsa", LastName = "Queen"},
                    new Customer {FirstName = "Alex", LastName = "Baier", IsDeveloper = true}
                };
            else
                using (IRandomAccessStream stream = await storageFile.OpenAsync(FileAccessMode.Read))
                {
                    using (var dataReader = new DataReader(stream))
                    {
                        await dataReader.LoadAsync((uint) stream.Size);
                        string json = dataReader.ReadString((uint) stream.Size);
                        customerList = JsonConvert.DeserializeObject<List<Customer>>(json);
                    }
                }

            return customerList;
        }

        public async Task SaveCustomersAsync(IEnumerable<Customer> customers)
        {
            StorageFile storageFile =
                await LocalFolder.CreateFileAsync(_customersFileName, CreationCollisionOption.ReplaceExisting);

            using (IRandomAccessStream stream = await storageFile.OpenAsync(FileAccessMode.ReadWrite))
            {
                using (var dataWriter = new DataWriter(stream))
                {
                    string json = JsonConvert.SerializeObject(customers, Formatting.Indented);
                    dataWriter.WriteString(json);
                    await dataWriter.StoreAsync();
                }
            }
        }
    }
}