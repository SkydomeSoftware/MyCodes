using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skrypt_Potrzeby_Mieszkańców : MonoBehaviour
{
    public GameObject S_spichlerz;
    public int Mission;



    public void Start()
    {
        StartCoroutine(Utrata_surowców());
    }

    IEnumerator Utrata_surowców()

    {

        if (Mission >= 4)
        {
            if  (S_spichlerz.GetComponent<Skrypt_spichlerz>().jabłka <= 1)
              S_spichlerz.GetComponent<Skrypt_spichlerz>().jabłka -= 1;
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine(Utrata_surowców());
    }
}
