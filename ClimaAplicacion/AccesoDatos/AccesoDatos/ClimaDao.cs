using AccesoDatos.Entidades;
using AccesoDatos.Mapeadores;
using ClassLibrary2;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AccesoDatos.AccesoDatos
{
    public class ClimaDao
    {
        /// <summary>
        /// obtenerClimas
        /// </summary>
        /// <returns></returns>
        public List<ClimaBO> obtenerClimas()
        {
            using (ClimaDBEntities db = new ClimaDBEntities())
            {
                var result = from clima in db.Clima
                             select new ClimaBO()
                             {
                                 descripcion = clima.descripcion == null ? string.Empty : clima.descripcion,
                                 fecha = clima.fecha == null ? DateTime.MinValue : clima.fecha,
                                 id = clima.id,
                                 idCiudad = clima.idCiudad == null ? 0 : clima.idCiudad,
                                 nombreCiudad = clima.Ciudad.nombre == null ? string.Empty : clima.Ciudad.nombre,
                                 temperatura = clima.temperaturaCentigrados == null ? 0 : clima.temperaturaCentigrados
                             };
                return result.ToList();
            }
        }

        /// <summary>
        /// agregarClima
        /// </summary>
        /// <param name="objetoGuardar"></param>
        /// <returns></returns>
        public int agregarClima(ClimaBO objetoGuardar)
        {
            using (ClimaDBEntities db = new ClimaDBEntities())
            {
                if (objetoGuardar == null)
                { throw new Exception("El parámetro no está configurado"); }

                var climaDB = db.Clima.FirstOrDefault(e => e.id == objetoGuardar.id);
                if (climaDB == null || climaDB.id <= 0)
                {
                    if (!(objetoGuardar == null))
                    {
                        // No existe el registro y creamos uno nuevo
                        climaDB = ClimaMapeador.Mapper(objetoGuardar);
                        db.Clima.Add(climaDB);
                    }
                }
                else
                {
                    // Modificar el objeto
                    ClimaMapeador.Mapper(objetoGuardar);
                }
                db.SaveChanges();
                return climaDB.id;
            }
        }
    }
}
