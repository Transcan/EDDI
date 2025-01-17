﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Controls;

namespace EddiEddpMonitor
{
    [ExcludeFromCodeCoverage]
    public class DataGridNumericColumn : DataGridTextColumn
    {
        protected override object PrepareCellForEdit(System.Windows.FrameworkElement editingElement, System.Windows.RoutedEventArgs editingEventArgs)
        {
            if (editingElement is TextBox edit)
            {
                edit.PreviewTextInput += OnPreviewTextInput;
            }

            return base.PrepareCellForEdit(editingElement, editingEventArgs);
        }

        void OnPreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            try
            {
                Convert.ToInt32(e.Text);
            }
            catch
            {
                // Show some kind of error message if you want

                // Set handled to true
                e.Handled = true;
            }
        }
    }
}
