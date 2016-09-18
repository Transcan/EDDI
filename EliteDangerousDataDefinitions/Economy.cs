﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace EliteDangerousDataDefinitions
{
    /// <summary>
    /// Economy types
    /// </summary>
    public class Economy
    {
        private static readonly List<Economy> ECONOMIES = new List<Economy>();

        public string name { get; private set; }

        public string edname { get; private set; }

        private Economy(string edname, string name)
        {
            this.edname = edname;
            this.name = name;

            ECONOMIES.Add(this);
        }

        public static readonly Economy None = new Economy("$economy_None", "None");
        public static readonly Economy Agriculture = new Economy("$economy_Agri", "Agriculture");
        public static readonly Economy Colony = new Economy("$economy_Colony", "Colony");
        public static readonly Economy Extraction = new Economy("$economy_Extraction", "Extraction");
        public static readonly Economy Refinery = new Economy("$economy_Refinery", "Refinery");
        public static readonly Economy Industrial = new Economy("$economy_Industrial", "Industrial");
        public static readonly Economy Terraforming = new Economy("$economy_Terraforming", "Terraforming");
        public static readonly Economy HighTech = new Economy("$economy_HighTech", "High Tech");
        public static readonly Economy Service = new Economy("$economy_Service", "Service");
        public static readonly Economy Tourism = new Economy("$economy_Tourism", "Tourism");
        public static readonly Economy Military = new Economy("$economy_Military", "Military");

        public static Economy FromName(string from)
        {
            foreach (Economy s in ECONOMIES)
            {
                if (from == s.name)
                {
                    return s;
                }
            }
            Logging.Report("Unknown economy name " + from);
            return null;
        }

        public static Economy FromEDName(string from)
        {
            foreach (Economy s in ECONOMIES)
            {
                if (from == s.edname)
                {
                    return s;
                }
            }
            Logging.Report("Unknown economy ED name " + from);
            return null;
        }
    }
}
