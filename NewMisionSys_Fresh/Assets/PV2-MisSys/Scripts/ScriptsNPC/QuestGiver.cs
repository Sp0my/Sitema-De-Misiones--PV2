using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public QuestSystem dataB;
    public int id_Mision;
    public QuestPanel questPanel;

    [Space]
    public bool isStarted = false;
    public Quest quest;
    public bool rewarded = false;
    public bool isRewardGiver;
    //QUIEN ENTREGA LA RECOMPENSA
    public QuestGiver questRewarder;
    //EL NPC QUE DIRIGIÓ AL JUGADOR HASTA ESTE NPC
    public QuestGiver prevQuestGiver;

    public Jugador jugador;

    private void Start()
    {
        for (int i = 0; i < dataB.misions.Length; i++)
        {
            if (dataB.misions[i].id == this.id_Mision)
            {
                quest.name = dataB.misions[i].name;
                quest.id = dataB.misions[i].id;
                quest.idEnemigo = dataB.misions[i].enemyID;
                quest.totalAmount = dataB.misions[i].cantidad;
                quest.retainsItems = dataB.misions[i].keepsItems;
                quest.type = (Quest.QuestType)dataB.misions[i].tipo;
                if (dataB.misions[i].Datos != null)
                {
                    if (dataB.misions[i].Datos.Count > 0)
                    {
                        quest.itemsARecoger.Add(dataB.misions[i].Datos[0]);
                        if (quest.itemsARecoger.Count > 1)
                        {
                            quest.itemsARecoger.RemoveAt(i);
                        }
                    }
                }
                if (quest.destino != null)
                {
                    quest.destino.GetComponent<Destino_Script>().id_Quest = quest.id;
                    quest.destino.SetActive(false);
                }
            }
        }
        if (isRewardGiver)
        {
           questRewarder  = this;
        }
        else
        {
            questRewarder.dataB = this.dataB;
            questRewarder.id_Mision = this.id_Mision;
            questRewarder.isStarted = this.isStarted;
            questRewarder.rewarded = this.rewarded;
            questRewarder.quest = this.quest;
            questRewarder.prevQuestGiver = this.prevQuestGiver;
            questRewarder.quest.destino = this.quest.destino;
            questRewarder.questPanel = this.questPanel;
            if (quest.destino != null)
            {
                quest.destino.GetComponent<Destino_Script>().id_Quest = quest.id;
            }
            if (questRewarder.questRewarder == questRewarder)
            {
                questRewarder.isRewardGiver = true;
            }
            else
            {
                questRewarder.isRewardGiver = false;
            }
        }
    }

    public void ContactoConJugador(Jugador jug)
    {
        jugador = jug;

        if (!rewarded)
        {
            if (!isStarted)
            {
                if (prevQuestGiver == null)
                {
                    questPanel.accept_Button.gameObject.SetActive(true);
                    questPanel.deny_Button.gameObject.SetActive(true);

                    questPanel.ActualizarPanel(dataB.misions[id_Mision].name, dataB.misions[id_Mision].description);

                    questPanel.accept_Button.onClick.RemoveAllListeners();
                    questPanel.accept_Button.onClick.AddListener(AceptarQuest);
                    questPanel.accept_Button.onClick.AddListener(jug.questTrackPanel.ActualizarBotones);
                    questPanel.deny_Button.onClick.RemoveAllListeners();
                    questPanel.deny_Button.onClick.AddListener(delegate () { questPanel.gameObject.SetActive(false); });
                }
                else
                {
                    questPanel.accept_Button.gameObject.SetActive(false);
                    questPanel.deny_Button.gameObject.SetActive(false);
                    questPanel.ActualizarPanel("", "No tengo ninguna misión para ti, amigo!");
                }
            }
            else
            {
                questRewarder.isStarted = true;

                if (jug.questTrack.activeQuest.Exists(x => x.id == id_Mision)) 
                {
                    if(jug.questTrack.activeQuest.Find(x => x.id == id_Mision).complete)
                    {
                        if (isRewardGiver)
                        {
                            jug.questTrack.rewarders.Remove(this);
                            jug.Recompensa(quest);
                            rewarded = true;

                            var questTerminada = jug.questTrack.activeQuest.Find(x => x.id == id_Mision);
                            jug.questTrack.CompletedQuests.Add(questTerminada);
                            jug.questTrack.activeQuest.Remove(questTerminada);

                            if (quest.destino != null)
                            {
                                Destroy(quest.destino);
                                quest.destino = null;
                            }

                            if (prevQuestGiver != null)
                            {
                                prevQuestGiver.rewarded = true;
                            }
                        }
                        else
                        {
                            Debug.LogWarning("Para finalizar debes ver a " + questRewarder.name);
                            questPanel.accept_Button.gameObject.SetActive(false);
                            questPanel.deny_Button.gameObject.SetActive(false);
                            questPanel.ActualizarPanel(dataB.misions[id_Mision].name, "Para finalizar debes ver a " + questRewarder.name);
                        }
                    }
                    else
                    {
                        questPanel.accept_Button.gameObject.SetActive(false);
                        questPanel.deny_Button.gameObject.SetActive(false);

                        if (dataB.misions[id_Mision].tipo == QuestSystem.Mision.QuestType.Recoleccion)
                        {
                            jug.questTrack.ActualizarQuest(id_Mision, (Quest.QuestType)dataB.misions[id_Mision].tipo, jug.questTrack.DiscriminacionDeItems(dataB.misions[id_Mision].Datos[0].itemID));
                            questPanel.ActualizarPanel(dataB.misions[id_Mision].name, "Aún no has recolectado todos los items. Te faltan: "
                                + (jug.questTrack.DiscriminacionDeItems(dataB.misions[id_Mision].Datos[0].itemID) - dataB.misions[id_Mision].Datos[0].cantidad));
                        }
                        else
                        {
                            jug.questTrack.ActualizarQuest(id_Mision, (Quest.QuestType)dataB.misions[id_Mision].tipo);
                            questPanel.ActualizarPanel(dataB.misions[id_Mision].name, "Aún no has completado el objetivo.");
                        }
                    }
                }


            }
        }
    }

    public void AceptarQuest()
    {
        questPanel.gameObject.SetActive(false);
        Debug.LogWarning("Mision " + dataB.misions[id_Mision].name + " indicada!");
        jugador.questTrack.activeQuest.Add(new Quest { id = id_Mision, totalAmount = quest.totalAmount, idEnemigo = quest.idEnemigo, itemsARecoger = quest.itemsARecoger, type = quest.type, destino = quest.destino});

        //VERIFICAMOS ESTO PORQUE PUEDE PASAR QUE EL JUGADOR YA TENGA EL OBJECTO EN EL INVENTARIO
        if (dataB.misions[id_Mision].tipo == QuestSystem.Mision.QuestType.Recoleccion)
        {
            jugador.questTrack.ActualizarQuest(id_Mision, (Quest.QuestType)dataB.misions[id_Mision].tipo, jugador.questTrack.DiscriminacionDeItems(dataB.misions[id_Mision].Datos[0].itemID));
        }
        else
        {
            jugador.questTrack.ActualizarQuest(id_Mision, (Quest.QuestType)dataB.misions[id_Mision].tipo);
        }

        questRewarder.isStarted = true;
        this.isStarted = true;

        if (quest.destino != null)
        {
            quest.destino.SetActive(true);
        }

        if (jugador.questTrack.activeQuest.Exists(x => x.id == id_Mision))
        {
            if(jugador.questTrack.activeQuest.Find(x => x.id == id_Mision).complete)
            {
                ContactoConJugador(jugador);
            }
        }

        jugador.questTrack.rewarders.Add(questRewarder);
    }
}
