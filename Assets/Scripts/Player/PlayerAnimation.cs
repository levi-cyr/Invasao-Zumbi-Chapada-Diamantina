using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator playerAnim; // animator do player
    public string[] idleDirections = { "Idle N", "Idle NO", "Idle O", "Idle SO", "Idle S", "Idle SE", "Idle L", "Idle NE" }; // direção do player na animação de idle
    public string[] runDirections = { "Run N", "Run NO", "Run O", "Run SO", "Run S", "Run SE", "Run L", "Run NE" }; // direção do player na animação de run

    int lastDirection; // variavel para guardar a ultima direção que seu personagem estava olhando (assim qnd vc para de se movimentar ele para na ultima movimentação bonitin)

    void Start()
    {
        playerAnim = GetComponent<Animator>();
    }

    public void SetDirection(Vector2 direction)
    {
        string[] directionArray = null;

        if (direction.magnitude < 0.01f) // se a velocidade do vetor de movimento for proxima a 0 (ou menor), o player soltou o input de movimento
        {
            directionArray = idleDirections; // estando parado, ele faz com que a direção seja uma das Idle
        }
        else // se não
        {
            directionArray = runDirections; // em movimento, ele seleciona uma direção que seja correndo
            lastDirection = DirectionToIndex(direction); // e seta a ultima posição para saber qual será o último número selecionado (0 se for norte, 1 se for norte oeste, etc)
        }

        playerAnim.Play(directionArray[lastDirection]); // toca a animação com o mesmo nome de um dos array ai, 
        // entao se vc para de se movimentar enquanto corria para baixo, ele armazenou a animação de idle e vai 
        // tocar ela só pela String de nome
    }

    int DirectionToIndex(Vector2 direction)
    {
        Vector2 norDirection = direction.normalized; // normaliza o vetor para a direção ser padronizada com o tamanho de 1
        float step = 360 / 8; // movimento do player é em 8 direções em um circulo, entao dividimos para poder separar o angulo da animação
        float offset = step / 2; // divide o angulo na metade, se era 45 ali em cima aqui fica 22.5, é para poder alinhar o movimento

        float angle = Vector2.SignedAngle(Vector2.up, norDirection); // calcula o angulo de norte (cima) e a direção do jogador
        angle += offset; // aqui só alinha o angulo por causa do offset ali
        if (angle < 0) // deixa o angulo positivo
        {
            angle += 360;
        }

        float stepCount = angle / step; // divide o angulo atual por 45 para saber qual das 8 direções o player ta
        return Mathf.FloorToInt(stepCount); // arredonda o resultado para baixo, pois se o angulo de 100º é dividido por 45, seria 2.22 (array 2, mas não tem array neste valor, então arredondamos para 2)
    }
}
