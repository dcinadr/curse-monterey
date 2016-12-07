using System.Threading.Tasks;

namespace Curse.Monterey.WebService
{
    public abstract class WebSocketHandler
    {
        public const int MaxMessageLength = 1024;
        private readonly WebSocketSender _sender;

        protected WebSocketHandler(WebSocketSender sender)
        {
            _sender = sender;
        }

        public abstract void OnOpen();

        public abstract void OnClose();

        public abstract void OnMessage(string message);

        public virtual async Task SendMessage(string message)
        {
            await _sender.Send(message);
        }
    }
}
