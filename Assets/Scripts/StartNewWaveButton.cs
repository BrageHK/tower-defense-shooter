using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartNewWaveButton : MonoBehaviour
{
    public Button nextWaveButton;
    public LevelController level;

    private void Start()
    {
        Button button = nextWaveButton.GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);
    }

    private void TaskOnClick()
    {
        level.StartNextWave();
        Debug.Log("Clicked Button!!");
    }
}
