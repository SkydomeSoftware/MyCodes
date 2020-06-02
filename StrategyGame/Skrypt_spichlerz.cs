using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class Skrypt_spichlerz : MonoBehaviour
{




    //surowce
    public int drewno;
    public int kamień;
    public int żelazo;

    //materiały
    public int deski;
    public int narzędzia;
    //żywność
    public int jabłka;
    public int mięso;

    //max pojemność
    public int P_budynku;
    public int MaxPojemość;

    //Właściwości budynku
    public string koszta_string;
  
    public int[] MaxPojemośćArray;
    public int[] K_drewno;
    public int[] K_kamień;
    public int[] K_żelazo;
    public int[] K_deski;
    public int[] K_narzędzia;
    public int[] L_pracowników;


    //połączone budynki
    public GameObject S_zagroda;
    public GameObject Skrypt_spichlerz_g;

    //Wyświetlane teksty
    public GameObject Panel_ulepszenie;
    public GameObject Text_poziom;
    public GameObject Text_pojemność_spichlerza;
    public GameObject Text_drewno;
    public GameObject Text_kamień;
    public GameObject Text_żelazo;
    public GameObject Text_deski;
    public GameObject Text_jabłka;
    public GameObject Text_narzędzia;
    public GameObject Text_mięso;

    void Update()
    {

        if (drewno >= MaxPojemość)
            drewno = MaxPojemość;

        if (kamień >= MaxPojemość)
            kamień = MaxPojemość;

        if (żelazo >= MaxPojemość)
            żelazo = MaxPojemość;

        if (deski >= MaxPojemość)
            deski = MaxPojemość;

        if (jabłka >= MaxPojemość)
            jabłka = MaxPojemość;

        if (narzędzia >= MaxPojemość)
            narzędzia = MaxPojemość;

        if (mięso >= MaxPojemość)
            mięso = MaxPojemość;

        Text_drewno.GetComponent<Text>().text = drewno.ToString();
        Text_kamień.GetComponent<Text>().text = kamień.ToString();
        Text_żelazo.GetComponent<Text>().text = żelazo.ToString();
        Text_deski.GetComponent<Text>().text = deski.ToString();
        Text_jabłka.GetComponent<Text>().text = jabłka.ToString();
        Text_narzędzia.GetComponent<Text>().text = narzędzia.ToString();
        Text_mięso.GetComponent<Text>().text = mięso.ToString();
        Text_pojemność_spichlerza.GetComponent<Text>().text = MaxPojemość.ToString();
    }



    public void Uleszpenie_budynku()
    {

        int i = P_budynku;
        if (this.GetComponent<Skrypt_spichlerz>().drewno >= K_drewno[i] &&
            this.GetComponent<Skrypt_spichlerz>().kamień >= K_kamień[i] &&
            this.GetComponent<Skrypt_spichlerz>().żelazo >= K_żelazo[i] &&
            this.GetComponent<Skrypt_spichlerz>().deski >= K_deski[i] &&
            this.GetComponent<Skrypt_spichlerz>().narzędzia >= K_narzędzia[i] &&
            L_pracowników[i] - S_zagroda.GetComponent<Skrypt_Zagroda>().Spich_pracownicy + S_zagroda.GetComponent<Skrypt_Zagroda>().Akt_pracownicy < S_zagroda.GetComponent<Skrypt_Zagroda>().Max_pracownicy)
        {
            this.GetComponent<Skrypt_spichlerz>().drewno -= K_drewno[i];
            this.GetComponent<Skrypt_spichlerz>().kamień -= K_kamień[i];
            this.GetComponent<Skrypt_spichlerz>().żelazo -= K_żelazo[i];
            this.GetComponent<Skrypt_spichlerz>().deski -= K_deski[i];
            this.GetComponent<Skrypt_spichlerz>().narzędzia -= K_narzędzia[i];
            this.GetComponent<Skrypt_spichlerz>().MaxPojemość = MaxPojemośćArray[i];
            P_budynku += 1;
            Text_poziom.GetComponent<Text>().text = "Poziom " + P_budynku;
        }
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
