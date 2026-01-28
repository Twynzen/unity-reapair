# Guia Completa: Unity3D + Colaboracion con IA
## Prueba Tecnica de Tutor - Analisis, Correccion y Metodologia de Ensenanza

---

## PARTE 1: RESPUESTAS AL TEST DE CONOCIMIENTOS UNITY3D

### Pregunta 1: Que barra de herramientas enumera todos los componentes del objeto?
**Respuesta: Inspector**

El Inspector es la ventana que muestra TODOS los componentes adjuntos al GameObject seleccionado. Cuando seleccionas un objeto en la Hierarchy o Scene, el Inspector muestra su Transform, scripts, colliders, renderers, etc.

- **Hierarchy**: Lista todos los GameObjects de la escena en forma de arbol
- **Scene**: Vista 3D/2D del entorno de juego
- **Console**: Muestra mensajes de log, warnings y errores
- **Inspector**: Muestra y permite editar TODOS los componentes del objeto seleccionado

> **Documentacion**: [Unity Inspector](https://docs.unity3d.com/Manual/UsingTheInspector.html)

---

### Pregunta 2: Que parametro del componente Transformar afecta al tamano del objeto?
**Respuesta: Scale**

El componente Transform tiene exactamente 3 parametros:
- **Position**: Ubicacion en el espacio (x, y, z)
- **Rotation**: Orientacion/giro del objeto (en grados Euler)
- **Scale**: Tamano del objeto. Scale(1,1,1) = tamano original. Scale(2,2,2) = doble de tamano

> **Documentacion**: [Transform Component](https://docs.unity3d.com/ScriptReference/Transform.html)

---

### Pregunta 3: Que grupo de componentes se utiliza para calcular las colisiones entre objetos?
**Respuesta: Collider**

Los Colliders son los componentes que definen la forma fisica del objeto para deteccion de colisiones:
- **BoxCollider**: Caja rectangular
- **SphereCollider**: Esfera
- **CapsuleCollider**: Capsula
- **MeshCollider**: Sigue la forma del mesh 3D
- **CharacterController**: Collider especial para personajes (hereda de Collider)

Nota: **Rigidbody** maneja la FISICA (gravedad, fuerzas, masa), pero no calcula colisiones por si solo. Necesita un Collider adjunto.

> **Documentacion**: [Collider Component](https://docs.unity3d.com/Manual/CollidersOverview.html)

---

### Pregunta 4: Que pasaria si desactivaramos un objeto en el inspector?
**Respuesta: El objeto se comportara como si no existiera en la escena**

Al desmarcar la casilla de activacion (checkbox superior izquierda del Inspector):
- `SetActive(false)` desactiva el GameObject completamente
- No se renderiza, no ejecuta scripts, no participa en fisica
- Sus hijos tambien se desactivan
- Sigue existiendo en memoria pero no "vive" en la escena

> **Documentacion**: [GameObject.SetActive](https://docs.unity3d.com/ScriptReference/GameObject.SetActive.html)

---

### Pregunta 5: Puede un objeto tener varios componentes identicos anadidos?
**Respuesta: Si**

Un GameObject puede tener multiples instancias del mismo componente. Ejemplo comun: multiples AudioSource para reproducir varios sonidos simultaneamente, o multiples Colliders para formas fisicas complejas.

Excepcion: Algunos componentes como Transform solo permiten UNO por objeto.

---

### Pregunta 6: Como se llama la estructura que utiliza Unity3D para almacenar vectores?
**Respuesta: Vector3**

Unity usa `Vector3` para representar posiciones, direcciones y escalas en espacio 3D:
```csharp
Vector3 posicion = new Vector3(1.0f, 2.0f, 3.0f); // x, y, z
Vector3.zero;     // (0, 0, 0)
Vector3.forward;  // (0, 0, 1)
Vector3.up;       // (0, 1, 0)
```

Tambien existe `Vector2` para 2D y `Vector4` para casos especiales.

> **Documentacion**: [Vector3](https://docs.unity3d.com/ScriptReference/Vector3.html)

---

### Pregunta 7: Cual es el nombre de la clase que se utiliza para gestionar las entradas del usuario?
**Respuesta: Input**

La clase `Input` gestiona TODA la entrada del usuario:
```csharp
Input.GetAxis("Horizontal");      // Teclado WASD/Flechas (-1 a 1)
Input.GetMouseButtonDown(0);       // Click izquierdo
Input.GetKeyDown(KeyCode.Space);   // Tecla espacio
Input.GetAxis("Mouse X");          // Movimiento del mouse
```

> **Documentacion**: [Input Class](https://docs.unity3d.com/ScriptReference/Input.html)

---

### Pregunta 8: Como podemos marcar un campo para que se muestre en el inspector?
**Respuesta: public Y [SerializeField]**

Dos formas de mostrar un campo en el Inspector:
```csharp
public float speed = 5f;              // Visible por ser public
[SerializeField] private float hp;    // Visible por SerializeField (mantiene encapsulamiento)
private float hidden;                 // NO visible (private sin SerializeField)
protected float alsoHidden;           // NO visible (protected sin SerializeField)
```

**Buena practica**: Usar `[SerializeField] private` para mantener encapsulacion pero permitir configuracion desde el editor.

> **Documentacion**: [SerializeField](https://docs.unity3d.com/ScriptReference/SerializeField.html)

---

### Pregunta 9: Que le ocurre a un objeto que aloja un script con Destroy(this) en Start()?
**Respuesta: El componente script se autodestruira**

`Destroy(this)` donde `this` es el script MonoBehaviour:
- Destruye SOLO el componente script, no el GameObject
- El GameObject sigue existiendo con sus otros componentes
- Si fuera `Destroy(gameObject)` destruiria el objeto completo

**Este es exactamente el Error #6 que encontramos en el proyecto!**

> **Documentacion**: [Object.Destroy](https://docs.unity3d.com/ScriptReference/Object.Destroy.html)

---

### Pregunta 10: Que funcion nos permite crear un objeto a partir del codigo del juego?
**Respuesta: Instantiate**

```csharp
// Crear una copia del prefab
GameObject clon = Instantiate(prefab);

// Crear con posicion y rotacion especificas
GameObject clon = Instantiate(prefab, posicion, rotacion);
```

Esto es exactamente lo que usa nuestro PlayerController para crear balas:
```csharp
GameObject buf = Instantiate(bullet);
```

> **Documentacion**: [Object.Instantiate](https://docs.unity3d.com/ScriptReference/Object.Instantiate.html)

---

### Pregunta 11: La funcion Start es llamada ________?
**Respuesta: Una vez**

Ciclo de vida de un MonoBehaviour:
1. `Awake()` - Al crear el objeto (una vez)
2. `OnEnable()` - Al activarse
3. `Start()` - Antes del primer Update (UNA VEZ)
4. `Update()` - Cada frame (muchas veces, ~60/seg)
5. `FixedUpdate()` - Cada paso de fisica (~50/seg)
6. `OnDisable()` / `OnDestroy()` - Al desactivar/destruir

> **Documentacion**: [Execution Order](https://docs.unity3d.com/Manual/ExecutionOrder.html)

---

### Pregunta 12: Vector3 pos = ???? para almacenar la posicion del objeto
**Respuesta: transform.position**

```csharp
Vector3 pos = transform.position;  // Posicion mundial del objeto
```

`transform` es la referencia al componente Transform del GameObject actual. `.position` retorna un Vector3 con las coordenadas (x, y, z) en el espacio mundial.

---

### Pregunta 13: Tipo de imagen para el componente de imagen
**Respuesta: Sprite**

En Unity, las imagenes para UI deben ser importadas como **Sprite**:
- En el Inspector del asset de imagen: Texture Type = "Sprite (2D and UI)"
- El componente `Image` de UI requiere un Sprite, no una textura raw

> **Documentacion**: [Sprites](https://docs.unity3d.com/Manual/Sprites.html)

---

### Pregunta 14: Que accion realiza ScreenPointToRay?
**Respuesta: Dispara un haz especial en un punto especifico para leer la informacion**

```csharp
Ray ray = camera.GetComponent<Camera>().ScreenPointToRay(
    new Vector3(Screen.width / 2, Screen.height / 2, 0)
);
```

Esta linea:
1. Obtiene la camara del objeto
2. Crea un **Ray** (rayo) desde el centro exacto de la pantalla hacia la escena 3D
3. El Ray puede usarse con `Physics.Raycast()` para detectar que objetos hay en esa direccion

Se usa comunmente para: apuntar armas, seleccionar objetos, detectar superficies.

> **Documentacion**: [Camera.ScreenPointToRay](https://docs.unity3d.com/ScriptReference/Camera.ScreenPointToRay.html)

---

### Pregunta 15: La luz esferica que irradia en todas direcciones es...
**Respuesta: Point Light**

Tipos de luz en Unity:
- **Point Light**: Esfera que irradia en TODAS direcciones (como una bombilla)
- **Spot Light**: Cono de luz (como una linterna)
- **Directional Light**: Luz paralela infinita (como el sol)
- **Area Light**: Luz de superficie rectangular (solo baked)

> **Documentacion**: [Lighting Types](https://docs.unity3d.com/Manual/Lighting.html)

---

## PARTE 2: ANALISIS TECNICO DEL PROYECTO

### 2.1 Estructura del Proyecto

```
Assets/
|-- Bullet.cs                    <- Script de la bala (2 errores)
|-- PlayerController.cs          <- Script principal del jugador (5 errores)
|-- PlayerLook.cs                <- Script de camara FPS (1 error)
|-- Game.unity                   <- Escena principal (7 errores)
|-- bullet.prefab                <- Prefab de la bala
|-- New Terrain.asset            <- Terreno del nivel
|-- Materials/                   <- Materiales del juego
|-- Kenney Character Assets/     <- Modelos de personajes
|-- Weapons/                     <- Modelos de armas
|-- TerrainTextures/             <- Texturas del terreno
|-- TerrainLayers/               <- Capas del terreno
|-- space-kit-1.0/               <- Pack de assets espaciales
```

### 2.2 Arquitectura del Juego

```
[Player GameObject]
  |-- Transform (posicion, rotacion)
  |-- CharacterController (movimiento + colision)
  |-- PlayerController.cs (logica del juego)
  |-- PlayerLook.cs (control de camara)
  |-- [Camera hijo]
  |-- [Rifle/Arms hijo]
      |-- rifleStart (punto de disparo)

[Enemy GameObjects x3]
  |-- Transform
  |-- CharacterController (collider)
  |-- Modelo 3D (characterSmall.fbx + skin alienA)

[Canvas UI]
  |-- HpText (muestra vida)
  |-- GameOver Panel (derrota)
  |-- Victory Panel (victoria)

[Bullet Prefab]
  |-- Transform
  |-- Bullet.cs (movimiento + deteccion)
  |-- Modelo 3D (ammo_machinegun)
```

### 2.3 Flujo del Juego

```
INICIO
  |-> Start(): Inicializar salud a 100, obtener CharacterController
  |-> Update() cada frame:
  |     |-> Leer WASD -> Mover jugador
  |     |-> Leer Mouse -> Rotar camara (PlayerLook)
  |     |-> Click izquierdo -> Instanciar bala
  |     |     |-> Bala viaja en direccion forward
  |     |     |-> Si bala toca Enemy -> Destruir ambos
  |     |-> Detectar proximidad:
  |     |     |-> Heal -> +50 HP
  |     |     |-> Enemy -> Game Over
  |     |     |-> Finish -> Victoria
  |     |-> Repetir
  |
  V
FIN: Victoria (3 enemigos eliminados) o Game Over (contacto enemigo)
```

---

## PARTE 3: DETALLE DE CADA ERROR Y SU CORRECCION

### Error #1: using System.Security.Cryptography (Bullet.cs:1)

**Antes:**
```csharp
using System.Security.Cryptography;  // Criptografia? En un juego de disparos?
using UnityEngine;
```

**Despues:**
```csharp
using UnityEngine;  // Solo lo necesario
```

**Concepto para ensenar:** Los `using` statements importan namespaces. Solo importa lo que necesitas. Los namespaces organizan el codigo en grupos logicos (UnityEngine para el motor, System para funcionalidades .NET).

---

### Error #2: Bloque de colision vacio (Bullet.cs:22-25)

**Antes:**
```csharp
if (item.tag == "Enemy")
{
    // ... silencio absoluto ...
}
```

**Despues:**
```csharp
if (item.tag == "Enemy")
{
    Destroy(item.gameObject);  // Elimina al enemigo
    Destroy(gameObject);       // Elimina la bala
    return;                    // Sale del loop (importante!)
}
```

**Concepto para ensenar:**
- `Destroy(item.gameObject)` destruye el enemigo encontrado por el collider
- `Destroy(gameObject)` destruye la bala misma (el gameObject que contiene este script)
- `return` es crucial: sin el, el foreach seguiria iterando sobre colliders que ya no existen
- `Physics.OverlapSphere()` detecta colliders en una esfera de radio dado

---

### Error #3 y #4: Imports erroneos (PlayerController.cs:3-4)

**Antes:**
```csharp
using System.Security.Cryptography;  // No se usa
using UnityEditor;                   // ROMPE LA BUILD!
```

**Despues:**
```csharp
// Eliminados ambos
```

**Concepto para ensenar:**
- `UnityEditor` es el error mas critico: solo existe en el editor, NO en builds
- Al hacer Build -> Windows, el compilador no encuentra UnityEditor y falla
- Si necesitas codigo solo-editor, usa directivas de preprocesador:
```csharp
#if UNITY_EDITOR
using UnityEditor;
#endif
```

---

### Error #5: Salud inicial en 0 (PlayerController.cs:17)

**Antes:**
```csharp
public float health = 0;  // Naces muerto
```

**Despues:**
```csharp
public float health = 100;  // Naces con vida completa
```

**Concepto para ensenar:**
- Los valores iniciales de campos publicos pueden ser sobreescritos por el Inspector
- En la escena, el valor serializado era `health: 0`, por lo que se corrigio tambien ahi
- `ChangeHealth(0)` en Start() no cambia el valor, solo actualiza el UI Text

---

### Error #6: Destroy(this) en Start() (PlayerController.cs:21)

**Antes:**
```csharp
void Start()
{
    Destroy(this);        // Destruye el script INMEDIATAMENTE
    ChangeHealth(0);      // Esto nunca se ejecuta realmente con efecto
}
```

**Despues:**
```csharp
void Start()
{
    characterController = GetComponent<CharacterController>();
    ChangeHealth(0);      // Inicializa el texto de HP
}
```

**Concepto para ensenar:**
- `Destroy(this)` donde `this` = MonoBehaviour -> destruye el COMPONENTE script
- `Destroy(gameObject)` -> destruye el OBJETO completo
- `GetComponent<T>()` busca un componente de tipo T en el mismo GameObject
- Start() se ejecuta una sola vez, ideal para inicializacion

---

### Error #7: Codigo faltante - Movimiento (PlayerController.cs)

**Codigo agregado:**
```csharp
[SerializeField] private float moveSpeed = 5f;
private CharacterController characterController;

// En Update():
float moveX = Input.GetAxis("Horizontal");   // A/D o flechas
float moveZ = Input.GetAxis("Vertical");     // W/S o flechas
Vector3 movement = transform.right * moveX + transform.forward * moveZ;

if (characterController != null)
{
    characterController.Move(movement * moveSpeed * Time.deltaTime);
}
```

**Concepto para ensenar:**
- `Input.GetAxis("Horizontal/Vertical")` retorna -1 a 1 de forma suave
- `transform.right` y `transform.forward` son vectores direccionales del jugador
- Se combinan para crear movimiento relativo a donde mira el jugador
- `CharacterController.Move()` mueve respetando colisiones y terreno
- `Time.deltaTime` hace el movimiento independiente del framerate

---

### Error #8: xAxisClamp usa eje equivocado (PlayerLook.cs:20)

**Antes:**
```csharp
xAxisClamp -= rotateX;  // Trackea mouse HORIZONTAL para clamp VERTICAL??
```

**Despues:**
```csharp
xAxisClamp -= rotateY;  // Trackea mouse VERTICAL para clamp VERTICAL
```

**Concepto para ensenar:**
- El "clamp" limita la rotacion vertical para que no puedas mirar 360 grados
- rotateX = movimiento horizontal del mouse (girar izquierda/derecha)
- rotateY = movimiento vertical del mouse (mirar arriba/abajo)
- El clamp debe seguir el eje VERTICAL (-90 a 90 grados)

---

### Errores #9-#15: Errores de escena (Game.unity)

Estos errores son en la configuracion de la escena, no en el codigo:

| # | Error | Correccion |
|---|-------|-----------|
| 9 | mouseSense = 0 | Cambiar a 2 |
| 10 | bullet prefab sin asignar | Asignar bullet.prefab |
| 11 | CharacterController del player desactivado | Activar |
| 12 | Enemy (1) tag "Untagged" | Cambiar a "Enemy" |
| 13 | Enemy (2) tag "Untagged" | Cambiar a "Enemy" |
| 14 | Enemy CharacterController desactivado | Activar |
| 15 | Enemy (2) CharacterController desactivado | Activar |

---

## PARTE 4: METODOLOGIA DE ENSENANZA CON IA

### 4.1 El Paradigma: "Code Review Colaborativo con IA"

La metodologia que usamos para resolver este proyecto representa un nuevo enfoque pedagogico:

**El estudiante NO solo corrige codigo. Aprende a:**
1. Describir problemas a una IA con precision tecnica
2. Analizar el output de la IA criticamente
3. Validar las correcciones en el entorno real (Unity)
4. Documentar el proceso de depuracion

### 4.2 Flujo de Trabajo Estudiante + Claude Code

```
PASO 1: EXPLORAR
  Estudiante: "Analiza la estructura del proyecto Unity"
  Claude: [Lee archivos, analiza scripts, mapea dependencias]
  Resultado: Diagrama completo de la arquitectura

PASO 2: DIAGNOSTICAR
  Estudiante: "Que errores encuentras?"
  Claude: [Compara contra documentacion oficial, detecta anti-patrones]
  Resultado: Lista priorizada de errores con explicaciones

PASO 3: CORREGIR
  Estudiante: "Corrige los scripts"
  Claude: [Modifica archivos con cambios minimos y seguros]
  Estudiante: [Abre Unity, verifica que compila, prueba jugabilidad]

PASO 4: DOCUMENTAR
  Claude: [Genera documentacion tecnica detallada]
  Estudiante: [Valida, ajusta, aporta contexto de su experiencia]

PASO 5: REFLEXIONAR
  Ambos: Que aprendimos? Que patrones se repiten? Como prevenirlo?
```

### 4.3 Ejercicios Propuestos para Estudiantes

#### Ejercicio 1: "Encuentra el Bug" (Nivel Basico)
Dar a los estudiantes un script con UN error (como `Destroy(this)`) y pedirles que:
1. Identifiquen que pasa al ejecutar
2. Expliquen POR QUE falla
3. Lo corrijan
4. Verifiquen en Unity

#### Ejercicio 2: "Code Review con IA" (Nivel Intermedio)
Los estudiantes pegan su codigo en Claude y deben:
1. Formular la pregunta correcta
2. Evaluar si la respuesta de la IA es correcta
3. Aplicar la correccion
4. Explicar el razonamiento al profesor

#### Ejercicio 3: "Extiende el Juego" (Nivel Avanzado)
Despues de corregir, agregar nuevas funcionalidades:
- Sistema de puntuacion
- Municion limitada
- Enemigos que se mueven
- Sonidos de disparo y explosion
- Pantalla de pausa

### 4.4 Conceptos Clave por Script

| Script | Conceptos Unity/C# |
|--------|-------------------|
| Bullet.cs | FixedUpdate, Physics.OverlapSphere, Destroy, Time.deltaTime |
| PlayerController.cs | Input, Instantiate, GetComponent, CharacterController, SerializeField, Tags |
| PlayerLook.cs | Quaternion.Euler, eulerAngles, Cursor.lockState, Clamp de rotacion |

### 4.5 Errores Pedagogicos Comunes y Como la IA Ayuda a Detectarlos

| Error Comun del Estudiante | Como Claude lo Detecta |
|---------------------------|----------------------|
| `using` innecesarios | Analiza dependencias reales del codigo |
| `Destroy(this)` vs `Destroy(gameObject)` | Conoce la API de Unity y sus sutilezas |
| `UnityEditor` en builds | Sabe que namespaces son solo-editor |
| Tags no asignados | Puede leer archivos .unity (YAML) y verificar |
| Componentes desactivados | Analiza campos m_Enabled en archivos de escena |
| Valores iniciales incorrectos | Traza el flujo logico desde Start() |

---

## PARTE 5: PASOS PARA EL ESTUDIANTE EN SU PC

### Lo que TU (en tu PC con Unity) debes verificar:

1. **Abrir el proyecto** en Unity (version 2021.3+ recomendada)

2. **Verificar compilacion**:
   - La Console de Unity no debe mostrar errores rojos
   - Si hay errores, revisar que los 3 scripts estan bien guardados

3. **Verificar la escena Game.unity**:
   - Seleccionar el Player -> Inspector -> Verificar:
     - PlayerController tiene bullet.prefab asignado
     - CharacterController esta activado (checkbox marcada)
     - health = 100
     - moveSpeed = 5
   - Seleccionar cada Enemy -> Verificar:
     - Tag = "Enemy" (dropdown superior del Inspector)
     - CharacterController activado
   - Seleccionar el objeto con PlayerLook -> Verificar:
     - mouseSense = 2

4. **Probar el juego** (Play):
   - WASD para mover
   - Mouse para mirar
   - Click izquierdo para disparar
   - Eliminar los 3 aliens

5. **Build para Windows**:
   - File -> Build Settings -> Windows -> Build
   - Si compila sin errores, el proyecto esta completo

### Checklist final:
- [ ] Los 3 scripts corregidos y sin errores de compilacion
- [ ] El jugador se mueve con WASD
- [ ] La camara responde al mouse
- [ ] Click izquierdo dispara balas
- [ ] Las balas destruyen a los aliens al impactar
- [ ] Los 3 enemigos son eliminables
- [ ] El juego compila para Windows sin errores
- [ ] ERRORS_LIST.txt incluido en el proyecto

---

## PARTE 6: REFERENCIAS DE DOCUMENTACION OFICIAL

### Documentacion Unity Usada en Este Proyecto:

1. **MonoBehaviour Lifecycle**: https://docs.unity3d.com/Manual/ExecutionOrder.html
2. **Input System**: https://docs.unity3d.com/ScriptReference/Input.html
3. **Physics.OverlapSphere**: https://docs.unity3d.com/ScriptReference/Physics.OverlapSphere.html
4. **Object.Destroy**: https://docs.unity3d.com/ScriptReference/Object.Destroy.html
5. **Object.Instantiate**: https://docs.unity3d.com/ScriptReference/Object.Instantiate.html
6. **CharacterController**: https://docs.unity3d.com/ScriptReference/CharacterController.html
7. **CharacterController.Move**: https://docs.unity3d.com/ScriptReference/CharacterController.Move.html
8. **Transform**: https://docs.unity3d.com/ScriptReference/Transform.html
9. **Vector3**: https://docs.unity3d.com/ScriptReference/Vector3.html
10. **Quaternion.Euler**: https://docs.unity3d.com/ScriptReference/Quaternion.Euler.html
11. **SerializeField**: https://docs.unity3d.com/ScriptReference/SerializeField.html
12. **Camera.ScreenPointToRay**: https://docs.unity3d.com/ScriptReference/Camera.ScreenPointToRay.html
13. **Tags**: https://docs.unity3d.com/Manual/Tags.html
14. **Building for Windows**: https://docs.unity3d.com/Manual/BuildSettings.html
15. **Colliders Overview**: https://docs.unity3d.com/Manual/CollidersOverview.html

---

## PARTE 7: INNOVACION - INTEGRANDO IA EN LA ENSENANZA DE UNITY

### 7.1 Por que usar IA para ensenar Unity?

1. **Deteccion de errores exhaustiva**: La IA puede analizar archivos de escena (.unity YAML) que un humano normalmente no lee manualmente
2. **Explicaciones contextualizadas**: Cada error viene con referencia a documentacion oficial
3. **Velocidad**: 15 errores encontrados y documentados en minutos
4. **Consistencia pedagogica**: Las explicaciones siempre incluyen el "por que", no solo el "que"

### 7.2 Modelo de Clase Propuesto: "Debugging Colaborativo"

**Duracion**: 2 horas

| Tiempo | Actividad | Rol IA | Rol Estudiante |
|--------|-----------|--------|----------------|
| 0-15min | Intro al proyecto roto | N/A | Abrir Unity, observar que falla |
| 15-30min | Pegar scripts en Claude | Responde con errores | Formula preguntas claras |
| 30-60min | Aplicar correcciones | Explica cada cambio | Edita en Unity, verifica |
| 60-80min | Probar jugabilidad | N/A | Play test, reportar bugs |
| 80-100min | Documentar hallazgos | Genera plantilla | Llena con sus palabras |
| 100-120min | Reflexion grupal | N/A | Debate: que aprendimos? |

### 7.3 Competencias Desarrolladas

- **Lectura de codigo**: Entender scripts C# ajenos
- **Debugging sistematico**: Priorizar errores por severidad
- **Pensamiento critico con IA**: Validar respuestas, no confiar ciegamente
- **Documentacion tecnica**: Escribir reportes de errores claros
- **Trabajo colaborativo humano-IA**: Dividir tareas segun fortalezas

### 7.4 Metricas de Evaluacion

| Criterio | Puntos | Descripcion |
|----------|--------|-------------|
| Errores identificados | 0-5 | Cada error correcto vale puntos |
| Correcciones aplicadas | 0-5 | Cada fix funcional vale puntos |
| Documentacion | 0-3 | Claridad y completitud del reporte |
| Uso de IA | 0-2 | Calidad de las preguntas formuladas |
| **Total** | **15** | Minimo 10 para aprobar |

---

*Documento generado colaborativamente entre el tutor y Claude Code (IA)*
*Proyecto: unity-reapair | Branch: claude/unity-project-collaboration-f5mqN*
