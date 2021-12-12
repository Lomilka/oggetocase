using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Load : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textLoad;
    [SerializeField] private Slider _loadProgress;

    private void Start()
    {
        StartCoroutine(LoadPanel());
    }

    private IEnumerator LoadPanel()
    {
        while (true)
        {
            if (_loadProgress.value < 100)
            {
                _textLoad.text = "Загружено на " + _loadProgress.value.ToString() + "%";
                yield return new WaitForSeconds(Random.Range(0.01f, 0.1f));
                _loadProgress.value += 3;
            }
            else
            {
                StopAllCoroutines();
                yield return null;
            }
        }   
    }
}
