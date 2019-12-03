using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MiLista
{
    public class Operacion//clase publica con todo el programa
    {
        public List<Shinobi> Ninjas;//lista publica para trabajarla en distintos metodos, contiene el archivo directo del string
        public Operacion()//constructor de la lista, para tenerla llena desde que se accede a la clase
        {   
            Ninjas = ObtenerShinobis();//lista publica que trabaja con el metodo para llenarse
        }
        internal void Principal()//esto llamas desde el main, la bienvenida
        {
            Console.WriteLine("Bienvenido a la lista de shinobis, presione cualquier tecla para acceder a la lista de Shinobis");
            Console.ReadKey();
            Console.Clear();
            Menu();//despues de saludar y limpiar pantalla llegas al menu
        }
        public void Menu()//menu, puesto que trabajaremos con una lista ya hecha, lo mas practico es desplegarla de una vez y trabajar dos opciones, detallar a un elemento o salir
        {
            try//en caso de que el usuario use sintaxis indebida
            {
                ShowNinjas();//metodo de despliegue de la lista de objetos
                Console.WriteLine("\n\nSeleccione con un numero la accion a realizar: \n1.- Detallar Shinobi.\n2.- Salir.");//despliegue del menu
                switch (int.Parse(Console.ReadLine()))//capturas directo sin crear variable
                {
                    case 1://si el usuario quiere detallar ira al metodo despues de limpiar
                        Console.Clear();
                        DetailNinja();//metodo para acceder al detallado de objeto
                        break;
                    case 2:
                        System.Environment.Exit(-1);//si el usuario quiere salir con esto se va
                        break;
                    default://un default para cuando el tipo de dato es valido pero no esta dentro del rango de opciones validas
                        Console.Clear();
                        Console.WriteLine("Seleccione una opcion valida, por favor.");
                        Console.ReadKey();
                        Console.Clear();
                        Menu();//limpia y te manda al menu despues de regañarte
                        break;
                }

            }
            catch(Exception ex) //es un segundo default, porque solo pedimos numeros
            {
                Console.Clear();
                Console.WriteLine("Seleccione una opcion valida, por favor.");
                Console.ReadKey();
                Console.Clear();
                Menu();//trabaja igual que el default porque solo queremos decirle que elija 1 o 2
            }
        }
        public void ShowNinjas() //metodo para desplegar la lista publica
        {
            Console.WriteLine("Estos son los shinobis que hay en la lista: ");
            foreach (var item in Ninjas)//foreach para cada elemento de la lista
            {
                Console.WriteLine("{0}.- {1}", item.ID, item.Nombre);//solo desplegamos id y nombre para saber como buscar el objeto deseado
            }
        }
        public List<string> ObtenerLineas(string path)//es metodo tipo lista de strings porque es pasajero, aqui se saca la info del txt
        {
            List<string> lineas = new List<string>();//creas tu lista
            if (File.Exists(path))//buscas en el file si existe
            {
                string[] datos = File.ReadAllLines(path);//creas un array de datos que sacara su info del txt
                foreach (var item in datos)//foreach para buscar en el array
                {
                    lineas.Add(item);//por cada elemento que este en el array, se agrega a la lista de strings
                }
            }
            else
            {
                Console.WriteLine("El archivo no existe");//si no hay archivo se devuelve un null
                return null;
            }
            return lineas;//cuando se llene la lista de strings se devuelve al metodo que sigue
        }
        public List<Shinobi> ObtenerShinobis()
        {
            Shinobi s = new Shinobi();//instancia de shinobi para manejar objetos
            var lineas = ObtenerLineas("Lista.txt");//jalas tu lista con un var para optimizar
            List<Shinobi> shinobis = new List<Shinobi>();//creas lista de objetos
            foreach (var item in lineas)//foreach para buscar en la lista de strings
            {
                string[] datos = item.Split(',');//por cada elemento dentro de tu lsita creas un arreglo de cinco elementos, divididos por una coma
                shinobis.Add(new Shinobi { ID = int.Parse(datos[0]), Nombre = datos[1], Rango = datos[2], Elemento = int.Parse(datos[3]), Aldea = datos[4] });//cada que llenes tu arreglo de 5 elementos, los conviertes en atributos del objeto y los agregas a la lista 
            }
            return shinobis;//devuelves la lista de objetos llenos, la cual es enviada al ctor y a su vez es global
        }
        public void DetailNinja() //metodo para detallar un objeto
        {
            try//en caso de que el usuario use sintaxis indebida
            {
                Console.Clear();//limpias para remover el menu
                ShowNinjas();//muestras lista para que el usuario sepa que busca
                Shinobi s = new Shinobi();//instancias para llenar y desplegar objeto
                Console.WriteLine("Seleccione con un numero, el shinobi a detallar: ");
                int ninjaid = int.Parse(Console.ReadLine());//usas este id como tu elemento buscador
                foreach (var item in Ninjas)//foreach para la busqueda
                {
                    if (ninjaid == item.ID)//si el elemento aparece en tu lsita
                    {
                        s = item;//conviertes a ese elemento en tu objeto a desplegar
                    }
                }
                string element = "";//usamos un int en un atributo para no tener strings variados, asi generalizas los atributos a cosas especificas, como el status del to do list
                switch (s.Elemento)//con el switch asignamos a cada valor un string especifico
                {
                    case 1:
                        element = "Elemento Viento";
                        break;
                    case 2:
                        element = "Elemento Rayo";
                        break;
                    case 3:
                        element = "Elemento Tierra";
                        break;
                    case 4:
                        element = "Elemento Agua";
                        break;
                    case 5:
                        element = "Elemento Fuego";
                        break;
                }
                Console.Clear();//limpiamos para desplegar el objeto 
                Console.WriteLine("Nombre:  {0}\nRango:  {1}\nElemento:  {2}\nAldea:  {3}", s.Nombre, s.Rango, element, s.Aldea);//desplegamos todos los atributos de modo normal pero en vez de usar el atributo element usamos el string que acabamos de crear y llenar con el switch
                Console.WriteLine("\nSi desea modificar algun dato presione 1, en caso de querer volver a la lista presione 2.");
                int option = int.Parse(Console.ReadLine());//le decimos al usuario si quiere editar o no, con un try catch por si hay un error en la sintaxis
                if (option == 1)//si dice que si modificaremos el objeto aqui
                {
                    s=EditNinja(s);//el objeto elegido a ver, sera sustituido por su nueva version que sera enviada al metodo de cambio como sobrecarga, como diria thanos, use las gemas para destruir las gemas, something like that
                    UpdateTxt();//despues de modificar la lista se llama este metodo para actualizar el archivo txt
                    Console.Clear();//se limpia y se vuelve al menu
                    Menu();
                }
                else//si se niega a editar accedemos al menu de manera normal
                {
                    Console.ReadKey();
                    Console.Clear();
                    Menu();
                }
            }
            catch(Exception e) //en caso de excepcion se interrumpe la edicion del objeto
            {
                Console.WriteLine("Se ha detectado el error: {0}\nPresiona cualquier tecla para volver al menu",e.Message);
                Console.ReadKey();
                Console.Clear();
                Menu();
            }
        }
        public Shinobi EditNinja(Shinobi s) //meetodo para cuando se acepte la edicion del objeto
        {
            try//en caso de error de sintaxis
            {
                Console.WriteLine("Seleccione con un numero el atributo a modificar:\n1.-Nombre\n2.-Rango\n3.-Elemento\n4.-Aldea");
                switch (int.Parse(Console.ReadLine()))//con el numero se elige el atributo a modificar (solamente uno por vuelta)
                {
                    case 1:
                        Console.WriteLine("Ingrese un nuevo nombre: ");
                        string nombre = Console.ReadLine();
                        s.Nombre = nombre;
                        Console.WriteLine("Se ha modificado el Nombre.");
                        break;
                    case 2:
                        Console.WriteLine("Ingrese un nuevo rango: ");
                        string rango = Console.ReadLine();
                        s.Rango = rango;
                        Console.WriteLine("Se ha modificado el Rango.");
                        break;
                    case 3:
                        Console.WriteLine("Seleccione con un numero el nuevo elemento:\n1.- Elemento Aire\n2.- Elemento Rayo\n3.- Elemento Tierra\n4.- Elemento Agua\n5.- Elemento Fuego");
                        int elemento = int.Parse(Console.ReadLine());
                        s.Elemento = elemento;
                        Console.WriteLine("Se ha modificado el Elemento.");
                        break;
                    case 4:
                        Console.WriteLine("Ingrese la nueva aldea: ");
                        string aldea = Console.ReadLine();
                        s.Aldea = aldea;
                        Console.WriteLine("Se ha modificado la Aldea.");
                        break;
                    default: break;
                }
                Console.WriteLine("Presione ENTER para continuar");
                Console.ReadKey();
                return s;//se devuelve el objeto modificado
            }
            catch(Exception exe) //en caso de error de sintaxis el objeto se devolvera sin cambios (por eso se llena una variable primero)
            {
                Console.WriteLine("Se ha detectado el error: {0}\nSe regresara al menu",exe.Message);
                Menu();
                return s;
            }
        }
        public void UpdateTxt()//metodo que se usa despues de modificar un objeto de una lista
        {//no es mas que todo el proceso de crear la lista global del constructor pero a la inversa, literalmente lo traduje al reves, donde en vez de split use join
            List<string> lineas = new List<string>();//haces una lista de strings para fusionarla
            foreach (var ninja in Ninjas) //por cada elemento en tu lista de objetos
            {
                string[] atributos = new string[5];//llenas un vector de strings
                atributos[0] = Convert.ToString(ninja.ID);
                atributos[1] = ninja.Nombre;
                atributos[2] = ninja.Rango;
                atributos[3] = Convert.ToString(ninja.Elemento);
                atributos[4] = ninja.Aldea;//el cual llenas de los atributos del objeto
                lineas.Add(string.Join(",", atributos));//luego para agregarlo a tu lista de strings usas el join en vez del split, aqui es la inversa
            }
            var joinedstring = string.Join("\n",lineas);//luego usas otro join para fusionar tu lista de strings, usas el backslash n para dividir tu string, en el join anterior usaste una coma, con esto cada renglon sera el objeto hecho string, en otras palabras cada elemento de la lista de strings que acabamos de hacer usara un renglon del txt
            File.WriteAllText("Lista.txt", joinedstring);//por ultimo solo hay que meter este gran string de 20 renglones al archivo txt, y con eso el programa queda completo
        }
    }
}
;