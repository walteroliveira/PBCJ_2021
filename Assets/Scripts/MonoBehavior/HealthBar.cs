/// <summary>
/// Script que controla a barra de vida do personagem
/// Muda o texto da barra de vida e o quanto ela está preenchida de acordo com a vida do player
/// </summary>
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public DamagePoints damagePoints;       // Objeto de leitura dos dados de quantos pontos tem o plauer
    public Player character;                // Receberá o objeto do player
    public Image HPSlide;                     // Receberá a barra de medição
    public Text HPText;                     // Receberá os dados de HP
    float MaxDamagePoints;                  // Armaena a quantidade limite de vida do player

    // Start is called before the first frame update
    void Start()
    {
        MaxDamagePoints = character.MaxDamagePoints; // Maximo de vida da barra de vida
    }

    // Update is called once per frame
    void Update()
    {
        if (character != null)      // Se o personagem não for nulo
        {
            HPSlide.fillAmount = damagePoints.value / MaxDamagePoints;  // Preenche o slide de vida com a porcentagem da vida máxima que o player tem   
            HPText.text = "HP: " + (HPSlide.fillAmount * 100);          // Atualiza o texto com a vida do personagem
        }
    }
}
