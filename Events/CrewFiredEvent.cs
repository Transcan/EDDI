﻿using System;
using Utilities;

namespace EddiEvents
{
    [PublicAPI]
    public class CrewFiredEvent : Event
    {
        public const string NAME = "Crew fired";
        public const string DESCRIPTION = "Triggered when you fire crew";
        public const string SAMPLE = "{\"timestamp\":\"2016-08-09T08: 46:29Z\",\"event\":\"CrewFire\",\"Name\":\"Margaret Parrish\",\"CrewID\":236064708}";

        [PublicAPI("The name of the crewmember being fired")]
        public string name { get; private set; }

        [PublicAPI("The ID of the crewmember being assigned")]
        public long crewid { get; private set; }

        public CrewFiredEvent(DateTime timestamp, string name, long crewid) : base(timestamp, NAME)
        {
            this.name = name;
            this.crewid = crewid;
        }
    }
}
