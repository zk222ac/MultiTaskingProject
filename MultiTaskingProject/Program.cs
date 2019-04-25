using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultiTaskingProject
{
    class Program
    {
        static void Main(string[] args)
        {
            // main thread method
            MainThread();

           

            Console.ReadKey();

        }

        
        static void MainThread()
        {
            // Implementation Technique for running asynchronously process
            Task.Run(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    Console.WriteLine($"Main Thread is running : Count{i}");
                    Thread.Sleep(500);
                }
            });



            //Task.Run(() =>
            //{
            //    var resource = GetInternetResource().Result;
            //    Console.WriteLine(resource);
            //});

            Task.Run(() =>
            {
                GetTaskResource();
            });


            // Task.Run(() => { GetInternetResource(); });

            // worker Thread 1
            Thread task1 = new Thread(WorkerTask1) {Priority = ThreadPriority.Highest};
            task1.Start();


            // worker thread 2
            Thread task2 = new Thread(WorkerTask2) {Priority = ThreadPriority.Normal};
            task2.Start();

            // worker thread 3
            Thread task3 = new Thread(WorkerTask3) {Priority = ThreadPriority.AboveNormal};
            task3.Start();


        }
        static void WorkerTask1()
        {
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine($"Worker Task 1 - Count{i}" );
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Thread.Sleep(500);
            }
        }
        static void WorkerTask2()
        {
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine($"Worker Task 2 - Count{i}");
                Console.BackgroundColor = ConsoleColor.DarkMagenta;
                Thread.Sleep(500);
            }
        }
        static void WorkerTask3()
        {
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine($"Worker Task 3 - Count{i}");
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Thread.Sleep(500);
            }
        }

        static async void GetTaskResource()
        {
            var res = await GetInternetResource();
            Console.WriteLine(res);
        }

        static async Task<string> GetInternetResource()
        {
            HttpClient client = new HttpClient();

            string data = await client.GetStringAsync("https://www.gmail.com");

            return data;
        }
    }
}
