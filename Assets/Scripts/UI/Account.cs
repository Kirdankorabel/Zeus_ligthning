using TMPro;
using UnityEngine;

public class Account : Singleton<Account>
{
    [SerializeField] private AbilitiesShop _abilitiesShop;
    [SerializeField] private TMP_Text _gameCurrencyCounttext;
    [SerializeField] private string _playerPrefName = "Balance";
    private int _gameCurrencyCount;

    public bool IsEmpty => _gameCurrencyCount == 0;
    public int GameCurrencyCount
    {
        get { return _gameCurrencyCount; }
        set
        { 
            _gameCurrencyCount = value;
            _gameCurrencyCounttext.text = value.ToString(); 
            PlayerPrefs.SetInt(_playerPrefName, _gameCurrencyCount);
        }
    }

    private void Awake()
    {
        GameCurrencyCount = PlayerPrefs.GetInt(_playerPrefName, 100);
    }

    private void Start()
    {
        _abilitiesShop.OnAbilityPurchasing += (value) => GameCurrencyCount -= value;
        Coin.OnWalletHitted += () => GameCurrencyCount++;
    }
}
