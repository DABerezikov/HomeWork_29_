using System.Windows;
using HomeWork_29_.Services.Interfaces;
using HomeWork_29_.ViewModels;
using HomeWork_29_.Views.Windows;
using HomeWork_29_DB.Entityes;

namespace HomeWork_29_.Services
{
    internal class UserDialog : IUserDialog
    {
        public bool Edit(Product product)
        {
            var product_editor_model = new ProductEditorViewModel(product);

            var product_editor_window = new ProductEditorWindow()
            {
                DataContext = product_editor_model
            };

            if (product_editor_window.ShowDialog() != true) return false;

            product.Name = product_editor_model.Name;

            return true;
        }

        public bool ConfirmInformation(string Information, string Caption) => MessageBox
            .Show(
                Information, Caption,
                MessageBoxButton.YesNo,
                MessageBoxImage.Information) 
                == MessageBoxResult.Yes;

        public bool ConfirmWarning(string Warning, string Caption) => MessageBox
             .Show(
                 Warning, Caption,
                 MessageBoxButton.YesNo,
                 MessageBoxImage.Warning) == MessageBoxResult.Yes;

        public bool ConfirmError(string Error, string Caption) => MessageBox
            .Show(
                Error, Caption,
                MessageBoxButton.YesNo,
                MessageBoxImage.Error) == MessageBoxResult.Yes;
    }


}
