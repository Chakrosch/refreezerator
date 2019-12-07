using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.IO;
public class PlayButton : MonoBehaviour
{
    
    public Button playButton;
    
// Start is called before the first frame update
    void Start () {
        Button btn = playButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick(){
      
        SceneManager.LoadScene("TestScene_Moritz", LoadSceneMode.Single);
      
    }
   
    
    //Highscore mechanic; execute on player death or return to safety
    [MenuItem("Tools/Write file")]
    static void WriteString()
    {
        string path = "Assets/highscore.txt";

        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine("Test");
        writer.Close();

        
    }

    [MenuItem("Tools/Read file")]
    static void ReadString()
    {
        string path = "Assets/highscore.txt";

        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path); 
        Debug.Log(reader.ReadToEnd());
        reader.Close();
    }
}
