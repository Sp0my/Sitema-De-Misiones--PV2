using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Este script se agregará a un empty game object, más un tag "destino",
// llebara un BOX COLLIDER con IS TRIGGER activado y después se volverá
// un prefab para su utilización libre en relación al punto donde se
// quiere que llegue el jugador. 
public class Destino_Script : MonoBehaviour
{
    public int id_Quest;
    public bool reached = false;
}
