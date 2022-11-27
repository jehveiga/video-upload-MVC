using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Video_Upload_MVC.Controllers
{
    public class MvcUploadController : Controller
    {
        // GET: MvcUpload
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase videoFile)
        {
            if (videoFile != null) // Tratamento de nulos do objeto
            {
                string fileName = Path.GetFileName(videoFile.FileName); // Obtem o nome do arquivo com sua extensão

                if (videoFile.ContentLength < 2147483647) // Verifica o tamanho do arquivo 
                {
                    videoFile.SaveAs(Server.MapPath($"~/VideoFiles/{fileName}")); // Salvando o arquivo no servidor 

                    // Para salvar no banco

                    // Obtendo a connection do banco, configurado no Web.config
                    string mainConn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                    // criando uma nova instancia de conexão passando a connection obtida
                    SqlConnection sqlConnection = new SqlConnection(mainConn);

                    sqlConnection.Open();
                    // criando o comando que será usado para inserir os dados do arquivo, com os devidos parametros
                    string sqlquery = "INSERT INTO [dbo].[Video_Files] VALUES (@Vname, @Vpath)";

                    // Criando uma nova instacia de SQLComand, passando os parametros de comando usado e a conexão que será usada para o banco SQL
                    SqlCommand sqlCommand = new SqlCommand(sqlquery, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@Vname", fileName);
                    sqlCommand.Parameters.AddWithValue("@Vpath", $"~/VideoFiles/{fileName}");
                    if (sqlCommand.ExecuteNonQuery() > 0)
                    {
                        ViewData["message"] = $"registro {fileName} salvo com sucesso";
                        
                    }
                    else
                    {
                        ViewData["message"] = $"Não foi possível salvar o registro {fileName}";
                    }

                    sqlConnection.Close();

                }
            }

            return RedirectToAction("Index");
        }
    }
}