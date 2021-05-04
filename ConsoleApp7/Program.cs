using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp7
{
    class Program
    {
        public static DataClasses1DataContext context = new DataClasses1DataContext();
        static void Main(string[] args)
        {
            // DataSource();
            Joining();
            Console.Read();
        }

        //IntroToLinq
        static void IntroToLINQ()
        {
            int[] numeros = new int[7] { 0, 1, 2, 3, 4, 5, 6 };


            var numQuery =
                from num in numeros
                where (num % 2) == 0
                select num;

            foreach (int num in numQuery)
            {
                Console.Write("{0, 1}", num);
            }
        }

        //IntroToLinq Lambda
        static void IntroToLINQLambda()
        {
            int[] numeros = new int[7] { 0, 1, 2, 3, 4, 5, 6 };

            var numQuery = numeros.Where(c => c % 2 == 0);

            foreach (int num in numQuery)
            {
                Console.Write("{0, 1}", num);
            }
        }

        //DataSource
        static void DataSource()
        {
            var queryAllCustomers = from cust in context.clientes
                                    select cust;

            foreach (var item in queryAllCustomers)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }

        // DataSourceLambda
        static void DataSourceLambda()
        {
            var Allcustomers = context.clientes.ToList();
            foreach (var i in Allcustomers)
            {
                Console.WriteLine(i.NombreCompañia);
            }
        }

        //Filtering
        static void Filtering()
        {
            var queryLondonCustomers = from cust in context.clientes
                                       where cust.Ciudad == "Londres"
                                       select cust;
            foreach (var item in queryLondonCustomers)
            {
                Console.WriteLine(item.NombreContacto);
            }
        }

        //Filtering Lambda
        static void FilteringLambda()
        {
            var queryLondonCustomers = context.clientes.Where(c => c.Ciudad == "Londres");
            foreach (var item in queryLondonCustomers)
            {
                Console.WriteLine(item.NombreContacto);
            }
        }

        //Ordering
        static void Ordering()
        {
            var queryLondonCustomers3 = from cust in context.clientes
                                        where cust.Ciudad == "Londres"
                                        orderby cust.NombreCompañia ascending
                                        select cust;
            foreach (var item in queryLondonCustomers3)
            {

                Console.WriteLine(item.NombreCompañia);
            }
        }

        //Ordering Lambda
        static void OrderingLambda()
        {
            var queryLondonCustomers3 = context.clientes.Where(c => c.Ciudad == "Londres").OrderBy(c => c.NombreCompañia).ToList();
            foreach (var item in queryLondonCustomers3)
            {

                Console.WriteLine(item.NombreCompañia);
            }
        }

        //Grouping
        static void Grouping()
        {
            var queryCustomersByCity = from cust in context.clientes
                                       group cust by cust.Ciudad;

            foreach (var customerGroup in queryCustomersByCity)
            {
                Console.WriteLine(customerGroup.Key);

                foreach (clientes customer in customerGroup)
                {
                    Console.WriteLine(" {0}", customer.NombreCompañia);
                }
            }
        }
         
        //Grouping Lambda
        static void GroupingLambda()
        {
            var queryCustomersByCity = context.clientes.GroupBy(c => c.Ciudad);

            foreach (var customerGroup in queryCustomersByCity)
            {
                Console.WriteLine(customerGroup.Key);

                foreach (clientes customer in customerGroup)
                {
                    Console.WriteLine(" {0}", customer.NombreCompañia);
                }
            }
        }

        //Grouping 2
        static void Grouping2()
        {
            var custQuery = from cust in context.clientes
                            group cust by cust.Ciudad into custGroup
                            where custGroup.Count() > 2
                            orderby custGroup.Key
                            select custGroup;

            foreach (var item in custQuery)
            {
                Console.WriteLine(item.Key);
            }
        }

        //Grouping 2 Lambda
        static void Grouping2Lambda()
        {
            var custQuery = context.clientes
            .GroupBy(c => c.Ciudad)
            .Where(c => c.Key.Count() > 2)
            .OrderBy(c => c.Key);

            foreach (var item in custQuery)
            {
                Console.WriteLine(item.Key);
            }
        }

        //Joining
        static void Joining()
        {
            var innerJoinQuery = from cust in context.clientes
                                 join dist in context.Pedidos on cust.idCliente equals dist.IdCliente
                                 select new { CustomerName = cust.NombreCompañia, DistributorName = dist.PaisDestinatario };

            foreach (var item in innerJoinQuery)
            {
                Console.WriteLine(item.CustomerName);
            }
        }

        //Joining Lambda
        static void JoiningLambda()
        {
            var innerJoinQuery = context.clientes
                                    .Join(context.Pedidos, 
                                        c => c.idCliente,
                                        b => b.IdCliente,
                                        (a, b) => new { a.NombreCompañia, b.PaisDestinatario }
                                    );

            foreach (var item in innerJoinQuery)
            {
                Console.WriteLine($"{ item.NombreCompañia} y destinatario : {item.PaisDestinatario}");
            }
        }
    }
}
