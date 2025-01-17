﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace EddiSpeechService
{
    /// <summary>Translations for Elite items for text-to-speech</summary>
    public static partial class Translations
    {
        public static string GetTranslation(string val, bool useICAO = false, string type = null)
        {
            // Translations from fixed dictionaries
            string translation = val;
            type = !string.IsNullOrEmpty(type) ? type.ToLowerInvariant() : null;

            switch (type)
            {
                case "power":
                    translation = getPhoneticPower(val);
                    break;
                case "planettype":
                    translation = getPhoneticPlanetClass(val);
                    break;
                case "shipmodel":
                    translation = getPhoneticShipModel(val);
                    break;
                case "shipmanufacturer":
                    translation = getPhoneticShipManufacturer(val);
                    break;
                case "station":
                    translation = getPhoneticStation(val);
                    break;
                case "starsystem":
                    translation = getPhoneticStarSystem(val, useICAO);
                    break;
                case "body":
                    translation = getPhoneticBody(val, useICAO);
                    break;
                case "faction":
                    translation = getPhoneticFaction(val, useICAO);
                    break;
                default:
                    if (translation == val)
                    {
                        translation = getPhoneticPower(val);
                    }
                    if (translation == val)
                    {
                        translation = getPhoneticPlanetClass(val);
                    }
                    if (translation == val)
                    {
                        translation = getPhoneticShipModel(val);
                    }
                    if (translation == val)
                    {
                        translation = getPhoneticShipManufacturer(val);
                    }
                    if (translation == val)
                    {
                        translation = getPhoneticStation(val);
                    }
                    if (translation == val)
                    {
                        translation = getPhoneticBody(val, useICAO);
                    }
                    if (translation == val)
                    {
                        translation = getPhoneticStarSystem(val, useICAO);
                    }
                    if (translation == val)
                    {
                        // Faction names can include system names, so we need to recognize system names first
                        translation = getPhoneticFaction(val, useICAO);
                    }
                    break;
            }
            return translation;
        }

        // Various handy regexes so we don't keep recreating them
        private static readonly Regex ALPHA_DOT = new Regex(@"[A-Z]\.");
        private static readonly Regex ALPHA_THEN_NUMERIC = new Regex(@"[A-Za-z]+[0-9]+");
        private static readonly Regex UPPERCASE = new Regex(@"([A-Z]{2,})|(?:([A-Z])(?:\s|$))");
        private static readonly Regex TEXT = new Regex(@"([A-Za-z]{1,3}(?:\s|$))");
        private static readonly Regex DIGIT = new Regex(@"\d+(?:\s|$)");
        private static readonly Regex THREE_OR_MORE_DIGITS = new Regex(@"\d{3,}");
        private static readonly Regex DECIMAL_DIGITS = new Regex(@"( point )(\d{2,})");
        private static readonly Regex SECTOR = new Regex("(.*) ([A-Za-z][A-Za-z]-[A-Za-z] .*)");
        private static readonly Regex MOON = new Regex(@"^[a-z]$");
        private static readonly Regex SUBSTARS = new Regex(@"^\bA[BCDE]?[CDE]?[DE]?[E]?\b|\bB[CDE]?[DE]?[E]?\b|\bC[DE]?[E]?\b|\bD[E]?\b$");
        private static readonly Regex SYSTEMBODY = new Regex(@"^(.*?) ([A-E]+ ){0,2}(Belt(?:\s|$)|Cluster(?:\s|$)|Ring|\d{1,2}(?:\s|$)|[A-Za-z](?:\s|$)){1,12}$");
        private static readonly Regex SHORTBODY = new Regex(@"(?=\S)(?<STARS>(?<=^|\s)A?B?C?D?E?)? ?(?<PLANET>(?<=^|\s)\d{1,2})? ?(?<MOON>(?<=^|\s)[a-z])? ?(?<SUBMOON>(?<=^|\s)[a-z])? ?(?>(?<=^|\s)(?<RINGORBELTGROUP>[A-Z]) (?<RINGORBELTTYPE>Belt|Ring))? ?(?>(?<=^|\s)(?<CLUSTER>Cluster) (?<CLUSTERNUMBER>\d*))?$");
        private static readonly Regex PROC_GEN_SYSTEM = new Regex(@"^(?<SECTOR>[\w\s'.()-]+) (?<COORDINATES>(?<l1>[A-Za-z])(?<l2>[A-Za-z])-(?<l3>[A-Za-z]) (?<mcode>[A-Za-z])(?:(?<n1>\d+)-)?(?<n2>\d+))$");
        private static readonly Regex PROC_GEN_SYSTEM_BODY = new Regex(@"^(?<SYSTEM>(?<SECTOR>[\w\s'.()-]+) (?<COORDINATES>(?<l1>[A-Za-z])(?<l2>[A-Za-z])-(?<l3>[A-Za-z]) (?<mcode>[A-Za-z])(?:(?<n1>\d+)-)?(?<n2>\d+))) ?(?<BODY>.*)$");

        private static string replaceWithPronunciation(string sourcePhrase, string[] pronunciation)
        {
            StringBuilder sb = new StringBuilder();
            int i = 0;
            foreach (string source in sourcePhrase.Split(' '))
            {
                if (i > 0)
                {
                    sb.Append(" ");
                }
                sb.Append("<phoneme alphabet=\"ipa\" ph=\"");
                sb.Append(pronunciation[i++]);
                sb.Append("\">");
                sb.Append(source);
                sb.Append("</phoneme>");
            }
            return sb.ToString();
        }

        public static string ICAO(string callsign, bool passDash = false)
        {
            if (callsign == null)
            {
                return null;
            }

            var elements = new List<string>();
            foreach (char c in callsign.ToUpperInvariant())
            {
                switch (c)
                {
                    case 'A':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"ˈælfə\">alpha</phoneme>");
                        break;
                    case 'B':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"ˈbrɑːˈvo\">bravo</phoneme>");
                        break;
                    case 'C':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"ˈtʃɑɹli\">charlie</phoneme>");
                        break;
                    case 'D':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"ˈdɛltə\">delta</phoneme>");
                        break;
                    case 'E':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"ˈeko\">echo</phoneme>");
                        break;
                    case 'F':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"ˈfɒkstrɒt\">foxtrot</phoneme>");
                        break;
                    case 'G':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"ɡɒlf\">golf</phoneme>");
                        break;
                    case 'H':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"hoːˈtel\">hotel</phoneme>");
                        break;
                    case 'I':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"ˈindiˑɑ\">india</phoneme>");
                        break;
                    case 'J':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"ˈdʒuːliˑˈet\">juliet</phoneme>");
                        break;
                    case 'K':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"ˈkiːlo\">kilo</phoneme>");
                        break;
                    case 'L':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"ˈliːmɑ\">lima</phoneme>");
                        break;
                    case 'M':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"maɪk\">mike</phoneme>");
                        break;
                    case 'N':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"noˈvembə\">november</phoneme>");
                        break;
                    case 'O':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"ˈɒskə\">oscar</phoneme>");
                        break;
                    case 'P':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"pəˈpɑ\">papa</phoneme>");
                        break;
                    case 'Q':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"keˈbek\">quebec</phoneme>");
                        break;
                    case 'R':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"ˈroːmiˑo\">romeo</phoneme>");
                        break;
                    case 'S':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"siˈerə\">sierra</phoneme>");
                        break;
                    case 'T':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"ˈtænɡo\">tango</phoneme>");
                        break;
                    case 'U':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"ˈjuːnifɔːm\">uniform</phoneme>");
                        break;
                    case 'V':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"ˈvɪktə\">victor</phoneme>");
                        break;
                    case 'W':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"ˈwiski\">whiskey</phoneme>");
                        break;
                    case 'X':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"ˈeksˈrei\">x-ray</phoneme>");
                        break;
                    case 'Y':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"ˈjænki\">yankee</phoneme>");
                        break;
                    case 'Z':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"ˈzuːluː\">zulu</phoneme>");
                        break;
                    case '0':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"ˈzɪərəʊ\">zero</phoneme>");
                        break;
                    case '1':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"ˈwʌn\">one</phoneme>");
                        break;
                    case '2':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"ˈtuː\">two</phoneme>");
                        break;
                    case '3':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"ˈtriː\">tree</phoneme>");
                        break;
                    case '4':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"ˈfoʊ.ər\">fawer</phoneme>");
                        break;
                    case '5':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"ˈfaɪf\">fife</phoneme>");
                        break;
                    case '6':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"ˈsɪks\">six</phoneme>");
                        break;
                    case '7':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"ˈsɛvɛn\">seven</phoneme>");
                        break;
                    case '8':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"ˈeɪt\">eight</phoneme>");
                        break;
                    case '9':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"ˈnaɪnər\">niner</phoneme>");
                        break;
                    case '-':
                        if (passDash)
                        {
                            elements.Add(" " + Properties.Phrases.dash + " ");
                        }

                        break;
                }
            }

            return string.Join(" ", elements).Trim();
        }

        public static string sayAsLettersOrNumbers(string part, bool useLongNumbers = false, bool useICAO = false)
        {
            var matchConditions = new Regex(@"([A-Z])|(\d+)|([a-z])|(\S)");

            var elements = new List<string>();
            foreach (var match in matchConditions.Matches(part))
            {
                var matchAsString = match.ToString();
                if (long.TryParse(matchAsString, out long number))
                {
                    // Handle numbers
                    if (useICAO)
                    {
                        elements.Add(ICAO(matchAsString));
                    }
                    else if (!useLongNumbers)
                    {
                        foreach (var c in matchAsString)
                        {
                            elements.Add($"{c}");
                        }
                    }
                    else
                    {
                        // Handle leading zeros
                        if (number > 0)
                        {
                            elements.AddRange(matchAsString.TakeWhile(s => s == '0').Select(s => s.ToString()));
                        }
                       
                        // Handle the number
                        elements.Add($"{number}");
                    }
                }
                else if (!(new Regex(@"\w").IsMatch(matchAsString)))
                {
                    // Handle non-word and non-number characters
                    foreach (var c in matchAsString)
                    {
                        if (matchAsString == "-")
                        {
                            elements.Add(Properties.Phrases.dash);
                        }
                        else if (matchAsString == ".")
                        {
                            elements.Add(Properties.Phrases.point);
                        }
                        else if (matchAsString == "+")
                        {
                            elements.Add(Properties.Phrases.plus);
                        }
                        else
                        {
                            elements.Add(matchAsString);
                        }
                    }
                }
                else
                {
                    // Handle strings
                    if (useICAO)
                    {
                        elements.Add(ICAO(matchAsString));
                    }
                    else
                    {
                        foreach (var c in matchAsString)
                        {
                            elements.Add(@"<say-as interpret-as=""characters"">" + c + @"</say-as>");
                        }
                    }
                }
            }
            return string.Join(" ", elements).Trim();
        }
    }
}
