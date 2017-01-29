using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace Curse.Monterey.WebService.DbAccess
{
    public class RedisClient
    {
        private static ConnectionMultiplexer _redis;

        public static ConnectionMultiplexer Redis
        {
            get
            {
                if (_redis == null)
                {
                    _redis = ConnectionMultiplexer.Connect();
                }
            }
        }
    }
}
