using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class Nick : MonoBehaviour
{
    public TextMeshProUGUI usernameInput;
    public TextMeshProUGUI username;
    public TextMeshProUGUI username2;
    public TextMeshProUGUI username3;
    public TextMeshProUGUI username4;

    void Start()
    {
        username.text = PlayerPrefs.GetString("nick");
        username2.text = PlayerPrefs.GetString("nick");
        username3.text = PlayerPrefs.GetString("nick");
        username4.text = PlayerPrefs.GetString("nick");
    }

    public void SaveUsername()
    {
        PlayerPrefs.SetString("nick", usernameInput.text);
        username.text = PlayerPrefs.GetString("nick");
        username2.text = PlayerPrefs.GetString("nick");
        username3.text = PlayerPrefs.GetString("nick");
        username4.text = PlayerPrefs.GetString("nick");
    }
}