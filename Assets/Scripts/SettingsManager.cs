using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public Toggle fpsToggle;
    public Toggle scoreToggle;
    public Toggle blockFallToggle;
    public Toggle muteToggle;

    void Start() {
        configureToggleSetting(fpsToggle, "fpsOn", false);
        configureToggleSetting(scoreToggle, "scoreOn", true);
        configureToggleSetting(blockFallToggle, "blockFall", true);
        configureToggleSetting(muteToggle, "muteOn", false);
    }
    private void saveIntSetting(string key, int value) {
        PlayerPrefs.SetInt(key, value);
        PlayerPrefs.Save();
    }
    private void loadToggleSetting(Toggle toggle, string key) {
        toggle.isOn = intToBool(PlayerPrefs.GetInt(key));
    }

    private void configureToggleSetting(Toggle toggle, string key, bool defaultVal) {
        if (!PlayerPrefs.HasKey(key)) {
            saveIntSetting(key, boolToInt(defaultVal));
        }
        loadToggleSetting(toggle, key);
        toggle.onValueChanged.AddListener(delegate {
            saveIntSetting(key, boolToInt(toggle.isOn));     
        });
    }

    public static bool intToBool(int num) {
        if (num == 0) {
            return false;
        }
        return true;
    } 

    public static int boolToInt(bool val) {
        if (val) {
            return 1;
        }
        return 0;
    } 







}
