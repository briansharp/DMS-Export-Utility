using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace WindowsFormsApplication1
{
    class directory : conn
    {
        /// <summary>
        /// Returns a int array of folder ID's inside given folder
        /// </summary>
        /// <param name="FolderId">The parent folder ID for the ID's to be returned</param>
        /// <returns>integer array of folder ID's</returns>
        public List<int> ChildFolderIds(int FolderId)
        {
            List<int> Ids = new List<int>();
            SqlConnection conn = new SqlConnection(ConnString());
            conn.Open();
            string query = "SELECT TocId FROM [Toc] WHERE ParentId = " + FolderId + " AND [VolumeId] is null";

            SqlCommand cmd = new SqlCommand(query, conn);
            DataTable dt = new DataTable();
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                da.Fill(dt);
            }

            foreach (DataRow row in dt.Rows) { Ids.Add((int)row[0]); }
            return Ids;
        }

        /// <summary>
        /// Returns a list of folder names based on parent folder ID
        /// </summary>
        /// <param name="FolderId">Parent folder ID</param>
        /// <returns>string array of folder names</returns>
        public List<string> ChildFolderNames(int FolderId)
        {
            List<string> names = new List<string>();
            SqlConnection conn = new SqlConnection(ConnString());
            conn.Open();
            string query = "SELECT [Name] FROM [Toc] WHERE ParentId = " + FolderId + " AND [VolumeId] is null";

            SqlCommand cmd = new SqlCommand(query, conn);
            DataTable dt = new DataTable();
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                da.Fill(dt);
            }

            foreach (DataRow row in dt.Rows) { names.Add((string)row[0]); }
            return names;
        }

        /// <summary>
        /// Returns a int array of document ID's inside given folder
        /// </summary>
        /// <param name="FolderId">The parent folder ID for the document ID's to be returned</param>
        /// <returns>An integer array of document ID's</returns>
        public List<int> ChildDocumentIds(int FolderId)
        {
            List<int> Ids = new List<int>();
            SqlConnection conn = new SqlConnection(ConnString());
            conn.Open();
            string query = "SELECT TocId FROM [Toc] WHERE ParentId = " + FolderId + " AND [VolumeId] is not null";

            SqlCommand cmd = new SqlCommand(query, conn);
            DataTable dt = new DataTable();
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                da.Fill(dt);
            }

            foreach (DataRow row in dt.Rows) { Ids.Add((int)row[0]); }
            return Ids;
        }

        /// <summary>
        /// Gets folder name based on folder ID
        /// </summary>
        /// <param name="FolderId">The folders ID</param>
        /// <returns>Name of folder as string</returns>
        public string FolderName(int FolderId)
        {
            string name = string.Empty;

            SqlConnection conn = new SqlConnection(ConnString());
            SqlCommand cmd = new SqlCommand("SELECT [Name] FROM [Toc] WHERE TocId = " + FolderId, conn);
            conn.Open();
            name = (string)cmd.ExecuteScalar();
            conn.Close();

            return name;
        }

    }
}
