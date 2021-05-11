using UnityEngine;
using UnityEngine.UI;
public class UI_MainMenu : MonoBehaviour
{

    GameManager gm;

    private void OnEnable()
    {
        gm = GameManager.GetInstance();
    }
    
    public void Comecar()
    {
        gm.ChangeState(GameManager.GameState.CONFIGURATION);
    }
}