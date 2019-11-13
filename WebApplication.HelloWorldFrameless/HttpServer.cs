using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebApplication.HelloWorldFrameless
{
    public class HttpServer
    {
        public static HttpListener Listener;
        public static string Url = "http://localhost:8080/";
        
        public static async Task HandledIncomingConnection()
        {
            var serverIsRunning = true;

            while (serverIsRunning)
            {
                var context = await Listener.GetContextAsync();
                var req = context.Request;
                var res = context.Response;

                Console.WriteLine($"{req.HttpMethod} request made to {req.Url}");
                
                if ((req.HttpMethod == "POST") && (req.Url.AbsolutePath == "/shutdown"))
                {
                    Console.WriteLine("Shutdown requested");
                    serverIsRunning = false;
                }
                
                var buffer = Encoding.UTF8.GetBytes("hello, leah");
                res.ContentType = "text/html";
                res.ContentEncoding = Encoding.UTF8;
                res.ContentLength64 = buffer.LongLength;
                
                await res.OutputStream.WriteAsync(buffer, 0, buffer.Length);
                res.Close();
                

//                if (req.HttpMethod == "POST" && req.Url.AbsolutePath== "/new")
//                {
//                    var inputStreamReader = new StreamReader(req.InputStream, req.ContentEncoding);
//                    var body = inputStreamReader.ReadToEnd();
//
//                    Console.WriteLine($"This is the request body that came through:");
//                    Console.WriteLine(body);
//
//                    var dataAsNameClass = JsonConvert.DeserializeObject<User>(body);
//
//                    Console.WriteLine("This worked:");
//                    Console.WriteLine($"Firstname is: {dataAsNameClass.FirstName}");
//                    Console.WriteLine($"LastName is: {dataAsNameClass.LastName}");
//
//                    var backToString = JsonConvert.SerializeObject(dataAsNameClass);
//                    
//                    var buffer = System.Text.Encoding.UTF8.GetBytes(backToString);
//                    res.ContentLength64 = buffer.Length;
//                    
//                    await res.OutputStream.WriteAsync(buffer, 0, buffer.Length);
//                    res.Close();
//                }
                
//                if(req.HttpMethod == "POST")
//                {
//                    var body = req.InputStream;
//                    var encoding = req.ContentEncoding;
//                    var reader = new StreamReader(body, encoding);
//                    var requestBody = reader.ReadToEnd();
//
//                    Console.WriteLine($"This is the request body that came through:");
//                    Console.WriteLine(requestBody);
//
//                    var dataAsNameClass = JsonConvert.DeserializeObject<User>(requestBody);
//
//                    Console.WriteLine("This worked:");
//                    Console.WriteLine($"Firstname is: {dataAsNameClass.FirstName}");
//                    Console.WriteLine($"LastName is: {dataAsNameClass.LastName}");
//
//                    var backToString = JsonConvert.SerializeObject(dataAsNameClass);
//                    
//                    var buffer = System.Text.Encoding.UTF8.GetBytes(backToString);
//                    res.ContentLength64 = buffer.Length;
//                    res.StatusCode = (int)HttpStatusCode.NotFound;
//
//                    await res.OutputStream.WriteAsync(buffer, 0, buffer.Length);
//                    res.Close();
//                }
////                else if((req.HttpMethod == "POST"))
////                {
////                    Console.WriteLine($"Here are the query params: {req.QueryString.GetValues(0)[0]} ");
////                }
////                else if(req.HttpMethod == "GET" && routeResource1 != string.Empty)
////                {
////                    var message = $"hey {routeResource1}";
////                    var buffer = System.Text.Encoding.UTF8.GetBytes(message);
////                    res.ContentLength64 = buffer.Length;
////
////                    await res.OutputStream.WriteAsync(buffer, 0, buffer.Length);
////                    res.Close();
////                }

            }
        }
        static void Main(string[] args)
        {
            Listener = new HttpListener();
            Listener.Prefixes.Add(Url);
            Listener.Start();
            
            Console.WriteLine($"listening on {Url}");
            
            
            var listenTask = HandledIncomingConnection();
            listenTask.GetAwaiter().GetResult();
            
            Listener.Close();
        }
    }
}