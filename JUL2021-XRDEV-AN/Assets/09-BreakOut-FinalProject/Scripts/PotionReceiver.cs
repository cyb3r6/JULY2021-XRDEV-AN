using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PotionReceiver : MonoBehaviour
{
    [Serializable]
    public class PotionPouredEvent: UnityEvent<string> { }


    public string[] acceptedPotionTypes;

    public PotionPouredEvent OnPotionPoured;
    public void ReceivePotion(string potionType)
    {
        if(potionType == "Shrink")
        {
            MasterController.instance.Shrink();
        }
        if (potionType == "Grow")
        {
            MasterController.instance.Grow();
        }
        if (potionType == "Default")
        {
            MasterController.instance.ReturnToNormal();
        }
        if (potionType == "Unlocker")
        {
            OnPotionPoured?.Invoke(potionType);
        }
    }
   
}
