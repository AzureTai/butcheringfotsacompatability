using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProtoBuf;
using Vintagestory.API.Common;
using Vintagestory.API.Server;

namespace ButcheringFotSACompatability.Migration
{
    public static class CerviniMigration
    {
        public static void Apply(ICoreServerAPI serverApi)
        {
            ArgumentNullException.ThrowIfNull(serverApi);

            IAsset? asset = serverApi.Assets.TryGet(new AssetLocation("butcheringfotsacompatability:config/remaps.json"));
            if (asset == null)
            {
                throw new FileNotFoundException("The compatibility mod's config/remaps.json asset is missing.");
            }

            JObject remaps = JObject.Parse(asset.ToText());
            if (remaps[CerviniRemapFilter.GroupCode] == null)
            {
                return;
            }

            Dictionary<int, string> storedItemCodes = new Dictionary<int, string>();
            byte[]? itemData = serverApi.WorldManager.SaveGame.GetData("ItemIDs");
            if (itemData != null)
            {
                using (MemoryStream stream = new MemoryStream(itemData, false))
                {
                    storedItemCodes = Serializer.Deserialize<Dictionary<int, string>>(stream);
                }

                if (storedItemCodes == null)
                {
                    throw new InvalidDataException("The saved ItemIDs table could not be decoded for Cervini migration.");
                }
            }

            if (CerviniRemapFilter.Filter(remaps, storedItemCodes))
            {
                // Change only the in-memory asset. The engine owns saved IDs and migration bookkeeping.
                asset.Data = Encoding.UTF8.GetBytes(remaps.ToString(Formatting.None));
            }
        }
    }
}
