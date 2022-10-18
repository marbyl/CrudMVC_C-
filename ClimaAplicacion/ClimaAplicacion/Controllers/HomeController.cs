using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ClimaAplicacion.ServiceReference1;

namespace ClimaAplicacion.Controllers
{
    public class HomeController : Controller
    {
        #region Propiedades
        public IService1 clienteServicio { get; set; }
        #endregion
        public HomeController()
        {
            clienteServicio = new Service1Client();

        }
        public ActionResult Index()
        {
            var prueba = clienteServicio.obtenerClimas();
            return View();
        }

        /// <summary>
        /// ConsultarClimas
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public ActionResult ConsultarClimas(int page = 0, int rows = 0)
        {
            var lsClima = clienteServicio.obtenerClimas();
            try
            {
                return T_GetData(lsClima.AsQueryable(),
                    row => row.id.ToString(),
                    row => new string[]
            {
                row.id.ToString(),
                row.nombreCiudad.ToString(),
                row.temperatura.ToString(),
                row.descripcion,
                row.fecha.ToString()
            },
                    new string[] { "Id", "Ciudad", "temperatura", "descripcion" },
                    page, rows, string.Empty, lsClima.Count()); ;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// CrudClimas
        /// </summary>
        /// <param name="oper"></param>
        /// <param name="id"></param>
        /// <param name="fe"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        public ActionResult CrudClimas(string oper, string id, bool? fe, FormCollection collection)
        {
            try
            {
                switch (oper)
                {
                    case "add":

                        ClimaBO climaAgregar = new ClimaBO();
                        climaAgregar.idCiudad = Convert.ToInt32(collection[0]);
                        climaAgregar.temperatura = Convert.ToInt32(collection[1]);
                        climaAgregar.descripcion = collection[2];
                        climaAgregar.fecha = Convert.ToDateTime(collection[3]);

                        clienteServicio.agregarClima(climaAgregar);
                        break;
                    case "del":
                        //cliente.EliminarBanco(Convert.ToInt32(id));
                        break;

                }
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message)
                {

                };
            }
        }

        /// <summary>
        /// TraerCiudades
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        public string TraerCiudades(string term)
        {
            StringBuilder _select = new StringBuilder();
            _select.Append("<select id='CiudadDes'><option value=0>Seleccione una opción</option>");
            _select.Append("<option value=1>" + "Garagoa" + "</option>");
            _select.Append("<option value=2>" + "Bogotá" + "</option>");
            _select.Append("<option value=3>" + "Medellín" + "</option>");
            _select.Append("<option value=4>" + "Pasto" + "</option>");
            _select.Append("<option value=5>" + "Cali" + "</option>");
            _select.Append("</select>");
            return _select.ToString();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        protected static string AsCsv(string[] columnames, IEnumerable<string[]> items)
        {
            var csvBuilder = new StringBuilder();
            csvBuilder.AppendLine("sep=,");
            string lineh = string.Join(",", columnames.Select(p => ToCsvValue(p)).ToArray());
            csvBuilder.AppendLine(lineh);
            foreach (var item in items)
            {
                string line = string.Join(",", item.Select(p => ToCsvValue(p)).ToArray());
                csvBuilder.AppendLine(line);
            }
            return csvBuilder.ToString();
        }

        protected static string ToCsvValue(string item)
        {
            try
            {
                if (item != null)
                    return string.Format("\"{0}\"", item.ToString().Replace("\"", "-"));
                else
                    return string.Format("\\");
            }
            catch (Exception)
            {
                throw new HttpException("Información inconmpleta");
            }
        }

        protected ActionResult T_GetData<T>(
            IQueryable<T> qbase,
            Func<T, string> fkey,
            Func<T, string[]> fmap,
            string[] columnames,
            int page, int rows, string oper, int totalregistros
            )
        {
            IQueryable<T> qry = qbase;

            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            int totalRecords = totalregistros;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)pageSize);

            if (oper == "excel")
            {
                var csvrows = from fila in qry.ToList() select fmap(fila);
                var scontenido = AsCsv(columnames, csvrows);
                Response.AddHeader("Content-Disposition", "attachment; filename=ExcelExport.csv");
                Response.ContentType = "text/csv";
                Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
                Response.Write(scontenido);
                Response.End();
                return Content(scontenido, "text/csv");
            }

            var result = new
            {
                total = totalPages,
                page = page,
                records = totalRecords,
                rows = from row in qry.ToList()
                       select new
                       {
                           id = fkey(row),
                           cell = fmap(row)
                       },
            };

            return Json(result, JsonRequestBehavior.AllowGet);
        }


    }
}