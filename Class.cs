using System;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorDeImagenes
{
    internal class Edit
    {
        public static void Renombrar(string entrada)
        {
            string carpeta = entrada;

            string[] archivos = Directory.GetFiles(carpeta);

            foreach (var archivo in archivos)
            {
                string nombreArchivo = Path.GetFileName(archivo);

                string nuevoNombre = nombreArchivo.Replace("_", "-");

                string nuevaRuta = Path.Combine(carpeta, nuevoNombre);

                File.Move(archivo, nuevaRuta);

                Console.WriteLine($"{nuevoNombre}");
            }

            Console.WriteLine("Proceso completado.");
        }

        public static void Mostrar(string entrada)
        {
            string carpeta = entrada;

            string[] archivos = Directory.GetFiles(carpeta);

            foreach (var archivo in archivos)
            {
                string nombreArchivo = Path.GetFileName(archivo);

                string codigo = string.Concat("\"", "40-", nombreArchivo.Substring(0, 4), "\"");

                Console.WriteLine($",{codigo},,{nombreArchivo}");
            }

            Console.WriteLine("Proceso completado.");
        }

        public static void Redimensionar(string entrada, string salida)
        {
            string directorioEntrada = entrada;
            string directorioSalida = salida;

            // Tamaño deseado para las imágenes redimensionadas
            int nuevoAncho = 600;
            int nuevoAlto = 800;

            if (!Directory.Exists(directorioEntrada))
            {
                Console.WriteLine("El directorio de entrada no existe.");
                return;
            }

            if (!Directory.Exists(directorioSalida))
            {
                Console.WriteLine("El directorio de salida no existe. Creándolo...");
                Directory.CreateDirectory(directorioSalida);
            }

            string[] archivosImagen = Directory.GetFiles(directorioEntrada, "*.jpg");

            foreach (var archivo in archivosImagen)
            {
                try
                {
                    using (var imagenOriginal = Image.Load(archivo))
                    {
                        imagenOriginal.Mutate(x => x.Resize(new ResizeOptions
                        {
                            Size = new Size(nuevoAncho, nuevoAlto),
                            Mode = ResizeMode.Max
                        }));

                        string nombreArchivoSalida = Path.Combine(directorioSalida, Path.GetFileName(archivo));
                        imagenOriginal.Save(nombreArchivoSalida);

                        Console.WriteLine($"Imagen redimensionada y guardada: {nombreArchivoSalida}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al procesar la imagen {archivo}: {ex.Message}");
                }
            }

            Console.WriteLine("Proceso completado.");
        }
    }
}