// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.

namespace Eco.Mods.TechTree
{
    using System.ComponentModel;
    using Eco.Gameplay.Items;
    using Eco.World.Blocks;
    using Eco.Shared.Serialization;
    using Eco.Shared.Localization;
    using Eco.Core.Items;

    [Serialized, Weight(28000)]
    [LocDisplayName("Wet Tailings")]
    [MaxStackSize(200000)]
    [RequiresTool(typeof(ShovelItem))]
    [Tag("Excavatable", 1)]
    [Ecopedia("Blocks", "Byproducts", true, InPageTooltip.DynamicTooltip)]
    public class WetTailingsItem : BlockItem<WetTailingsBlock>
    {
        public override LocString DisplayNamePlural         { get { return Localizer.DoStr("Wet Tailings"); } }
        public override LocString DisplayDescription        { get { return Localizer.DoStr("Waste product from concentrating ore. The run-off creates ground pollution; killing nearby plants and seeping into the water supply."); } }
        public override bool CanStickToWalls                { get { return false; } }
    }
}
