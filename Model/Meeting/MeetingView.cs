using ES_HomeCare_API.Model.Common;
using System;
using System.Collections.Generic;

namespace ES_HomeCare_API.Model.Meeting
{
    public class MeetingView
    {
        public long MeetingId { get; set; }
        public string MeetingDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public UserView Employee { get; set; }
        public UserView Client { get; set; }
        public List<string> Notes { get; set; }
        public short IsStatus { get; set; }
    }
}