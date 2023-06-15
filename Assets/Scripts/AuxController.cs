using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AuxController : MonoBehaviour
{
    [SerializeField] IOController controller;
    [SerializeField] public Dictionary<string, int> deathDict;
    [SerializeField] private TextMeshProUGUI muertesTotales;

    // Start is called before the first frame update
    void Start()
    {
        deathDict = new Dictionary<string, int>();
        deathDict = controller.ReadDeaths();
        SetMuertesTotales();
    }

    private void SetMuertesTotales()
    {
        if (deathDict.TryGetValue("Total", out var totalDeaths))
        {
            muertesTotales.text += totalDeaths.ToString();
        }
    }
}
