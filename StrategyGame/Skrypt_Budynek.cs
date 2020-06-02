using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class Skrypt_Budynek : MonoBehaviour
{
   

    //Levelowanie
    public string surowiec;
   
    //Właściwości budynku
    public int[] Ilość_dodawanego_surowca;
    public int[] K_drewno;
    public int[] K_kamień;
    public int[] K_żelazo;
    public int[] K_deski;
    public int[] K_narzędzia;
    public int[] L_pracowników;


    public string koszta_string;
    public int P_budynku;
   
    //inne budynki
    public GameObject S_spichlerz;
    public GameObject S_zagroda;
      
    //GameObjecty Prefaba
    public GameObject Text_poziom;

    public GameObject Panel_ulepszenie;


    void Start()
    {
        StartCoroutine(Dodaj_surowiec());
    }
    IEnumerator Dodaj_surowiec()
    {
        int i = P_budynku;

        switch (surowiec)
        {
            case "drewno":
            S_spichlerz.GetComponent<Skrypt_spichlerz>().drewno += Ilość_dodawanego_surowca[i];
                break;
            case "kamień":
                S_spichlerz.GetComponent<Skrypt_spichlerz>().kamień += Ilość_dodawanego_surowca[i];
                break;
            case "żelazo":
                S_spichlerz.GetComponent<Skrypt_spichlerz>().żelazo += Ilość_dodawanego_surowca[i];
                break;
            case "jabłka":
            S_spichlerz.GetComponent<Skrypt_spichlerz>().jabłka += Ilość_dodawanego_surowca[i];
                break;
            case "mięso":
                S_spichlerz.GetComponent<Skrypt_spichlerz>().mięso += Ilość_dodawanego_surowca[i];
                break;
        }
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(Dodaj_surowiec());
    }

    public void Ulepszenie_budynku()
    {
        int i = P_budynku;

        if (S_spichlerz.GetComponent<Skrypt_spichlerz>().drewno >= K_drewno[i] &&
            S_spichlerz.GetComponent<Skrypt_spichlerz>().kamień >= K_kamień[i] &&
            S_spichlerz.GetComponent<Skrypt_spichlerz>().żelazo >= K_żelazo[i] &&
            S_spichlerz.GetComponent<Skrypt_spichlerz>().deski >= K_deski[i] &&
            S_spichlerz.GetComponent<Skrypt_spichlerz>().narzędzia >= K_narzędzia[i])
        {
            if (surowiec == "drewno" && S_zagroda.GetComponent<Skrypt_Zagroda>().Akt_pracownicy - S_zagroda.GetComponent<Skrypt_Zagroda>().Pracownicy_drewno + L_pracowników[i] <= S_zagroda.GetComponent<Skrypt_Zagroda>().Max_pracownicy)
                Ulepszenie_budynku2();
            if (surowiec == "kamień" && S_zagroda.GetComponent<Skrypt_Zagroda>().Akt_pracownicy - S_zagroda.GetComponent<Skrypt_Zagroda>().Pracownicy_kamień + L_pracowników[i] <= S_zagroda.GetComponent<Skrypt_Zagroda>().Max_pracownicy)
                Ulepszenie_budynku2();
            if (surowiec == "żelazo" && S_zagroda.GetComponent<Skrypt_Zagroda>().Akt_pracownicy - S_zagroda.GetComponent<Skrypt_Zagroda>().Pracownicy_żelazo + L_pracowników[i] <= S_zagroda.GetComponent<Skrypt_Zagroda>().Max_pracownicy)
                Ulepszenie_budynku2();
            if (surowiec == "jabłka" && S_zagroda.GetComponent<Skrypt_Zagroda>().Akt_pracownicy - S_zagroda.GetComponent<Skrypt_Zagroda>().Pracownicy_jabłka+ L_pracowników[i] <= S_zagroda.GetComponent<Skrypt_Zagroda>().Max_pracownicy)
                Ulepszenie_budynku2();
            if (surowiec == "mięso" && S_zagroda.GetComponent<Skrypt_Zagroda>().Akt_pracownicy - S_zagroda.GetComponent<Skrypt_Zagroda>().Pracownicy_jabłka + L_pracowników[i] <= S_zagroda.GetComponent<Skrypt_Zagroda>().Max_pracownicy)
                Ulepszenie_budynku2();
        }
        
    }

    

             void Ulepszenie_budynku2()

            {
        int i = P_budynku;
        S_spichlerz.GetComponent<Skrypt_spichlerz>().drewno -= K_drewno[i];
            S_spichlerz.GetComponent<Skrypt_spichlerz>().kamień -= K_kamień[i];
            S_spichlerz.GetComponent<Skrypt_spichlerz>().żelazo -= K_żelazo[i];
            S_spichlerz.GetComponent<Skrypt_spichlerz>().deski -= K_deski[i];
            S_spichlerz.GetComponent<Skrypt_spichlerz>().narzędzia -= K_narzędzia[i];
            switch (surowiec)
            {
                case "drewno":
                    S_zagroda.GetComponent<Skrypt_Zagroda>().Pracownicy_drewno = L_pracowników[i];
                    break;
                case "kamień":
                    S_zagroda.GetComponent<Skrypt_Zagroda>().Pracownicy_kamień = L_pracowników[i];
                    break;
                case "żelazo":
                    S_zagroda.GetComponent<Skrypt_Zagroda>().Pracownicy_żelazo = L_pracowników[i];
                    break;
                case "jabłka":
                    S_zagroda.GetComponent<Skrypt_Zagroda>().Pracownicy_jabłka = L_pracowników[i];
                    break;
                case "mięso":
                    S_zagroda.GetComponent<Skrypt_Zagroda>().Pracownicy_mięso = L_pracowników[i];
                    break;
            }
            P_budynku += 1;
            Text_poziom.GetComponent<Text>().text = "Poziom " + P_budynku;
        }
    
        public void Włącz_panel_ulepszenie()
    {
        int i = P_budynku;
        Panel_ulepszenie.GetComponent<Info_ulepszenie>().Int_Koszt_ulepszenia_Drewno = K_drewno[i];
        Panel_ulepszenie.GetComponent<Info_ulepszenie>().Int_Koszt_ulepszenia_Kamień = K_kamień[i];
        Panel_ulepszenie.GetComponent<Info_ulepszenie>().Int_Koszt_ulepszenia_Żelazo = K_żelazo[i];
        Panel_ulepszenie.GetComponent<Info_ulepszenie>().Int_Koszt_ulepszenia_Deski = K_deski[i];
        Panel_ulepszenie.GetComponent<Info_ulepszenie>().Int_Koszt_ulepszenia_Narzędzia = K_narzędzia[i];
        Panel_ulepszenie.GetComponent<Info_ulepszenie>().Int_Koszt_ulepszenia_Pracownicy = L_pracowników[i];
        Panel_ulepszenie.GetComponent<Info_ulepszenie>().budynek_do_ulepszenia = this.gameObject;
        Panel_ulepszenie.SetActive(true);
    }

}
