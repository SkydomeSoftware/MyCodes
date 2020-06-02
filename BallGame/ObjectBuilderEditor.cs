
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;









[CustomEditor(typeof(CharacterController2D))]

public class ObjectBuilderEditor0 : Editor
{
    bool ShowMovingProporties = true;
    bool ShowCameraProporties = true;
    bool ShowOtherProporties = true;
    bool ShowDetectors = true;
    bool ShowOthers = false;







    public override void OnInspectorGUI()
    {




        GUIStyle HeaderStyle = new GUIStyle();
        HeaderStyle.fontSize = 15;
        HeaderStyle.fontStyle = FontStyle.Bold;

        GUIStyle HeaderSmallStyle = new GUIStyle();
        HeaderSmallStyle.fontSize = 11;
        HeaderSmallStyle.fontStyle = FontStyle.Bold;



        CharacterController2D Character = (CharacterController2D)target;
        GUILayout.Space(10);

       

        EditorGUILayout.LabelField("Właściwości postaci:", HeaderStyle, GUILayout.Height(20));
       
        Character.gameObject.GetComponent<SpriteRenderer>().sprite = (Sprite)EditorGUILayout.ObjectField("Tekstura postaci:", Character.gameObject.GetComponent<SpriteRenderer>().sprite, typeof(Sprite), true);

      


        ShowMovingProporties = EditorGUILayout.Foldout(ShowMovingProporties, "Ustawienia poruszania się:");
        if (ShowMovingProporties)
            if (Selection.activeTransform)
            {
                
                Character.jumpHeight = EditorGUILayout.Slider("Wysokość skoku:", Character.jumpHeight, 0, 30);
              
                Character.moveSpeed = EditorGUILayout.Slider("prędkość poruszania:", Character.moveSpeed, 0, 5);
                Character.pushForce = EditorGUILayout.Slider("Siła pchnięcia:", Character.pushForce, 0, 5);
       
                Character.rotationForce = EditorGUILayout.Slider("Prędkość obrotu:", Character.rotationForce, 0, 3);
              
                Character.gameObject.GetComponent<Rigidbody2D>().mass = EditorGUILayout.FloatField("Masa postaci:", Character.gameObject.GetComponent<Rigidbody2D>().mass);
                Character.gameObject.GetComponent<Rigidbody2D>().angularDrag = EditorGUILayout.FloatField("Siła tarcia:", Character.gameObject.GetComponent<Rigidbody2D>().angularDrag);
          
                GUILayout.Space(20);
            }
     
        

        ShowCameraProporties = EditorGUILayout.Foldout(ShowCameraProporties, "Ustawienia kamery:");
        if (ShowCameraProporties)
            if (Selection.activeTransform)
            {
                Character.cameraOffset = EditorGUILayout.Vector3Field("Pozycja kamery:", Character.cameraOffset);
                Character.FollowSpeed = EditorGUILayout.Slider("Prędkość podążania kamery:", Character.FollowSpeed, 0, 10);
                Character.PlayerCamera = (Camera)EditorGUILayout.ObjectField("Kamera", Character.PlayerCamera, typeof(Camera), true);
                GUILayout.Space(20);
            }
    


        ShowOtherProporties = EditorGUILayout.Foldout(ShowOtherProporties, "Ustawienia tekstu nad graczem");
        if (ShowOtherProporties)
            if (Selection.activeTransform)
            {
                Character.UpText = (TextMesh)EditorGUILayout.ObjectField("Nazwa postaci obiekt:", Character.UpText, typeof(TextMesh), true);
                Character.UpTextOffset = EditorGUILayout.Vector3Field("Pozycja tekstu:", Character.UpTextOffset);
                Character.UpText.text = EditorGUILayout.TextField("Nazwa gracza:", Character.UpText.text);
                Character.UpText.color = EditorGUILayout.ColorField("Kolor tekstu", Character.UpText.color);
               
               
                GUILayout.Space(20);
            }

        ShowDetectors = EditorGUILayout.Foldout(ShowDetectors, "Debbuger:");
        if (ShowDetectors)
            if (Selection.activeTransform)
            {
                EditorGUILayout.LabelField("Czujniki:", HeaderSmallStyle, GUILayout.Width(100));


                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("", GUILayout.Width(66));
                EditorGUILayout.LabelField("L.górny", GUILayout.Width(50));
                EditorGUILayout.Toggle(Character.leftUpCollider, GUILayout.Width(25));
                EditorGUILayout.LabelField("", GUILayout.Width(20));
                EditorGUILayout.Toggle(Character.rightUpCollider, GUILayout.Width(25));
                EditorGUILayout.LabelField("P.górny", GUILayout.Width(50));

                EditorGUILayout.EndHorizontal();






                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("", GUILayout.Width(66));
                EditorGUILayout.LabelField("", GUILayout.Width(66));
                EditorGUILayout.LabelField("Górny", GUILayout.Width(40));
                Character.UpColliderDistance = EditorGUILayout.FloatField(Character.UpColliderDistance, GUILayout.Width(25));
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("", GUILayout.Width(66));
                EditorGUILayout.LabelField("", GUILayout.Width(76));
                Character.upCollider = EditorGUILayout.Toggle(Character.upCollider, GUILayout.Width(90));
                EditorGUILayout.EndHorizontal();



                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("", GUILayout.Width(66));
                Character.LeftColliderDistance = EditorGUILayout.FloatField(Character.LeftColliderDistance, GUILayout.Width(25));
                EditorGUILayout.LabelField("Lewy", GUILayout.Width(38));
                Character.leftCollider = EditorGUILayout.Toggle(Character.leftCollider, GUILayout.Width(16));
                Character.rightCollider = EditorGUILayout.Toggle(Character.rightCollider, GUILayout.Width(16));
                EditorGUILayout.LabelField("Prawy", GUILayout.Width(45));
                Character.RightColliderDistance = EditorGUILayout.FloatField(Character.RightColliderDistance, GUILayout.Width(25));
                EditorGUILayout.EndHorizontal();



                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("", GUILayout.Width(66));
                EditorGUILayout.LabelField("", GUILayout.Width(76));
                Character.downCollider = EditorGUILayout.Toggle(Character.downCollider, GUILayout.Width(90));
                EditorGUILayout.EndHorizontal();


                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("", GUILayout.Width(66));
                EditorGUILayout.LabelField("", GUILayout.Width(66));
                EditorGUILayout.LabelField("Dolny", GUILayout.Width(40));
                Character.DownColliderDIstance = EditorGUILayout.FloatField(Character.DownColliderDIstance, GUILayout.Width(25));
                EditorGUILayout.EndHorizontal();


                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("", GUILayout.Width(66));
                EditorGUILayout.LabelField("L.dolny", GUILayout.Width(50));

                EditorGUILayout.Toggle(Character.downLeftCollider, GUILayout.Width(25));
                EditorGUILayout.LabelField("", GUILayout.Width(20));
                EditorGUILayout.Toggle(Character.downRightCollider, GUILayout.Width(25));
                EditorGUILayout.LabelField("P.dolny", GUILayout.Width(50));

                EditorGUILayout.EndHorizontal();

             

                GUILayout.Space(25);

                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.LabelField("Postać spada:", GUILayout.Width(85));
                EditorGUILayout.Toggle(Character.isFalling, GUILayout.Width(16));


                EditorGUILayout.LabelField("Platforma:", GUILayout.Width(85));
                Character.platformCollider = EditorGUILayout.Toggle(Character.platformCollider, GUILayout.Width(16));
                EditorGUILayout.LabelField("Przeciwnik:", GUILayout.Width(65));
                Character.enemyCollider = EditorGUILayout.Toggle(Character.enemyCollider, GUILayout.Width(16));
                EditorGUILayout.EndHorizontal();



                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Idzie w lewo:", GUILayout.Width(85));
                Character.AIGoLeft = EditorGUILayout.Toggle(Character.AIGoLeft, GUILayout.Width(16));

                EditorGUILayout.LabelField("Idzie w prawo:", GUILayout.Width(85));
                Character.AIGoRight = EditorGUILayout.Toggle(Character.AIGoRight, GUILayout.Width(16));
                

                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Idzie:", GUILayout.Width(85));
                Character.isAIGoing = EditorGUILayout.Toggle(Character.isAIGoing, GUILayout.Width(16));
                EditorGUILayout.EndHorizontal();

                GUILayout.Space(10);

                Character.TdownCollider = (Toggle)EditorGUILayout.ObjectField("TdownCollider", Character.TdownCollider, typeof(Toggle), true);
                Character.TleftCollider = (Toggle)EditorGUILayout.ObjectField("TleftCollider", Character.TleftCollider, typeof(Toggle), true);
                Character.TrightCollider = (Toggle)EditorGUILayout.ObjectField("TrightCollider", Character.TrightCollider, typeof(Toggle), true);
                Character.TrightUpCollider = (Toggle)EditorGUILayout.ObjectField("TrightUpCollider", Character.TrightUpCollider, typeof(Toggle), true);
                Character.TleftUpCollider = (Toggle)EditorGUILayout.ObjectField("TleftUpCollider", Character.TleftUpCollider, typeof(Toggle), true);
                Character.TdownRightCollider = (Toggle)EditorGUILayout.ObjectField("TdownRightCollider", Character.TdownRightCollider, typeof(Toggle), true);
                Character.TdownLeftCollider = (Toggle)EditorGUILayout.ObjectField("TdownLeftCollider", Character.TdownLeftCollider, typeof(Toggle), true);
                Character.TenemyCollider = (Toggle)EditorGUILayout.ObjectField("TenemyCollider", Character.TenemyCollider, typeof(Toggle), true);
               
               
            }


        ShowOthers = EditorGUILayout.Foldout(ShowDetectors, "Inne:");
        if (ShowDetectors)
            if (Selection.activeTransform)
            {
                Character.CoinsText = (Text)EditorGUILayout.ObjectField("Obiekt tekst monety", Character.CoinsText, typeof(Text), true);
            }
    }
}










[CustomEditor(typeof(RotateObject))]
public class ObjectBuilderEditor : Editor
{

    
    public override void OnInspectorGUI()
    {

        GUIStyle HeaderStyle = new GUIStyle();
        HeaderStyle.fontSize = 15;
        HeaderStyle.fontStyle = FontStyle.Bold;
        
        
        RotateObject RotationObject = (RotateObject)target;
        GUILayout.Space(10);
        EditorGUILayout.LabelField("Właściwości obrotu:", HeaderStyle, GUILayout.Height(20));
        RotationObject.RotationSpeed = EditorGUILayout.Slider("Prędkość obrotu:", RotationObject.RotationSpeed, 0, 50);
        RotationObject.turnRight = EditorGUILayout.Toggle("Obrót w prawo", RotationObject.turnRight);
        RotationObject.turnLeft = EditorGUILayout.Toggle("Obrót w lewo", RotationObject.turnLeft);
       
    }
}
[CustomEditor(typeof(ScaleObject))]
public class ObjectBuilderEditor2 : Editor
{
    public override void OnInspectorGUI()
    {
        GUIStyle HeaderStyle = new GUIStyle();
        HeaderStyle.fontSize = 15;
        HeaderStyle.fontStyle = FontStyle.Bold;

        DrawDefaultInspector();
        ScaleObject ScaleObject = (ScaleObject)target;
        GUILayout.Space(10);
        EditorGUILayout.LabelField("Właściwości skalowania:", HeaderStyle, GUILayout.Height(20));
        ScaleObject.ScallingSpeed = EditorGUILayout.Slider("Prędkość skalowania:", ScaleObject.ScallingSpeed, 0, 0.15f);
        EditorGUILayout.LabelField("Minimalna wielkość:", ScaleObject.min.ToString("0.00"));
        EditorGUILayout.LabelField("Maksymalna wielkość:", ScaleObject.max.ToString("0.00"));
        EditorGUILayout.MinMaxSlider(ref ScaleObject.min, ref ScaleObject.max, 0, 2);
     
        if (GUILayout.Button("Zeruj ustawienia"))
        {
            ScaleObject.min = 0.8f;
            ScaleObject.max = 1.2f;
            ScaleObject.ScallingSpeed = 0;
        }
    }
}
