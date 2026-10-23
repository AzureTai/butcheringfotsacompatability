using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;

namespace ButcheringFotSACompatability.Migration
{
    public static class CerviniRemapFilter
    {
        public const string GroupCode = "butcheringfotsacompatability:v1.0.9-cervini";

        public static bool Filter(JObject remaps, Dictionary<int, string> storedItemCodes)
        {
            ArgumentNullException.ThrowIfNull(remaps);
            ArgumentNullException.ThrowIfNull(storedItemCodes);
            JToken? groupToken = remaps[GroupCode];
            if (groupToken == null)
            {
                return false;
            }

            JArray? commands = groupToken as JArray;
            if (commands == null)
            {
                throw new InvalidDataException("The Cervini remapping group must be an array.");
            }

            HashSet<string> savedCodes = new HashSet<string>(StringComparer.Ordinal);
            foreach (KeyValuePair<int, string> entry in storedItemCodes)
            {
                if (string.IsNullOrWhiteSpace(entry.Value))
                {
                    throw new InvalidDataException("The saved ItemIDs table contains an empty item code.");
                }

                if (entry.Value.StartsWith("butcheringfotsacompatability:deadcervini-", StringComparison.Ordinal))
                {
                    savedCodes.Add(entry.Value);
                }
            }

            JArray applicableCommands = new JArray();
            foreach (JToken commandToken in commands)
            {
                if (commandToken.Type != JTokenType.String)
                {
                    throw new InvalidDataException("A Cervini remapping command is not a string.");
                }

                string? command = commandToken.Value<string>();
                if (string.IsNullOrWhiteSpace(command))
                {
                    throw new InvalidDataException("A Cervini remapping command is empty.");
                }

                string[] parts = command.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != 5 || parts[0] != "/iir" || parts[1] != "remapq" || parts[4] != "force")
                {
                    throw new InvalidDataException("An unsupported command was found in the Cervini remapping group.");
                }

                // The engine's command order is NEW code followed by OLD code.
                if (!parts[3].StartsWith("butcheringfotsacompatability:deadcervini-", StringComparison.Ordinal)
                    || parts[2] != "butchering:deadcervidae-" + parts[3].Substring("butcheringfotsacompatability:deadcervini-".Length))
                {
                    throw new InvalidDataException("A Cervini remapping command contains an unexpected source or destination.");
                }

                if (savedCodes.Contains(parts[3]))
                {
                    applicableCommands.Add(command);
                }
            }

            if (applicableCommands.Count == commands.Count && commands.Count != 0)
            {
                return false;
            }

            if (applicableCommands.Count == 0)
            {
                remaps.Remove(GroupCode);
            }
            else
            {
                remaps[GroupCode] = applicableCommands;
            }

            return true;
        }
    }
}
