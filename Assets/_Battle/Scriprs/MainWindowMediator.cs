using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BattleScripts
{
    internal class MainWindowMediator : MonoBehaviour
    {
        [Header("Player Stats")]
        [SerializeField] private TMP_Text _countMoneyText;
        [SerializeField] private TMP_Text _countHealthText;
        [SerializeField] private TMP_Text _countPowerText;
        [SerializeField] private TMP_Text _countCrimeLevelText;

        [Header("Enemy Stats")]
        [SerializeField] private TMP_Text _countPowerEnemyText;

        [Header("Money Buttons")]
        [SerializeField] private Button _addMoneyButton;
        [SerializeField] private Button _minusMoneyButton;

        [Header("Health Buttons")]
        [SerializeField] private Button _addHealthButton;
        [SerializeField] private Button _minusHealthButton;

        [Header("Power Buttons")]
        [SerializeField] private Button _addPowerButton;
        [SerializeField] private Button _minusPowerButton;

        [Header("Crime Level Buttons")]
        [SerializeField] private Button _addCrimeLevelButton;
        [SerializeField] private Button _minusCrimeLevelButton;

        [Header("Other Buttons")]
        [SerializeField] private Button _fightButton;
        [SerializeField] private Button _passButton;


        private PlayerData _money;
        private PlayerData _heath;
        private PlayerData _power;
        private PlayerData _crimeLevel;

        private Enemy _enemy;


        private void Start()
        {
            _enemy = new Enemy("Enemy Flappy");

            _money = CreatePlayerData(_enemy, DataType.Money);
            _heath = CreatePlayerData(_enemy, DataType.Health);
            _power = CreatePlayerData(_enemy, DataType.Power);
            _crimeLevel = CreatePlayerData(_enemy, DataType.CrimeLevel);
            Subscribe();

        }

        private void Subscribe()
        {
            _addMoneyButton.onClick.AddListener(IncreaseMoney);
            _minusMoneyButton.onClick.AddListener(DecreaseMoney);

            _addHealthButton.onClick.AddListener(IncreaseHealth);
            _minusHealthButton.onClick.AddListener(DecreaseHealth);

            _addPowerButton.onClick.AddListener(IncreasePower);
            _minusPowerButton.onClick.AddListener(DecreasePower);

            _addCrimeLevelButton.onClick.AddListener(IncreaseCrimeLevel);
            _minusCrimeLevelButton.onClick.AddListener(DecreaseCrimeLevel);

            _fightButton.onClick.AddListener(Fight);

        }

        private void OnDestroy()
        {
            Unsubscibe();

            RemovePlayerData(ref _money);
            RemovePlayerData(ref _heath);
            RemovePlayerData(ref _power);
            RemovePlayerData(ref _crimeLevel);
        }

        private void Unsubscibe()
        {
            _addMoneyButton.onClick.RemoveListener(IncreaseMoney);
            _minusMoneyButton.onClick.RemoveListener(DecreaseMoney);

            _addHealthButton.onClick.RemoveListener(IncreaseHealth);
            _minusHealthButton.onClick.RemoveListener(DecreaseMoney);

            _addPowerButton.onClick.RemoveListener(IncreasePower);
            _minusPowerButton.onClick.RemoveListener(DecreasePower);

            _addCrimeLevelButton.onClick.RemoveListener(IncreaseCrimeLevel);
            _minusCrimeLevelButton.onClick.RemoveListener(DecreaseCrimeLevel);

            _fightButton.onClick.RemoveAllListeners();
        }

        private PlayerData CreatePlayerData(IEnemy enemy ,DataType dataType)
        {
            var playerData = new PlayerData(dataType);
            playerData.Attach(enemy);
            return playerData;
        }

        private void RemovePlayerData(ref PlayerData playerData)
        {
            playerData.Detach(_enemy);
            playerData = null;
        }



        private void AddMoney(int addition) => AddToValue(_money, addition);
        private void IncreaseMoney() => AddMoney(+1);
        private void DecreaseMoney() => AddMoney(-1);


        private void AddHealth(int addition) => AddToValue(_heath, addition);
        private void IncreaseHealth() => AddHealth(+1);
        private void DecreaseHealth() => AddHealth(-1);

        private void AddPower(int addition) => AddToValue(_power, addition);
        private void IncreasePower() => AddPower(+1);
        private void DecreasePower() => AddPower(-1);

        private void AddCrimeLevel(int addition) => AddToValue(_crimeLevel, addition);
        private void IncreaseCrimeLevel() => AddCrimeLevel(+1);
        private void DecreaseCrimeLevel() => AddCrimeLevel(-1);


        private void AddToValue(PlayerData playerData, int addition)
        {
            playerData.Value += addition;
            ChangeDataWindow(playerData);
        }

       
        private void Fight()
        {
            CheckCrimeLevel(5);
            bool isWin = _power.Value >= _enemy.CalcPower();
            string color = isWin ? "#07FF00" : "#FF0000";
            string message = isWin ? "Win" : "Lose";
            Debug.Log($"<color={color}>{message}!!!</color>");
        }

        private int CheckCrimeLevel(int crimeLevel)
        {
            _crimeLevel.Value = crimeLevel;
            if (3 < crimeLevel && crimeLevel < 5)
            {
                _passButton.GetComponent<Button>().enabled = false;
            }
            return crimeLevel;
        }

        private void ChangeDataWindow(PlayerData playerData)
        {
            int value = playerData.Value;
            DataType dataType = playerData.DataType;
            TMP_Text textComponent = GetTextComponent(dataType);
            textComponent.text = $"Player {dataType:F} {value}";
            int enemyPower = _enemy.CalcPower();
            _countPowerEnemyText.text = $"Enemy Power {enemyPower}";
        }

        private void SetData(int data, DataType dataType)
        {
            PlayerData playerData = GetPlayerData(dataType);
            playerData.Value = data;
        }

        private PlayerData GetPlayerData(DataType dataType) =>
            dataType switch
            {
                DataType.Money => _money,
                DataType.Health => _heath,
                DataType.Power => _power,
                DataType.CrimeLevel => _crimeLevel,
                _ => throw new ArgumentOutOfRangeException(nameof(dataType), dataType, null)
            };
        

        private TMP_Text GetTextComponent(DataType dataType) =>
            dataType switch
            {
                DataType.Money => _countMoneyText,
                DataType.Health => _countHealthText,
                DataType.Power => _countPowerText,
                DataType.CrimeLevel => _countCrimeLevelText,
                _ => throw new ArgumentOutOfRangeException(nameof(dataType), dataType, null)
            };
    }
}
