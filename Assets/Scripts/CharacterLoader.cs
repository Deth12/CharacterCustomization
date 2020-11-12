using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterLoader : MonoBehaviour
{
    private CustomizationSet _maleCustomization;
    private CustomizationSet _femaleCustomization;

    [SerializeField] private Transform _spawnPoint = default;
    
    [Header("Buttons")]
    // Gender Select
    [SerializeField] private Button _loadMaleButton = default;
    [SerializeField] private Button _loadFemaleButton = default; 
    // Customization
    [SerializeField] private Button _nextLegsButton = default;
    [SerializeField] private Button _previousLegsButton = default;
    
    [SerializeField] private Button _nextTorsoButton = default;
    [SerializeField] private Button _previousTorsoButton = default;

    [Header("Panels")]
    [SerializeField] private Transform _genderSelectPanel = default;
    [SerializeField] private Transform _customizationPanel = default;
    
    private GameObject _currentCharacter;
    private CharacterLoadout _currentCharacterLoadout;
    
    [SerializeField] private List<Texture2D> _currentLegTextures;
    private int _legsIndex = 0;
    
    [SerializeField] private List<Texture2D> _currentTorsoTextures;
    private int _torsoIndex = 0;
    
    private void Start()
    {
        ToggleGenderSelect(true);
        
        if (LoadCustomizationSets())
        {
            _loadMaleButton.onClick.AddListener(()=>{ SpawnCharacter(_maleCustomization);});
            _loadFemaleButton.onClick.AddListener(() => { SpawnCharacter(_femaleCustomization);});
        }
        else
        {
            throw new Exception("Customization sets loading failed");
        }
    }

    private bool LoadCustomizationSets()
    {
        _maleCustomization = Resources.Load<CustomizationSet>($"Customization Sets/Male");
        _femaleCustomization = Resources.Load<CustomizationSet>($"Customization Sets/Female");
        return _maleCustomization != null && _femaleCustomization != null;
    }

    private void SpawnCharacter(CustomizationSet set)
    {
        if(_currentCharacter != null)
            Destroy(_currentCharacter);
        
        var prefab = Resources.Load<GameObject>($"Characters/{set.CharacterPrefabName}");
        _currentCharacter = Instantiate(prefab, _spawnPoint.position, _spawnPoint.rotation);
        _currentCharacterLoadout = _currentCharacter.GetComponent<CharacterLoadout>();
        
        EnableCharacterCustomization(set);
    }

    private void EnableCharacterCustomization(CustomizationSet set)
    {
        ToggleGenderSelect(false);

        _currentLegTextures = set.LoadLegTextures();
        
        _previousLegsButton.onClick.AddListener(PreviousLegsTexture);
        _nextLegsButton.onClick.AddListener(NextLegsTexture);

        _previousTorsoButton.onClick.AddListener(PreviousTorsoTexture);
        _nextTorsoButton.onClick.AddListener(NextTorsoTexture);
    }

    private void NextLegsTexture()
    {
        if (_currentLegTextures.Count != 0)
        {
            _currentCharacterLoadout.SetLegsTexture(_currentLegTextures[_legsIndex]);
            _legsIndex = _legsIndex != _currentLegTextures.Count - 1 ? _legsIndex + 1 : 0;
        }
    }
    
    private void PreviousLegsTexture()
    {
        if (_currentLegTextures.Count != 0)
        {
            _currentCharacterLoadout.SetLegsTexture(_currentLegTextures[_legsIndex]);
            _legsIndex = _legsIndex != 0 ? _legsIndex - 1 : 0;
        }
    }
    
    private void NextTorsoTexture()
    {
        if (_currentLegTextures.Count != 0)
        {
            _currentCharacterLoadout.SetTorsoTexture(_currentLegTextures[_torsoIndex]);
            _torsoIndex = _torsoIndex != _currentLegTextures.Count - 1 ? _torsoIndex + 1 : 0;
        }
    }
    
    private void PreviousTorsoTexture()
    {
        if (_currentTorsoTextures.Count != 0)
        {
            _currentCharacterLoadout.SetTorsoTexture(_currentTorsoTextures[_torsoIndex]);
            _torsoIndex = _torsoIndex != 0 ? _torsoIndex - 1 : 0;
        }
    }
    
    private void ToggleGenderSelect(bool enabled)
    {
        _genderSelectPanel.gameObject.SetActive(enabled);
        _customizationPanel.gameObject.SetActive(!enabled);
    }
}
