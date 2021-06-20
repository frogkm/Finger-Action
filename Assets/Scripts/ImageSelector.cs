using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ImageSelector : MonoBehaviour
{

    public Image image;
    public Button leftBtn;
    public Button rightBtn;

    public string prefKey;
    public string imgFolder;

    private UnityEngine.Object[] images;
    private int idx;
    private char[] sep = { '.' };

    // Start is called before the first frame update
    void Start()
    {
        images = Resources.LoadAll(imgFolder, typeof(Sprite));
        if (!PlayerPrefs.HasKey(prefKey)) {
            PlayerPrefs.SetString(prefKey, imgFolder + "/" + images[0].name);
        }
        Sprite currentBackground = Resources.Load<Sprite>(PlayerPrefs.GetString(prefKey));
        string initialFile = PlayerPrefs.GetString(prefKey);
        for (int i = 0; i < images.Length; i++) {
            if (imgFolder + "/" + images[i].name == initialFile) {
                idx = i;
                break;
            }
        }
        image.sprite = Resources.Load<Sprite>(imgFolder + "/" + images[idx].name);
        leftBtn.onClick.AddListener(delegate {
            cycleLeft();
        });
        rightBtn.onClick.AddListener(delegate {
            cycleRight();
        });
        
    }
    
    private void saveCurrentSetting() {
        image.sprite = Resources.Load<Sprite>(imgFolder + "/" + images[idx].name);
        PlayerPrefs.SetString(prefKey, imgFolder + "/" + images[idx].name);
        PlayerPrefs.Save();
    }

    private void cycleLeft() {
        idx --;
        if (idx < 0) {
            idx = images.Length - 1;
        }
        saveCurrentSetting();
    }

    private void cycleRight() {
        idx ++;
        if (idx > images.Length - 1) {
            idx = 0;
        }
        saveCurrentSetting();
    }
    
}
