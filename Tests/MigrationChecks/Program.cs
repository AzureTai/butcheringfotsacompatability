using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using ButcheringFotSACompatability;
using ButcheringFotSACompatability.Migration;
using Newtonsoft.Json.Linq;
using ProtoBuf;
using Vintagestory.API.Common;
using Vintagestory.Server;

namespace MigrationChecks
{
    public static class Program
    {
        public static int Main(string[] arguments)
        {
            if (arguments.Length != 1)
            {
                Console.Error.WriteLine("Supply the absolute path to cervini-remaps.json.");
                return 2;
            }

            JArray patches = JArray.Parse(File.ReadAllText(arguments[0]));
            JArray? commands = patches[0]["value"] as JArray;
            if (commands == null || commands.Count != 12)
            {
                throw new InvalidDataException("Expected the twelve original Cervini commands.");
            }

            CheckCleanWorld(commands);
            CheckLegacyWorld(commands);
            CheckPartialWorld(commands);
            CheckInvalidCommands(commands);
            CheckSystemOrder();
            Console.WriteLine("PASS: clean, legacy, partial, repeated, unrelated and invalid input checks; actual VS 1.22.7 AutoRemap preserves all twelve numeric IDs.");
            return 0;
        }

        private static JObject CreateRemaps(JArray commands)
        {
            JObject remaps = new JObject();
            remaps[CerviniRemapFilter.GroupCode] = commands.DeepClone();
            remaps["othermod:untouched"] = new JArray("unrelated command");
            return remaps;
        }

        private static void CheckCleanWorld(JArray commands)
        {
            JObject remaps = CreateRemaps(commands);
            Dictionary<int, string> savedCodes = new Dictionary<int, string>();
            savedCodes.Add(0, "game:air");
            savedCodes.Add(1, "game:stick");
            Require(CerviniRemapFilter.Filter(remaps, savedCodes), "Clean world should remove the group.");
            Require(remaps[CerviniRemapFilter.GroupCode] == null, "Clean world still contains remaps.");
            Require(remaps["othermod:untouched"]?[0]?.Value<string>() == "unrelated command", "Unrelated group changed.");
            Require(!CerviniRemapFilter.Filter(remaps, savedCodes), "Repeated filtering should do nothing.");
        }

        private static void CheckLegacyWorld(JArray commands)
        {
            Dictionary<int, string> savedCodes = new Dictionary<int, string>();
            Dictionary<int, AssetLocation> engineCodes = new Dictionary<int, AssetLocation>();
            for (int index = 0; index < commands.Count; index++)
            {
                string[] parts = ReadCommand(commands[index]);
                savedCodes.Add(index, parts[3]);
                engineCodes.Add(index, new AssetLocation(parts[3]));
            }

            // Match the engine's actual protobuf ItemIDs serialization, including an entry at ID zero.
            using (MemoryStream stream = new MemoryStream())
            {
                Serializer.Serialize(stream, savedCodes);
                stream.Position = 0;
                savedCodes = Serializer.Deserialize<Dictionary<int, string>>(stream);
            }

            JObject remaps = CreateRemaps(commands);
            Require(!CerviniRemapFilter.Filter(remaps, savedCodes), "Complete legacy group should remain unchanged.");
            ServerSystemItemIdRemapper engine = (ServerSystemItemIdRemapper)RuntimeHelpers.GetUninitializedObject(typeof(ServerSystemItemIdRemapper));
            for (int index = 0; index < commands.Count; index++)
            {
                string[] parts = ReadCommand(commands[index]);
                Require(engine.AutoRemap(parts[3], parts[2], engineCodes), "Engine rejected a retained migration.");
                Require(engineCodes[index].Equals(new AssetLocation(parts[2])), "The legacy numeric ID did not retain its destination.");
            }

            Require(engineCodes.Count == 12, "Engine migration changed the number of numeric IDs.");
            savedCodes.Clear();
            foreach (KeyValuePair<int, AssetLocation> entry in engineCodes)
            {
                savedCodes.Add(entry.Key, entry.Value.ToShortString());
            }

            Require(CerviniRemapFilter.Filter(remaps, savedCodes), "Migrated world should no longer offer old sources.");
            Require(remaps[CerviniRemapFilter.GroupCode] == null, "Migrated world still has legacy commands.");
        }

        private static void CheckPartialWorld(JArray commands)
        {
            JObject remaps = CreateRemaps(commands);
            Dictionary<int, string> savedCodes = new Dictionary<int, string>();
            savedCodes.Add(3456, ReadCommand(commands[7])[3]);
            Require(CerviniRemapFilter.Filter(remaps, savedCodes), "Partial world was not filtered.");
            JArray? retained = remaps[CerviniRemapFilter.GroupCode] as JArray;
            Require(retained != null && retained.Count == 1, "Partial world must retain exactly one command.");
            Require(remaps[CerviniRemapFilter.GroupCode]?[0]?.Value<string>() == commands[7].Value<string>(), "Partial world retained the wrong command.");
        }

        private static void CheckInvalidCommands(JArray commands)
        {
            JObject remaps = CreateRemaps(commands);
            JArray invalid = new JArray(commands[0].DeepClone(), "/iir remapq wrong:destination wrong:source force");
            remaps[CerviniRemapFilter.GroupCode] = invalid;
            string original = remaps.ToString();
            bool rejected = false;
            try
            {
                CerviniRemapFilter.Filter(remaps, new Dictionary<int, string>());
            }
            catch (InvalidDataException)
            {
                rejected = true;
            }

            Require(rejected, "Malformed migration data must be reported.");
            Require(remaps.ToString() == original, "Malformed data caused a partial asset mutation.");
        }

        private static void CheckSystemOrder()
        {
            ButcheringFotSACompatabilityModSystem system = new ButcheringFotSACompatabilityModSystem();
            Require(system.ShouldLoad(EnumAppSide.Server), "System must load on the server.");
            Require(!system.ShouldLoad(EnumAppSide.Client), "System must not load on the client.");
            Require(system.ExecuteOrder() > 0.05 && system.ExecuteOrder() < 0.2, "System must run between patching and remapping.");

            int modSystemCount = 0;
            Type[] types = typeof(ButcheringFotSACompatabilityModSystem).Assembly.GetTypes();
            foreach (Type type in types)
            {
                if (type.IsSubclassOf(typeof(ModSystem)))
                {
                    modSystemCount++;
                    Require(type == typeof(ButcheringFotSACompatabilityModSystem), "An unexpected independent ModSystem was found.");
                }
            }

            Require(modSystemCount == 1, "The mod must expose exactly one main ModSystem.");
        }

        private static string[] ReadCommand(JToken token)
        {
            string? command = token.Value<string>();
            if (command == null)
            {
                throw new InvalidDataException("Missing test command.");
            }

            return command.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        }

        private static void Require(bool condition, string message)
        {
            if (!condition)
            {
                throw new InvalidOperationException(message);
            }
        }
    }
}
