using ButcheringFotSACompatability.Migration;
using Vintagestory.API.Common;
using Vintagestory.API.Server;

namespace ButcheringFotSACompatability
{
    public sealed class ButcheringFotSACompatabilityModSystem : ModSystem
    {
        public override bool ShouldLoad(EnumAppSide side)
        {
            return side == EnumAppSide.Server;
        }

        public override double ExecuteOrder()
        {
            // JSON patches run at 0.05; the item loader triggers remapping at 0.2 in VS 1.22.7.
            return 0.1;
        }

        public override void AssetsLoaded(ICoreAPI api)
        {
            ICoreServerAPI? serverApi = api as ICoreServerAPI;
            if (serverApi == null)
            {
                return;
            }

            CerviniMigration.Apply(serverApi);
        }
    }
}
