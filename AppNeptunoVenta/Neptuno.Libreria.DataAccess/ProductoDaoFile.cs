﻿using System.Collections.Generic;
using System.Linq;
using System.IO;
using Neptuno.Libreria.Service;
using Neptuno.Libreria.Entity;

namespace Neptuno.Libreria.DataAccess
{
    public class ProductoDaoFile : IServicioNeptunoGenerico<Producto>
    {
        // variables
   
        private string temporal = @"..\Temporal.dat";
        private string archivo = System.IO.Path.GetTempPath().ToString() + "temporal.dat" ;
        private FileStream fs;
        private BinaryReader br;
        private BinaryWriter bw;
        private string datos;

        //constructor
        public ProductoDaoFile()
        {
            if (!File.Exists(archivo))
            {
                //crear objeto de la clase FileStream
                fs = new FileStream(archivo, FileMode.Create, FileAccess.Write);
                fs.Close();
                //MessageBox.Show("Archivo creado con exito !");
            }
        }

        //metodos de persistencia de datos en archivo .dat

        public void create(Producto o)
        {
            try
            {
                //abrir archivo para escritura
                fs = new FileStream(archivo, FileMode.Append, FileAccess.Write);
                //crear objeto para escribir datos en el archivo
                bw = new BinaryWriter(fs);
                //escribir datos en el archivo
                bw.Write(o.IdProducto);
                bw.Write(o.NombreProducto);
                bw.Write(o.IdCategoria);
                bw.Write(o.PrecioUnidad);
                bw.Write(o.Stock);
                //cerrar archivo
                bw.Close();
            }
            catch (IOException ex)
            {
                throw ex;
            }
            finally
            {
                fs.Close();
            }
        }

        public void delete(Producto o)
        {

        }

        public Producto findForId(int o)
        {
            Producto pro1 = null;
            try
            {
                if (File.Exists(archivo))
                {
                    //abrir archivo para lectura
                    fs = new FileStream(archivo, FileMode.Open, FileAccess.Read);
                    //crear objeto para leer datos del archivo
                    br = new BinaryReader(fs);
                    do
                    {
                       Producto pro = new Producto()
                        {
                            IdProducto = br.ReadInt32(),
                            NombreProducto = br.ReadString(),
                            IdCategoria = br.ReadInt32(),
                           //br.ReadString(),
                            PrecioUnidad = br.ReadDecimal(),
                            Stock = br.ReadInt32()
                        };
                        if (pro.IdProducto == o)
                        {
                            pro1 = pro;
                        }                       

                    } while (true);
                }
                else
                {
                    //MessageBox.Show("Archivo no existe");
                }
            }
            catch (IOException ex)
            {
                //throw ex;
            }
            finally
            {
                br.Close();
                fs.Close();
            }         
            return pro1;
        }

        public List<Producto> readAll()
        {
            List<Producto> lproducto = new List<Producto>();
            try
            {
                if (File.Exists(archivo))
                {
                    //abrir archivo para lectura
                    fs = new FileStream(archivo, FileMode.Open, FileAccess.Read);
                    //crear objeto para leer datos del archivo
                    br = new BinaryReader(fs);
                    do
                    {
                        Producto pro = new Producto()
                        {
                            IdProducto = br.ReadInt32(),
                            NombreProducto = br.ReadString(),
                            IdCategoria = br.ReadInt32(),
                            // Categoria = br.ReadString(),
                            PrecioUnidad = br.ReadDecimal(),
                            Stock = br.ReadDecimal()
                        };
                        lproducto.Add(pro);

                    } while (true);
                }
                else
                {
                    //MessageBox.Show("Archivo no existe");                    
                }
            }
            catch (IOException ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);//throw ex;
            }
            finally
            {
                br.Close();
                fs.Close();
            }
            return lproducto;
        }


        public void update(Producto o)
        {

        }


        public List<Producto> readProductsCondition(List<Producto> ls)
        {
            List<Producto> lsresult = new List<Producto>();
            var consulta = from product in ls
                           where product.PrecioUnidad > 20
                           select product;

            lsresult = consulta.ToList();
            return lsresult;

        }
    }
}
