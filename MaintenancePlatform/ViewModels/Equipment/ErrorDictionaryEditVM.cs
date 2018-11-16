using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using MaintenancePlatform.Views.Equipment;
using ZNC.DataEntiry;
using ZNC.Utility.Command;

namespace MaintenancePlatform.ViewModels.Equipment
{
    /// <summary>
    /// AndrewChien 2017/10/22 10:26:41
    /// AutomaticCoder代码生成器生成
    /// </summary>
    public class ErrorDictionaryEditVM : ChildPageViewModel
    {
        ErrorDictionaryEditView View;
        internal void PageLoad(object sender, RoutedEventArgs e)
        {
            View = (ErrorDictionaryEditView)sender;
        }

        #region Command
        private ICommand _BtnSave;
        public ICommand BtnSave
        {
            get
            {
                if (_BtnSave == null)
                {
                    _BtnSave = new DelegateCommand(BtnSave_Click);
                }
                return _BtnSave;
            }
        }
        //保存
        private void BtnSave_Click()
        {
            bool success = false;
        }

        private ICommand _BtnUpload;
        public ICommand BtnUpload
        {
            get
            {
                if (_BtnUpload == null)
                {
                    _BtnUpload = new DelegateCommand(BtnUpload_Click);
                }
                return _BtnUpload;
            }
        }

        /// <summary>
        /// 保存图片路径
        /// </summary>
        private void BtnUpload_Click()
        {

        }



        private ICommand _BtnReturn;
        public ICommand BtnReturn
        {
            get
            {
                if (_BtnReturn == null)
                {
                    _BtnReturn = new DelegateCommand(BtnReturn_Click);
                }
                return _BtnReturn;
            }
        }
        //关闭窗口
        private void BtnReturn_Click()
        {

        }

        /// <summary>
        /// 数据保存前判别各项录入数据的有效性
        /// </summary>
        /// <returns></returns>
        private bool InputValidity()
        {
            bool bValid = true;
            return bValid;
        }
        #endregion
    }
}
