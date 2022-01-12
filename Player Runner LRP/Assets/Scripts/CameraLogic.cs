using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLogic : MonoBehaviour
{
    [SerializeField]Transform[] lookAtAr;
    [SerializeField] GameObject instructions;
    Transform lookAt;
    Vector3 startOffset;
    Vector3 moveVector;

    float transition = 0.0f;
    float animationDuration = 3.0f;
    Vector3 animationOPffset = new Vector3(0, 5, 5);
    int index;
    void Start()
    {
        index = PlayerPrefs.GetInt("CharacterSelected");
        //lookAt = GameObject.FindGameObjectWithTag("Player").transform;
        lookAt = lookAtAr[index].transform;
        startOffset = transform.position - lookAt.position;
    }

    // Update is called once per frame
    void Update()
    {
        moveVector = lookAt.position + startOffset;

        moveVector.x = 0;
        moveVector.y = Mathf.Clamp(moveVector.y, 3, 5);

        if(transition>1.0f)
        {
            transform.position = moveVector;
            instructions.SetActive(false);
        }
        else
        {
            transform.position = Vector3.Lerp(moveVector + animationOPffset, moveVector, transition);
            transition += Time.deltaTime * 1 / animationDuration;
            transform.LookAt(lookAt.position + Vector3.up);
        }
    }
}
