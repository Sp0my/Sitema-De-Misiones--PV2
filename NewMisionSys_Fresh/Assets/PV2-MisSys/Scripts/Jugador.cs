using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//LINKEO PARA EL INVENTARIO
//using UnityStandarAssets.Characters.FirstPerson;

public class Jugador : MonoBehaviour
{
    public int experiencia;
    public int oro;

    //LINKEO PARA EL INVENTARIO
    //public GameObject inventario;

    public QuestSystem dataBase;
    public QuestTracker questTrack;
    public QuestTrackerPanel questTrackPanel;
    public QuestPanel questPanel;

    [HideInInspector]
    public List<GameObject> invLocal = new List<GameObject>();


    private void Start()
    {
        //mouseLook = gameObject<FirstPersonController>().m_MouseLook;

        questTrack = GetComponent<QuestTracker>();
        questTrackPanel.gameObject.SetActive(false);
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("NPC_mision"))
        {
            if (Input.GetKeyDown(KeyCode.E)) //ESTE BOTON ES EDITABLE
            {
                other.GetComponent<QuestGiver>().ContactoConJugador(this);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Destino"))
        {
            other.GetComponent<Destino_Script>().reached = true;
            questTrack.ActualizarQuest(other.GetComponent<Destino_Script>().id_Quest, Quest.QuestType.Entrega);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NPC_mision"))
        {
            questPanel.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        //APARTADO PARA EL COMBATE Y SU RECOMPENSA
        if (Input.GetKeyDown(KeyCode.F)) //ESTE APARTADO ES MODULAR   
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 200))
            {
                if (hit.transform.CompareTag("Enemy"))
                {
                    var enem = hit.transform.GetComponent<Enemy>();
                    enem.gameObject.SetActive(false);
                    questTrack.EnemyDeath(enem.id);
                }
            }
        }
        //FIN DEL APARTADO



        if (Input.GetKeyDown(KeyCode.Q))
        {
             questTrackPanel.ActualizarBotones();
             questTrackPanel.ActualizarDescripcionesConInfo(-1);
             questTrackPanel.gameObject.SetActive(!questTrackPanel.gameObject.activeSelf);
        }



        //APARTADO PARA EL INVENTARIO EN EL SCRIPT PLAYER

        


        //FIN DEL APARTADO
    }


    public void Recompensa(Quest quest)
    {
        questTrackPanel.ActualizarBotones();

        experiencia += dataBase.misions[quest.id].xp;
        oro += dataBase.misions[quest.id].gold;

        questPanel.accept_Button.gameObject.SetActive(false);
        questPanel.deny_Button.gameObject.SetActive(false);

        if (dataBase.misions[quest.id].hasSpecialR)
        {
            if (dataBase.misions[quest.id].specialR.Length > 1)
            {
                string s = "Bien hecho! Completaste " + dataBase.misions[quest.id].name + ", somo recompensa has obtenido Oro(" + dataBase.misions[quest.id].gold +
                    ")," + "Experiencia(" + dataBase.misions[quest.id].xp + ") y los siguientes items: ";

                for (int i = 0; i < dataBase.misions[quest.id].specialR.Length; i++)
                {
                    s = string.Format("(0) {i}", s, dataBase.misions[quest.id].specialR[i].nombre);
                }
                questPanel.ActualizarPanel(quest.name, s);
            }
            else
            {
                questPanel.ActualizarPanel(dataBase.misions[quest.id].name, "Bien hecho! Completaste " + dataBase.misions[quest.id].name + ", como recompensa" +
                    " has obtenido Oro(" + dataBase.misions[quest.id].gold + "), Esperiencia(" + dataBase.misions[quest.id].xp + ") y " + dataBase.misions[quest.id].specialR[0].nombre + ",");
            }
        }
        else
        {
            questPanel.ActualizarPanel(dataBase.misions[quest.id].name, "Bien hecho! Completaste " + dataBase.misions[quest.id].name + ", como recompensa" +
                " has obtenido Oro(" + dataBase.misions[quest.id].gold + ") y Experencia(" + dataBase.misions[quest.id].xp + ").");
        }

        
        //APARTADO PARA CONEXIÓN ENTRE EL SISTEMA DE RECOMPENSAS CON EL SISTEMA DE INVENTARIO

        //if (quest.retainsItems)
        //{
        //    List<GameObject> its = new List<GameObject>();
        //    int cantidadAEliminar = quest.itemsARecoger[0].cantidad;

        //    for (int i = 0; i < invLocal.Count; i++)
        //    {
        //        if (invLocal[i].GetComponent<ItemSuelto>().ID == quest.itemsARecoger[0].itemID && cantidadAEliminar > 0)
        //        {
        //            its.Add(invLocal[i]);

        //            inventario.GetComponent<InventarioNuevo>().EliminarItem(invLocal[i].GetComponent<ItemSuelto>().ID, cantidadAEliminar, false);

        //            cantidadAEliminar --;
        //        }
        //    }
        //    invLocal.RemoveAll(itemB =>
        //    {
        //        return its.Find(itemA => itemA == itemB);
        //    });

        //    foreach (var item in its)
        //    {
        //        Destroy(item);
        //    }
        //    its.Clear();

        //}

        ////APARTADO PARA CONEXIÓN CON EL SISTEMA DE INVENTARIO EN CASO DE RECIBIR UNA RECOMPENSA ESPECIAL

        //if (dataBase.misions[quest.id].hasSpecialR)
        //{
        //    foreach (var item in dataBase.misions[quest.id].specialR)
        //    {
        //        ItemSuelto itemSuelto = inventario.GetComponent<InventarioNuevo>().itemsSueltos.Find(Matrix4x4 => Matrix4x4.ID == item.reward.GetComponent<ItemSuelto>().ID);
        //        inventario.GetComponent<InventarioNuevo>().AgregarItem(item.reward.GetComponent<ItemSuelto>().ID, item.reward.GetComponent<ItemSuelto>().cantidad);

        //        if (itemSuelto != null)
        //        {
        //            itemSuelto.cantidad += 1;
        //        }
        //        else
        //        {
        //            ver nuevoIt = Instantiate(item.reward.GetComponent<ItemSuelto>());
        //            nuevoIt.Inv = inventario.GetComponent<InventarioNuevo>();
        //            nuevoIt.transform.Setparent(this.transform);
        //            inventario.GetComponent<InventarioNuevo>().itemsSUeltos.Add(nuevoIt);
        //            invLocal.Add(nuevoIt.gameObject);
        //            nuevoIt.gameObject.SetActive(false);
        //        }
        //        questTrack.VerifyItem(item.reward.GetComponent<ItemSuelto>().ID);

        //        print("Nuevo objectivo obtenido: " + item.nombre);
        //    }
        //}
    }
     









}
