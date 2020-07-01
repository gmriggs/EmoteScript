using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

using Newtonsoft.Json;

using EmoteScriptLib;

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
                Converter.sql2es(fi);
                return;
            }

            if (fi.Extension.Equals(".json", StringComparison.OrdinalIgnoreCase))
            {
                // convert json to es
                Converter.json2es(fi);
                return;
            }

            // convert es to sql / json
            var emoteTable = new EmoteTable();

            emoteTable.EmoteSets = Parser.ParseFile(fi);

            if (emoteTable.EmoteSets == null)
                return;

            //emoteTable.BuildLinks();
            //emoteTable.ClearLinks();

            emoteTable.Wcid = Converter.GetObjectId(fi);
            //emoteTable.Wcid = 33970;

            //ShowSQL(emoteTable);
            //ShowJSON(emoteTable.EmoteSets);
            //ShowScript(emoteTable.EmoteSets);

            //return;

            if (args.Length < 2 || !args[1].Contains("json", StringComparison.OrdinalIgnoreCase))
            {
                // convert script to sql
                Converter.es2sql(emoteTable, fi);
            }
            else
            {
                // convert script to json
                Converter.es2json(emoteTable, fi);
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
