using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        directory dir = new directory();
        document doc = new document();


        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Export();
        }


        private void Export()
        {
            string DestinationRootDir = tbxDestinationRoot.Text;
            
            // Make sure last char is a backslash
            if (DestinationRootDir.Substring(DestinationRootDir.Length - 1) != "\\") { DestinationRootDir = DestinationRootDir + '\\';  }

            // Get name of root folder    
            string RootFolderName = dir.FolderName(1);

            // Create root folder inside location provided by user
            if (!Directory.Exists(DestinationRootDir + RootFolderName)) { Directory.CreateDirectory(DestinationRootDir + RootFolderName); }

            // Create Child Docs
            List<int> ChildDocs = dir.ChildDocumentIds(1);
            foreach (int docId in ChildDocs)
            { 
                // Create a Folder
                if (!Directory.Exists(DestinationRootDir + RootFolderName + "\\" + doc.DocumentName(docId)) {Directory.CreateDirectory(DestinationRootDir + RootFolderName + "\\" + doc.DocumentName(docId));}

                // Get Image Paths
                List<string> ImgPaths = doc.ImagePaths(docId);

                // Move Images
                foreach (string ImgPath in ImgPaths)
                {
                    File.Copy(ImgPath, DestinationRootDir + RootFolderName + "\\" + doc.DocumentName(docId) + "\\" + ImgPath.Substring(ImgPath.LastIndexOf("\\") + 1));
                }
            }
            
            // Create Child Folders
            List<int> ChildFolders = dir.ChildFolderIds(1);
            foreach (int fid in ChildFolders)
            {
                if (!Directory.Exists(DestinationRootDir + RootFolderName + "\\" + dir.FolderName(fid))) { Directory.CreateDirectory(DestinationRootDir + RootFolderName + "\\" + dir.FolderName(fid)); }
                Recurse(0, DestinationRootDir + RootFolderName + "\\" + dir.FolderName(fid) + "\\");
            }

        }

        private void Recurse(int FolderId, string CurrentDirectory)
        { 

            // Create Child Docs
            List<int> ChildDocs = dir.ChildDocumentIds(FolderId);
            foreach (int docId in ChildDocs)
            { 
                // Create a Folder
                if (!Directory.Exists(CurrentDirectory + doc.DocumentName(docId)) {Directory.CreateDirectory(CurrentDirectory + doc.DocumentName(docId));}

                // Get Image Paths
                List<string> ImgPaths = doc.ImagePaths(docId);

                // Move Images
                foreach (string ImgPath in ImgPaths)
                {
                    File.Copy(ImgPath, CurrentDirectory + doc.DocumentName(docId) + "\\" + ImgPath.Substring(ImgPath.LastIndexOf("\\") + 1)); 
                }
            }
      

            // Create Child Folders
            List<int> ChildFolders = dir.ChildFolderIds(FolderId);
            foreach (int fid in ChildFolders)
            {
                if (!Directory.Exists(CurrentDirectory + dir.FolderName(fid))) { Directory.CreateDirectory(CurrentDirectory + dir.FolderName(fid)); }
                Recurse(fid, CurrentDirectory + dir.FolderName(fid) + "\\");
            }


        }

    }
}
