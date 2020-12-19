using MilestoneCST247.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RestService
{
    [DataContract]
    public class StatsDTO
    {
        public StatsDTO (int ErrorCode, string ErrorMsg, List<PublishedGame> Data)
        {
            this.ErrorCode = ErrorCode;
            this.ErrorMsg = ErrorMsg;
            this.Data = Data;
        }

        [DataMember]
        public int ErrorCode { get; set; }
        [DataMember]
        public string ErrorMsg { get; set; }
        [DataMember]
        public List<PublishedGame> Data { get; set; }
    }
}