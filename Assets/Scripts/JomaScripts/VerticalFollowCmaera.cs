using UnityEngine;

public class VerticalFollowCmaera : MonoBehaviour
{
    public Transform target;       // Objetivo a seguir (jugador)
    public Vector3 offset = new Vector3(0, 2, -10); // Offset de la c�mara
    private float highestY;        // Almacena la posici�n Y m�s alta alcanzada
    private float initialX;        // Almacena la posici�n X inicial

    private void Start()
    {
        if (target != null)
        {
            // Guarda la posici�n X inicial de la c�mara
            initialX = transform.position.x;
            // Inicializa con la posici�n Y inicial del jugador + offset
            highestY = target.position.y + offset.y;
        }
    }

    private void LateUpdate()
    {
        if (target != null)
        {
            // Si el jugador sube por encima del valor guardado, actualiza highestY
            if (target.position.y + offset.y > highestY)
            {
                highestY = target.position.y + offset.y;
            }

            // Mueve la c�mara manteniendo la X inicial, usando highestY en Y, y el offset en Z
            transform.position = new Vector3(
                initialX,                   // Mantiene la posici�n X fija
                highestY,                   // Solo sigue el eje Y (m�s alto)
                target.position.z + offset.z // Mantiene el offset en Z
            );
        }
    }
}
