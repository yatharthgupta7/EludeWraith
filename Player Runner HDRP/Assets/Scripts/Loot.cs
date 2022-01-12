using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{

    [SerializeField]float turnSpeed = 90f;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag!="Player")
        {
            return;
        }
        GameManager.instance.IncrementPotion();
        Destroy(gameObject);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, turnSpeed * Time.deltaTime, 0);
    }

}
