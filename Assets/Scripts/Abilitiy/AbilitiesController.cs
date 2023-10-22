using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitiesController : MonoBehaviour
{
    [SerializeField] private GameController _gameController;
    [SerializeField] private List<Ability> _abilities;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Column[] _columns;
    [SerializeField] private Image _abilityImage;

    private Ability _ability;
    private bool _dragged;

    public bool IsDragged => _dragged;

    private void Start()
    {
        foreach(var ability in _abilities)
        {
            ability.OnAbilitySelected += (ability) => SelectAbility(ability);
        }
        foreach (var column in _columns)
        {
            column.OnPointerEnterEvent += (column) => UseAbility(column);
        }
    }

    private void SelectAbility(Ability ability)
    {
        if (_gameController.IsDragged || IsDragged)
            return;
        _ability = ability;
        _abilityImage.sprite = ability.Sprite;
        StartCoroutine(DraggingCorutine());
    }

    private void UseAbility(Column column)
    {
        if (_ability == null)
            return;
        _ability.Use(column);
        _ability.Use(_columns);
        _abilityImage.gameObject.SetActive(false);
        _dragged = false;
        _ability = null;
    }

    private IEnumerator DraggingCorutine()
    {
        _dragged = true;
        _abilityImage.gameObject.SetActive(true);

        while (Input.GetMouseButton(0))
        {
            _abilityImage.transform.localPosition = (Input.mousePosition - new Vector3(Screen.width, Screen.height) / 2f) / _canvas.scaleFactor;
            yield return null;
        }
        _abilityImage.gameObject.SetActive(false);
        _dragged = false;
        _ability = null;
        yield return null;
    }
}
