﻿using Syncfusion.Windows.PdfViewer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ICMS.View.UC_Dialog
{
    /// <summary>
    /// Interaction logic for UC_ViewCertificatePdfFile_Dialog.xaml
    /// </summary>
    public partial class UC_ViewCertificatePdfFile_Dialog : UserControl
    {
        public UC_ViewCertificatePdfFile_Dialog()
        {
            InitializeComponent();
            HideVerticalToolbar();

        }

        /// <summary>
        /// Hides the left side (vertical) toolbar of PDF Viewer.
        /// </summary>
        private void HideVerticalToolbar()
        {
            // Hides the thumbnail icon. 
            pdfViewer.ThumbnailSettings.IsVisible = false;

            // Hides the bookmark icon. 
            pdfViewer.IsBookmarkEnabled = false;

            // Hides the layer icon. 
            pdfViewer.EnableLayers = false;

            // Hides the organize page icon. 
            pdfViewer.PageOrganizerSettings.IsIconVisible = false;

            // Hides the redaction icon. 
            pdfViewer.EnableRedactionTool = false;

            // Hides the form icon. 
            pdfViewer.FormSettings.IsIconVisible = false;

        
        }

        #region Helper Methods
        private void HideOpenTool()
        {
            // Get the instance of the toolbar using its template name 
            DocumentToolbar toolbar = pdfViewer.Template.FindName("PART_Toolbar", pdfViewer) as DocumentToolbar;

            // Get the instance of the file menu button using its template name.
            ToggleButton FileButton = (ToggleButton)toolbar.Template.FindName("PART_FileToggleButton", toolbar);

            //Get the instance of the file menu button context menu and the item collection.
            ContextMenu FileContextMenu = FileButton.ContextMenu;
            foreach (MenuItem FileMenuItem in FileContextMenu.Items)
            {
                //Get the instance of the open menu item using its template name and disable its visibility.
                if (FileMenuItem.Name == "PART_OpenMenuItem")
                {
                    //Set the visibility of the item to collapsed.
                    FileMenuItem.Visibility = Visibility.Collapsed;
                }
            }
        }


        private void HideUnnecessaryButton()
        {
            DocumentToolbar toolbar = pdfViewer.Template.FindName("PART_Toolbar", pdfViewer) as DocumentToolbar;

            // HideOpenTool
            ToggleButton FileButton = (ToggleButton)toolbar.Template.FindName("PART_FileToggleButton", toolbar);
            ContextMenu FileContextMenu = FileButton.ContextMenu;
            foreach (MenuItem FileMenuItem in FileContextMenu.Items)
            {
                if (FileMenuItem.Name == "PART_OpenMenuItem")
                {
                    FileMenuItem.Visibility = Visibility.Collapsed;
                }
            }



            // First Page Tool
            Button goFirstPageButton = (Button)toolbar.Template.FindName("PART_ButtonGoToFirstPage", toolbar);
            goFirstPageButton.Visibility = System.Windows.Visibility.Collapsed;

            // FLast Page Tool
            Button goLastPageButton = (Button)toolbar.Template.FindName("PART_ButtonGoToLastPage", toolbar);
            goLastPageButton.Visibility = System.Windows.Visibility.Collapsed;

            // Search tool
            Button textSearchButton = (Button)toolbar.Template.FindName("PART_ButtonTextSearch", toolbar);
            textSearchButton.Visibility = System.Windows.Visibility.Collapsed;

            //  HideThumbnailTool
            OutlinePane outlinePane = pdfViewer.Template.FindName("PART_OutlinePane", pdfViewer) as OutlinePane;
            ToggleButton thumbnailButton = (ToggleButton)outlinePane.Template.FindName("PART_ThumbnailButton", outlinePane);
            thumbnailButton.Visibility = Visibility.Collapsed;

            // Sticky note tool
            ToggleButton stickyNoteButton = (ToggleButton)toolbar.Template.FindName("PART_StickyNote", toolbar);
            stickyNoteButton.Visibility = Visibility.Collapsed;

        }

        #endregion

        #region Handlers
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            HideUnnecessaryButton();
        }
        #endregion

    }


}
