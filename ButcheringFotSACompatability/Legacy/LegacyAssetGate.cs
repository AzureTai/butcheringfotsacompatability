using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;

namespace ButcheringFotSACompatability.Legacy
{
    /// <summary>Pure JSON edits used by <see cref="LegacyItemGating"/>; kept free of game types so they can be tested.</summary>
    public static class LegacyAssetGate
    {
        /// <summary>True when the installed pack version is older than the version that removed the legacy codes.</summary>
        public static bool PackStillDefinesCodes(string installedVersion, string removedInVersion)
        {
            if (!PackVersion.TryParse(installedVersion, out int[] installed))
            {
                throw new InvalidDataException("Unreadable pack version '" + installedVersion + "'.");
            }

            if (!PackVersion.TryParse(removedInVersion, out int[] removed))
            {
                throw new InvalidDataException("Unreadable rule version '" + removedInVersion + "'.");
            }

            return PackVersion.Compare(installed, removed) < 0;
        }

        /// <summary>Sets "enabled": false on an item type. Returns true if it was not already off.</summary>
        public static bool DisableItemType(JObject itemType)
        {
            ArgumentNullException.ThrowIfNull(itemType);
            JToken? enabled = itemType["enabled"];
            bool wasOn = enabled == null || enabled.Type != JTokenType.Boolean || enabled.Value<bool>();
            itemType["enabled"] = false;
            return wasOn;
        }

        /// <summary>Sets "enabled": false on every recipe whose ingredient code starts with one of the prefixes. Returns how many were on.</summary>
        public static int DisableRecipes(JArray recipes, IReadOnlyList<string> ingredientPrefixes)
        {
            ArgumentNullException.ThrowIfNull(recipes);
            ArgumentNullException.ThrowIfNull(ingredientPrefixes);
            int switchedOff = 0;
            foreach (JToken token in recipes)
            {
                JObject? recipe = token as JObject;
                if (recipe == null || !UsesIngredient(recipe, ingredientPrefixes))
                {
                    continue;
                }

                JToken? enabled = recipe["enabled"];
                if (enabled == null || enabled.Type != JTokenType.Boolean || enabled.Value<bool>())
                {
                    switchedOff++;
                }

                recipe["enabled"] = false;
            }

            return switchedOff;
        }

        private static bool UsesIngredient(JObject recipe, IReadOnlyList<string> ingredientPrefixes)
        {
            JObject? ingredients = recipe["ingredients"] as JObject;
            if (ingredients == null)
            {
                return false;
            }

            foreach (KeyValuePair<string, JToken?> ingredient in ingredients)
            {
                string? code = ingredient.Value?["code"]?.Value<string>();
                if (code == null)
                {
                    continue;
                }

                foreach (string prefix in ingredientPrefixes)
                {
                    if (code.StartsWith(prefix, StringComparison.Ordinal))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
