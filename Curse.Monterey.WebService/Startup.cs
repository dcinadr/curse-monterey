using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Curse.Monterey.WebService.Models;
using Microsoft.Owin;
using Owin;

namespace Curse.Monterey.WebService
{
    using WebSocketAccept = Action<IDictionary<string, object>,Func<IDictionary<string, object>, Task>>;
    using WebSocketCloseAsync =Func<int,string,CancellationToken,Task>;
    using WebSocketReceiveAsync =Func<ArraySegment<byte>,CancellationToken,Task<Tuple<int,bool,int>>>;
    using WebSocketSendAsync =Func<ArraySegment<byte>,int,bool,CancellationToken,Task>;
    using WebSocketReceiveResult = Tuple<int,bool,int>;

    public class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}");
            appBuilder.UseWebApi(config);
            //appBuilder.Use(UpgradeToWebSockets);
        }

        private Task UpgradeToWebSockets(IOwinContext context, Func<Task> next)
        {
            var accept = context.Get<WebSocketAccept>("websocket.Accept");
            if (accept == null)
            {
                // Not a websocket request
                return next();
            }

            accept(null, WebSocketEcho);

            return Task.FromResult<object>(null);
        }

        private async Task WebSocketEcho(IDictionary<string, object> websocketContext)
        {
            var sendFunc = (WebSocketSendAsync)websocketContext["websocket.SendAsync"];
            var receiveFunc = (WebSocketReceiveAsync)websocketContext["websocket.ReceiveAsync"];
            var closeFunc = (WebSocketCloseAsync)websocketContext["websocket.CloseAsync"];
            var cancellationToken = (CancellationToken)websocketContext["websocket.CallCancelled"];

            var handler = new WebSocketConnection(new WebSocketSender(sendFunc, cancellationToken));
            handler.OnOpen();

            byte[] buffer = new byte[WebSocketHandler.MaxMessageLength];
            WebSocketReceiveResult received = await receiveFunc(new ArraySegment<byte>(buffer), cancellationToken);

            object status;
            while (!websocketContext.TryGetValue("websocket.ClientCloseStatus", out status) || (int)status == 0)
            {
                if (received.Item1 == 1)
                {
                    var message = Encoding.UTF8.GetString(buffer, 0, received.Item3);
                    handler.OnMessage(message);
                }

                received = await receiveFunc(new ArraySegment<byte>(buffer), cancellationToken);
            }

            handler.OnClose();
            await closeFunc((int)websocketContext["websocket.ClientCloseStatus"], (string)websocketContext["websocket.ClientCloseDescription"], cancellationToken);
        }
    }
}
