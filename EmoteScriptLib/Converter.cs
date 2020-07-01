using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

using Newtonsoft.Json;

namespace EmoteScriptLib
{
    public static class Converter
    {
        public static void es2sql(EmoteTable emoteTable, FileInfo esFile)
        {
            var sqlFilename = Path.ChangeExtension(esFile.FullName, ".sql");
            var sqlFile = new FileInfo(sqlFilename);

            // check if file already exists?

            // output sql file
            OutputSQL(emoteTable, sqlFile);
        }

        public static List<string> es2sql(string[] esLines, uint? wcid = null)
        {
            var emoteTable = new EmoteTable();

            emoteTable.EmoteSets = Parser.ParseLines(esLines);

            if (emoteTable.EmoteSets == null)
                return null;

            emoteTable.Wcid = wcid;

            var sqlLines = BuildSQL(emoteTable);

            return sqlLines;
        }

        public static void es2json(EmoteTable emoteTable, FileInfo esFile)
        {
            var jsonFilename = Path.ChangeExtension(esFile.FullName, ".json");
            var jsonFile = new FileInfo(jsonFilename);

            emoteTable.NormalRange();

            var jsonTable = new JSON.EmoteTable(emoteTable);

            // check if file already exists?

            // output json file
            OutputJSON(jsonTable, jsonFile);
        }

        public static string es2json(string[] esLines, uint? wcid = null)
        {
            var emoteTable = new EmoteTable();

            emoteTable.EmoteSets = Parser.ParseLines(esLines);

            if (emoteTable.EmoteSets == null)
                return null;

            emoteTable.Wcid = wcid;

            emoteTable.NormalRange();

            var jsonTable = new JSON.EmoteTable(emoteTable);

            var jsonLines = BuildJSON(jsonTable);

            return jsonLines;
        }

        public static void sql2es(FileInfo sqlFile)
        {
            var sqlLines = File.ReadAllLines(sqlFile.FullName);

            var sqlReader = new SQL.SQLReader();

            var emoteTable = sqlReader.ReadEmoteTable(sqlLines);

            emoteTable.BuildLinks();

            //ShowScript(emoteTable.EmoteSets);

            var esFilename = Path.ChangeExtension(sqlFile.FullName, ".es");

            // check if file already exists?

            var esFile = new FileInfo(esFilename);

            OutputScript(emoteTable.EmoteSets, esFile);
        }

        public static List<string> sql2es(string[] sqlLines)
        {
            var sqlReader = new SQL.SQLReader();

            var emoteTable = sqlReader.ReadEmoteTable(sqlLines);

            emoteTable.BuildLinks();

            //ShowScript(emoteTable.EmoteSets);

            var esLines = BuildScript(emoteTable.EmoteSets);

            return esLines;
        }

        public static void json2es(FileInfo jsonFile)
        {
            var settings = new JsonSerializerSettings();
            settings.ContractResolver = new JSON.LowercaseContractResolver();

            var json = File.ReadAllText(jsonFile.FullName);

            var jsonEmoteTable = JsonConvert.DeserializeObject<JSON.EmoteTable>(json, settings);

            var emoteTable = new EmoteTable(jsonEmoteTable);

            emoteTable.SetValidBranches();

            emoteTable.BuildLinks();

            //ShowScript(emoteTable.EmoteSets);

            var esFilename = Path.ChangeExtension(jsonFile.FullName, ".es");

            // check if file already exists?

            var esFile = new FileInfo(esFilename);

            OutputScript(emoteTable.EmoteSets, esFile);
        }

        public static List<string> json2es(string json)
        {
            var settings = new JsonSerializerSettings();
            settings.ContractResolver = new JSON.LowercaseContractResolver();

            var jsonEmoteTable = JsonConvert.DeserializeObject<JSON.EmoteTable>(json, settings);

            var emoteTable = new EmoteTable(jsonEmoteTable);

            emoteTable.SetValidBranches();

            emoteTable.BuildLinks();

            var esLines = BuildScript(emoteTable.EmoteSets);

            return esLines;
        }

        public static void OutputSQL(EmoteTable emoteTable, FileInfo sqlFile)
        {
            var sqlLines = BuildSQL(emoteTable);

            File.WriteAllLines(sqlFile.FullName, sqlLines);

            Console.WriteLine($"Compiled {sqlFile.FullName}");
        }

        public static uint? GetObjectId(FileInfo file)
        {
            var match = Regex.Match(file.Name, @"^(\d+)");

            if (match.Success)
                return uint.Parse(match.Groups[1].Value);
            else
                return null;
        }

        public static List<string> BuildSQL(EmoteTable emoteTable)
        {
            emoteTable.NormalRange();

            return EmoteScriptLib.SQL.SQLWriter.GetSQL(emoteTable);
        }

        public static void ShowSQL(EmoteTable emoteTable)
        {
            var sqlLines = BuildSQL(emoteTable);

            foreach (var sqlLine in sqlLines)
                Console.WriteLine(sqlLines);
        }

        public static void OutputJSON(JSON.EmoteTable emoteTable, FileInfo jsonFile)
        {
            var json = BuildJSON(emoteTable);

            File.WriteAllText(jsonFile.FullName, json);

            Console.WriteLine($"Compiled {jsonFile.FullName}");
        }

        public static string BuildJSON(JSON.EmoteTable emoteTable)
        {
            var settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            settings.Formatting = Formatting.Indented;
            //settings.ContractResolver = new LowercaseContractResolver();

            return JsonConvert.SerializeObject(emoteTable, settings);
        }

        public static void ShowJSON(EmoteTable emoteTable)
        {
            var jsonTable = new JSON.EmoteTable(emoteTable);

            var json = BuildJSON(jsonTable);

            Console.WriteLine(json);
        }

        public static void OutputScript(List<EmoteSet> emoteSets, FileInfo esFile)
        {
            var script = BuildScript(emoteSets);

            File.WriteAllLines(esFile.FullName, script);

            Console.WriteLine($"Compiled {esFile.FullName}");
        }

        public static List<string> BuildScript(List<EmoteSet> emoteSets, int depth = 0)
        {
            var scriptLines = new List<string>();

            var indent = string.Concat(Enumerable.Repeat("    ", depth));

            for (var i = 0; i < emoteSets.Count; i++)
            {
                var emoteSet = emoteSets[i];

                if (depth == 0 && emoteSet.Inline)
                    continue;

                if (depth > 0 && !emoteSet.Inline)
                    continue;

                //Console.WriteLine($"{emoteSet}");

                if (i > 0 && depth == 0)
                    scriptLines.Add(string.Empty);

                scriptLines.Add($"{indent}{emoteSet.ToString(true)}");

                foreach (var emote in emoteSet.Emotes)
                    scriptLines.AddRange(BuildScript(emote, depth + 1));
            }
            return scriptLines;
        }

        public static List<string> BuildScript(Emote emote, int depth)
        {
            //Console.WriteLine($"{emote}");

            var scriptLines = new List<string>();

            var indent = string.Concat(Enumerable.Repeat("    ", depth));

            scriptLines.Add($"{indent}- {emote.ToString(true)}");

            if (emote.Branches != null)
                scriptLines.AddRange(BuildScript(emote.Branches, depth + 1));

            return scriptLines;
        }

        public static void ShowScript(List<EmoteSet> emoteSets)
        {
            var script = BuildScript(emoteSets);

            foreach (var line in script)
                Console.WriteLine(line);
        }
    }
}
