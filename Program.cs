using Microsoft.Extensions.Configuration;
using System.Net.Sockets;

namespace CustomerDemo
{
    internal class Program
    {
        private static IConfiguration _iconfiguration;
        static void Main(string[] args)
        {
            try
            {
                GetAppSettingFile();
                EmpDisplay();
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static void GetAppSettingFile()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("Appsettings.json", optional: false,reloadOnChange:true);
            _iconfiguration = builder.Build();


        }
        static void EmpDisplay()
        {
            
            Strongly_type indata = new Strongly_type(_iconfiguration);

            List<Customer> ls = indata.GetList();
            foreach (var x in ls)
                Console.WriteLine("{0}  {1}  {2}  {3}", x.ID, x.Name, x.Address,x.Mobile_No);
            
            //Console.ReadLine();

            Console.WriteLine("--------------------------------------------------------------");
            Customer cos1 = new Customer { Name = "Raj", Address = "mnc", Mobile_No = "5001" };
            int ans=indata.AddData(cos1);
            Console.WriteLine(ans);
            Console.WriteLine("---------------------------------------------------------------");
           
            foreach (var x in ls)
                Console.WriteLine("{0}  {1}  {2}  {3}", x.ID, x.Name, x.Address, x.Mobile_No);


        }

    }
}