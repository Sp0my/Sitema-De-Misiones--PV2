using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTracker : MonoBehaviour
{
    public QuestSystem db;

    //Misiones comenzadas(incompletas/enproceso)
    public List<Quest> activeQuest = new List<Quest>();

    //Misiones terminadas(se completaron todos los requisistos)
    public List<Quest> finishedQuests = new List<Quest>();

    //Misiones terminadas(se cobro la recompensa)
    public List<Quest> CompletedQuests = new List<Quest>();

    //Lista que almacena los NPCs que nos dieron los quests
    [HideInInspector] public List<QuestGiver> rewarders = new List<QuestGiver>();


    //IDENTIFICADOR DE MISIONES AL MATAR ENEMIGOS
    public void EnemyDeath(int enemy_ID)
    {
        if (activeQuest.Count > 0)
        {
            for (int i = 0; i < activeQuest.Count; i++)
            {
                if (activeQuest[i].idEnemigo == enemy_ID)
                {
                    activeQuest[i].currentAmount++;
                    if (activeQuest[i].currentAmount < activeQuest[i].totalAmount)
                    {
                        print("Cantidad restante de enemigos: " + (activeQuest[i].totalAmount - activeQuest[i].currentAmount));
                    }
                    ActualizarQuest(activeQuest[i].id, activeQuest[i].type);
                    break;
                }
            }
        }
    }

    //IDENTIFICADOR DE MISIONES GENERAL
    public void ActualizarQuest(int quest_ID, Quest.QuestType type, int? canItems = null)
    {
        var val = activeQuest.Find(x => x.id == quest_ID);
        if (type == Quest.QuestType.Matar)
        {
            if(val.currentAmount >= val.totalAmount)
            {
                Debug.LogWarning("Quest: " + db.misions[val.id].name + " completada!");
                val.complete = true;
            }
            else
            {
                print("Aún no has terminado la Misión: " + db.misions[val.id].name);
            }
        }


        if(type == Quest.QuestType.Entrega)
        {
            if (val.destino.GetComponent<Destino_Script>().reached)
            {
                Debug.LogWarning("Quest: " + db.misions[val.id].name + " completada!");
                val.complete = true;
            }
            else
            {
                print("Aún no has llegado al objectivo.");
            }
        }

        if (type == Quest.QuestType.Recoleccion)
        {
            foreach (var item in val.itemsARecoger)
            {
                if (canItems != item.cantidad)
                {
                    Debug.LogWarning("Quest: " + db.misions[val.id].name + " completada");
                    val.complete = true;
                }
                else
                {
                    print("Aún no has recolectado todos los itemes, te faltan: " + (item.cantidad - canItems));
                }
            }
        }
    }


    public void VerifyItem( int item_ID)
    {
        Quest q = null;
        if (activeQuest.Count > 0)
        {
            if (activeQuest.Exists(x => x.itemsARecoger.Exists(a => a.itemID == item_ID)))
            {
                q = activeQuest.Find(x => x.itemsARecoger.Exists(a => a.itemID == item_ID));
            }
            else
            {
                q = null;
                return;
            }

            for (int i = 0; i > activeQuest.Count; i++)
            {
                if (q.itemsARecoger[0].itemID == item_ID && activeQuest[i].id == q.id)
                {
                    int cantidad = DiscriminacionDeItems(db.misions[activeQuest[i].id].Datos[0].itemID);
                    ActualizarQuest(activeQuest[i].id, activeQuest[i].type, cantidad);
                }
            }
        }
    }


    //APARTADO CONECTADO DIRECTAMENTE PARA UN SISTEMA DE INVENTARIO
    public int DiscriminacionDeItems(int id)
    {
        int itemsMatched = 0;

        foreach (var item in GetComponent<Jugador>().invLocal)
        {
            //if (item.GetComponent<ItemSuelto>().ID == id)
            //{
            //    itemsMatched++;
            //}
        }
        return itemsMatched;
    }
}
