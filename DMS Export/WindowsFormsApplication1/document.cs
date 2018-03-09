using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;



namespace WindowsFormsApplication1
{
    class document : conn
    {
        /// <summary>
        /// Gets an array of image paths based on the document ID
        /// </summary>
        /// <param name="DocumentId"></param>
        /// <returns>String array of image paths</returns>
        public List<string> ImagePaths(int DocumentId)
        {
            List<string> paths = new List<string>();

            SqlConnection conn = new SqlConnection(ConnString());
            conn.Open();
            string query = "SELECT [storeid] FROM [Doc] WHERE [tocid] = " + DocumentId;

            SqlCommand cmd = new SqlCommand(query, conn);

            DataTable dt = new DataTable();
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                da.Fill(dt);
            }

            foreach (DataRow row in dt.Rows) { paths.Add(VolumePath(DocumentId) + ImagePath((int)row[0])); }

            return paths;
        }

        /// <summary>
        /// Gets a document name based on ID
        /// </summary>
        /// <param name="DocumentId">The documents ID</param>
        /// <returns>Document name as string</returns>
        public string DocumentName(int DocumentId)
        {
            string name = string.Empty;

            SqlConnection conn = new SqlConnection(ConnString());
            SqlCommand cmd = new SqlCommand("SELECT [Name] FROM [Toc] WHERE TocId = " + DocumentId, conn);
            conn.Open();
            name = (string)cmd.ExecuteScalar();
            conn.Close();

            return name;
        }

        /// <summary>
        /// Gets the volume path for the given document ID
        /// </summary>
        /// <param name="DocumentId">The documents ID</param>
        /// <returns>Volume path as string</returns>
        public string VolumePath(int DocumentId)
        { 
            string path = string.Empty;

            SqlConnection conn = new SqlConnection(ConnString());
            SqlCommand cmd = new SqlCommand("SELECT Vol.FixedPath FROM Vol INNER JOIN Toc ON Vol.VolumeId = Toc.VolumeId WHERE TocId = " + DocumentId, conn);
            conn.Open();
            path = (string)cmd.ExecuteScalar();
            conn.Close();

            return path;
        }

        //private string VolumePath(int VolumeId)
        //{
        //    // database values do not end with \
        //    string path = string.Empty;

        //    SqlConnection conn = new SqlConnection(ConnString());
        //    SqlCommand cmd = new SqlCommand("SELECT [FixedPath] FROM [Vol] WHERE [VolumeId] = " + VolumeId, conn);
        //    conn.Open();
        //    path = (string)cmd.ExecuteScalar();
        //    conn.Close();

        //    return path;
        //}

        /// <summary>
        /// Converts storeId to hex and returns image path (eg. /00/00/00/01/00000001.TIF)
        /// </summary>
        /// <param name="StoreId">Integer StoreId</param>
        /// <returns>image path as string</returns>
        private string ImagePath(int StoreId)
        {
            string hexVal = StoreId.ToString("X");
            while (hexVal.Length < 8) { hexVal = "0" + hexVal; }

            return "/" + hexVal.Substring(0, 2) + "/" + hexVal.Substring(2, 2) + "/" + hexVal.Substring(4, 2) + "/" + hexVal.Substring(6, 2) + "/" + hexVal + ".TIF";
        }

        private void ExcuteScalarEx()
        {
            //using (SqlConnection con = new SqlConnection(ConnString())
            //{
            //    System.Data.SqlClient.SqlCommand cmd = new SqlCommand("SELECT * FROM whatever WHERE id = 5", con);
            //    try
            //    {
            //        conn.Open();
            //        newID = (int)cmd.ExecuteScalar();
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(ex.Message);
            //    }
            //}
        }

    }
}
