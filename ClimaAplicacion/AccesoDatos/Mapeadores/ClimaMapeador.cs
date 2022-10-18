using AccesoDatos.Entidades;
using ClassLibrary2;

namespace AccesoDatos.Mapeadores
{
    public static class ClimaMapeador
    {
        internal static Clima Mapper(ClimaBO origen)
        {
            Clima destino = new Clima();
            if (origen == null)
            { destino = default(Clima); }
            else
            {
                if (destino == null)
                { destino = new Clima(); }

                destino.fecha = origen.fecha;
                destino.descripcion = origen.descripcion;
                destino.id = origen.id;
                destino.idCiudad = origen.idCiudad;
                destino.temperaturaCentigrados = origen.temperatura;
            }

            return destino;
        }
    }
}
