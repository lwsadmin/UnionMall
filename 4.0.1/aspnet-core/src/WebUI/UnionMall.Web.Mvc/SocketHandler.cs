using System;
using System.Threading.Tasks;
using System.Text;
using System.Net.WebSockets;
using System.Threading;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace UnionMall.Web.Startup
{
    public class SocketHandler
    {
        public const int BufferSize = 4096;

        WebSocket socket;
        static readonly Encoding utf8 = Encoding.UTF8;

        SocketHandler(WebSocket socket)
        {
            this.socket = socket;
        }

        async Task EchoLoop(string type)
        {
            var buffer = new byte[BufferSize];
            var seg = new ArraySegment<byte>(buffer);

            while (this.socket.State == WebSocketState.Open)
            {

                //string can=
                var incoming = await this.socket.ReceiveAsync(seg, CancellationToken.None);

                // show how to use the input and change the response
                var input = utf8.GetString(buffer, 0, incoming.Count);  // convert buffer to string
                var s = $"服务器推送: {input}";            // add echo prefix
                var b = utf8.GetBytes(s);            // create outgoing bytes

                var outgoing = new ArraySegment<byte>(b);
                switch (type)
                {
                    case "goods":
                        for (int i = 0; i < 10; i++)
                        {
                            await this.socket.SendAsync(utf8.GetBytes(type), WebSocketMessageType.Text, true, CancellationToken.None);
                        }
                     
                        break;
                    case "member":
                        await this.socket.SendAsync(utf8.GetBytes(type), WebSocketMessageType.Text, true, CancellationToken.None);
                        break;
                    default:
                        break;
                }



            }
        }

        public static async Task Acceptor(HttpContext hc, Func<Task> n)
        {
            if (!hc.WebSockets.IsWebSocketRequest)
                return;
            string type = "goods";// hc.Request.Query["type"];

            // string  sc = hc.Request.Form.Keys.ToString() ;
            var socket = await hc.WebSockets.AcceptWebSocketAsync();
            var h = new SocketHandler(socket);
            await h.EchoLoop(type);
        }

        /// <summary>
        /// branches the request pipeline for this SocketHandler usage
        /// </summary>
        /// <param name="app"></param>
        public static void Map(IApplicationBuilder app)
        {
            //Microsoft.AspNetCore.Http.HttpContext.

            app.UseWebSockets();
            app.Use(SocketHandler.Acceptor);
        }
    }
}
