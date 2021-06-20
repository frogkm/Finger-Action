using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        loadBackground();
    }

    public void loadBackground() {
        if (!PlayerPrefs.HasKey("background")) {
            PlayerPrefs.SetString("background", "Backgrounds/spaceBackground");
        }
        GetComponent<Image>().sprite = Resources.Load<Sprite>(PlayerPrefs.GetString("background"));
    }

}
