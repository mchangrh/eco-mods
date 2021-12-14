// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.

namespace Eco.Mods.TechTree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Eco.Core.Items;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.Interactions;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Players;
    using Eco.Shared.Items;
    using Eco.Shared.Localization;
    using Eco.Shared.Math;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.World.Blocks;

    [Tag("Excavation")]
    public partial class ExcavatorItem : WorldObjectItem<ExcavatorObject> { }

    [Serialized]
    [RequireComponent(typeof(StandaloneAuthComponent))]
    [RequireComponent(typeof(MovableLinkComponent))]
    [RequireComponent(typeof(FuelSupplyComponent))]
    [RequireComponent(typeof(FuelConsumptionComponent))]
    [RequireComponent(typeof(AirPollutionComponent))]
    [RequireComponent(typeof(VehicleComponent))]
    [RequireComponent(typeof(VehicleToolComponent))]
    public class ExcavatorObject : PhysicsWorldObject
    {
        protected ExcavatorObject() { }
        public override LocString DisplayName                     { get { return Localizer.DoStr("Excavator"); } }

        static ExcavatorObject()
        {
            WorldObject.AddOccupancy<ExcavatorObject>(new List<BlockOccupancy>(0));
        }

        private static string[] fuelTagList = new string[]
        {
            "Liquid Fuel"
        };

        private Player Driver { get { return this.GetComponent<VehicleComponent>().Driver; } }

        public static Dictionary<Type, int> GetBlockStackSizeMap(int diggableSize, int minableSize)
        {
            var blockMap = new Dictionary<Type, int>();
            var blockItems = Item.AllItems.Where(x => x is BlockItem).Cast<BlockItem>();

            foreach (var item in blockItems.Where(x => x.OriginType.HasAttribute<Diggable>())) blockMap.Add(item.Type, diggableSize);
            foreach (var item in blockItems.Where(x => x.OriginType.HasAttribute<Minable>()))  blockMap.Add(item.Type, minableSize);

            return blockMap;
        }

        protected override void Initialize()
        {
            base.Initialize();

            this.GetComponent<FuelSupplyComponent>().Initialize(2, fuelTagList);
            this.GetComponent<FuelConsumptionComponent>().Initialize(70);
            this.GetComponent<AirPollutionComponent>().Initialize(0.7f);
            this.GetComponent<VehicleComponent>().Initialize(14, 1);

            var tool = this.GetComponent<VehicleToolComponent>();
            tool.Initialize(30, 2147483647, new DirtItem(), 100, 200, 0, VehicleUtilities.GetInventoryRestriction(this));
            tool.Inventory.AddInvRestriction(new PutOnlyRestriction());
            tool.HiddenFromUI = false;
        }

        /// <inheritdoc cref="IInteractionChecker.CanInteract"/>
        public override bool CanInteract(Vector3 objectPosition, Vector3 interactPosition, InteractionInfo info)
        {
            const float MaxDistanceWithinObject = 10f; // custom distance for crane, because of long arm
            return Vector2.WrappedDistance(objectPosition.XZ, interactPosition.XZ) <= MaxDistanceWithinObject;
        }
    }
}
