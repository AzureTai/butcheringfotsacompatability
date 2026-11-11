using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Vintagestory.API.Common;
using Vintagestory.API.Server;

namespace ButcheringFotSACompatability.Legacy
{
    /// <summary>
    /// JSON patches can only check whether a pack is installed, not its version. The legacy items for
    /// codes a pack removed are switched on by patch whenever the pack is present, so on pack versions
    /// that still define those codes this switches the items and their conversion recipes back off.
    /// Runs after JSON patches (0.05) and before items (0.2) and recipes load.
    /// </summary>
    public static class LegacyItemGating
    {
        public static void Apply(ICoreServerAPI serverApi)
        {
            ArgumentNullException.ThrowIfNull(serverApi);
            foreach (LegacyItemRule rule in LegacyItemRule.All)
            {
                Mod? pack = serverApi.ModLoader.GetMod(rule.ModId);
                if (pack == null)
                {
                    continue;
                }

                string installed = pack.Info.Version;
                if (!PackVersion.TryParse(installed, out _))
                {
                    // Leave the patch result as it is rather than stop the server over an odd version string.
                    serverApi.Logger.Warning(
                        "[butcheringfotsacompatability] Could not read the {0} version '{1}'; legacy items left as patched.",
                        rule.ModId,
                        installed);
                    continue;
                }

                if (!LegacyAssetGate.PackStillDefinesCodes(installed, rule.RemovedInVersion))
                {
                    continue;
                }

                bool itemSwitchedOff = EditItemType(serverApi, rule);
                int recipesSwitchedOff = EditRecipes(serverApi, rule);
                serverApi.Logger.Notification(
                    "[butcheringfotsacompatability] {0} {1} still defines the codes removed in {2}: legacy items {3}, {4} conversion recipes switched off.",
                    rule.ModId,
                    installed,
                    rule.RemovedInVersion,
                    itemSwitchedOff ? "switched off" : "already off",
                    recipesSwitchedOff);
            }
        }

        private static bool EditItemType(ICoreServerAPI serverApi, LegacyItemRule rule)
        {
            IAsset asset = Require(serverApi, rule.ItemTypeAsset);
            JObject itemType = JObject.Parse(asset.ToText());
            bool changed = LegacyAssetGate.DisableItemType(itemType);
            asset.Data = Encoding.UTF8.GetBytes(itemType.ToString(Formatting.None));
            return changed;
        }

        private static int EditRecipes(ICoreServerAPI serverApi, LegacyItemRule rule)
        {
            IAsset asset = Require(serverApi, rule.RecipeAsset);
            JArray recipes = JArray.Parse(asset.ToText());
            int changed = LegacyAssetGate.DisableRecipes(recipes, rule.IngredientPrefixes);
            asset.Data = Encoding.UTF8.GetBytes(recipes.ToString(Formatting.None));
            return changed;
        }

        private static IAsset Require(ICoreServerAPI serverApi, string location)
        {
            IAsset? asset = serverApi.Assets.TryGet(new AssetLocation(location));
            if (asset == null)
            {
                throw new FileNotFoundException("The compatibility asset " + location + " is missing.");
            }

            return asset;
        }
    }
}
