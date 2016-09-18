﻿using EliteDangerousDataDefinitions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteDangerousEvents
{
    public class DockedEvent : Event
    {
        public const string NAME = "Docked";
        public const string DESCRIPTION = "Triggered when your ship docks at a station or outpost";
        public static DockedEvent SAMPLE = new DockedEvent(DateTime.Now, "Wolf 289", "Kotov Refinery", Superpower.Federation, "Wolf 289 Gold Federal Industry", State.CivilWar, Economy.Extraction, Government.Corporate, "High");
        public static Dictionary<string, string> VARIABLES = new Dictionary<string, string>();

        static DockedEvent()
        {
            SAMPLE.raw = "{\"timestamp\":\"2016-06-10T14:32:03Z\",\"event\":\"Docked\",\"StationName\":\"Kotov Refinery\",\"StationType\":\"Outpost\",\"StarSystem\":\"Wolf 289\",\"Faction\":\"Wolf 289 Gold Federal Industry\",\"FactionState\":\"CivilWar\",\"Allegiance\":\"Federation\",\"Economy\":\"$economy_Extraction\",\"Government\":\"$government_Corporate\",\"Security\":\"$SYSTEM_SECURITY_high_anarchy;\"}";

            VARIABLES.Add("station", "The station at which the commander has docked");
            VARIABLES.Add("system", "The system at which the commander has docked");
            VARIABLES.Add("allegiance", "The allegiance of the station at which the commander has docked");
            VARIABLES.Add("faction", "The faction controlling the station at which the commander has docked");
            VARIABLES.Add("factionstate", "The state of the faction controlling the station at which the commander has docked");
            VARIABLES.Add("economy", "The economy of the station at which the commander has docked");
            VARIABLES.Add("government", "The government of the station at which the commander has docked");
            VARIABLES.Add("security", "The security of the station at which the commander has docked");
        }

        [JsonProperty("system")]
        public string system { get; private set; }

        [JsonProperty("station")]
        public string station { get; private set; }

        [JsonProperty("allegiance")]
        public Superpower allegiance { get; private set; }

        [JsonProperty("faction")]
        public string faction { get; private set; }

        [JsonProperty("factionstate")]
        public State factionstate { get; private set; }

        [JsonProperty("economy")]
        public Economy economy { get; private set; }

        [JsonProperty("government")]
        public Government government { get; private set; }

        [JsonProperty("security")]
        public string security { get; private set; }

        public DockedEvent(DateTime timestamp, string system, string station, Superpower allegiance, string faction, State factionstate, Economy economy, Government government, string security) : base(timestamp, NAME)
        {
            this.system = system;
            this.station = station;
            this.allegiance = allegiance;
            this.faction = faction;
            this.factionstate = factionstate;
            this.economy = economy;
            this.government = government;
            this.security = security;
        }
    }
}
