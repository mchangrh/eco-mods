// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.

namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Objects;
    using Eco.Shared.Math;

    [RequireComponent(typeof(PublicStorageComponent))]
    [RequireComponent(typeof(StockpileComponent))]
    [RequireComponent(typeof(WorldStockpileComponent))]
    public partial class LumberStockpileObject : WorldObject
    {
        public static readonly Vector3i DefaultDim = new Vector3i(8, 8, 8);

        protected override void OnCreate()
        {
            base.OnCreate();
            StockpileComponent.ClearPlacementArea(this.Creator, this.Position3i, DefaultDim, this.Rotation, 1);
        }

        protected override void PostInitialize()
        {
            base.PostInitialize();
            
            this.GetComponent<StockpileComponent>().Initialize(DefaultDim, 1);
            
            var storage = this.GetComponent<PublicStorageComponent>();
            storage.Initialize(DefaultDim.x * DefaultDim.z);
            storage.Storage.AddInvRestriction(new StockpileStackRestriction(DefaultDim.y * 10)); // limit stack sizes to the y-height of the LumberStockpile

            this.GetComponent<LinkComponent>().Initialize(30);
        }
    }
}