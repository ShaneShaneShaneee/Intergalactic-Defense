using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RoundsSurvived : MonoBehaviour
{
    [SerializeField] Text wavesText;

    void OnEnable()
    {
        StartCoroutine(AnimateText());
    }

    IEnumerator AnimateText()
    {
        wavesText.text = "0";
        int round = 0;

        yield return new WaitForSeconds(.7f);

        while(round < PlayerStats.waves)
        {
            round++;
            wavesText.text = round.ToString();

            yield return new WaitForSeconds(.1f);
        }
    }
}
