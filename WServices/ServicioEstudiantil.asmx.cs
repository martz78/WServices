using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml;

namespace WServices
{

	
	/// <summary>
	/// Descripción breve de ServicioEstudiantil
	/// </summary>
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[System.ComponentModel.ToolboxItem(false)]
	// Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
	// [System.Web.Script.Services.ScriptService]
	public class ServicioEstudiantil : System.Web.Services.WebService
	{
		private string connectionString = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;

		[WebMethod]
		public XmlDocument CreateUsuario(string nombre, string apellido, int telefono)
		{
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string query = "INSERT INTO estudiante(nombre, apellido, telefono) VALUES (@nombre, @apellido,@telefono)";
				SqlCommand command = new SqlCommand(query, connection);
				command.Parameters.AddWithValue("@nombre", nombre);
				command.Parameters.AddWithValue("@apellido", apellido);
				command.Parameters.AddWithValue("@telefono", telefono);

				connection.Open();
				command.ExecuteNonQuery();
			}

			XmlDocument xmlResponse = new XmlDocument();

			XmlElement rootElement = xmlResponse.CreateElement("Response");
			xmlResponse.AppendChild(rootElement);

			XmlElement responseElement = xmlResponse.CreateElement("Message");
			responseElement.InnerText = "Datos Registrados Correctamente.";
			rootElement.AppendChild(responseElement);

			return xmlResponse;
		}
	

	[WebMethod]

	public DataSet GetUsuarios()
	{
		using (SqlConnection connection = new SqlConnection(connectionString))
		{
			string query = "SELECT * FROM estudiantes";
			SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
			DataSet dataSet = new DataSet();

			connection.Open();
			adapter.Fill(dataSet, "estudiante");

			return dataSet;
		}


		}
		[WebMethod]
		public XmlDocument UpdateUsuario( int id ,string nuevonombre, string nuevoapellido, int nuevotelefono)
		{
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string query = "UPDATE estudiante SET nombre = @nuevonombre, apellido = @nuevoapellido, telefono = @nuevotelefono WHERE id = @id";
				SqlCommand command = new SqlCommand(query, connection);
				command.Parameters.AddWithValue("@nuevonombre", nuevonombre);
				command.Parameters.AddWithValue("@nuevoapellido", nuevoapellido);
				command.Parameters.AddWithValue("@nuevotelefono", nuevotelefono);
				command.Parameters.AddWithValue("@id", id);

				connection.Open();
				command.ExecuteNonQuery();
			}

			XmlDocument xmlResponse = new XmlDocument();

			XmlElement rootElement = xmlResponse.CreateElement("Response");
			xmlResponse.AppendChild(rootElement);

			XmlElement responseElement = xmlResponse.CreateElement("Message");
			responseElement.InnerText = "Se ha Realizado una Modificacion Exitosa.";
			rootElement.AppendChild(responseElement);

			return xmlResponse;
		}

		[WebMethod]
		public XmlDocument DelateUsuario(int id)
		{
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string query = "DELETE estudiante WHERE id = @id";
				SqlCommand command = new SqlCommand(query, connection);
				command.Parameters.AddWithValue("@id", id);

				connection.Open();
				command.ExecuteNonQuery();
			}

			XmlDocument xmlResponse = new XmlDocument();

			XmlElement rootElement = xmlResponse.CreateElement("Response");
			xmlResponse.AppendChild(rootElement);

			XmlElement responseElement = xmlResponse.CreateElement("Message");
			responseElement.InnerText = "Registro Eliminado Correctamente.";
			rootElement.AppendChild(responseElement);

			return xmlResponse;
		}
	}
}
