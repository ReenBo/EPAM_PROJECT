using ET.Interface;
using ET.Structures;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET.Core
{
    public class LevelSystem : ILevelSystem
    {
        private int _currentLevel = 1;
        private float _currentExperience = 0f;

        private int _levelUp = 1;

        private float _amountOfExperienceToLevelUp = 100f;
        private readonly float _levelConversionModifier = 0.2f;

        private int _minExperience = 0;
        private int _maxExperience = 0;

        public LevelSystem(
            int CurrentLevel, float CurrentExperience, int MaxExperience, float AmountOfExperienceToLevelUp)
        {
            _currentLevel = CurrentLevel;
            _currentExperience = CurrentExperience;
            _maxExperience = MaxExperience;
            _amountOfExperienceToLevelUp = AmountOfExperienceToLevelUp;
        }

        public event Action<float, float, int, int> onExperiencePlayerChange;

        public int CurrentLevel { get => _currentLevel; set => _currentLevel = value; }
        public float CurrentExperience { get => _currentExperience; set => _currentExperience = value; }

        public void Init<S>(S structur) where S : struct
        {
        }

        public void Subscribe(IEnemy enemy)
        {
            enemy.OnExperienceEarned += CalculateExperiencePlayer;
        }

        private void CalculateExperiencePlayer(int experience)
        {
            _currentExperience += experience;

            _maxExperience = (int)_amountOfExperienceToLevelUp;

            if (_currentExperience >= _maxExperience)
            {
                _currentExperience = _minExperience;

                _amountOfExperienceToLevelUp += _amountOfExperienceToLevelUp * _levelConversionModifier;

                _currentLevel += _levelUp;
            }

            onExperiencePlayerChange.Invoke(experience, _currentExperience, _maxExperience, _currentLevel);
        }
    }
}
