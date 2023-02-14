using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "QuestSystem", order = 1)]
public class QuestSystem : ScriptableObject
{
    [System.Serializable]
    public struct Mision
    {
        public string name;
        public string description;
        public int id;
        public QuestType tipo;

        [System.Serializable]
        public enum QuestType
        {
            Recoleccion,
            Matar,
            Entrega
        }

        //Tipos de Misiones, recompensas y datos que almacenarán
        [Header("Misiones de Recolección")]
        public bool diferentItems;
        public bool keepsItems;
        public List<ItemsARecoger> Datos;

        [System.Serializable]
        public struct ItemsARecoger
        {
            public string nombre;
            public int cantidad;
            public int itemID;
        }


        [Header("Misiones de Matar")]
        public int cantidad;
        public int enemyID;


        [Header("Recompensas")]
        public int gold;
        public int xp;
        public bool hasSpecialR;
        public SpecialRewards[] specialR;

        [System.Serializable]
        public struct SpecialRewards
        {
            public string nombre;
            public GameObject reward;
        }
    }
    public Mision[] misions;
    //
    //public float precisionDestino = 1.5f;  
}
