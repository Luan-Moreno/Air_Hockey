using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int PlayerScore1 = 0;
    public static int PlayerScore2 = 9;
    public GUISkin layout;
    private GameObject theBall;

    // Posição do botão no MUNDO DO JOGO.
    public Vector2 resetButtonWorldPosition = new Vector2(10, 5);

    // Tamanho do botão (ainda em pixels de TELA).
    public Vector2 buttonSize = new Vector2(120, 40);

    void Start()
    {
        theBall = GameObject.FindGameObjectWithTag("Ball");
        if (theBall == null)
        {
            Debug.LogError("Bola não encontrada! Verifique a tag 'Ball'.");
        }
    }

    public void Score(string wallID)
    {
        if (wallID == "PontoAbaixo")
        {
            PlayerScore2++;
            if (theBall != null)
            {
                theBall.GetComponent<BallControl>().ResetBall(0, -2);
            }
        }
        else if (wallID == "PontoAcima")
        {
            PlayerScore1++;
            if (theBall != null)
            {
                theBall.GetComponent<BallControl>().ResetBall(0, 2);
            }
        }
        Debug.Log("Placar: Jogador Vermelho: " + PlayerScore1 + " | PlayerIA: " + PlayerScore2);
    }

    public void ResetGame()
    {
        PlayerScore1 = 0;
        PlayerScore2 = 0;

        if (theBall != null)
        {
            theBall.GetComponent<BallControl>().ResetBall(0, 0);
        }
    }

    void OnGUI()
    {
        if (layout != null)
        {
            GUI.skin = layout;
        }

        GUIStyle scoreStyle = new GUIStyle(GUI.skin.label);
        scoreStyle.fontSize = 50;
        scoreStyle.alignment = TextAnchor.MiddleCenter;

        GUI.Label(new Rect(Screen.width / 2 - 300, 20, 100, 100), "" + PlayerScore1, scoreStyle);
        GUI.Label(new Rect(Screen.width / 2 + 300, 20, 100, 100), "" + PlayerScore2, scoreStyle);

        GUIStyle winStyle = new GUIStyle(GUI.skin.label);
        winStyle.normal.textColor = Color.red;
        winStyle.fontSize = 50;
        winStyle.alignment = TextAnchor.MiddleCenter;

        GUIStyle winStyle2 = new GUIStyle(GUI.skin.label);
        winStyle2.normal.textColor = Color.blue;
        winStyle2.fontSize = 50;
        winStyle2.alignment = TextAnchor.MiddleCenter;

        if (PlayerScore1 >= 10)
        {
            GUI.Label(new Rect(Screen.width / 2 - 300, 200, 600, 100), "VITÓRIA DO PLAYER", winStyle2);
            if (theBall != null)
            {
                theBall.GetComponent<BallControl>().ResetBall(0, -2);
            }
        }
        else if (PlayerScore2 >= 10)
        {
            GUI.Label(new Rect(Screen.width / 2 - 300, 200, 600, 100), "VITÓRIA DA IA", winStyle);
            if (theBall != null)
            {
                theBall.GetComponent<BallControl>().ResetBall(0, 2);
            }
        }

        Vector2 screenPos = Camera.main.WorldToScreenPoint(resetButtonWorldPosition);
        screenPos.x -= buttonSize.x / 2;
        screenPos.y -= buttonSize.y / 2;
        screenPos.y = Screen.height - screenPos.y;

        if (GUI.Button(new Rect(screenPos.x, screenPos.y, buttonSize.x, buttonSize.y), "Reset"))
        {
            ResetGame();
        }
    }
}