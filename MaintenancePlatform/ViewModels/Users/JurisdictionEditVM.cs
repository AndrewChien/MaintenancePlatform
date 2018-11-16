using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using MaintenancePlatform.Views.Users;
using ZNC.Component.Helper;
using ZNC.DataAnalysis.BIZ.Users;
using ZNC.DataEntiry;
using ZNC.Utility.Command;

namespace MaintenancePlatform.ViewModels.Users
{
    /// <summary>
    /// AndrewChien 2017/10/22 10:26:41
    /// AutomaticCoder代码生成器生成
    /// </summary>
    public class JurisdictionEditVM : ChildPageViewModel
    {
        JurisdictionEditView View;
        private Jurisdiction md;
        internal void PageLoad(object sender, RoutedEventArgs e)
        {
            View = (JurisdictionEditView)sender;

            if (View.MD != null)
            {
                //View = (FuncModuleUpdateView)sender;
                View.Title = "修改";
                View.TitleImg.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/image/modify.png"));
                md = View.MD;//页面传参
                View.txtID.Text = md.ID.ToString();
                View.txtCode.Text = md.Code.ToString();
                View.txtName.Text = md.Name;
                View.txtRemark.Text = md.Remark;
                View.txtValue.Text = md.Value;
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
                if (!InputValidity(md)) return;//验证失败
                var model = new Jurisdiction();
                model.Code = int.Parse(View.txtCode.Text);
                model.Name = View.txtName.Text.Trim();
                model.Remark = View.txtRemark.Text;
                model.Value = View.txtValue.Text;
                if (md != null) model.ID = int.Parse(View.txtID.Text);//修改需要ID的值
                if (new JurisdictionBIZ().Insert(model) > 0)
                {
                    MessageBox.Show("保存成功!");
                    View.Close();
                }
                else
                {
                    MessageBox.Show("保存失败!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败!原因是：" + ex.Message);
                UIHelper.WriteLog(ex.Message);
            }
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
        private bool InputValidity(Jurisdiction model)
        {
            bool bValid = true;
            var code = View.txtCode.Text.Trim();
            if (string.IsNullOrEmpty(code))
            {
                MessageBox.Show("代码不能为空！");
                View.txtID.Focus();
                return false;
            }
            else
            {
                if (model == null)
                {
                    if (new JurisdictionBIZ().SelectExist(" where Code =" + code))
                    {
                        MessageBox.Show("代码已存在，请重新输入！");
                        View.txtCode.Focus();
                        return false;
                    }
                }
            }
            if (string.IsNullOrEmpty(View.txtName.Text))
            {
                MessageBox.Show("请输入名称！");
                View.txtName.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(View.txtValue.Text))
            {
                MessageBox.Show("请输入值！");
                View.txtName.Focus();
                return false;
            }
            return bValid;
        }
        #endregion
    }
}
