using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager_Script : MonoBehaviour
{
    public SkinChange skins;
    public ShopHats prefs;
    public Volume volumes;
    public Nick nickSet;

    [SerializeField] int skin;
    [SerializeField] int hat;
    [SerializeField] int face;
    [SerializeField] float volume;
    [SerializeField] string nick;


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

    }
    public void SetPrefs()
    {
        skin = skins.skinId;
        hat = prefs.equippedHat;
        face = prefs.equippedFace;
        volume = volumes.volumeSlider.value;
        nick = nickSet.usernameInput.text;

        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.SetInt("skin", skin);
        PlayerPrefs.SetInt("hat", hat);
        PlayerPrefs.SetInt("face", face);
        PlayerPrefs.SetString("nick", nick);
    }

}