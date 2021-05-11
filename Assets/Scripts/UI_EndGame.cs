using UnityEngine;
using UnityEngine.UI;
public class UI_EndGame : MonoBehaviour
{
    public Text message;

    GameManager gm;

    public PlayerController pc;

    private void OnEnable()
    {
        gm = GameManager.GetInstance();

        message.text = "Você Ganhou!!!";
    }

    public void Voltar()
    {
        gm.ChangeState(GameManager.GameState.MENU);
        pc.Reset();
    }
}