using Tool;
using System;
using UnityEngine;
using JetBrains.Annotations;
using Feature.AbilitySystem.Abilities;
using System.Collections.Generic;

namespace Feature.AbilitySystem
{
    internal interface IAbilitiesController
    { }

    internal class AbilitiesController : BaseController
    {
       

        private readonly IAbilitiesView _view;
        private readonly IAbilitiesRepository _repository;
        private readonly IAbilityActivator _abilityActivator;


        public AbilitiesController(
            [NotNull] IAbilitiesView view,
            [NotNull] IAbilitiesRepository repository,
            [NotNull] IEnumerable<IAbilityItem> itemConfigs,
            [NotNull] IAbilityActivator abilityActivator)
        {
            _view
                = view ?? throw new ArgumentException(nameof(view));
            
            _repository
                = repository ?? throw new ArgumentException(nameof(repository));

            _abilityActivator
                = abilityActivator ?? throw new ArgumentNullException(nameof(abilityActivator));

            _view.Display(itemConfigs, OnAbilityViewClicked);
        }




        private void OnAbilityViewClicked(string abilityId)
        {
            if (_repository.Items.TryGetValue(abilityId, out IAbility ability))
                ability.Apply(_abilityActivator);
        }
    }
}
