using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Proyecto_unidad_5_y_6_aaaAAAAa
{
    class Program
    {
        

        class Comentario : IComparable, IEquatable<Comentario>
        {
            public string Nombre_autor { get; set; }
            public string Correo { get; set; }
            public string Usuario { get; set; }
            public string Ip { get; set; }
            public DateTime Fecha { get; set; }
            public int id { get; set; }
            public string Texto { get; set; }
            public int Likes { get; set; }
            
            public override string ToString()
            {
                return String.Format($"{id} - {Nombre_autor} - {Correo} - {Usuario} - {Ip} - {Fecha} - {Texto} - {Likes}");
            }
            public int CompareTo(object obj)
            {
                return this.Fecha.CompareTo((obj as Comentario).Fecha);
            }
            public bool Equals(Comentario other)
            {
                if (other != null) return false;
                return (this.id.Equals(other.id));
            }

        }
        class ComentarioDB
        {
            
            public static void SaveToFile(List<Comentario> comentarios, string path)
            {
                StreamWriter textOut = null;
                try
                {
                    textOut = new StreamWriter(new FileStream(path, FileMode.Create, FileAccess.Write));
                    foreach (var comentario in comentarios)
                    {
                        textOut.Write(comentario.id + "|");
                        textOut.Write(comentario.Nombre_autor + "|");
                        textOut.Write(comentario.Correo + "|");
                        textOut.Write(comentario.Usuario + "|");
                        textOut.Write(comentario.Texto + "|");
                        textOut.Write(comentario.Ip + "|");
                        textOut.Write(comentario.Fecha + "|");
                        textOut.WriteLine(comentario.Likes);
                    }
                }
                catch (IOException)
                {
                    Console.WriteLine("Ya existe el archivo");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                finally
                {
                    if (textOut != null)
                        textOut.Close();
                }
            }
            public static List<Comentario> ReadFromFile(string path)
            {
                List<Comentario> products = new List<Comentario>();

                StreamReader textIn = new StreamReader(new FileStream(path, FileMode.Open, FileAccess.Read));
                while (textIn.Peek() != -1) //Leer hasta el final
                {
                    string row = textIn.ReadLine();
                    string[] columns = row.Split('|');
                    Comentario p = new Comentario();

                    p.id = int.Parse(columns[0]);
                    p.Nombre_autor = columns[1];
                    p.Correo = columns[2];
                    p.Usuario = columns[3];
                    p.Texto = columns[4];
                    p.Ip = columns[5];
                    p.Fecha = DateTime.Parse(columns[6]);
                    p.Likes = int.Parse(columns[7]);
                    products.Add(p);


                }


                textIn.Close();

                return products;
            }
        }
        static int Capturaid()
        {
            Console.WriteLine("Id: ");
            string l = Console.ReadLine();
            int Id = 0;
            Id = int.Parse(l);
            try
            {
                Id = int.Parse(l);
            }
            catch (FormatException e)
            {
                Console.WriteLine("No se pudo capturar el id, solo se pueden poner números");
                Console.WriteLine(e.Message);
            }
            catch (OverflowException e)
            {
                Console.WriteLine(e.Message);
            }
            return Id;
        }
        static void Main(string[] args)
        {
            /*
            
            List<Comentario> comentarios = new List<Comentario>();

            comentarios.Add(new Comentario() { Nombre_autor= "Pablo", Texto= "OH NO", Likes= 35, Correo="joselito@mm.com", id = 0 , Fecha = new DateTime(2021, 06, 17, 06, 25, 49), Ip="132.032.00.1", Usuario = "LOKO"});
            comentarios.Add(new Comentario() { Nombre_autor = "Perla", Texto = "OH SI", Likes = 32, Correo = "perlukis@mm.com", id = 1, Fecha = new DateTime(2021,06,17,08,36,25), Ip = "132.032.00.1", Usuario = "LOKO" });
            ComentarioDB.SaveToFile(comentarios, @"C:\Users\personal\comentarioooos.txt");

            */
            List<Comentario> comentarios = ComentarioDB.ReadFromFile(@"C:\Users\personal\comentarioooos.txt");
            comentarios.Sort();comentarios.Reverse();
            foreach (var p in comentarios)
            {
                Console.WriteLine(p);
            }

            Console.WriteLine();
            Console.WriteLine("¿Desea agregar un comentario?: (si o no)");
            string x = Console.ReadLine();
            Console.WriteLine();
            if (x == "si")
            {
                int Id = 0;
                try
                {
                    Id = Capturaid();
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                Console.WriteLine("Nombre: ");
                string nombre = Console.ReadLine();
                Console.WriteLine("Correo: ");
                string correo = Console.ReadLine();
                Console.WriteLine("Usuario: ");
                string usuario = Console.ReadLine();
                Console.WriteLine("Comentario: ");
                string texto = Console.ReadLine();
                Console.WriteLine("IP: ");
                string ip = Console.ReadLine();
                comentarios.Add(new Comentario { id = Id, Nombre_autor = nombre, Correo = correo, Usuario = usuario, Texto = texto, Ip = ip, Likes = 0, Fecha = DateTime.Now });
                ComentarioDB.SaveToFile(comentarios, @"C:\Users\personal\comentarioooos.txt");
            }
            if (x == "no")
            {
                Console.WriteLine("Esta bien");
            }
            if (x!="si")
            {
                Console.WriteLine("Solo se puede si o no: ");
            }
            if (x != "no")
            {
                Console.WriteLine("Solo se puede si o no: ");
            }
            comentarios.Sort();
            foreach (var p in comentarios)
            {
                Console.WriteLine(p);
            }
            Console.ReadKey();
        }
    }
}
