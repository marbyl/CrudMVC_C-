using AccesoDatos.AccesoDatos;
using AccesoDatos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ClimaServicio
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
    // NOTE: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class Service1 : IService1
    {
        /// <summary>
        /// obtenerClimas
        /// </summary>
        /// <returns></returns>
        public List<ClimaBO> obtenerClimas()
        {
            ClimaDao dao = new ClimaDao();
            return dao.obtenerClimas();
        }

        /// <summary>
        /// agregarClima
        /// </summary>
        /// <param name="objetoGuardar"></param>
        /// <returns></returns>
        public int agregarClima(ClimaBO objetoGuardar)
        {
            ClimaDao dao = new ClimaDao();
            return dao.agregarClima(objetoGuardar);
        }
    }
}
