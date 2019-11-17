using System;
using System.Net;
using System.Threading.Tasks;
using FoodOrderApi.CustomisedApiExceptions;
using FoodOrderApp;
using FoodOrderApp.CustomisedDomainExceptions;

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
                string responseBody = null;
                var statusCode = HttpStatusCode.OK;
                Console.WriteLine($"{req.HttpMethod} request made to {req.Url}\n");

                try
                {
                    if (req.HttpMethod == "GET" && req.Url.AbsolutePath == "/orders")
                    {
                        ConsoleRequestMessage(req.HttpMethod);
    
                        responseBody = responseSender.GetResponseBody(orderController.GetAllOrders());
                    }
                    else if (req.HttpMethod == "GET" && req.Url.AbsolutePath.StartsWith("/orders/"))
                    {
                        ConsoleRequestMessage(req.HttpMethod);

                        var orderId = int.Parse(req.Url.Segments[2]);
                        
                        responseBody = responseSender.GetResponseBody(orderController.GetOrderById(orderId));
                    }
                    else if (req.HttpMethod == "POST" && req.Url.AbsolutePath == "/orders")
                    {
                        var order = await requestParser.GetOrder(req);

                        ConsoleRequestMessage(req.HttpMethod);

                        responseBody = responseSender.GetResponseBody(orderController.CreateOrder(order));

                        statusCode = HttpStatusCode.Created;
                    }
                    else if (req.HttpMethod == "PUT" && req.Url.AbsolutePath.StartsWith($"/orders/"))
                    {
                        var order = await requestParser.GetOrder(req);

                        ConsoleRequestMessage(req.HttpMethod);
                        
                        OrderRequestValidator.IsOrderIdExistOnRequestBody(order);
                        
                        if (order.Id != null && OrderRequestValidator.IsOrderIdValid((int)order.Id, req))
                        {
                            responseBody = responseSender.GetResponseBody(orderController.ReplaceOrder(order));
                        }
                    }
                    else if (req.HttpMethod == "PATCH" && req.Url.AbsolutePath.StartsWith($"/orders/"))
                    {
                        var order = await requestParser.GetOrder(req);

                        ConsoleRequestMessage(req.HttpMethod);
                        
                        OrderRequestValidator.IsOrderIdExistOnRequestBody(order);
                        
                        if (order.Id != null && OrderRequestValidator.IsOrderIdValid((int)order.Id, req))
                        {
                            responseBody = responseSender.GetResponseBody(orderController.UpdateOrder(order));
                        }
                    }
                    else if (req.HttpMethod == "DELETE" && req.Url.AbsolutePath.StartsWith($"/orders/"))
                    {

                        ConsoleRequestMessage(req.HttpMethod);
                        
                        var orderId = int.Parse(req.Url.Segments[2]);
                        
                        if (OrderRequestValidator.IsOrderIdValid(orderId, req))
                        {
                            responseBody = responseSender.GetResponseBody(orderController.DeleteOrderById(orderId));
                        }
                    }
                    else
                    {
                        throw new RouteNotFoundException("Error: Not Found");
                    }
                    
                    responseSender.SendSuccessResponse(res, statusCode, responseBody);

                }
                catch (RouteNotFoundException e)
                {
                    responseSender.SendFailResponseWithMessage(res, HttpStatusCode.NotFound, e.Message);
                }
                catch (InvalidOrderRequestException e)
                {
                    responseSender.SendFailResponseWithMessage(res, HttpStatusCode.BadRequest, e.Message);
                }
                catch (OrderDoesNotFoundException e)
                {
                    responseSender.SendFailResponseWithMessage(res, HttpStatusCode.NotFound, e.Message);
                } 
                catch (InvalidOrderException e)
                {
                    responseSender.SendFailResponseWithMessage(res, HttpStatusCode.BadRequest, e.Message);
                }
                catch (Exception e)
                {
                    responseSender.SendFailResponseWithMessage(res, HttpStatusCode.BadRequest, e.Message);
                }
            }
        }

        private static void ConsoleRequestMessage(string requestMethod)
        {
            Console.WriteLine($"\n{requestMethod} Request Received.\n");
        }
    }
}