using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class Skrypt_Zagroda : MonoBehaviour
{

   

   

    //Liczba pracowników
    public int[] Max_pracownicyArray;    //Tablica Maksymalnej liczby pracowników według poziomu
    public int Max_pracownicy;           //Maksymalna liczba pracowników na poziom
    public int Akt_pracownicy;           //Aktualna liczba pracowników

    public int Spich_pracownicy;
    public int Pracownicy_drewno;
    public int Pracownicy_kamień;
    public int Pracownicy_żelazo;
    public int Pracownicy_jabłka;
    public int Pracownicy_mięso;

    //Właściwości zagrody
    public int P_budynku;                //Poziom budynku
    public int[] K_drewno;               //Koszt drewna
    public int[] K_kamień;               //Koszt kamienia
    public int[] K_żelazo;               //Koszt żelaza
    public int[] K_deski;                //Koszt desek
    public int[] K_narzędzia;            //Koszt narzędzi
    public int[] L_pracowników;          //Koszt pracownicy


    //GameObjecty Prefaba
    public string koszta_string;
    public GameObject Panel_ulepszenie;
    public GameObject Skrypt_spichlerz_g;
    public GameObject UpTextPojemość;
   
    public GameObject Text_poziom;

    void Update()  //Funkcja licząca liczbę wszystkich pracowników
    {
      Akt_pracownicy = Pracownicy_drewno + Spich_pracownicy  + Pracownicy_kamień + Pracownicy_żelazo + Pracownicy_jabłka;
      UpTextPojemość.GetComponent<Text>().text = Akt_pracownicy + "/" + Max_pracownicy.ToString();
    }

    
    public void Ulepszenie_budynku()
    {
        int i = P_budynku;
        if (Skrypt_spichlerz_g.GetComponent<Skrypt_spichlerz>().drewno >= K_drewno[i] &&
            Skrypt_spichlerz_g.GetComponent<Skrypt_spichlerz>().kamień >= K_kamień[i] && 
            Skrypt_spichlerz_g.GetComponent<Skrypt_spichlerz>().kamień >= K_kamień[i] && 
            Skrypt_spichlerz_g.GetComponent<Skrypt_spichlerz>().deski >= K_deski[i] &&
            Skrypt_spichlerz_g.GetComponent<Skrypt_spichlerz>().narzędzia >= K_narzędzia[i])
        {
            Skrypt_spichlerz_g.GetComponent<Skrypt_spichlerz>().drewno -= K_drewno[i];
            Skrypt_spichlerz_g.GetComponent<Skrypt_spichlerz>().kamień -= K_kamień[i];
            Skrypt_spichlerz_g.GetComponent<Skrypt_spichlerz>().żelazo -= K_żelazo[i];
            Skrypt_spichlerz_g.GetComponent<Skrypt_spichlerz>().deski -= K_deski[i];
            Skrypt_spichlerz_g.GetComponent<Skrypt_spichlerz>().narzędzia -= K_narzędzia[i];
            this.GetComponent<Skrypt_Zagroda>().Max_pracownicy = Max_pracownicyArray[i];
            P_budynku += 1;
        }
      
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