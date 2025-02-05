using System.Data;
using Microsoft.Data.SqlClient;
using MvsCoreLinqToSQL.Models;

namespace MvsCoreLinqToSQL.Repositories
{
    
    public class RepositoryEnfermo
    {
        private DataTable tablaEnfermos;

        SqlCommand com;
        SqlConnection cn;

        public RepositoryEnfermo()
        {
            string connectionString = @"Data Source=LOCALHOST\SQLEXPRESS;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=sa;Encrypt=True;Trust Server Certificate=True";
            string sql = "select * from ENFERMO";
            SqlDataAdapter adEnf = new SqlDataAdapter(sql, connectionString);
            this.tablaEnfermos = new DataTable();

            adEnf.Fill(this.tablaEnfermos);

            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;
        }        

        public List<Enfermo> GetEnfermos()
        {
            var consulta = from datos in this.tablaEnfermos.AsEnumerable()
                           select datos;

            List<Enfermo> enfermos = new List<Enfermo>();
            foreach (var row in consulta)
            {
                Enfermo enf = new Enfermo();
                enf.Inscripcion = row.Field<string>("INSCRIPCION");
                enf.Apellido = row.Field<string>("APELLIDO");
                enf.Direccion = row.Field<string>("DIRECCION");
                enf.FechaNacimiento = row.Field<DateTime>("FECHA_NAC");
                enf.Sexo = row.Field<string>("S");
                enf.NumSegSoc = row.Field<string>("NSS");
                enfermos.Add(enf);
            }
            return enfermos;
        }

        public Enfermo FindEnfermo (string inscripcion)
        {
            var consulta = from datos in this.tablaEnfermos.AsEnumerable()
                           where datos.Field<string>("INSCRIPCION") == inscripcion
                           select datos;

            var row = consulta.First();

            Enfermo enf = new Enfermo();
            enf.Inscripcion = row.Field<string>("INSCRIPCION");
            enf.Apellido = row.Field<string>("APELLIDO");
            enf.Direccion = row.Field<string>("DIRECCION");
            enf.FechaNacimiento = row.Field<DateTime>("FECHA_NAC");
            enf.Sexo = row.Field<string>("S");
            enf.NumSegSoc = row.Field<string>("NSS");

            return enf;
        }

        public async Task DeleteEnfermo(string inscripcion)
        {
            string sql = "delete from ENFERMO where INSCRIPCION=@inscripcion";

            this.com.Parameters.AddWithValue("@inscripcion", inscripcion);

            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;

            await this.cn.OpenAsync();
            await this.com.ExecuteNonQueryAsync();
            await this.cn.CloseAsync();
            this.com.Parameters.Clear();
        }
    }
}
