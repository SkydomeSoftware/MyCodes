using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Zadowolenie_mieszkańców : MonoBehaviour {

    public int zadowoenie;



    public GameObject Up_zadowolenie;
    public GameObject Skrypt_spichlerz_g;

    public void Start()
    {
        StartCoroutine(Automat());
    }


    IEnumerator Automat()
    {
            if (Skrypt_spichlerz_g.GetComponent<Skrypt_spichlerz>().jabłka == 0)
            {
                zadowoenie -= 1;
            }
            else if (Skrypt_spichlerz_g.GetComponent<Skrypt_spichlerz>().jabłka > 0)
            {
                zadowoenie += 1;
            }

        if (zadowoenie > 100)
            zadowoenie = 100;
        else if (zadowoenie < 0)
            zadowoenie = 0;
        Up_zadowolenie.GetComponent<Text>().text = zadowoenie + "%";
        yield return new WaitForSeconds(1f);
        StartCoroutine(Automat());
    }
    
}
