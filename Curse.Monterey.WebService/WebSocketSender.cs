using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Curse.Monterey.WebService
{
    using WebSocketSendAsync = Func<ArraySegment<byte>, int, bool, CancellationToken, Task>;

    public class WebSocketSender
    {
        private readonly byte[] _buffer = new byte[WebSocketHandler.MaxMessageLength];
        private readonly WebSocketSendAsync _sendFunc;
        private readonly CancellationToken _cancellationToken;

        public WebSocketSender(WebSocketSendAsync sendFunc, CancellationToken cancellationToken)
        {
            _sendFunc = sendFunc;
            _cancellationToken = cancellationToken;
        }

        public async Task Send(string message)
        {
            var bytes = Encoding.UTF8.GetBytes(message);
            if (bytes.Length > WebSocketHandler.MaxMessageLength)
            {
                throw new InvalidOperationException("Message larger than the maximum size of " + WebSocketHandler.MaxMessageLength);
            }

            Array.Copy(bytes, 0, _buffer, 0, bytes.Length);
            await _sendFunc(new ArraySegment<byte>(_buffer, 0, bytes.Length), 1, true, _cancellationToken);
        }
    }
}
