1. PlayerController.cs - using System.Security.Cryptography (innecesario)
2. PlayerController.cs - using UnityEditor (impide compilar para Windows)
3. PlayerController.cs - health = 0 (jugador nace muerto)
4. PlayerController.cs - Destroy(this) en Start (destruye el script)
5. PlayerController.cs - Sin código de movimiento WASD
6. Bullet.cs - using System.Security.Cryptography (innecesario)
7. Bullet.cs - Bloque de colisión vacío (no destruye enemigo ni bala)
8. PlayerLook.cs - xAxisClamp -= rotateX (debe ser rotateY)
9. Escena - mouseSense = 0 (cámara no se mueve)
10. Escena - bullet prefab no asignado
11. Escena - Enemigos sin Tag "Enemy"
12. Escena - Player Arms apuntaba a objeto incorrecto
