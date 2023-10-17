using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilitiesShop : MonoBehaviour
{
    [SerializeField] private List<Ability> _abilities;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private TMP_Text _count;
    [SerializeField] private Button _buyAbilityButton;
    [SerializeField] private Button _closeButton;

    private Ability _ability;

    public event Action<int> OnAbilityPurchasing;

    private void Start()
    {
        foreach (var ability in _abilities)
        {
            ability.OnAbilitiesEnded += (abl) => OpenShop(abl);
        }
        _buyAbilityButton.onClick.AddListener(Buy10);
        _closeButton.onClick.AddListener(() => Close());
        gameObject.SetActive(false);
    }

    private void Close()
    {
        this.gameObject.SetActive(false);
        Time.timeScale = GameController.Instance.TimeScale;
    }

    private void OpenShop(Ability ability)
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
        _ability = ability;
        _count.text = Account.Instance.GameCurrencyCount.ToString();
        _buyAbilityButton.gameObject.SetActive(true);
        _text.text = $"{ability.Name} are over. Buy 10 for {ability.PriceFor10}?";
    }

    private void Buy10()
    {
        if (Account.Instance.GameCurrencyCount < _ability.PriceFor10)
        {
            _text.text = $"Not enough money";
            _buyAbilityButton.gameObject.SetActive(false);
        }
        else
        {
            _ability.AddAbilities(10);
            Close();
            gameObject.SetActive(false);
            OnAbilityPurchasing?.Invoke(_ability.PriceFor10);
        }
    }
}
