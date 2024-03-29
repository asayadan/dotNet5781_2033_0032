﻿using System;

namespace BO
{
    public class Trip
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int LineId { get; set; }
        public int InStation { get; set; }
        public TimeSpan InAt { get; set; }
        public int OutStation { get; set; }
        public TimeSpan OutAt { get; set; }
    }
}
