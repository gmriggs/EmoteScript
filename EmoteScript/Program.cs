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

            if (int.TryParse(filename, out _))
            {
                var cwd = new DirectoryInfo(Directory.GetCurrentDirectory());
                var files = cwd.GetFiles(filename + " *.es");
                if (files.Count() == 1)
                    filename = files[0].FullName;
            }
            
            if (!filename.Contains("."))
                filename += ".es";

            if (!File.Exists(filename))
            {
                Console.WriteLine($"{filename} not found");
                return;
            }

            var fi = new FileInfo(filename);

            //var lines = Parser.ParseLines(fi);
            //return;
            
            var emoteSets = Parser.ParseFile(fi);

            if (emoteSets == null)
                return;

            //ShowScript(emoteSets);

            //ShowEmoteSets(emoteSets);

            var flatten = EmoteSet.Flatten(emoteSets);

            var objectId = GetObjectId(fi);

            //ShowSQL(flatten, objectId);
            //ShowJSON(flatten);

            if (args.Length < 2 || !args[1].Contains("json", StringComparison.OrdinalIgnoreCase))
            {
                // get .sql output filename
                var sqlFilename = Path.ChangeExtension(fi.FullName, ".sql");
                var sqlFile = new FileInfo(sqlFilename);

                // output sql file
                OutputSQL(flatten, sqlFile);
            }
            else
            {
                // get .json output filename
                var jsonFilename = Path.ChangeExtension(fi.FullName, ".json");
                var jsonFile = new FileInfo(jsonFilename);

                // output json file
                OutputJSON(flatten, jsonFile);
            }
        }

        public static void ShowSQL(List<EmoteSet> emoteSets, string objectId)
        {
            var sqlLines = SQLWriter.GetSQL(emoteSets, objectId);

            foreach (var sqlLine in sqlLines)
                Console.WriteLine(sqlLine);
        }

        public static void ShowJSON(List<EmoteSet> emoteSets)
        {
            var settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            settings.Formatting = Formatting.Indented;
            settings.ContractResolver = new LowercaseContractResolver();
            
            var json = JsonConvert.SerializeObject(emoteSets, settings);
            Console.WriteLine(json);
        }

        public static string GetObjectId(FileInfo file)
        {
            var objectId = "#####";
            var match = Regex.Match(file.Name, @"^(\d+)");
            if (match.Success)
                objectId = match.Groups[1].Value;

            return objectId;
        }

        public static void OutputSQL(List<EmoteSet> emoteSets, FileInfo sqlFile)
        {
            var objectId = GetObjectId(sqlFile);
            
            var sqlLines = SQLWriter.GetSQL(emoteSets, objectId);

            File.WriteAllLines(sqlFile.FullName, sqlLines);

            Console.WriteLine($"Compiled {sqlFile.FullName}");
        }

        public static void OutputJSON(List<EmoteSet> emoteSets, FileInfo jsonFile)
        {
            var settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            settings.Formatting = Formatting.Indented;
            settings.ContractResolver = new LowercaseContractResolver();

            var json = JsonConvert.SerializeObject(emoteSets, settings);

            File.WriteAllText(jsonFile.FullName, json);

            Console.WriteLine($"Compiled {jsonFile.FullName}");
        }

        public static void ShowScript(List<EmoteSet> emoteSets)
        {
            var output = new ScriptWriter(emoteSets);
            var script = output.BuildScript();

            foreach (var line in script)
                Console.WriteLine(line);
        }

        public static void ShowEmoteSets(List<EmoteSet> emoteSets)
        {
            for (var i = 0; i < emoteSets.Count; i++)
            {
                var emoteSet = emoteSets[i];

                if (i > 0)
                    Console.WriteLine();

                Console.WriteLine(emoteSet);

                foreach (var emote in emoteSet.Emotes)
                    ShowEmote(emote);
            }
        }

        public static void ShowEmote(Emote emote, int depth = 1)
        {
            var indent = string.Concat(Enumerable.Repeat("    ", depth));

            Console.WriteLine($"{indent}- {emote}");

            if (emote.Branches != null)
            {
                foreach (var branch in emote.Branches)
                {
                    Console.WriteLine($"{indent}    {branch.Key}:");

                    foreach (var branchEmote in branch.Value.Emotes)
                        ShowEmote(branchEmote, depth + 2);
                }
            }
        }

        public static void ShowUsage()
        {
            Console.WriteLine($"EmoteScript - a scripting language for Asheron's Call emotes");
            Console.WriteLine($"Usage: EmoteScript <filename>");
            Console.WriteLine($"Converts an EmoteScript file into a sql/json file");
        }
    }
}
