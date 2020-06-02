using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Materiały : MonoBehaviour
{
    public GameObject Skrypt_spichlerz_g;
   
    public GameObject ButtonText_automat;
    public string materiał;


    public int włączone_wyłączone;

    public int Ilość_materiału;
    
    public int Koszt_stworzenia_drewno;
    public int Koszt_stworzenia_kamień;
    public int Koszt_stworzenia_żelazo;
    public int Koszt_stworzenia_deski;
    public int Koszt_stworzenia_narzędzia;
    public float Czas_powtórki;


    
        
  

    IEnumerator Automat()
    {
        if (włączone_wyłączone == 1)
        {
            Tworzenie_Materiału();
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(Automat());
        }
        
    }


    public void Tworzenie_Materiału()
    {
        if (materiał == "deski")
        {
            if (Skrypt_spichlerz_g.GetComponent<Skrypt_spichlerz>().deski <= Skrypt_spichlerz_g.GetComponent<Skrypt_spichlerz>().MaxPojemość - 1)
            {
                if (Skrypt_spichlerz_g.GetComponent<Skrypt_spichlerz>().drewno >= Koszt_stworzenia_drewno &&
                    Skrypt_spichlerz_g.GetComponent<Skrypt_spichlerz>().kamień >= Koszt_stworzenia_kamień &&
                    Skrypt_spichlerz_g.GetComponent<Skrypt_spichlerz>().żelazo >= Koszt_stworzenia_żelazo)
                {
                    Skrypt_spichlerz_g.GetComponent<Skrypt_spichlerz>().deski += Ilość_materiału;
                    Skrypt_spichlerz_g.GetComponent<Skrypt_spichlerz>().drewno -= Koszt_stworzenia_drewno;
                }
            }
        }
        if (materiał == "narzędzia")
        {
            if (Skrypt_spichlerz_g.GetComponent<Skrypt_spichlerz>().narzędzia <= Skrypt_spichlerz_g.GetComponent<Skrypt_spichlerz>().MaxPojemość - 1)
            {
                if (Skrypt_spichlerz_g.GetComponent<Skrypt_spichlerz>().drewno >= Koszt_stworzenia_drewno &&
                    Skrypt_spichlerz_g.GetComponent<Skrypt_spichlerz>().kamień >= Koszt_stworzenia_kamień &&
                    Skrypt_spichlerz_g.GetComponent<Skrypt_spichlerz>().żelazo >= Koszt_stworzenia_żelazo)
                {
                    Skrypt_spichlerz_g.GetComponent<Skrypt_spichlerz>().narzędzia += Ilość_materiału;
                    Skrypt_spichlerz_g.GetComponent<Skrypt_spichlerz>().drewno -= Koszt_stworzenia_drewno;
                    Skrypt_spichlerz_g.GetComponent<Skrypt_spichlerz>().żelazo -= Koszt_stworzenia_żelazo;

                }
            }
        }
    }



    public void Włącz_wyłącz_Automat()
    {
        if (włączone_wyłączone == 0)
        {
            włączone_wyłączone = 1;
            StartCoroutine(Automat());
            ButtonText_automat.GetComponent<Image>().color = Color.green;
        }
        else
            
        {
            włączone_wyłączone = 0;
            ButtonText_automat.GetComponent<Image>().color = Color.red;
        }
    }
}

