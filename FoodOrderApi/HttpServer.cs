using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FoodOrderApp;

namespace FoodOrderApi
{
    public static class HttpServer
    {
        private static HttpListener _listener;
        private static string _url = "http://localhost:8080/";

        public static void RunServer()
        {
            _listener = new HttpListener();
            _listener.Prefixes.Add(_url);
            _listener.Start();

            Console.WriteLine($"listening on {_url}\n");

            var listenTask = HandledIncomingConnection();
            listenTask.GetAwaiter().GetResult();

            _listener.Close();
        }

        private static async Task HandledIncomingConnection()
        {
            const bool serverIsRunning = true;

            while (serverIsRunning)
            {
                var context = await _listener.GetContextAsync();
                var req = context.Request;
                var res = context.Response;
                var requestParser = new OrderRequestParser();
                var responseSender = new OrderResponseSender();
                var orderController = new OrderController(new OrdersDataInMemory());

                Console.WriteLine($"{req.HttpMethod} request made to {req.Url}\n");

                try
                {
                    if (req.HttpMethod == "GET" && req.Url.AbsolutePath == "/orders")
                    {
                        ConsoleRequestMessage(req.HttpMethod);

                        var orderList = orderController.GetAllOrders();

                        responseSender.SendSuccessResponse(res, 200, orderList);
                    }
                    else if (req.HttpMethod == "GET" && req.Url.AbsolutePath.StartsWith("/orders/"))
                    {
                        ConsoleRequestMessage(req.HttpMethod);

                        var orderId = int.Parse(req.Url.Segments[2]);
                        
                        var responseBody = orderController.GetOrderById(orderId);
                        responseSender.SendSuccessResponse(res, 200, responseBody);
                    }
                    else if (req.HttpMethod == "POST" && req.Url.AbsolutePath == "/orders")
                    {
                        var order = await requestParser.GetOrder(req);

                        ConsoleRequestMessage(req.HttpMethod);

                        var newOrder = orderController.CreateOrder(order);

                        responseSender.SendSuccessResponse(res, 201, newOrder);
                    }
                    else if (req.HttpMethod == "PUT" && req.Url.AbsolutePath.StartsWith($"/orders/"))
                    {
                        var order = await requestParser.GetOrder(req);

                        ConsoleRequestMessage(req.HttpMethod);

                        if (OrderRequestValidator.IsOrderIdValid(order, req))
                        {
                            var responseBody = orderController.UpdateOrder(order);
                            responseSender.SendSuccessResponse(res, 200, responseBody);
                        }
                    }
                    else if (req.HttpMethod == "DELETE" && req.Url.AbsolutePath.StartsWith($"/orders/"))
                    {
                        var order = await requestParser.GetOrder(req);

                        ConsoleRequestMessage(req.HttpMethod);

                        if (OrderRequestValidator.IsOrderIdValid(order, req))
                        {
                            var responseBody = orderController.DeleteOrderById(order.Id);
                            responseSender.SendSuccessResponse(res, 200, responseBody);
                        }
                    }
                    else
                    {
                        var buffer = Encoding.UTF8.GetBytes("all other routes didn't work.");
                        res.ContentLength64 = buffer.Length;

                        await res.OutputStream.WriteAsync(buffer, 0, buffer.Length);
                        res.Close();
                    }
                }
                catch (PermissionException e)
                {
                    responseSender.SendFailResponseWithMessage(res, e.Message, 400);
                }
                catch (InvalidOrderException e)
                {
                    responseSender.SendFailResponseWithMessage(res, e.Message, 400);
                }
                catch (Exception e)
                {
                    responseSender.SendFailResponseWithMessage(res, e.Message, 400);
                }
            }
        }

        private static void ConsoleRequestMessage(string requestMethod)
        {
            Console.WriteLine($"\n{requestMethod} Request Received.\n");
        }
    }
}