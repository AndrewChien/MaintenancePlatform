using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using MaintenancePlatform.Views.Systems;
using Telerik.Windows.Controls;
using ZNC.Component.Helper;
using ZNC.DataAnalysis.BIZ.Systems;
using ZNC.DataEntiry;
using ZNC.Utility.Command;

namespace MaintenancePlatform.ViewModels.Systems
{

    /// <summary>
    /// AndrewChien 2017/10/22 10:32:21
    /// AutomaticCoder代码生成器生成
    /// </summary>
    public class DictionaryVM : ChildPageViewModel
    {
        DictionaryView View;
        internal void PageLoad(object sender, RoutedEventArgs e)
        {
            View = (DictionaryView)sender;
            LoadcmbEnable();
            LoadcmbUpName();
            LoadRadtree();
        }

        private void LoadRadtree()
        {
            View.radTreeView.Height = 500;
            View.radTreeView.Height = View.Canvas1.ActualHeight;
            View.radTreeView.Items.Clear();
            RadTreeViewItem category1 = new RadTreeViewItem();
            (category1).Header = "菜单模块";
            category1.Foreground = new SolidColorBrush(Colors.Green);
            category1.Tag = 0;
            category1.DefaultImageSrc = "/image/add.png";
            category1.IsExpanded = true;
            getAll(category1);
            View.radTreeView.Items.Add(category1);
        }

        private void LoadcmbEnable()
        {
            var collection = new DictionaryBIZ().Select(" and UplevelCode=1");
            if (collection.Count > 0)
            {
                View.cmbEnable.ItemsSource = collection;
                View.cmbEnable.DisplayMemberPath = "Name";//DisplayMemberPath显示项
                View.cmbEnable.SelectedValuePath = "Code";//SelectedValuePath绑值项
                //view.cmbType.SelectedItem = view.cmbType.Items[0];
            }
            else
            {
                UIHelper.ShowMessageBox("数据获取错误!", false);
            }
        }

        private void LoadcmbUpName()
        {
            var collection = new DictionaryBIZ().Select("");
            if (collection.Count > 0)
            {
                View.cmbUpName.ItemsSource = collection;
                View.cmbUpName.DisplayMemberPath = "Name";//DisplayMemberPath显示项
                View.cmbUpName.SelectedValuePath = "Code";//SelectedValuePath绑值项
                //view.cmbType.SelectedItem = view.cmbType.Items[0];
            }
            else
            {
                UIHelper.ShowMessageBox("数据获取错误!", false);
            }
        }

        private void getAll(RadTreeViewItem node)
        {

            ObservableCollection<Dictionary> dcCollection =
                new DictionaryBIZ().Select(" and UplevelCode = " + node.Tag);
            foreach (Dictionary s in dcCollection)
            {
                RadTreeViewItem category = new RadTreeViewItem();
                (category).Header = s.Name;

                category.Tag = s.Code;
                category.IsExpanded = true;
                category.DefaultImageSrc = "/image/add.png";
                getAll(category);
                node.Items.Add(category);

            }
        }

        #region Command

        private ICommand _BtnSearch;
        public ICommand BtnSearch
        {
            get
            {
                if (_BtnSearch == null)
                {
                    _BtnSearch = new DelegateCommand<object>(BtnSearch_Click);
                }
                return _BtnSearch;
            }
        }
        /// <summary>
        /// 查询记录
        /// </summary>
        private void BtnSearch_Click(object sender)
        {

        }

        private ICommand _BtnInsertRoot;
        public ICommand BtnInsertRoot
        {
            get
            {
                if (_BtnInsertRoot == null)
                {
                    _BtnInsertRoot = new DelegateCommand<object>(BtnInsertRoot_Click);
                }
                return _BtnInsertRoot;
            }
        }
        /// <summary>
        /// 添加根项记录
        /// </summary>
        private void BtnInsertRoot_Click(object sender)
        {
            if (MessageBoxResult.Yes == MessageBox.Show("确定添加根项？","提示",MessageBoxButton.YesNo))
            {
                var topCode = new DictionaryBIZ().SelectTop("", "Code");
                var topID= new DictionaryBIZ().SelectTop("", "ID");
                if (topCode.Count>0)
                {
                    Dictionary dc = new Dictionary();
                    dc.ID= topID[0].Code + 1;
                    dc.Code = topCode[0].Code + 1;
                    dc.Name = View.txtName.Text.Trim();
                    dc.EnableStatus = 2;
                    dc.Remark = View.txtRemark.Text;
                    dc.Type = View.txtType.Text.Trim();
                    dc.UplevelCode = 0;
                    dc.UplevelName = "0";
                    new DictionaryBIZ().Insert(dc);
                    MessageBox.Show("添加成功！");
                    LoadRadtree();
                }
                else
                {
                    UIHelper.ShowMessageBox("无法获取字典表!", false);
                }
                
            }
        }

        private ICommand _BtnInsertBranch;
        public ICommand BtnInsertBranch
        {
            get
            {
                if (_BtnInsertBranch == null)
                {
                    _BtnInsertBranch = new DelegateCommand<object>(BtnInsertBranch_Click);
                }
                return _BtnInsertBranch;
            }
        }
        /// <summary>
        /// 添加子项记录
        /// </summary>
        private void BtnInsertBranch_Click(object sender)
        {
            try
            {
                if (MessageBoxResult.Yes == MessageBox.Show("确定为" + View.radTreeView.SelectedItem + "添加子项？", "提示", MessageBoxButton.YesNo))
                {
                    var item = View.radTreeView.SelectedValue as RadTreeViewItem;
                    if (item == null)
                    {
                        MessageBox.Show("请先选中节点，再添加其子项！");
                        return;
                    }
                    var topCode = new DictionaryBIZ().SelectTop("", "Code");
                    var topID = new DictionaryBIZ().SelectTop("", "ID");
                    Dictionary dc = new Dictionary();
                    dc.ID = topID[0].Code + 1;
                    dc.Code = topCode[0].Code + 1;
                    dc.Name = View.txtName.Text.Trim();
                    dc.EnableStatus = 2;
                    dc.Remark = View.txtRemark.Text;
                    dc.Type = View.txtType.Text.Trim();
                    dc.UplevelCode = int.Parse(item.Tag.ToString());
                    dc.UplevelName = View.radTreeView.SelectedItem.ToString();
                    new DictionaryBIZ().Insert(dc);
                    MessageBox.Show("添加成功！");
                    LoadRadtree();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("添加失败！因为："+ex.Message);
            }
        }

        private ICommand _BtnDelete;
        public ICommand BtnDelete
        {
            get
            {
                if (_BtnDelete == null)
                {
                    _BtnDelete = new DelegateCommand<object>(BtnDelete_Click);
                }
                return _BtnDelete;
            }
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        private void BtnDelete_Click(object sender)
        {
            try
            {
                if (MessageBoxResult.Yes == MessageBox.Show("确定删除" + View.radTreeView.SelectedItem + "？", "提示", MessageBoxButton.YesNo))
                {
                    var item = View.radTreeView.SelectedValue as RadTreeViewItem;
                    if (item == null)
                    {
                        MessageBox.Show("请先选中节点再删除！");
                        return;
                    }
                    Dictionary dic = new Dictionary();
                    dic.Code = int.Parse(item.Tag.ToString());
                    new DictionaryBIZ().delete(dic,"Code");
                    MessageBox.Show("删除成功！");
                    LoadRadtree();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("删除失败！因为：" + ex.Message);
            }
        }

        private ICommand _BtnUpdate;
        public ICommand BtnUpdate
        {
            get
            {
                if (_BtnUpdate == null)
                {
                    _BtnUpdate = new DelegateCommand<object>(BtnUpdate_Click);
                }
                return _BtnUpdate;
            }
        }

        /// <summary>
        /// 修改记录
        /// </summary>
        private void BtnUpdate_Click(object sender)
        {
            try
            {
                if (MessageBoxResult.Yes == MessageBox.Show("确定修改" + View.radTreeView.SelectedItem + "？", "提示", MessageBoxButton.YesNo))
                {
                    var item = View.radTreeView.SelectedValue as RadTreeViewItem;
                    if (item == null)
                    {
                        MessageBox.Show("请先选中节点再修改！");
                        return;
                    }
                    Dictionary dc = new Dictionary();
                    dc.ID = int.Parse(View.txtID.Text);
                    dc.Code = int.Parse(View.txtCode.Text);
                    dc.Name = View.txtName.Text.Trim();
                    dc.EnableStatus = int.Parse(View.cmbEnable.SelectedValue.ToString());
                    dc.Remark = View.txtRemark.Text;
                    dc.Type = View.txtType.Text.Trim();
                    dc.UplevelCode = int.Parse(View.cmbUpName.SelectedValue.ToString());
                    var ddm = View.cmbUpName.SelectedItem as Dictionary;
                    dc.UplevelName = ddm.UplevelName;
                    new DictionaryBIZ().Update(dc);
                    MessageBox.Show("修改成功！");
                    LoadRadtree();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("修改失败！因为：" + ex.Message);
            }
        }

        #endregion
        #region
        private ObservableCollection<Dictionary> _DictionaryCollection;
        public ObservableCollection<Dictionary> DictionaryCollection
        {
            get
            {
                if (_DictionaryCollection == null)
                {
                    _DictionaryCollection = new ObservableCollection<Dictionary>();
                }
                return _DictionaryCollection;
            }
            set
            {
                base.SetValue(ref _DictionaryCollection, value, () => this.DictionaryCollection);

            }
        }
        #endregion
    }
}
