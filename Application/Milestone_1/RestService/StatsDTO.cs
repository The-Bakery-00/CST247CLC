using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RestService
{
    public class StatsDTO
    {
        public StatsDTO (int ErrorCode, string ErrorMsg, List<GameStats> Data)
        {
            this.ErrorCode = ErrorCode;
            this.ErrorMsg = ErrorMsg;
            this.Data = Data;
        }

        [DataMember]
        public int ErrorCode { get; set; }
        public string ErrorMsg { get; set; }
        public List<GameStats> Data { get; set; }
    }
}