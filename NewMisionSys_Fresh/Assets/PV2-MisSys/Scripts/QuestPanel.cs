using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class QuestPanel : MonoBehaviour
{
    [HideInInspector]
    public GameObject questMainPanel;
    public TextMeshProUGUI questName;
    public Text questDescripcion;
    public Button accept_Button;
    public Button deny_Button;

    void Awake()
    {
        questMainPanel = this.gameObject;
        questMainPanel.SetActive(false);
    }

    public void ActualizarPanel (string questName, string questDescription)
    {
        this.questName.text = questName;
        this.questDescripcion.text = questDescription;
        questMainPanel.SetActive(true);
    }
}
