using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Necesario para trabajar con UI


public class EjercicioAhorcadoUI : MonoBehaviour
{
    public GameObject[] partesMonigote;
    public InputField inputField;
    public Text textoPalabraOculta;
    public Text textoMensaje;
    public Text textoIntentos;
    public Text textoTematica;
    public Button botonLetra;
    public Button botonPalabra;
    public Button botonReiniciar;


    string tematica;
    string palabraSeleccionada;
    string palabraOculta;
    int intentos;

    string[] animales = { "capibara", "perro", "gato", "pajaro", "elefante", "tigre", "leon" };
    string[] Lol = { "nami", "urgot", "azir", "bardo", "leona", "diana", "lucian","ahri", "viktor", "thresh", "ashe", "tryndamere", "malphite", "briar", "jinx", "vi", "brand", "teemo", "garen" };
    string[] frutas = { "manzana", "banana", "naranja", "fresa", "kiwi", "mango", "piña", "sandía" };
    string[] paises = { "argentina", "brasil", "chile", "colombia", "peru", "mexico", "francia", "alemania", "italia", "españa", "china", "japon", "australia" };
    string[] colores = { "rojo", "azul", "verde", "amarillo", "cian", "morado", "naranja", "rosa", "magenta", "gris", "negro", "blanco" };
    


    void Start()
    {
        ReiniciarJuego();
    }

    public void ReiniciarJuego()
    {
        intentos = 5;
        tematica = new string[]{"animales", "Lol", "frutas", "paises", "colores"}[Random.Range(0, 5)]; // Selecciona una temática aleatoria
        //palabraSeleccionada = animales[Random.Range(0, animales.Length)];
        palabraSeleccionada = tematica switch
        {
            "animales" => animales[Random.Range(0, animales.Length)],
            "Lol" => Lol[Random.Range(0, Lol.Length)],
            "frutas" => frutas[Random.Range(0, frutas.Length)],
            "paises" => paises[Random.Range(0, paises.Length)],
            "colores" => colores[Random.Range(0, colores.Length)],
            _ => "error"
        };
        palabraOculta = new string('*', palabraSeleccionada.Length);
        MostrarEstado("Comienza la partida. Adivina la palabra.");
        ActualizarUI();
        foreach (GameObject parte in partesMonigote)
        {
            parte.SetActive(false);
        }
    }

    public void ComprobarLetra()
    {
        if (intentos <= 0)
        {
            MostrarEstado("Has perdido, reinicia para volver a empezar");
            return;
        }

        string entrada = inputField.text.ToLower();
        if (entrada.Length != 1)
        {
            MostrarEstado("Por favor, ingresa solo una letra.");
            return;
        }

        char letra = entrada[0];
        bool letraAcertada = false;
        char[] letrasOcultas = palabraOculta.ToCharArray();

        for (int i = 0; i < palabraSeleccionada.Length; i++)
        {
            if (palabraSeleccionada[i] == letra)
            {
                letrasOcultas[i] = letra;
                letraAcertada = true;
            }
        }

        palabraOculta = new string(letrasOcultas);
        if (letraAcertada)
        {
            MostrarEstado("¡Letra correcta!");
        }
        else
        {
            intentos--;
            MostrarEstado("Letra incorrecta.");
            MostrarParteMonigote();
        }

        inputField.text = "";
        ActualizarUI();
        
        if (palabraOculta == palabraSeleccionada)
        {
            MostrarEstado("¡Has adivinado toda la palabra!");
        }
        else if (intentos < 1)
        {
            MostrarEstado("Has perdido. La palabra era: " + palabraSeleccionada);
        }
    }

    public void ComprobarPalabra()
    {
        if (intentos <= 0)
        {
            MostrarEstado("El juego ha terminado. Reinicia para volver a jugar.");
            return;
        }

        string entrada = inputField.text.ToLower();

        if (entrada == palabraSeleccionada)
        {
            palabraOculta = palabraSeleccionada;
            MostrarEstado("¡Has adivinado la palabra completa!");
        }
        else
        {
            intentos--;
            MostrarEstado("Palabra incorrecta.");
            MostrarParteMonigote();
        }

        inputField.text = "";
        ActualizarUI();

        if (palabraOculta == palabraSeleccionada)
        {
            MostrarEstado("¡Has ganado!");
        }
        else if (intentos < 1)
        {
            MostrarEstado("Has perdido. La palabra era: " + palabraSeleccionada);
        }
    }

    void ActualizarUI()
    {
        textoPalabraOculta.text = "Palabra: " + palabraOculta;
        textoIntentos.text = "Intentos: " + intentos;
        textoTematica.text = "Temática: " + tematica.ToUpper();
    }

    void MostrarEstado(string mensaje)
    {
        textoMensaje.text = mensaje;
    }
    
    void MostrarParteMonigote()
    {
        int parteIndex = 4 - intentos; // Asumiendo que tienes 5 partes del monigote (0 a 4)
        if (parteIndex >= 0 && parteIndex < partesMonigote.Length)
        {
            partesMonigote[parteIndex].SetActive(true);
        }
    }
    
    public void SalirDelJuego()
    {
        Application.Quit();
        Debug.Log("El juego se ha cerrado"); // Solo visible en el editor
    }




}










































