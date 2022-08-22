using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerTriggered : MonoBehaviour
{
    public static event Action<int> OnTriggeredArrow, OnTriggeredPotion, OnTriggeredGem;

    [SerializeField] private int arrowDamage;
    [SerializeField] private int addHealth;
    [SerializeField] private int addScore;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(TagManager.ARROW))
        {
            OnTriggeredArrow?.Invoke(arrowDamage);
            Destroy(other.gameObject);
        }
        else if(other.gameObject.CompareTag(TagManager.POTION))
        {
            OnTriggeredPotion?.Invoke(addHealth);
            Destroy(other.gameObject);
        }
        else if(other.gameObject.CompareTag(TagManager.GEM))
        {
            OnTriggeredGem?.Invoke(addScore);
            Destroy(other.gameObject);
        }
    }
}
