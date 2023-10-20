using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private AbilitiesShop _abilitiesShop;
    [SerializeField] private List<Button> _buttons;
    [SerializeField] private List<Image> _images;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _openButton;

    public event System.Action<Ability> OnAbilityBought;
    void Start()
    {
        InitializeButtons();
        _openButton.onClick.AddListener(() => OpenShop());
        _closeButton.onClick.AddListener(() => CloseShop());
        gameObject.SetActive(false);
    }

    private void OpenShop()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    private void CloseShop()
    {
        gameObject.SetActive(false);
        Time.timeScale = GameController.Instance.TimeScale;
    }

    private void InitializeButtons()
    {
        for (var i = 0; i < _buttons.Count; i++)
        {
            _images[i].sprite = _abilitiesShop.Abilities[i].Sprite;
            var abil = _abilitiesShop.Abilities[i];
            _buttons[i].onClick.AddListener(() => OnAbilityBought?.Invoke(abil));
        }
    }
}
