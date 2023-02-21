using UnityEngine;
using Game;
using Feature.AbilitySystem.Abilities;
using Tool;
using JetBrains.Annotations;
using System;

namespace Feature.AbilitySystem
{
    internal class AbilitiesContext : BaseContext
    {
        private readonly ResourcePath _viewPath = new("Prefabs/Ability/AbilitiesView");
        private readonly ResourcePath _dataSourcePath = new("Configs/Ability/AbilityItemConfigDataSource");

        public AbilitiesContext([NotNull] Transform placeForUI, [NotNull] IAbilityActivator abilityActivator)
        {
            if (placeForUI == null) throw new ArgumentNullException(nameof(placeForUI));
            if (abilityActivator == null) throw new ArgumentNullException(nameof(abilityActivator));

            CreateController(placeForUI, abilityActivator);
        }

        private AbilitiesController CreateController(Transform placeForUI, IAbilityActivator abilityActivator)
        {
            IAbilitiesView view = LoadView(placeForUI);
            AbilityItemConfig[] itemConfigs = LoadAbilityItemConfigs();
            IAbilitiesRepository repository = CreateRepository(itemConfigs);

            var abilitiesController = new AbilitiesController(view, repository, itemConfigs, abilityActivator);
            AddController(abilitiesController);
            return abilitiesController;
        }

        private AbilityItemConfig[] LoadAbilityItemConfigs() =>
            ContentDataSourceLoader.LoadAbilityItemConfigs(_dataSourcePath);

        private IAbilitiesRepository CreateRepository(AbilityItemConfig[] abilityItemConfigs)
        {
            var repository = new AbilitiesRepository(abilityItemConfigs);
            AddRepositories(repository);

            return repository;
        }

        private AbilitiesView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_viewPath);
            GameObject objectView = UnityEngine.Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<AbilitiesView>();
        }
    }

}

