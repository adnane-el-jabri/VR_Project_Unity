using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneHandler : MonoBehaviour
{
  public void GoToAParfait()
  {
      SceneManager.LoadScene(1); // We can set up the indexes of the scenes in (File > build settings)
  }

  public void GoToAPleurer()
  {
      SceneManager.LoadScene(2); // We can set up the indexes of the scenes in (File > build settings)
  }

  public void GoToNatureMorte()
  {
      SceneManager.LoadScene(3); // We can set up the indexes of the scenes in (File > build settings)
  }
}
