using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Menadżer_budynków : MonoBehaviour {
    public int Complete_mission;
    public int Zadowolenie;

    public Sprite[] Surowce_photos;
    public Sprite[] Budynki_photos;
    public Sprite[] Potrzeby_photos;

    public GameObject Mission_panel;
    public GameObject Mission_text;
    public GameObject Mission_photo_1; public Text Text_photo_1;
    public GameObject Mission_photo_2; public Text Text_photo_2;
    public GameObject Mission_photo_3; public Text Text_photo_3;

    public GameObject Up_zadowolenie;
    public GameObject GoPageSurowce;
    public GameObject GoPageBudynki;
    public GameObject GoPagePotrzeby;

    public GameObject spichlerz_skrypt; public GameObject spichlerz; public GameObject Up_spichlerz;
    
    public GameObject zagroda_skrypt;

    public GameObject zagroda; public GameObject Up_zagroda;

    public GameObject drewno; public GameObject Up_drewno;

    public GameObject kamień; public GameObject Up_kamień;

    public GameObject żelazo; public GameObject Up_żelazo;

    public GameObject tartak; public GameObject Up_tartak;

    public GameObject kowal; public GameObject Up_kowal;

    public GameObject jabłka; public GameObject Up_jabłka;

    public GameObject mięso; public GameObject Up_mięso;

    void Update()
    {

        //Zadanie1
        if (zagroda_skrypt.GetComponent<Skrypt_Zagroda>().P_budynku >= 0 &&
            Complete_mission == 0)
        {
            Mission_panel.SetActive(true);
            Mission_text.GetComponent<Text>().text = "Zbuduj zagrodę. Zapewni ona miejsce dla nowych mieszkańców. Mieszkańcy pracują w budynkach. Ilość aktualnej oraz maksymalnej liczby pracowników znajdziesz w panelu po prawej stronie";
            Text_photo_2.text = "zagroda\n" + " poziom 1";
            Mission_photo_1.SetActive(false); Mission_photo_2.SetActive(true); Mission_photo_3.SetActive(false);
            Mission_photo_2.GetComponent<Image>().sprite = Budynki_photos[0];
            Complete_mission = 1;
        }

        //Zadanie2
        if (zagroda_skrypt.GetComponent<Skrypt_Zagroda>().P_budynku >= 1 &&
            Complete_mission == 1)
        {
            Mission_panel.SetActive(true);
            Mission_panel.SetActive(true);
            Mission_text.GetComponent<Text>().text = "Zbuduj spichlerz. Zwiększaj jego pojemność aby pomieścić Twoje surowce. Możesz zobaczyć maksymalną pojemność w panelu po prawej stronie";
            spichlerz.SetActive(true); Up_spichlerz.SetActive(true);
            Text_photo_2.text = "spichlerz\n" + " poziom 1";
            Mission_photo_1.SetActive(false); Mission_photo_2.SetActive(true); Mission_photo_3.SetActive(false);
            Mission_photo_2.GetComponent<Image>().sprite = Budynki_photos[1];
            Complete_mission = 2;
        }

        //Zadanie3
        if (spichlerz_skrypt.GetComponent<Skrypt_spichlerz>().P_budynku >= 1 &&
            Complete_mission == 2)
        {
            Mission_panel.SetActive(true);
            Mission_text.GetComponent<Text>().text = "Zacznij wydobywać surowce. Rozbuduj produkcję drewna, kamienia, oraz żelaza na poziom 3";
            drewno.SetActive(true); Up_drewno.SetActive(true);
            kamień.SetActive(true); Up_kamień.SetActive(true);
            żelazo.SetActive(true); Up_żelazo.SetActive(true);
            Mission_photo_1.SetActive(true); Mission_photo_2.SetActive(true); Mission_photo_3.SetActive(true);
            Text_photo_1.text = "drewno\n" + "poziom 3"; Text_photo_2.text = "kamień\n" + " poziom 3"; Text_photo_3.text = "żelazo\n" + "poziom 3";
            Mission_photo_1.GetComponent<Image>().sprite = Surowce_photos[0]; Mission_photo_2.GetComponent<Image>().sprite = Surowce_photos[1]; Mission_photo_3.GetComponent<Image>().sprite = Surowce_photos[2];
            GoPageSurowce.GetComponent<Button>().onClick.Invoke();
            Complete_mission = 3;
        }

        //Zadanie4
        if (drewno.GetComponent<Skrypt_Budynek>().P_budynku >= 3 &&
       kamień.GetComponent<Skrypt_Budynek>().P_budynku >= 3 &&
       żelazo.GetComponent<Skrypt_Budynek>().P_budynku >= 3 &&
       Complete_mission == 3)
        {
            Mission_panel.SetActive(true);
            Mission_text.GetComponent<Text>().text = "Brawo. Teraz będziesz musiał zatroszczyć się o swoich mieszkańców. Zapewnij im dostawę żywności\n" + "Ulepsz produkcję jabłek.";
            jabłka.SetActive(true); Up_jabłka.SetActive(true);
            Mission_photo_1.SetActive(false); Mission_photo_2.SetActive(true); Mission_photo_3.SetActive(false);
            Text_photo_2.text = "jabłka\n" + " poziom 1";
            Mission_photo_2.GetComponent<Image>().sprite = Potrzeby_photos[0];
            GoPagePotrzeby.GetComponent<Button>().onClick.Invoke();
            Complete_mission = 4;
        }

        //Zadanie5
        if (jabłka.GetComponent<Skrypt_Budynek>().P_budynku >= 1 &&
        Complete_mission == 4)
        {
            Mission_panel.SetActive(true);
            Mission_text.GetComponent<Text>().text = "Świetnie. W prawym górnym rogu znajduje się zawodolenie mieszkańców. Zadbaj o to aby mieszkańcy mieli co jeść";
            Mission_photo_1.SetActive(false); Mission_photo_2.SetActive(false); Mission_photo_3.SetActive(false);
            Complete_mission = 5;
        }

        //Zadanie6
        if (jabłka.GetComponent<Skrypt_Budynek>().P_budynku >= 2 &&
            Complete_mission == 5)
        {
            Mission_panel.SetActive(true);
            Mission_text.GetComponent<Text>().text = "Poziom zadowolenia w Twojej wiosce rośnie. Teraz zajmij się produkcją desek. Pozwoli Ci to bardziej rozwinąć Twoją wioskę. Uzbieraj 30 desek aby ukończyć misję";
            tartak.SetActive(true); Up_tartak.SetActive(true);
            Mission_photo_1.SetActive(false); Mission_photo_2.SetActive(true); Mission_photo_3.SetActive(false);
            Text_photo_2.text = "deski";
            Mission_photo_2.GetComponent<Image>().sprite = Budynki_photos[2];
            GoPageBudynki.GetComponent<Button>().onClick.Invoke();
            Complete_mission = 6;
        }
        //Zadanie7
        if (spichlerz_skrypt.GetComponent<Skrypt_spichlerz>().deski >= 30 &&
               Complete_mission == 6)
        {
            Mission_panel.SetActive(true);
            Mission_text.GetComponent<Text>().text = "Super. Od teraz w Twojej wiose możesz produkować narzędzia. Stwórz 30 narzędzi";
            kowal.SetActive(true); Up_kowal.SetActive(true);
            Text_photo_2.text = "narzędzia";
            Mission_photo_2.GetComponent<Image>().sprite = Budynki_photos[3];
            Complete_mission = 7;
        }
        //Zadanie8
        if (spichlerz_skrypt.GetComponent<Skrypt_spichlerz>().narzędzia >= 30 &&
               Complete_mission == 7)
        {
            Mission_panel.SetActive(true);
            Mission_text.GetComponent<Text>().text = "Zadanie wykonane. Twoi mieszkańcy zaczynają domagać się nowego typu towarów. Zacznij produkować mięso";
            Text_photo_2.text = "mięso\n"+"poziom 3";
            Mission_photo_2.GetComponent<Image>().sprite = Budynki_photos[3];
            mięso.SetActive(true); Up_mięso.SetActive(true);
            Complete_mission = 8;
        }
    }
    public void Start()
    {
        StartCoroutine(Zaspokojenie_potrzeb());
        StartCoroutine(Spadek_zadowolenia());
    }

    IEnumerator Zaspokojenie_potrzeb()

    {
        if (Complete_mission >= 5)
        {
            if (spichlerz_skrypt.GetComponent<Skrypt_spichlerz>().jabłka >= 1)
            {
                spichlerz_skrypt.GetComponent<Skrypt_spichlerz>().jabłka -= 1;
                yield return new WaitForSeconds(1f);
            }
        }
        if (Complete_mission >= 8)
        {

            if (spichlerz_skrypt.GetComponent<Skrypt_spichlerz>().mięso >= 1)
            {
                spichlerz_skrypt.GetComponent<Skrypt_spichlerz>().mięso -= 1;
                yield return new WaitForSeconds(0.9f);
            }
        }
        yield return new WaitForSeconds(0.9f);
        StartCoroutine(Zaspokojenie_potrzeb());
    }

    
    IEnumerator Spadek_zadowolenia()
    {
        if (Complete_mission >= 5)
        {
            if (spichlerz_skrypt.GetComponent<Skrypt_spichlerz>().jabłka <= 0)
            {
                Zadowolenie -= 2;
            }
            else if (spichlerz_skrypt.GetComponent<Skrypt_spichlerz>().jabłka > 0)
            {
                Zadowolenie += 1;
            }
        }
        if (Complete_mission >= 7)
        {
            if (spichlerz_skrypt.GetComponent<Skrypt_spichlerz>().mięso <= 0)
            {
                Zadowolenie -= 2;
            }
            else if (spichlerz_skrypt.GetComponent<Skrypt_spichlerz>().mięso > 0)
            {
                Zadowolenie += 1;
            }
        }
        if (Zadowolenie > 100)
            Zadowolenie = 100;
        else if (Zadowolenie < 0)
            Zadowolenie = 0;

        Up_zadowolenie.GetComponent<Text>().text = Zadowolenie + "%";
        yield return new WaitForSeconds(1f);
        StartCoroutine(Spadek_zadowolenia());
    }

}




