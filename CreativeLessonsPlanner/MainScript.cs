using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.Networking;
namespace UnityEngine.UI.Extensions
{
    public class MainScript : MonoBehaviour
    {
        //SQL_Connection
        public string port, Server, Database, User, Password;
        private string connectionString;
        static MySqlCommand command;
        static MySqlDataReader reader;
        static string polecenie = "";

        public GameObject HorizontalLayoutGroup;

        //Parrents
        public Transform Parrent_CategoriesButtons;
        public Transform Parrent_IdeasButtons;
        public Transform Parrent_IdeaContent;

        //Carts
        public Transform Categories, IdeasList, IdeaContent, Settings, Quit;


        //Prefabs
        public GameObject PrefabButtonCategory, PrefabButtonIdea;

        //Arrays
        public string[] Categories_Names, Categories_Views, Categories_SpriteLinks;
        public string[] Ideas_Title_List, Ideas_Views_List, Ideas_Likes_List, Ideas_ID_List;
        public int[] Categories_IdeasCount;

        public string IdeaTitle;
        public string PictureUrl;
        public string[] IdeaContents;
        public TMP_Text TMPIdeaTitle;
        public GameObject[] TMPIdeaContents;
        public GameObject IdeaPicture;
        public TMP_InputField FindInput;

        public Sprite[] Categories_Sprites;

        public GameObject ConsolePanel;
        public Text MessageText;

        void Start()
        {

            StartCoroutine(GetCategories());
            //StartCoroutine(DownloadIdea("01_Kategorie", "5", returnValue =>
            // {
            //     Debug.Log(returnValue);
            // }));
        }


        IEnumerator GetNumberOfRows(string TableName, System.Action<string> callback)
        {
            WWWForm form = new WWWForm();
            form.AddField("TableName", TableName);
            using (UnityWebRequest www = UnityWebRequest.Post("XXXXX", form))
            {
                yield return www.SendWebRequest();
                if(www.isNetworkError || www.isHttpError)
                {
                    Debug.Log("ERROR");
                }
                callback(www.downloadHandler.text);
                
            }
        }
      IEnumerator Select(string TableName,System.Action<string> callback)
        {
            WWWForm form = new WWWForm();
            form.AddField("TableName", TableName);
            using (UnityWebRequest www = UnityWebRequest.Post("XXXXX", form))
            {
                yield return www.SendWebRequest();
                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log("ERROR");
                }
                callback(www.downloadHandler.text);

            }
        }
      IEnumerator GetIdeass(string TableName, System.Action<string> callback)
        {
            WWWForm form = new WWWForm();
            form.AddField("TableName", TableName);
            using (UnityWebRequest www = UnityWebRequest.Post("XXXXXX", form))
            {
                yield return www.SendWebRequest();
                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log("ERROR");
                }
                callback(www.downloadHandler.text);

            }
        }
      IEnumerator GetCategories()
        {
            string[] Categories = new string[0];

            StartCoroutine(Select("Kategorie", returnValue =>
             {
                 returnValue = returnValue.Remove(returnValue.Length - 1);
                 string[] text = returnValue.Split('#');
                 string[,] result = new string[text.Length, 3];

                 Array.Resize(ref Categories_Names, text.Length);
                 Array.Resize(ref Categories_Views, text.Length);
                 Array.Resize(ref Categories_SpriteLinks, text.Length);
                 Array.Resize(ref Categories_Sprites, text.Length);
                 Array.Resize(ref Categories_IdeasCount, text.Length);
                 for (int i = 0; i < text.Length; i++)
                 {
                     string[] fields = text[i].Split('|');
                     for (int j = 0; j < fields.Length; j++)
                     {
                         if (j == 3) break; // no more than 2 fields.   
                         result[i, j] = fields[j];
                     }
                     Categories_Names[i] = result[i, 0];
                     Categories_Views[i] = result[i, 1];
                     StartCoroutine(DownloadCategoriesSprites(result[i, 2],i));
                     PrefabButtonCategory.GetComponent<Prefab_Button_InMainMenu>().Title.text = result[i, 0];
                     Instantiate(PrefabButtonCategory, Parrent_CategoriesButtons);
                 }
               
                 
             }));
           
            yield return new WaitForSeconds(0.1f);


            for (int i = 0; i < Parrent_CategoriesButtons.childCount; i++)
            {
                var i2 = i;
                StartCoroutine(GetNumberOfRows((i + 1).ToString("00") + "_" + Categories_Names[i], returnValue2 =>
                {
                    (Categories_IdeasCount[i2]) = int.Parse(returnValue2);
                }));
                yield return new WaitForSeconds(0.1f);
                Parrent_CategoriesButtons.GetChild(i).transform.GetChild(2).GetComponent<TMP_Text>().text = Categories_IdeasCount[i].ToString() + "pomysłów";
                Parrent_CategoriesButtons.GetChild(i).transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().sprite = Categories_Sprites[i];
                Parrent_CategoriesButtons.GetChild(i).gameObject.GetComponent<Button>().onClick.AddListener(() => StartCoroutine(OpenCategory((i2 + 1).ToString("00") + "_" + Categories_Names[i2], Categories_IdeasCount[i2])));
            }
            
            yield return new WaitForSeconds(0.1f);
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)Parrent_CategoriesButtons.transform);
        }
      IEnumerator DownloadIdeas(string CategoryToDownload, int CountOfIdeas)
        {
            yield return new WaitForSeconds(0.1f);
            DestroyIdeasButtons();
            StartCoroutine(GetIdeass(CategoryToDownload, returnValue =>
            {
             
                Ideas_Title_List = null;
                Array.Resize(ref Ideas_Title_List, CountOfIdeas);
                returnValue = returnValue.Remove(returnValue.Length - 1);
                string[] text = returnValue.Split('#');
                string[,] result = new string[text.Length, 2];


                for (int i = 0; i < text.Length; i++)
                {

                    string[] fields = text[i].Split('|');
                    for (int j = 0; j < fields.Length; j++)
                    {
                        if (j == 3) break;  
                        result[i, j] = fields[j];
                    }
                    PrefabButtonIdea.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = result[i, 0];
                    PrefabButtonIdea.transform.GetChild(2).gameObject.transform.GetChild(2).gameObject.GetComponent<TMP_Text>().text = result[i, 1];
                    PrefabButtonIdea.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = (i + 1).ToString();
                    PrefabButtonIdea.transform.GetChild(2).gameObject.transform.GetChild(4).gameObject.GetComponent<TMP_Text>().text = CategoryToDownload;

                    Instantiate(PrefabButtonIdea, Parrent_IdeasButtons);
                 
                }
            }));

          

            for (int i = 0; i < Parrent_IdeasButtons.childCount; i++)
            {
                var i2 = i;
              //Parrent_IdeasButtons.GetChild(i).gameObject.GetComponent<Button>().onClick.AddListener(() => StartCoroutine(GetIdeass((CategoryToDownload, i2))));
                Parrent_IdeasButtons.GetChild(i).gameObject.GetComponent<Button>().onClick.AddListener(() => StartCoroutine(OpenCard(2)));

            }
            yield return new WaitForSeconds(0.1f);
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)Parrent_IdeasButtons.transform);
        }  
      IEnumerator DownloadCategoriesSprites(string Categories_SpriteLinks, int id)
        {
            UnityWebRequest FilePicture = UnityWebRequestTexture.GetTexture(Categories_SpriteLinks);
            yield return FilePicture.SendWebRequest();
            Texture2D FileTexture = ((DownloadHandlerTexture)FilePicture.downloadHandler).texture;
            Categories_Sprites[id] = Sprite.Create(FileTexture, new Rect(0, 0, FileTexture.width, FileTexture.height), new Vector2(0, 0));
       
        }







        IEnumerator DownloadIdea(string Category, string IdeaID, System.Action<string> callback)
        {
            WWWForm form = new WWWForm();
           
            form.AddField("Record", "1");
            form.AddField("TableName", "01_Konwersacje");
            using (UnityWebRequest www = UnityWebRequest.Post("http://www.skydomesoftware.usermd.net/DownloadIdea.php", form))
            {
                yield return www.SendWebRequest();
                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log("ERROR");
                }
                callback(www.downloadHandler.text);
            }
        }
        
      

        public void OpenCardVoid(int CardNumber)
        {
           
            if(CardNumber == 5 && HorizontalLayoutGroup.GetComponent<HorizontalScrollSnap>()._currentPage == 0)
            {
                Quit.gameObject.SetActive(true);
                Debug.Log("Wyjście");
            }
            else if(CardNumber == 5 && HorizontalLayoutGroup.GetComponent<HorizontalScrollSnap>()._currentPage != 0)
            {
                StartCoroutine(OpenCard(HorizontalLayoutGroup.GetComponent<HorizontalScrollSnap>()._currentPage-1));
            }
            else if (CardNumber == 1)
            {
                DestroyIdeasButtons();
                StartCoroutine(OpenCard(CardNumber));
            }
            else
            {
                StartCoroutine(OpenCard(CardNumber));
            }
        }


        public IEnumerator OpenCard(int CardNumber)
        {
            yield return new WaitForSeconds(0.5f);
            HorizontalLayoutGroup.GetComponent<HorizontalScrollSnap>().ChangePage(CardNumber);
        }


        public bool IsInternetConnection()
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public void ShowAlert(string message)
        {
            ConsolePanel.SetActive(true);
            MessageText.text = message;
        }
        IEnumerator OpenCategory(string CategoryToOpen, int CountOfIdeas)
        {
           // StartCoroutine(DownloadIdea(CategoryToOpen, CountOfIdeas));
            StartCoroutine(OpenCard(1));
           yield return new WaitForSeconds(0.1f);
        }
        IEnumerator DownloadIdea(string Category, string IdeaID)
        {
            Array.Resize(ref IdeaContents, 9);


            try
            {
                using (MySqlConnection polaczenie = new MySqlConnection(connectionString))
                {
                    polaczenie.Open();
                    polecenie = "select * from " + Category + " where ID = '" + IdeaID + "' ";
                    command = new MySqlCommand(polecenie, polaczenie);
                    command.ExecuteNonQuery();
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        for (int i = 0; i <= 8; i++)
                        {
                            IdeaTitle = reader["title"].ToString();
                            IdeaContents[i] = reader["pl" + (i + 1)].ToString();
                        }

                         PictureUrl = reader["picture"].ToString();
                    }
                    reader.Close();
                    polaczenie.Close();
                }
            }
            catch (MySqlException ex)
            {
                Debug.Log("ERROR: Download ideas" + ex.ToString());
            }
            finally
            {
                OpenIdea();
            }
            try
            {
                using (MySqlConnection polaczenie = new MySqlConnection(connectionString))
                {
                    polaczenie.Open();
                    polecenie = "update " + Category + " set views =" + "views+1" + " where ID = '" + IdeaID + "' ";
                    command = new MySqlCommand(polecenie, polaczenie);
                    command.ExecuteNonQuery();
                    reader = command.ExecuteReader();
                    reader.Close();
                    polaczenie.Close();
                }
            }
            catch (MySqlException ex)
            {
                 Debug.Log("Add view " + ex.ToString());
            }

            if (PictureUrl != "")
            {
                WWW FilePicture = new WWW(PictureUrl);
                yield return FilePicture;
                IdeaPicture.GetComponent<Image>().sprite = Sprite.Create(FilePicture.texture, new Rect(0, 0, FilePicture.texture.width, FilePicture.texture.height), new Vector2(0, 0));
                IdeaPicture.SetActive(true);
            }
            else
            {
                IdeaPicture.SetActive(false);
            }
            yield return null;
        }

        public void OpenIdea()
        {

            TMPIdeaTitle.text = IdeaTitle;
            for (int i = 0; i <= 8; i++)
            {
                TMPIdeaContents[i].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = IdeaContents[i];
                if (IdeaContents[i] != "")
                {
                    TMPIdeaContents[i].SetActive(true);
                }
                else
                {
                    TMPIdeaContents[i].SetActive(false);
                }
            }
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)Parrent_IdeaContent.transform);
        }
        
        public void DestroyIdeasButtons()
       {
            foreach (Transform child in Parrent_IdeasButtons.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }

        public void Find()
        {
            if (FindInput.text != "")
            {
                Ideas_Title_List = null;
                Ideas_Views_List = null;
                Ideas_Likes_List = null;
                Ideas_ID_List = null;
                Array.Resize(ref Ideas_Title_List, 0);
                Array.Resize(ref Ideas_Views_List, 0);
                Array.Resize(ref Ideas_Likes_List, 0);
                Array.Resize(ref Ideas_ID_List, 0);
                DestroyIdeasButtons();
                StartCoroutine(FindIdea(FindInput.text));
            }
        }

        IEnumerator FindIdea(string findtext)
        {
            for (int i = 0; i < Categories_Names.Length; i++)
            {
                var i2 = i;

                int id = 0;
                try
                {
                    using (MySqlConnection polaczenie = new MySqlConnection(connectionString))
                    {
                        polaczenie.Open();
                        polecenie = "SELECT* FROM " + (i2 + 1).ToString("00") + "_" + Categories_Names[i2] + " WHERE title LIKE '%" + findtext + "%'";

                        command = new MySqlCommand(polecenie, polaczenie);
                        command.ExecuteNonQuery();
                        reader = command.ExecuteReader();
                        Debug.Log("start");
                        while (reader.Read())
                        {
                            Array.Resize(ref Ideas_Title_List, Ideas_Title_List.Length + 1);
                            Array.Resize(ref Ideas_Views_List, Ideas_Views_List.Length + 1);
                            Array.Resize(ref Ideas_Likes_List, Ideas_Likes_List.Length + 1);
                            Array.Resize(ref Ideas_ID_List, Ideas_Likes_List.Length + 1);
                            Ideas_Title_List[id] = reader["title"].ToString();
                            Ideas_Views_List[id] = reader["views"].ToString();
                            Ideas_Likes_List[id] = reader["likes"].ToString();
                            Ideas_ID_List[id] = reader["id"].ToString();
                            id++;
                        }
                        reader.Close();
                        polaczenie.Close();
                    }
                }
                catch (MySqlException ex)
                {
                    Debug.Log("ERROR: Download Categories" + ex.ToString());
                }
                for (int x = 0; x < Ideas_Title_List.Length; x++)
                {
                    var i3 = x;
                    PrefabButtonIdea.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = Ideas_Title_List[i3];
                    PrefabButtonIdea.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = Ideas_ID_List[i3];
                    
                    PrefabButtonIdea.transform.GetChild(2).gameObject.transform.GetChild(2).gameObject.GetComponent<TMP_Text>().text = Ideas_Views_List[i3];
                    PrefabButtonIdea.transform.GetChild(2).gameObject.transform.GetChild(3).gameObject.GetComponent<TMP_Text>().text = Ideas_Likes_List[i3];
                    PrefabButtonIdea.transform.GetChild(2).gameObject.transform.GetChild(4).gameObject.GetComponent<TMP_Text>().text = Categories_Names[i2];
                    GameObject IdeaButton =  Instantiate(PrefabButtonIdea, Parrent_IdeasButtons);
                    IdeaButton.gameObject.GetComponent<Button>().onClick.AddListener(() => StartCoroutine(DownloadIdea((i2+1).ToString("00") + "_" + Categories_Names[i2], IdeaButton.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text)));
                    IdeaButton.gameObject.GetComponent<Button>().onClick.AddListener(() => StartCoroutine(OpenCard(2)));
                    
                    IdeaButton.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = new Color(1, 1, 1, 0f);
                }
                Ideas_Title_List = null;
                Ideas_Views_List = null;
                Ideas_Likes_List = null;
                Ideas_ID_List = null;

                Array.Resize(ref Ideas_Title_List, 0);
                Array.Resize(ref Ideas_Views_List, 0);
                Array.Resize(ref Ideas_Likes_List, 0);
                Array.Resize(ref Ideas_ID_List, 0);
                yield return null;
                LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)Parrent_IdeasButtons.transform);
            }
        }
    }
}
  




