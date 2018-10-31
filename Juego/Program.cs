using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;


/* 
   * Es un programa para adivinar palabras, contiene un diccionario y nos permite añadir palabras nuevas.
   * 
   * Funciones utilizadas:   
   * 
   * Lista de palabras - Creamosuna lista para guardar un numero determinados de objetos en nuestra collecion
   *                     Para nuestra lista utilizamos los métodos:
   *                     .Add() se usa para agregar varios objetos
   *                     .Insert() Inserta un elemento en la Lista en el índice especificado.
   *                     .Sort() Ordena los elementos en la Lista
   *                     
   *                     
   * Palabras al azar - el generador devuelve al azar un elemento de la lista
   *                    .Next() devuelve aleatoriamente un argumento dentro de un intervalo
   *                    .Count() devuelve la cantidad de elemento de una secuencia
   *                    
   *                   
                         
   * Mostrar el desarrollo de la palabra " _a_a " 
   *                    Para mostrar la palabra con los caracteres  " _ " por cada letra, utilizamos un bucle foreach  
   *                    en la palabra para recorrer  en iteración e ir reemplazando la letra por " _ ".
   *                    
   *                    
   * Adivinar palabra - Se comprueba mediante  un foreach  la letra ingresada por el jugador 
   *                    y se compara con la palabra, si el jugador ingresa una letra correcta, entonces se agrega la letra

   * Nueva jugada -    Una vez terminado el juego se les pregunta de volver a jugar.
   * 
   */

public class PalabrasList : List<string> // Clase creada para la lista de objetos
{
}
public class Adivinador
{
    /* 
    * 
    * El usuario debe poder escribir una palabra completa y luego cmnparar con las palabras en la lista del programa
    * 
    */
    
    private static PalabrasList lPalabras;
    private static Random randomR = new Random();

    public static void Main(string[] args)
    { /*Apariencia de la consola */
        Console.ForegroundColor = ConsoleColor.Cyan; //Cambiar color
       // Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "TEXT"));
        
        Console.Title = "Adivinador"; //Titulo consola
        Console.WriteLine("");
        Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "Bienvenido !\n\n\n\n"));     //Saludo

        inicializarPalabrasList();

        #region Menu
        //_______________________START- MENU______________________________________________ 
        int MenuOpciones = 0; //Crear  variable que sera usado 
        while (MenuOpciones != 5) //Bucle que muestra el menu hasta que el jugador hace una seleccion
        {

            Console.Write("\n\t1) Añadir palabras");
            Console.Write("\n\t2) Mostrar diccionario");
            Console.Write("\n\t3) Jugar");
            Console.Write("\n\t4) Créditos\n\n");
            Console.Write("\n\t5) Salir\n\n");

            Console.Write("\n\tSeleccionar 1-5: ");  //Seleccionar menu 

            MenuOpciones = Convert.ToInt32(Console.ReadLine()); //La eleccion del jugador se registra  en
                                                              //una variable creada previamente 
            switch (MenuOpciones) //Switch 
            { /*Opciones 1 - añadir palabra */
                case 1:
                    Console.Clear();//Limpiar la ventana de la consola
                    Console.Write("\n\tAñadir una palabra\n\n");
                    var insertar = Console.ReadLine(); //Leer palabra
                    lPalabras.Add(insertar); //Añadir palabra a la lista
                    Console.Write("\n\tDiccionario\n\n");
                    lPalabras.Sort(); // ordena la palabra recien agregada en la lista
                    foreach (string w in lPalabras)
                        Console.WriteLine(w); //Imprimir para  verificar la palabra que ha sido agregada
                    break;

                /*Opcion 2 - Mostrar diccionario*/
                case 2:
                    Console.Clear();
                    Console.Write("\n\t\t\t\tDiccionario\n\n");
                    foreach (string p in lPalabras) // Impresiones del diccionario
                        Console.WriteLine(p);
                    break;

                /*Opcion 3 - Jugar*/
                case 3:

                    Console.Clear();
                    int numIntentosInt =0; //Crear variable para los intentos
                    while (numIntentosInt == 0)//numeros de conjeturas (letras) 

                    {
                        /* Determina el numero de conjeturas (letras) del jugador*/
                        usuarioElegirIntentos(ref numIntentosInt);
                    }

                    /* selecciona palabras al azar*/
                    string palabra = randomPalabra();


                    /* crear una lista de caracteres mostrados */
                    List<char> letraElegida = new List<char>();
                    bool juegoTerminado = false;
                    while (juegoTerminado == false)
                    {
                        /* muestra cadenas a los jugadores en base a las conjeturas del jugador
                        *Si el juador no pudo adivnar algo "_ _ _ " */
                        string palabrasPantalla = mostrarPalabra(letraElegida, palabra);
                        /*Si la cadena devuelta tiene el caracter "_",
                        * No es correcto el número de letras adivinadas, COMPRABAR......................................
                        * pierde si la variable numIntentosRestantes es menor que 1.******************************************/
                        if (!palabrasPantalla.Contains("_"))
                        {
                            Console.Clear();
                            juegoTerminado = true;
                            Console.WriteLine("Felicidades, Ganaste! La palabra correcta era: " + palabra);
                            /* Comprobar si los jufadores quieren jugar de nuevo, si aceptan jugar de nuevo
                            * se establece la variable como verdadero, esto finalia el bucle.
                            * Si lo s jugadores no quieren volver a jugar, el metodo userReplay cerrara el programa.*/
                            usuarioNuevoJuego();
                        }
                        else if (numIntentosInt <= 0)
                        {
                            juegoTerminado = true;
                            Console.WriteLine("Que mal, perdiste! La palabra correcta era: " + palabra);
                            usuarioNuevoJuego();
                        }
                        else
                        {
                            /* Si el jugador no gana  ni pierde
                            * la palabras y el numero de conjeturas se muestran menos 1.*/
                            comprobarLetra(letraElegida, palabra, palabrasPantalla, ref numIntentosInt);
                        }
                    }

                    break;
                
                case 5: /*Opcion 5 - Terminar el juego*/
                    Console.WriteLine("\n\t\t\t\tPresione una tecla para terminar?\n\n");
                    break;

                case 4: /*Creditos*/
                    Console.Clear();
                    Console.WriteLine(String.Format("\n\t\t\t\t\t\t Créditos\n\n\n\t\t\t\t\t\t  Deza\n\t\t\t\t\t\t  Díaz\n\t\t\t\t\t\t  Melaro\n\t\t\t\t\t\t  Soria\n\n\n\n\n\n"));
                    break;
                default:
                    Console.WriteLine("Error[1]: Tecla equivocada, intenra de nuevo");
                    break;
                    
            }

        }
        #endregion
    }

    //_________________________Lista ____________________________________________ 
    private static void inicializarPalabrasList()
    {
        lPalabras = new PalabrasList();
        lPalabras.Add("pelota");   
        lPalabras.Insert(1, "hola"); 
        lPalabras.Sort();    
    }

    //___________________Seleccionar numero de conjeturas_________________________________________________________ 

    private static void usuarioElegirIntentos(ref int UsuarioNumIntentosInt)
    {
        string numIntentosString = ""; //Contenido vacio
        Console.WriteLine("Ingresa la cantidad de jugadas: "); //Indica a los jugadores que inrgesen el numero de conjeturas
        numIntentosString = Console.ReadLine(); //Carga el numero de conjeturas por jugadores
        try
        {
            UsuarioNumIntentosInt = Convert.ToInt32(numIntentosString); //Convertimos la variable string a entero (int). 
            if (!(UsuarioNumIntentosInt <= 20 & UsuarioNumIntentosInt >= 1))// Exepciones si el jugador abandona el juego
            {            //Numero graande o pequeños de conjeturas ...................................................................... 
                throw new Exception();
            }
        }
        catch (Exception)
        {
            UsuarioNumIntentosInt = -1;
            Console.WriteLine("Error[2]: numero equivocado de intentos");//Si la exepcion es verdadera
        }               //Se muestra mensaje de advertencia
    }

    //__________________PALABRAS AL AZAR_________________________________________________ 

    private static string randomPalabra()
    {
        return lPalabras[randomR.Next(0, lPalabras.Count() - 1)]; //*Listado random de palabras .Count() 
    }

    //________________________MOSTRAR PALABRAS___________________________________________ 

    private static string mostrarPalabra(List<char> letrasAdivinadas, string palabra)
    {
        string palabraMostrada = ""; //Inciar con contenidos vacios
        if (letrasAdivinadas.Count == 0)
        {
            foreach (char letra in palabra) //Mostrar caracter "_" por cada letra 
            {
                palabraMostrada += "_ ";
            }
            return palabraMostrada; //Devuelve valor
        }
        foreach (char letra in palabra)
        {
            bool comprobarLetra = false;//Se crea una variable bool que se usa para verificar
            foreach (char caracteres in letrasAdivinadas)//si el jugador ingreso una letra correcta.
            {
                if (caracteres == letra) //Si el jugador adivina la letra correspondiente
                {   //En la palabra actual, solo la letra en lugar de  "_". 
                    palabraMostrada += caracteres + " ";
                    comprobarLetra = true;//La variable Bool es true si la eleccion es correcta
                    break;
                }
                else
                {
                    comprobarLetra = false; //Si el jugador no adivina, continua; la variable Bool continuara en false.

                } 
            }
            if (comprobarLetra == false) //Si la variable Bool es false, no se mostrara ninguna letra, 
            {       //solo mostrara "_" 
                palabraMostrada += "_ ";
            }
        }
        return palabraMostrada;
    }

    //_____________________________ADIVINAR____________________________________________________________________________ 

    static void comprobarLetra(List<char> verificarCaracter, string palabra, string palabrasPantalla, ref int numIntentosRestantes)
    {
        string letras = "";
        foreach (char letra in verificarCaracter)
        {
            letras += " " + letra;
        }
        Console.WriteLine("Adivina una letra entre la  A-Z");
        Console.WriteLine("Letras Ingresadas: " + letras);
        Console.WriteLine("Jugadas: " + numIntentosRestantes);
        Console.WriteLine(palabrasPantalla);
        string input = Console.ReadLine();
       
        
        char letraIngresada = 'a';
        try
        {
            letraIngresada = Convert.ToChar(input);
            if (!char.IsLetter(letraIngresada))
            {
                throw new Exception();
            }
        }
        catch (Exception)
        {//Exepcion si el jugador escribe mas de 1 caracter o si no es una letra
            Console.WriteLine("Error[3]: Solo una letra a la vez");

        }
        bool repetir = false;
        for (int i = 0; i < verificarCaracter.Count; i++)
        {
            if (verificarCaracter[i] == letraIngresada)
            {//Exepcion si el jugador escribe una letra ya seleccionada
                Console.WriteLine("Error[4]: Ya has elegido esa letra, elige otra");
                repetir = true;

            }
        }
        if (repetir == false)//Si el jugador ingresa una letra correcta, sin exepcion, entonces agrega la letra. 
        { //la letra se agregara a letraIngresada, y luego mostrar como letra adivinada.
            verificarCaracter.Add(letraIngresada);
            numIntentosRestantes -= 1; //Intentos fallidos
        }
    }
  
    //_______________________Comprobar si los jugadores quieren volver a jugar______________________________________ 

    static void usuarioNuevoJuego()
    {
        Console.WriteLine("Quieres volver a jugar? (s/n)");// Ofrecer a los jugadores volver a jugar 
        string nuevoJuego = Console.ReadLine();//Cargar jugador.
        if (nuevoJuego == "n")//Si el jugador selecciona "n" el programma se termina. 
        {
            Environment.Exit(1);
        }
        Console.Clear();
    }
}