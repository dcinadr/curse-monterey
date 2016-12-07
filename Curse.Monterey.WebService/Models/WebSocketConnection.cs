using System;

namespace Curse.Monterey.WebService.Models
{
    public class WebSocketConnection : WebSocketHandler
    {
        public WebSocketConnection(WebSocketSender sender) : base(sender)
        {
        }

        public override void OnOpen()
        {
            throw new NotImplementedException();
        }

        public override void OnClose()
        {
            throw new NotImplementedException();
        }

        public override void OnMessage(string message)
        {
            throw new NotImplementedException();
        }
    }
}
