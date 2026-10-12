using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace semana_9
{
    internal class Program
    {
        static int max_emp = 50;
        static string[] nombres = new string[max_emp];
        static string[] correo_emp = new string[max_emp];
        static int cant_emp = 0;
        static string rutaArchivo="empleado_datos.txt";
        static public void Titulo()
        {
            Console.WriteLine("*************************");
            Console.WriteLine("sistemas de empleados");
            Console.WriteLine("**************************");
        }
        static public void Registro_empleado()
        {
            Console.WriteLine("*****REGISTRAR EMPLEADO*****");
            if (cant_emp >= max_emp)
            {
                Console.WriteLine("alcanzo la capacidad maxima de datos");
                return;
            }
            Console.WriteLine("ingresar nombres y apellidos: ");
            string nombres_emp = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(nombres_emp))
            {
                Console.WriteLine("ERROR, el nombre no puede estar vacio");
                return;
            }
            string nombres_Mayus = nombres_emp.ToUpper();
            string[] partes = nombres_Mayus.Split(' ');
            string primer_nombre;
            string primer_apellido = "";
            if (partes.Length > 0)
            {
                primer_nombre = partes[0];
                if (partes.Length > 1)
                {
                    primer_apellido = partes[1];
                }
            }
            else
            {
                primer_nombre = "usuario";
                primer_apellido = "";
            }
            string correo = $"{primer_nombre.ToLower()+$"{primer_apellido.ToLower()}"}@upn.edu.pe";
            nombres[cant_emp] = nombres_Mayus;
            correo_emp[cant_emp] = correo;
            cant_emp++;
        }
        static public void buscar_empleado()
        {
            Console.WriteLine("**********busqueda de empleado**********");
            Console.WriteLine("****************************************");
            if (cant_emp == 0)
            {
                Console.WriteLine("no hay regisdtro de empleados");
                string buscar = Console.ReadLine()?.Trim().ToUpper();
                if (string.IsNullOrEmpty(buscar))
                    return;
                bool encontrado = false;
                for (int i = 0; i < cant_emp; i++)
                {
                    if (nombres[i].Contains(buscar))
                    {
                        Console.WriteLine("Encontrado:nombres", nombres[i] + "correo", correo_emp[i]);
                        encontrado = true;
                    }
                }
                if (!encontrado)
                {
                    Console.WriteLine("no se encuentran coincidencias en el empleado");
                }
            }
        }
        static public void ordenar()
        {
            if (cant_emp <= 1)
            {
                Console.WriteLine("no hay registros para ordeanar");
                return;
            }
            for (int i = 0; i < cant_emp - 1; i++)
            {
                for (int j = 0; j < cant_emp - 1 - i; j++)
                {
                    if (string.Compare(nombres[j], nombres[j + 1]) > 0)
                    {
                        string temp_nom = nombres[j];
                        nombres[j] = nombres[j + 1];
                        nombres[j + 1] = temp_nom;

                        string temp_correo = correo_emp[j];
                        correo_emp[j] = correo_emp[j + 1];
                        correo_emp[j + 1] = temp_correo;
                    }
                }
            }
        }
        static public void mostrar()
        {
            Console.WriteLine("*****lista de empleados*****");
            Console.WriteLine("****************************");
            if (cant_emp == 0)
            {
                Console.WriteLine("no hay registros por mostrar");
                return;
            }
            Console.WriteLine("nombres y apellidos\t\tcorreo");
            for (int i = 0; i < cant_emp; i++)
            {
                Console.WriteLine(nombres[i] + "\t\t" + correo_emp[i]);
            }
        }
        static public void guardar_datos_archivo()
        {
            Console.WriteLine("*****guardar datos en archivo*****");
            Console.WriteLine("*****************************");
            if (cant_emp == 0)
            {
                Console.WriteLine("no hay registros por guardar");
                return;
            }
            using(StreamWriter escribir=new StreamWriter(rutaArchivo))
            {
                for(int i = 0; i < cant_emp; i++)
                {
                    escribir.WriteLine(nombres[i] + "," + correo_emp[i]);
                }
            }
            Console.WriteLine(cant_emp + "empleados guardados en el archivo");
        }
        static public void leer_datos_archivos()
        {
            Console.WriteLine("*****leer datos de archivo*****");
            Console.WriteLine("*******************************");
            if (!File.Exists(rutaArchivo))
            {
                Console.WriteLine("no existe el archivo de empleados");
                return;
            }
            int num_linea = 0;
            using (StreamReader lector=new StreamReader(rutaArchivo))
            {
                string linea;
                while ((linea = lector.ReadLine())!= null) {
                    string[] partes = linea.Split(',');
                    if (partes.Length == 2)
                    {
                        Console.WriteLine(num_linea + ".-" + "nombres: " + partes[0] + "correo: " + partes[1]);
                        num_linea++;
                    }
                }
            }
        }
        static public void cargar_datos_archivos()
        {
            Console.WriteLine("*****cargar datos de archivos*****");
            Console.WriteLine("**********************************");
            if (!File.Exists(rutaArchivo))
            {
                Console.WriteLine("no existe el archivo de empleados");
                return;
            }
            string[] lineas = File.ReadAllLines(rutaArchivo);
            for(int i=0;i<lineas.Length && i < max_emp; i++)
            {
                string[] partes = lineas[i].Split(',');
                if(partes.Length == 2)
                {
                    nombres[i]=partes[0];
                    correo_emp[i]=partes[1];
                    cant_emp++;
                }
            }
        }
        static void Main(string[] args)
        {
            int opc=0;
            Titulo();
            while (opc != 5)
            {
                Console.WriteLine("[1]Registrar empelado");
                Console.WriteLine("[2]Buscar empleado");
                Console.WriteLine("[3]ordenar alfabeticamente (A - Z)");
                Console.WriteLine("[4]mostrar empleados");
                Console.WriteLine("[5]guardar datos de archivos");
                Console.WriteLine("[6]leer datos de archivos ");
                Console.WriteLine("[7]cargar datos de archivos");
                Console.WriteLine("[8]salir");
                Console.WriteLine("ingresar opcion: ");
                if (!int.TryParse(Console.ReadLine(), out opc))
                {
                    Console.WriteLine("ingresar un valor numerico");
                    continue;
                }
                switch (opc)
                {
                    case 1:
                        Registro_empleado();
                        break;
                    case 2:
                        buscar_empleado();
                        break;
                    case 3:
                        ordenar();
                        break;
                    case 4:
                        mostrar();
                        break;
                    case 5:
                        guardar_datos_archivo(); break;
                    case 6:
                        leer_datos_archivos(); break;
                    case 7:
                        cargar_datos_archivos(); break;
                    case 8:
                        Console.WriteLine("gracias por usar el sistema..!!");
                        return;
                    default:
                        Console.WriteLine("ingresar un numero del 1-8");
                        break;
                }
                Console.ReadKey();
            }
        }
    }
}