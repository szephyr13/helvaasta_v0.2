using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NemoStartPosition : MonoBehaviour
{
    public Vector2 positionFromBeginning;
    public Vector2 positionFromKaida;

    //se lee la posición del personaje jugable para que cuando salga de la casa de la primera sospechosa se encuentre allí y no en la posición inicial de la escena 2
    void Start()
    {
        if (ChangeScene.lastSceneIndex == 3){
            transform.position = positionFromKaida;
        }
    }
}
