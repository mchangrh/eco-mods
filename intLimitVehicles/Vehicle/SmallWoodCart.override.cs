﻿// Copyright (c) Strange Loop Games. All rights reserved.
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
    [LocDisplayName("Small Wood Cart")]
    [Weight(5000)]
    [Ecopedia("Crafted Objects", "Vehicles", createAsSubPage: true, display: InPageTooltip.DynamicTooltip)]
    public partial class SmallWoodCartItem : WorldObjectItem<SmallWoodCartObject>
    {
        public override LocString DisplayDescription { get { return Localizer.DoStr("Small wood cart for hauling minimal loads."); } }
    }


    [RequiresSkill(typeof(LoggingSkill), 1)]
    public partial class SmallWoodCartRecipe : RecipeFamily
    {
        public SmallWoodCartRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                "SmallWoodCart",  //noloc
                Localizer.DoStr("Small Wood Cart"),
                new List<IngredientElement>
                {
                    new IngredientElement("HewnLog", 10, typeof(LoggingSkill)), //noloc
                    new IngredientElement("WoodBoard", 15, typeof(LoggingSkill)), //noloc
                },
                new List<CraftingElement>
                {
                    new CraftingElement<SmallWoodCartItem>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 3;
            this.LaborInCalories = CreateLaborInCaloriesValue(50, typeof(LoggingSkill));
            this.CraftMinutes = CreateCraftTimeValue(typeof(SmallWoodCartRecipe), 2, typeof(LoggingSkill));
            this.ModsPreInitialize();
            this.Initialize(Localizer.DoStr("Small Wood Cart"), typeof(SmallWoodCartRecipe));
            this.ModsPostInitialize();
            CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
        }

        /// <summary>Hook for mods to customize RecipeFamily before initialization. You can change recipes, xp, labor, time here.</summary>
        partial void ModsPreInitialize();
        /// <summary>Hook for mods to customize RecipeFamily after initialization, but before registration. You can change skill requirements here.</summary>
        partial void ModsPostInitialize();
    }

    [Serialized]
    [RequireComponent(typeof(StandaloneAuthComponent))]
    [RequireComponent(typeof(PublicStorageComponent))]
    [RequireComponent(typeof(MovableLinkComponent))]
    [RequireComponent(typeof(VehicleComponent))]
    [RequireComponent(typeof(TailingsReportComponent))]
    public partial class SmallWoodCartObject : PhysicsWorldObject, IRepresentsItem
    {
        static SmallWoodCartObject()
        {
            WorldObject.AddOccupancy<SmallWoodCartObject>(new List<BlockOccupancy>(0));
        }

        public override LocString DisplayName { get { return Localizer.DoStr("Small Wood Cart"); } }
        public Type RepresentedItemType { get { return typeof(SmallWoodCartItem); } }


        private SmallWoodCartObject() { }

        protected override void Initialize()
        {
            base.Initialize();
            
            this.GetComponent<PublicStorageComponent>().Initialize(8, 2147483647);
            this.GetComponent<VehicleComponent>().Initialize(10, 1, 1);
            this.GetComponent<VehicleComponent>().HumanPowered(0.5f);
        }
    }
}
