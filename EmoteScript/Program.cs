using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

using Newtonsoft.Json;

using EmoteScript.JSON;
using EmoteScript.SQL;

namespace EmoteScript
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                ShowUsage();
                return;
            }

            var filename = args[0];

            // if only a wcid is specified, search for <wcid>*.es
            if (int.TryParse(filename, out _))
            {
                var cwd = new DirectoryInfo(Directory.GetCurrentDirectory());
                var files = cwd.GetFiles(filename + " *.es");
                if (files.Count() == 1)
                    filename = files[0].FullName;
            }
            
            // if no extension is specified, assume .es
            if (!filename.EndsWith(".es", StringComparison.OrdinalIgnoreCase) && !filename.EndsWith(".sql", StringComparison.OrdinalIgnoreCase) && !filename.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                filename += ".es";

            if (!File.Exists(filename))
            {
                Console.WriteLine($"{filename} not found");
                return;
            }

            var fi = new FileInfo(filename);

            if (fi.Extension.Equals(".sql", StringComparison.OrdinalIgnoreCase))
            {
                // convert sql to es
                sql2es(fi);
                return;
            }

            if (fi.Extension.Equals(".json", StringComparison.OrdinalIgnoreCase))
            {
                // convert json to es
                json2es(fi);
                return;
            }

            // convert es to sql / json
            var emoteTable = new EmoteTable();

            emoteTable.EmoteSets = Parser.ParseFile(fi);

            if (emoteTable.EmoteSets == null)
                return;

            //emoteTable.BuildLinks();
            //emoteTable.ClearLinks();

            emoteTable.Wcid = GetObjectId(fi);
            //emoteTable.Wcid = 33970;

            //ShowSQL(emoteTable);
            //ShowJSON(emoteTable.EmoteSets);
            //ShowScript(emoteTable.EmoteSets);

            //return;

            if (args.Length < 2 || !args[1].Contains("json", StringComparison.OrdinalIgnoreCase))
            {
                // convert script to sql
                es2sql(emoteTable, fi);
            }
            else
            {
                // convert script to json
                es2json(emoteTable, fi);
            }
        }

        public static void es2sql(EmoteTable emoteTable, FileInfo esFile)
        {
            var sqlFilename = Path.ChangeExtension(esFile.FullName, ".sql");
            var sqlFile = new FileInfo(sqlFilename);

            // check if file already exists?

            // output sql file
            OutputSQL(emoteTable, sqlFile);
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

        public static void sql2es(FileInfo sqlFile)
        {
            var sqlLines = File.ReadAllLines(sqlFile.FullName);

            var sqlReader = new SQLReader();

            var emoteTable = sqlReader.ReadEmoteTable(sqlLines);

            emoteTable.BuildLinks();

            //ShowScript(emoteTable.EmoteSets);

            var esFilename = Path.ChangeExtension(sqlFile.FullName, ".es");

            // check if file already exists?

            var esFile = new FileInfo(esFilename);

            OutputScript(emoteTable.EmoteSets, esFile);
        }

        public static void json2es(FileInfo jsonFile)
        {
            var settings = new JsonSerializerSettings();
            settings.ContractResolver = new LowercaseContractResolver();

            var json = File.ReadAllText(jsonFile.FullName);

            var emoteTable = new EmoteTable();

            emoteTable.EmoteSets = JsonConvert.DeserializeObject<List<EmoteSet>>(json, settings);

            emoteTable.SetValidBranches();

            emoteTable.BuildLinks();

            //ShowScript(emoteTable.EmoteSets);

            var esFilename = Path.ChangeExtension(jsonFile.FullName, ".es");

            // check if file already exists?

            var esFile = new FileInfo(esFilename);

            OutputScript(emoteTable.EmoteSets, esFile);
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
            
            return SQLWriter.GetSQL(emoteTable);
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

        public static void ShowUsage()
        {
            Console.WriteLine($"EmoteScript - a scripting language for Asheron's Call emotes");
            Console.WriteLine($"Usage: EmoteScript <filename>");
            Console.WriteLine($"Converts an EmoteScript file into a sql/json file");
        }
    }
}
