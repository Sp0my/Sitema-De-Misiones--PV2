using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestTrackerPanel : MonoBehaviour
{
    public QuestSystem dataB;
    public Jugador jug;

    public Text questDescriptionText;
    public Text infoRecompensaText;
    public Button questNameButton;
    public Transform butonContainer1;
    public Transform butonContainer2;

    private bool showQuestFinished = false;
    private List<Button> poolButtons = new List<Button>();

    public void ActualizarBotones()
    {
        List<Quest> questsT;

        if (!showQuestFinished)
        {
            questsT = jug.questTrack.activeQuest;
        }
        else
        {
            questsT = jug.questTrack.CompletedQuests;
        }

        if (poolButtons.Count >= questsT.Count)
        {
            for (int i = 0; i < poolButtons.Count; i++)
            {
                if (i < questsT.Count)
                {
                    int a = dataB.misions[questsT[i].id].id;
                    poolButtons[i].GetComponentInChildren<Text>().text = dataB.misions[questsT[i].id].name;
                    poolButtons[i].onClick.RemoveAllListeners();
                    poolButtons[i].onClick.AddListener(() => ActualizarDescripcionesConInfo(a));
                    poolButtons[i].GetComponentInChildren<Text>().color = Color.white;
                    Transform x;
                    x = showQuestFinished == false ? x = butonContainer1 : x = butonContainer2;
                    poolButtons[i].transform.SetParent(x);
                    poolButtons[i].gameObject.SetActive(true);
                }
                else
                {
                    poolButtons[i].gameObject.SetActive(true);
                }
            }
        }
        else
        {
            for (int i = poolButtons.Count; i < questsT.Count; i++)
            {
                Transform x;
                x = showQuestFinished == false ? x = butonContainer1 : x = butonContainer2;
                var nuevoBoton = Instantiate(questNameButton, x);
                int a = dataB.misions[questsT[i].id].id;
                nuevoBoton.GetComponentInChildren<Text>().text = dataB.misions[questsT[i].id].name;
                nuevoBoton.onClick.RemoveAllListeners();
                nuevoBoton.onClick.AddListener(() => { ActualizarDescripcionesConInfo(a); });
                poolButtons.Add(nuevoBoton);
            }

            ActualizarBotones();
        }
    }

    public void ActualizarDescripcionesConInfo(int id)
    {
        if ( id == -1)
        {
            infoRecompensaText.text = string.Empty;
            questDescriptionText.text = string.Empty;
            return;
        }

        questDescriptionText.text = dataB.misions[id].description;

        if (!jug.questTrack.CompletedQuests.Exists(x => x.id == id))
        {
            switch (dataB.misions[id].tipo)
            {
                case QuestSystem.Mision.QuestType.Recoleccion:

                    if (jug.questTrack.DiscriminacionDeItems(dataB.misions[id].Datos[0].itemID) < dataB.misions[id].Datos[0].cantidad)
                    {
                        infoRecompensaText.text = "Items recogidos:" + "\n" + jug.questTrack.DiscriminacionDeItems(dataB.misions[id].Datos[0].itemID) + "/" + dataB.misions[id].Datos[0].cantidad;
                    }
                    else
                    {
                        infoRecompensaText.text = "Has recogido todos los items! Ve con " + jug.questTrack.rewarders.Find(x => x.id_Mision == id).name + " para finalizar con la misión.";
                    }

                    break;
                case QuestSystem.Mision.QuestType.Matar:

                    if (jug.questTrack.activeQuest.Find(x => x.id == id).currentAmount < dataB.misions[id].cantidad)
                    {
                        infoRecompensaText.text = "Enemigos Eleminados:" + "\n" + jug.questTrack.activeQuest.Find(x => x.id == id).currentAmount + "/" + dataB.misions[id].cantidad;
                    }
                    else
                    {
                        infoRecompensaText.text = "Has recogido todos los items! Ve con " + jug.questTrack.rewarders.Find(x => x.id_Mision == id).name + " para finalizar con la misión.";
                    }

                    break;
                case QuestSystem.Mision.QuestType.Entrega:

                    if (jug.questTrack.activeQuest.Find(x => x.id == id).destino.GetComponent<Destino_Script>().reached)
                    {
                        infoRecompensaText.text = "Aún no has llegado!";
                    }
                    else
                    {
                        infoRecompensaText.text = "Completado! Ve con " + jug.questTrack.rewarders.Find(x => x.id_Mision == id).name + " para finalizar con la misión.";
                    }

                    break;
                default:
                    infoRecompensaText.text = "[INFO]";
                    break;
            }
        }
    }

    public void SwapMisiones()
    {
        ActualizarDescripcionesConInfo(-1);
        showQuestFinished = !showQuestFinished;
    }


}
