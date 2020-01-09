using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using EmoteScript.Entity.Enum;

namespace EmoteScript
{
    public class Line
    {
        public static Line Parse(string line)
        {
            var pre = Preprocess(line);

            if (pre.Length == 0)
                return null;

            pre = ParseDelay(pre, out var delay);

            var cmd = ParseCommand(pre, out var parms);

            var tokens = Tokenize(parms);

            var dict = ParseDictionary(tokens);

            if (Enum.TryParse(cmd, true, out EmoteType type) && type != EmoteType.Give || line.Trim().StartsWith('-'))
            {
                var emote = new Emote_Line(type, dict, delay);
                return emote;
            }

            if (Enum.TryParse(cmd, true, out EmoteCategory category))
            {
                var emoteSet = new EmoteSet_Line(category, dict);
                return emoteSet;
            }

            return null;
        }

        /// <summary>
        /// Trims any leading / trailing whitespace,
        /// and removes - prefix
        /// </summary>
        public static string Preprocess(string line)
        {
            line = line.Trim();

            if (line.StartsWith("-"))
                line = line.Substring(1).Trim();

            return line;
        }

        public static string ParseCommand(string line, out string parms)
        {
            // assumes line has already been normalized,
            // ie. trimmed, - prefix removed
            var idx = line.IndexOf(':');
            if (idx == -1)
            {
                idx = line.IndexOf(' ');
                if (idx == -1)
                {
                    parms = string.Empty;
                    return line;
                }
            }

            var startIdx = 0;
            if (line.StartsWith("Delay"))
            {
                startIdx = line.IndexOf(",") + 2;
                idx -= startIdx;
            }

            parms = line.Substring(idx + 1).Trim();

            return line.Substring(startIdx, idx);
        }

        public static string ParseDelay(string line, out float? delay)
        {
            delay = null;
            
            if (!line.StartsWith("Delay", StringComparison.OrdinalIgnoreCase))
                return line;

            var suffix = line.Substring(5).TrimStart(':').Trim();

            var match = Regex.Match(suffix, @"^([\d.]+)");
            if (!match.Success)
            {
                Console.WriteLine($"Line.ParseDelay() - couldn't read delay from {suffix}");
                return line;
            }

            var delayStr = match.Groups[1].Value;

            if (!float.TryParse(delayStr, out var _delay))
            {
                Console.WriteLine($"Line.ParseDelay() - couldn't read delay from {match.Groups[1].Value}");
                return line;
            }

            delay = _delay;

            var idx = line.IndexOf(delayStr) + delayStr.Length;

            line = line.Substring(idx).TrimStart(',').Trim();

            //Console.WriteLine($"Delay: {delay}");
            //Console.WriteLine($"Suffix: {line}");

            return line;
        }

        public static List<string> Tokenize(string line)
        {
            line = line.Trim();
            
            if (line.Length == 0)
                return new List<string>();

            var tokens = new List<string>();

            while (true)
            {
                var delimiter = GetDelimiter(line);

                if (delimiter == -1)
                    break;

                var token = line.Substring(0, delimiter);
                tokens.Add(token);

                line = line.Substring(delimiter + 1);
            }

            if (line.Length > 0)
                tokens.Add(line);

            return Trim(tokens);
        }

        public static int GetDelimiter(string line)
        {
            var quoteIdx = line.IndexOf('\"');
            var commaIdx = line.IndexOf(',');

            if (quoteIdx == -1)
            {
                // directive follows?
                while (commaIdx != -1)
                {
                    var suffix = line.Substring(commaIdx + 1).Trim();
                    if (ValidSuffix(suffix))
                        break;

                    commaIdx = line.IndexOf(',', commaIdx + 1);
                }
                return commaIdx;
            }

            var startIdx = quoteIdx + 1;

            while (true)
            {
                quoteIdx = line.IndexOf('\"', startIdx);

                if (quoteIdx == -1)
                {
                    Console.WriteLine($"Line.GetDelimiter(): unterminated quotes on line {line}");
                    return -1;
                }

                if (line[quoteIdx - 1] != '\\')
                    break;

                startIdx = quoteIdx + 1;
            }
            return quoteIdx;
        }

        public static bool ValidSuffix(string line)
        {
            line = line.Trim();
            if (line.Length == -1)
                return true;

            var idx = line.IndexOf(':');
            if (idx == -1)
                idx = line.IndexOf(' ');

            if (idx == -1)
                return false;

            var suffix = line.Substring(0, idx).Trim();
            return IsField(suffix);
        }

        public static List<string> Trim(List<string> tokens)
        {
            var trim = new List<string>();

            foreach (var token in tokens)
                trim.Add(token.TrimStart('\"').Trim());

            return trim;
        }

        public static bool IsField(string fieldName)
        {
            return !int.TryParse(fieldName, out _) && (Enum.TryParse(fieldName, true, out EmoteField emoteField) || Enum.TryParse(fieldName, true, out EmoteSetField emoteSetField));
        }

        public static Dictionary<string, string> ParseDictionary(List<string> tokens)
        {
            var dict = new Dictionary<string, string>();

            foreach (var token in tokens)
            {
                var kvp = ParseKVP(token);

                if (dict.ContainsKey(kvp.Key))
                {
                    Console.WriteLine($"Line.ParseDictionary() - duplicate key {kvp.Key}");
                    Console.WriteLine(string.Join(',', tokens));
                    continue;
                }
                dict.Add(kvp.Key, kvp.Value);
            }
            return dict;
        }

        public static KeyValuePair<string, string> ParseKVP(string token)
        {
            var idx = token.IndexOf(':');
            if (idx == -1)
            {
                idx = token.IndexOf(' ');
                if (idx == -1 || !IsField(token.Substring(0, idx)))
                    return new KeyValuePair<string, string>(string.Empty, token);
            }

            var key = token.Substring(0, idx).Trim();
            var val = token.Substring(idx + 1).Trim();

            return new KeyValuePair<string, string>(key, val);
        }
    }
}
