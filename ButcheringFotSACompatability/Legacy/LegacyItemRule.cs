using System;
using System.Collections.Generic;

namespace ButcheringFotSACompatability.Legacy
{
    /// <summary>
    /// A set of legacy items that re-registers codes an upstream pack removed in <see cref="RemovedInVersion"/>.
    /// Below that version the pack still defines those codes itself, so the legacy items and their
    /// conversion recipes must stay switched off.
    /// </summary>
    public sealed class LegacyItemRule
    {
        public LegacyItemRule(string modId, string removedInVersion, string itemTypeAsset, string recipeAsset, IReadOnlyList<string> ingredientPrefixes)
        {
            ModId = modId ?? throw new ArgumentNullException(nameof(modId));
            RemovedInVersion = removedInVersion ?? throw new ArgumentNullException(nameof(removedInVersion));
            ItemTypeAsset = itemTypeAsset ?? throw new ArgumentNullException(nameof(itemTypeAsset));
            RecipeAsset = recipeAsset ?? throw new ArgumentNullException(nameof(recipeAsset));
            IngredientPrefixes = ingredientPrefixes ?? throw new ArgumentNullException(nameof(ingredientPrefixes));
            if (ingredientPrefixes.Count == 0)
            {
                throw new ArgumentException("At least one ingredient prefix is required.", nameof(ingredientPrefixes));
            }
        }

        public string ModId { get; }

        public string RemovedInVersion { get; }

        public string ItemTypeAsset { get; }

        public string RecipeAsset { get; }

        /// <summary>Conversion recipes whose ingredient code starts with one of these are switched off with the items.</summary>
        public IReadOnlyList<string> IngredientPrefixes { get; }

        public static IReadOnlyList<LegacyItemRule> All { get; } = new[]
        {
            // Bovinae 1.0.0 replaced the 0.3.6 pelt-{baby|female|male}-{species} codes with pelt-raw-*.
            new LegacyItemRule(
                "bovinae",
                "1.0.0",
                "bovinae:itemtypes/resource/compat-legacy-raw-pelt.json",
                "butcheringfotsacompatability:recipes/grid/bovinae-pelt-conversion.json",
                new[] { "bovinae:pelt-baby-", "bovinae:pelt-female-", "bovinae:pelt-male-" }),

            // Spheniscidae 1.0.20 removed the baby gender: 80 chick skins in raw, salted, oiled and finished states.
            new LegacyItemRule(
                "spheniscidae",
                "1.0.20",
                "spheniscidae:itemtypes/resource/compat-legacy-baby-pelt.json",
                "butcheringfotsacompatability:recipes/grid/spheniscidae-pelt-conversion.json",
                new[] { "spheniscidae:pelt-raw-baby-", "spheniscidae:pelt-salted-baby-", "spheniscidae:pelt-oiled-baby-", "spheniscidae:pelt-pelt-baby-" }),
        };
    }
}
