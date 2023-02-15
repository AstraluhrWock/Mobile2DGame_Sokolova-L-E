using System;
using System.Linq;
using Feature.Inventory.Items;
using Feature.Shed.Upgrade;
using Feature.AbilitySystem.Abilities;

namespace Tool
{
    internal static class ContentDataSourceLoader 
    {
        public static ItemConfig[] LoadItemConfigs(ResourcePath resourcePath)
        {
            var dataSource = ResourcesLoader.LoadObject<ItemConfigDataSource>(resourcePath);
            return dataSource == null ? Array.Empty<ItemConfig>() : dataSource.ItemConfig.ToArray();
        }

        public static UpgradeItemConfig[] LoadUpgradeItemConfigs(ResourcePath resourcePath)
        {
            var dataSource = ResourcesLoader.LoadObject<UpgradeItemConfigDataSource>(resourcePath);
            return dataSource == null ? Array.Empty<UpgradeItemConfig>() : dataSource.ItemConfigs.ToArray();
        }

        public static AbilityItemConfig[] LoadAbilityItemConfigs(ResourcePath resourcePath)
        {
            var dataSource = ResourcesLoader.LoadObject<AbilityItemConfigDataSource>(resourcePath);
            return dataSource == null ? Array.Empty<AbilityItemConfig>() : dataSource.AbilityConfigs.ToArray();
        }
    }
}
