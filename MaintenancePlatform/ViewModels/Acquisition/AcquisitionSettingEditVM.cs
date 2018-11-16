using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using MaintenancePlatform.Views.Acquisition;
using ZNC.Component.Helper;
using ZNC.DataAnalysis.BIZ.Acquisition;
using ZNC.DataEntiry;
using ZNC.Utility.Command;

namespace MaintenancePlatform.ViewModels.Acquisition
{
    /// <summary>
    /// AndrewChien 2017/10/22 10:26:41
    /// AutomaticCoder代码生成器生成
    /// </summary>
    public class AcquisitionSettingEditVM : ChildPageViewModel
    {
        AcquisitionSettingEditView View;
        AcquisitionSetting md;
        internal void PageLoad(object sender, RoutedEventArgs e)
        {
            View = (AcquisitionSettingEditView)sender;
            if (View.MD != null)
            {
                //View = (FuncModuleUpdateView)sender;
                View.Title = "修改";
                View.TitleImg.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/image/modify.png"));
                md = View.MD;//页面传参
                View.txtFuncID.Text = md.ID.ToString();
                View.txtFuncName.Text = md.AcquisitionName;
                View.txtFuncName.Text = md.Code.ToString();
            }
            else
            {
                View.Title = "添加";
                View.TitleImg.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/image/add.png"));
                //View.txtgnbh.Focus();
            }
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
            try
            {
                if (!InputValidity(md))
                    return;
                var model = new AcquisitionSetting();
                model.Code = int.Parse(View.cmbSys.Text);
                model.AcquisitionName = View.txtFuncName.Text.Trim();
                model.Remark = View.txtFuncName.Text;
                if (md != null) model.ID = int.Parse(View.txtFuncID.Text);//修改需要ID的值
                if (new AcquisitionSettingBIZ().Insert(model) > 0)
                {
                    MessageBox.Show("保存成功!");

                    View.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("保存失败!");
                UIHelper.WriteLog(e.Message);
            }
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

            //Microsoft.Win32.OpenFileDialog myDialog = new Microsoft.Win32.OpenFileDialog();
            //myDialog.Filter = "图片|*.jpg;*.png;*.gif;*.bmp;*.jpeg";
            //Nullable<bool> result = myDialog.ShowDialog();
            //if (result == true)
            //{
            //    View.txtImg.Text = myDialog.FileName.Substring(myDialog.FileName.LastIndexOf("\\") + 1);
            //}

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
            View.Close();
        }

        /// <summary>
        /// 数据保存前判别各项录入数据的有效性
        /// </summary>
        /// <returns></returns>
        private bool InputValidity(AcquisitionSetting model)
        {
            bool bValid = true;
            string ID = View.txtFuncID.Text.Trim();
            if (string.IsNullOrEmpty(ID))
            {
                MessageBox.Show("功能编号不能为空！");
                View.txtFuncID.Focus();
                return false;
            }
            else
            {
                if (model == null)
                {
                    if (new AcquisitionSettingBIZ().SelectExist(" where ID =" + ID))
                    {
                        MessageBox.Show("功能编号已存在，请重新输入！");
                        View.txtFuncID.Focus();
                        return false;
                    }
                }
            }
            string glxt = View.cmbSys.Text.Trim();
            if (string.IsNullOrEmpty(glxt))
            {
                MessageBox.Show("没有关联系统！");

                return false;
            }
            string gnmc = View.txtFuncName.Text.Trim();
            if (string.IsNullOrEmpty(gnmc))
            {
                MessageBox.Show("功能名称不能为空！");
                View.txtFuncName.Focus();
                return false;
            }
            else
            {
                if (new AcquisitionSettingBIZ().SelectExist(" where ID =" + ID))
                {
                    MessageBox.Show("功能名称已存在，请重新输入！");
                    View.txtFuncID.Focus();
                    return false;
                }
            }
            string path = View.txtPath.Text.Trim();
            if (string.IsNullOrEmpty(path))
            {
                MessageBox.Show("功能链接不能为空！");
                View.txtPath.Focus();
                return false;
            }
            return bValid;
        }
        #endregion
    }
}
