using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FoodOrderApp;
using Newtonsoft.Json;

namespace FoodOrderApi
{
    public class OrderRequestParser
    {
        public async Task<Order> GetOrder(HttpListenerRequest req)
        {
            var order = await DecodeRequest(req);
            return JsonConvert.DeserializeObject<Order>(order);
        }
        private async Task<String> DecodeRequest(HttpListenerRequest req)
        {
            //req sent by client is currently encoded in UTF8/ASCII etc;
            var body = req.InputStream; //the raw bytes 
            var encoding = req.ContentEncoding; //tell which format it is encoded, such as UTF8,UTF16 etc.
            var inputStreamReader = new StreamReader(body, encoding); //creating StreamReader object that can read bytes given encoding schema;
            return await inputStreamReader.ReadToEndAsync(); //read the actual byte, wait until all bytes are sent through; 
        }
    }
}

