﻿namespace Ecaq.Models
{
    public class ReCaptchaResponse
    {
        public bool success { get; set; }
        public DateTime challenge_ts { get; set; }
        public string hostname { get; set; }
        public double score { get; set; }
        public string action { get; set; }
        public List<string> errorcodes { get; set; }
    }
}
