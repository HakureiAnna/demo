using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure
{
    public class Constants
    {
        static Constants()
        {
            _db_conn_str = Environment.GetEnvironmentVariable("DB_CONN_STR");

            _sb_conn_str = Environment.GetEnvironmentVariable("SB_CONN_STR");
            _sb_host = Environment.GetEnvironmentVariable("SB_HOST");

            // inventory check request (order -> inventory)
            _sb_queue_ic_req = Environment.GetEnvironmentVariable("SB_QUEUE_IC_REQ");
            // inventory check response (inventory -> order)
            _sb_queue_ic_res = Environment.GetEnvironmentVariable("SB_QUEUE_IC_RES");

        }
        // database 
        private static readonly string _db_conn_str;

        // service bus
        // required by all services
        private static readonly string _sb_conn_str;
        private static readonly string _sb_host;
        // custom queue/topic/subscription
        private static readonly string _sb_queue_ic_req;
        private static readonly string _sb_queue_ic_res;

        // database
        public static string DB_CONN_STR => _db_conn_str;

        // service bus
        // required by all services
        public static string SB_CONN_STR => _sb_conn_str;
        public static  string SB_HOST => _sb_host;
        // custom queue/topic/subscription
        public static string SB_QUEUE_IC_REQ => _sb_queue_ic_req;
        public static string SB_QUEUE_IC_RES => _sb_queue_ic_res;

    }
}
