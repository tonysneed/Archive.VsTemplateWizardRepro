using Newtonsoft.Json;
using System;

namespace SomeApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            const string greeting = "Hello VS App Template with Json.Net";
            string json = JsonConvert.SerializeObject(greeting);
            Console.WriteLine(json);
        }
    }
}
