// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.
// <auto-generated from VehicleTemplate.cs />

namespace Eco.Mods.TechTree
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Eco.Core.Items;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.Components.VehicleModules;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Math;
    using Eco.Shared.Networking;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    
    [Serialized]
    [LocDisplayName("Powered Cart")]
    [Weight(15000)]
    [AirPollution(0.1f)]
    [Ecopedia("Crafted Objects", "Vehicles", createAsSubPage: true, display: InPageTooltip.DynamicTooltip)]
    public partial class PoweredCartItem : WorldObjectItem<PoweredCartObject>
    {
        public override LocString DisplayDescription { get { return Localizer.DoStr("Large cart for hauling sizable loads."); } }
    }


    [RequiresSkill(typeof(BasicEngineeringSkill), 5)]
    public partial class PoweredCartRecipe : RecipeFamily
    {
        public PoweredCartRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                "PoweredCart",  //noloc
                Localizer.DoStr("Powered Cart"),
                new List<IngredientElement>
                {
                    new IngredientElement("WoodBoard", 30, typeof(BasicEngineeringSkill)), //noloc
                    new IngredientElement("Fabric", 20, typeof(BasicEngineeringSkill)), //noloc
                    new IngredientElement(typeof(CastIronStoveItem), 1, true),
                    new IngredientElement(typeof(IronWheelItem), 3, true),
                },
                new List<CraftingElement>
                {
                    new CraftingElement<PoweredCartItem>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 20;
            this.LaborInCalories = CreateLaborInCaloriesValue(200, typeof(BasicEngineeringSkill));
            this.CraftMinutes = CreateCraftTimeValue(typeof(PoweredCartRecipe), 10, typeof(BasicEngineeringSkill));
            this.ModsPreInitialize();
            this.Initialize(Localizer.DoStr("Powered Cart"), typeof(PoweredCartRecipe));
            this.ModsPostInitialize();
            CraftingComponent.AddRecipe(typeof(WainwrightTableObject), this);
        }

        /// <summary>Hook for mods to customize RecipeFamily before initialization. You can change recipes, xp, labor, time here.</summary>
        partial void ModsPreInitialize();
        /// <summary>Hook for mods to customize RecipeFamily after initialization, but before registration. You can change skill requirements here.</summary>
        partial void ModsPostInitialize();
    }

    [Serialized]
    [RequireComponent(typeof(StandaloneAuthComponent))]
    [RequireComponent(typeof(FuelSupplyComponent))]
    [RequireComponent(typeof(FuelConsumptionComponent))]
    [RequireComponent(typeof(PublicStorageComponent))]
    [RequireComponent(typeof(MovableLinkComponent))]
    [RequireComponent(typeof(AirPollutionComponent))]
    [RequireComponent(typeof(VehicleComponent))]
    [RequireComponent(typeof(TailingsReportComponent))]
    public partial class PoweredCartObject : PhysicsWorldObject, IRepresentsItem
    {
        static PoweredCartObject()
        {
            WorldObject.AddOccupancy<PoweredCartObject>(new List<BlockOccupancy>(0));
        }

        public override LocString DisplayName { get { return Localizer.DoStr("Powered Cart"); } }
        public Type RepresentedItemType { get { return typeof(PoweredCartItem); } }

        private static string[] fuelTagList = new string[]
        {
            "Burnable Fuel",
        };

        private PoweredCartObject() { }

        protected override void Initialize()
        {
            base.Initialize();
            
            this.GetComponent<PublicStorageComponent>().Initialize(18, 2147483647);
            this.GetComponent<FuelSupplyComponent>().Initialize(2, fuelTagList);
            this.GetComponent<FuelConsumptionComponent>().Initialize(35);
            this.GetComponent<AirPollutionComponent>().Initialize(0.1f);
            this.GetComponent<VehicleComponent>().Initialize(12, 1.5f, 1);
        }
    }
}
