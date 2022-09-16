﻿using HSDRawViewer.GUI;
using System.IO;
using System.Windows.Forms;

namespace HSDRawViewer.Tools
{
    public class FileIO
    {
        private static string PrevSaveLocation = null;

        private static string PrevOpenLocation = null;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static string OpenFolder()
        {
            FolderBrowserEx.FolderBrowserDialog folderBrowserDialog = new FolderBrowserEx.FolderBrowserDialog();
            folderBrowserDialog.Title = "Select a folder";
            // folderBrowserDialog.InitialFolder = @"C:\";
            folderBrowserDialog.AllowMultiSelect = false;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                return folderBrowserDialog.SelectedFolder;
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static string OpenFile(string filter, string filename = "")
        {
            using (OpenFileDialog d = new OpenFileDialog())
            {
                d.Filter = filter;
                d.FileName = filename;

                if (PrevOpenLocation != null)
                {
                    d.InitialDirectory = PrevOpenLocation;
                }

                if(d.ShowDialog() == DialogResult.OK)
                {
                    PrevOpenLocation = Path.GetDirectoryName(d.FileName);
                    return d.FileName;
                }
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static string[] OpenFiles(string filter)
        {
            using (OpenFileDialog d = new OpenFileDialog())
            {
                d.Filter = filter;
                d.Multiselect = true;

                if (d.ShowDialog() == DialogResult.OK)
                {
                    return d.FileNames;
                }
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static string SaveFile(string filter)
        {
            return SaveFile(filter, "");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static string SaveFile(string filter, string defaultName, string caption = "Save File")
        {
            using (SaveFileDialog d = new SaveFileDialog())
            {
                d.Title = caption;
                d.Filter = filter;

                d.FileName = defaultName;

                if (PrevSaveLocation != null)
                {
                    d.InitialDirectory = PrevSaveLocation;
                }

                if (d.ShowDialog() == DialogResult.OK)
                {
                    PrevSaveLocation = Path.GetDirectoryName(d.FileName);
                    return d.FileName;
                }
            }
            return null;
        }
    }
}
