using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YemekOtomasyonu
{
    public class Data
    {
       // public static string ConStr = "server=.; user id=sa; password=1234; database=YemekOtomasyonu;";
        public static string ConStr = "Server=.; Database=YemekOtomasyonu; uid=sa; pwd=9117;";


        public static string execScalar(string sql, string isNullStr = null)
        {
            SqlConnection conn = new SqlConnection(ConStr);
            conn.Open();
            SqlCommand command = new SqlCommand();
            command.CommandText = sql;
            command.Connection = conn;

            string result;
            if (isNullStr != null)
            {
                try
                {
                    result = command.ExecuteScalar().ToString();
                }
                catch (Exception)
                {
                    result = isNullStr;
                }
            }
            else
            {
                result = command.ExecuteScalar().ToString();
            }

            conn.Close();
            return result;
        }

        public static DataTable select(string sql)
        {
            SqlConnection conn = new SqlConnection(ConStr);
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            conn.Close();
            return dt;
        }

        public static void ExecSql(string sql)
        {
            SqlConnection conn = new SqlConnection(ConStr);
            conn.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = sql;
            command.ExecuteNonQuery();
            conn.Close();
        }




        public static string selectImageSql(string sql)
        {//resmi sql çekiyor
            SqlConnection conn = new SqlConnection("server=.; user id=sa; password=9117; database=YemekOtomasyonu;");
            conn.Open();
            SqlCommand command = new SqlCommand();
            command.CommandText = sql;
            command.Connection = conn;
            SqlDataReader cursor = command.ExecuteReader();
            byte[] imageBytes = null;
            if (cursor.Read())
            {
                imageBytes = (byte[])cursor[0];
            }
            cursor.Close();
            conn.Close();
            string base64String = Convert.ToBase64String(imageBytes, 0, imageBytes.Length);
            string imageString = "data:image/jpeg;base64," + base64String;
            return imageString;
        }

        public static string byteToImageString(byte[] imageBytes)//resmi img etiketi yüklemek için
        {
            string base64String = Convert.ToBase64String(imageBytes, 0, imageBytes.Length);
            string imageString = "data:image/jpeg;base64," + base64String;
            return imageString;
        }


        public static Image byteArrayToImage(byte[] byteArrayIn)
        {
            using (MemoryStream mStream = new MemoryStream(byteArrayIn))
            {
                return Image.FromStream(mStream);
            }
        }
    }
}
