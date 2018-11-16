using System.IO;
using System.Windows;

namespace ZNC.Component.Helper
{
    /// <summary>
    /// 界面层帮助类.
    /// </summary>
    public class UIHelper
    {
        /// <summary>
        /// Displays a message box that has a message.
        /// </summary>
        /// <param name="messageBoxText">A <see cref="System.String"/> that specifies the text to display.</param>
        /// <param name="isSuccessfully">Set to <c>true</c> if this operation is successfully.</param>
        public static void ShowMessageBox(string messageBoxText, bool isSuccessfully)
        {
            MessageBox.Show(messageBoxText, "Demon", MessageBoxButton.OK, isSuccessfully ? MessageBoxImage.Information : MessageBoxImage.Warning);
        }

        /// <summary>
        /// Displays a message box that has a message.
        /// </summary>
        /// <param name="messageBoxText">A <see cref="System.String"/> that specifies the text to display.</param>
        /// <param name="messageBoxImage">A <see cref="System.Windows.MessageBoxImage"/> that specifies the image to display.</param>
        public static void ShowMessageBox(string messageBoxText, MessageBoxImage messageBoxImage)
        {
            MessageBox.Show(messageBoxText, "Demon", MessageBoxButton.OK, messageBoxImage);
        }

        /// <summary>
        /// Displays a message box that has a message, title bar caption, and button; and that returns a result.
        /// </summary>
        /// <param name="messageBoxText">A <see cref="System.String"/> that specifies the text to display.</param>
        /// <param name="button">A <see cref="System.Windows.MessageBoxButton"/> value that specifies which button or buttons to display.</param>
        /// <param name="messageBoxImage">A <see cref="System.Windows.MessageBoxImage"/> that specifies the image to display.</param>
        /// <returns>A <see cref="System.Windows.MessageBoxResult"/> value that specifies which message box button is clicked by the user.</returns>
        public static MessageBoxResult ShowMessageBox(string messageBoxText, MessageBoxButton button, MessageBoxImage messageBoxImage)
        {
            return MessageBox.Show(messageBoxText, "Demon", button);
        }

        /// <summary>
        /// Displays a operation fail message box that has a message.
        /// </summary>
        /// <param name="messageBoxText">A <see cref="System.String"/> that specifies the text to display.</param>
        public static void ShowFailMessageBox(string messageBoxText)
        {
            MessageBox.Show(messageBoxText, "Demon", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        /// <summary>
        /// Displays a confirm message box that has a message, title bar caption, and button; and that returns a result.
        /// </summary>
        /// <param name="messageBoxText">A <see cref="System.String"/> that specifies the text to display.</param>
        /// <returns>A value that specifies user confirmed.</returns>
        public static bool ShowConfirmMessageBox(string messageBoxText)
        {
            return MessageBox.Show(messageBoxText, "Demon", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
        }

        /// <summary>
        /// Displays a exit confirm message box that has a message, title bar caption, and button; and that returns a result.
        /// </summary>
        /// <returns>A value that specifies user confirm to exit.</returns>
        public static bool ShowConfirmExitMessageBox()
        {
            return MessageBox.Show("Demon", "Demon", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
        }

        /// <summary>
        /// Displays a delete confirm message box that has a message, title bar caption, and button; and that returns a result.
        /// </summary>
        /// <returns>A value that specifies user confirm to delete.</returns>
        public static bool ShowConfirmDeleteMessageBox()
        {
            return MessageBox.Show("Demon", "Demon", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
        }

        public static void WriteLog(string message)
        {
            string path = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "log.txt");

            if (!File.Exists(path))
            {
                FileStream fs = File.Create(path);
                fs.Close();//关闭文件流
                fs.Dispose();//释放掉资源
            }

            StreamWriter writer = new StreamWriter(path, true, System.Text.Encoding.UTF8);
            writer.WriteLine(message);
            writer.Close();
            writer.Dispose();
        }

    }
}
