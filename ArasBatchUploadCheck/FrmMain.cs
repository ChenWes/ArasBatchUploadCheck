using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Threading;
using System.Threading.Tasks;
using Aras.IOM;

namespace ArasBatchUploadCheck
{
    public partial class FrmMain : Form
    {
        private CancellationTokenSource mc_sourceArasInnovatorConnection;
        private CancellationTokenSource mc_sourceCheckGarmentStyle;

        private HttpServerConnection mc_conn = null;
        private Innovator mc_innovator = null;

        private List<AttList> mc_List = new List<AttList>();

        enum AttributeType
        {
            ProductType,
            ProductCatetory,
            SubCatetory,

            GarmentType,
            Collection,
            Series,
            Gender,

            Cuff,
            Making,
            Fit,
            Pocket,

            Collar,
            Placket,
            Sleeve,
            Styling,

            Washing
        }

        private string GetAttributeColumnName(AttributeType pi_type)
        {
            string l_listname = string.Empty;

            switch (pi_type)
            {
                case AttributeType.ProductType:
                    l_listname = "cn_class0";
                    break;
                case AttributeType.ProductCatetory:
                    l_listname = "cn_class1";
                    break;
                case AttributeType.SubCatetory:
                    l_listname = "cn_class2";
                    break;
                case AttributeType.GarmentType:
                    l_listname = "cn_garment_type";
                    break;
                case AttributeType.Collection:
                    l_listname = "cn_collection";
                    break;
                case AttributeType.Series:
                    l_listname = "cn_series";
                    break;
                case AttributeType.Gender:
                    l_listname = "cn_gender";
                    break;
                case AttributeType.Cuff:
                    l_listname = "cn_cuff";
                    break;
                case AttributeType.Making:
                    l_listname = "cn_making";
                    break;
                case AttributeType.Fit:
                    l_listname = "cn_fit";
                    break;
                case AttributeType.Pocket:
                    l_listname = "cn_pocket";
                    break;
                case AttributeType.Collar:
                    l_listname = "cn_collar";
                    break;
                case AttributeType.Placket:
                    l_listname = "cn_placket";
                    break;
                case AttributeType.Sleeve:
                    l_listname = "cn_sleeve";
                    break;
                case AttributeType.Styling:
                    l_listname = "cn_styling";
                    break;
                case AttributeType.Washing:
                    l_listname = "cn_washing";
                    break;
                default:
                    break;
            }

            return l_listname;
        }

        private string GetAttributeListName(AttributeType pi_type)
        {
            string l_columnname = string.Empty;

            switch (pi_type)
            {
                case AttributeType.ProductType:
                    l_columnname = "GarmentType";
                    break;
                case AttributeType.ProductCatetory:
                    l_columnname = "Product Category";
                    break;
                case AttributeType.SubCatetory:
                    l_columnname = "Sub-Category";
                    break;
                case AttributeType.GarmentType:
                    l_columnname = "Garment Type";
                    break;
                case AttributeType.Collection:
                    l_columnname = "Collection";
                    break;
                case AttributeType.Series:
                    l_columnname = "Series";
                    break;
                case AttributeType.Gender:
                    l_columnname = "Gender";
                    break;
                case AttributeType.Cuff:
                    l_columnname = "Cuff";
                    break;
                case AttributeType.Making:
                    l_columnname = "Marking";
                    break;
                case AttributeType.Fit:
                    l_columnname = "Fit";
                    break;
                case AttributeType.Pocket:
                    l_columnname = "Pocket";
                    break;
                case AttributeType.Collar:
                    l_columnname = "Collar";
                    break;
                case AttributeType.Placket:
                    l_columnname = "Placket";
                    break;
                case AttributeType.Sleeve:
                    l_columnname = "Sleeve";
                    break;
                case AttributeType.Styling:
                    l_columnname = "Styling";
                    break;
                case AttributeType.Washing:
                    l_columnname = "Garment Washing";
                    break;
                default:
                    break;
            }

            return l_columnname;
        }

        public FrmMain()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;//解决线程安全


            SettingConnectionButton(false);

            pro_CheckItem.Minimum = 0;
            pro_CheckItem.Maximum = 100;
        }

        //--------------------------------------------------------------------------------------------

        private HttpServerConnection GetConnection(object para)
        {
            ArasConnectionPara l_connectionPara = (ArasConnectionPara)para;
            return IomFactory.CreateHttpServerConnection(l_connectionPara.l_serverurl.Trim(), l_connectionPara.l_db.Trim(), l_connectionPara.l_username.Trim(), Innovator.ScalcMD5(l_connectionPara.l_password.Trim()));
        }

        private Innovator GetInnovator(HttpServerConnection pi_conn)
        {
            return IomFactory.CreateInnovator(pi_conn);
        }

        private Item InnovatorLogin(HttpServerConnection pi_innovator)
        {
            return pi_innovator.Login();
        }

        private void ShowError(string errormessage)
        {
            MessageBox.Show(errormessage);
        }

        private void ShowError(Exception ex)
        {
            MessageBox.Show(ex.StackTrace, "System Run Error");
        }

        private void SettingProcess(int maxValue, int CurrentValue)
        {
            decimal getvalue = CurrentValue * 100 / maxValue;
            pro_CheckItem.Value = (int)getvalue;
            pro_CheckItem.Refresh();
        }

        private void SettingConnectionButton(bool bln_ConnectionFlag)
        {
            if(bln_ConnectionFlag==false)
            {
                mc_innovator = null;
                if(mc_conn!=null)
                {
                    mc_conn.Logout();
                    mc_conn = null;
                }
            }

            btn_ConnectionAras.Enabled = !bln_ConnectionFlag;
            btn_disconnection.Enabled = bln_ConnectionFlag;


            txt_serverurl.Enabled = !bln_ConnectionFlag;
            txt_DB.Enabled = !bln_ConnectionFlag;
            txt_username.Enabled = !bln_ConnectionFlag;
            txt_password.Enabled = !bln_ConnectionFlag;


            //----------------------------------------------

            btn_CheckItem.Enabled = bln_ConnectionFlag;
            btn_FixGarmentStyle.Enabled = bln_ConnectionFlag;
            btn_Cancel.Enabled = bln_ConnectionFlag;

            txt_SearchAML.Enabled = bln_ConnectionFlag;
            txt_SearchItem.Enabled = bln_ConnectionFlag;
            tre_Item.Enabled = bln_ConnectionFlag;

            if (bln_ConnectionFlag)
            {
                ClearCheckInfomation();                
            }
        }

        private void SettingProcessButton(bool bln_Cancel)
        {
            btn_CheckItem.Enabled = bln_Cancel;
            btn_FixGarmentStyle.Enabled = bln_Cancel;
            btn_Cancel.Enabled = !bln_Cancel;

            //txt_SearchAML.Enabled = !bln_Cancel;
            //txt_SearchItem.Enabled = !bln_Cancel;
            //tre_Item.Enabled = !bln_Cancel;
        }

        private void ClearCheckInfomation()
        {
            tre_Item.Nodes.Clear();
            txt_SearchItem.Clear();
        }

        private Item GetItemByAML(string pi_AML)
        {
            Item l_returnItem = null;
            if (string.IsNullOrEmpty(pi_AML))
            {
                return l_returnItem;
            }
            else
            {
                pi_AML = "<AML>" + pi_AML + "</AML>";
            }

            l_returnItem = mc_innovator.applyAML(pi_AML);

            return l_returnItem;
        }

        private AttList GetAttListByValue(string pi_ListName)
        {
            AttList ListItem = new AttList();

            StringBuilder temp_AML = new StringBuilder();
            temp_AML.Append("<Item type=\"List\" select=\"id,name\" action=\"get\">");
            temp_AML.Append("    <name>{0}</name>");
            temp_AML.Append("    <Relationships>");
            temp_AML.Append("        <Item type=\"Value\" select=\"value,label\" action=\"get\">");
            temp_AML.Append("        </Item>");
            temp_AML.Append("    </Relationships>");
            temp_AML.Append("</Item>");

            string l_getAML = string.Format(temp_AML.ToString(), pi_ListName);

            Item l_getListItem = GetItemByAML(l_getAML);

            if (l_getListItem.isError() || l_getListItem.isEmpty())
            {
                return null;
            }

            Item l_getValue_R = l_getListItem.getRelationships("Value");

            ListItem.id = l_getListItem.getProperty("id", "");
            ListItem.List = l_getListItem.getProperty("name", "");

            List<Value> l_valueList = new List<Value>();
            for (int ItemIDX = 0; ItemIDX < l_getValue_R.getItemCount(); ItemIDX++)
            {
                Item l_getValue_R_Item = l_getValue_R.getItemByIndex(ItemIDX);

                l_valueList.Add(new Value()
                {
                    value = l_getValue_R_Item.getProperty("value", ""),
                    label = l_getValue_R_Item.getProperty("label", "")
                });
            }

            ListItem.Value = l_valueList;


            return ListItem;
        }

        private AttList GetAttListByFilterValue(string pi_ListName)
        {
            AttList ListItem = new AttList();

            StringBuilder temp_AML = new StringBuilder();
            temp_AML.Append("<Item type=\"List\" select=\"id,name\" action=\"get\">");
            temp_AML.Append("    <name>{0}</name>");
            temp_AML.Append("    <Relationships>");
            temp_AML.Append("        <Item type=\"Filter Value\" select=\"filter,value,label\" action=\"get\">");
            temp_AML.Append("        </Item>");
            temp_AML.Append("    </Relationships>");
            temp_AML.Append("</Item>");

            string l_getAML = string.Format(temp_AML.ToString(), pi_ListName);

            Item l_getListItem = GetItemByAML(l_getAML);

            if (l_getListItem.isError() || l_getListItem.isEmpty())
            {
                return null;
            }

            Item l_getValue_R = l_getListItem.getRelationships("Filter Value");

            ListItem.id = l_getListItem.getProperty("id", "");
            ListItem.List = l_getListItem.getProperty("name", "");

            List<FilterValue> l_filtervalueList = new List<FilterValue>();
            for (int ItemIDX = 0; ItemIDX < l_getValue_R.getItemCount(); ItemIDX++)
            {
                Item l_getValue_R_Item = l_getValue_R.getItemByIndex(ItemIDX);

                l_filtervalueList.Add(new FilterValue()
                {
                    filter = l_getValue_R_Item.getProperty("filter", ""),
                    value = l_getValue_R_Item.getProperty("value", ""),
                    label = l_getValue_R_Item.getProperty("label", "")
                });
            }

            ListItem.FilterValue = l_filtervalueList;

            return ListItem;
        }

        private void GetAttributeList()
        {
            foreach (AttributeType Item in (AttributeType[])System.Enum.GetValues(typeof(AttributeType)))
            {
                AttList l_getData = new AttList();
                switch (Item)
                {
                    case AttributeType.ProductType:
                        l_getData = GetAttListByValue(GetAttributeListName(Item));//**
                        break;
                    case AttributeType.ProductCatetory:
                        l_getData = GetAttListByFilterValue(GetAttributeListName(Item));
                        break;
                    case AttributeType.SubCatetory:
                        l_getData = GetAttListByFilterValue(GetAttributeListName(Item));
                        break;
                    case AttributeType.GarmentType:
                        l_getData = GetAttListByValue(GetAttributeListName(Item));//**
                        break;
                    case AttributeType.Collection:
                        l_getData = GetAttListByFilterValue(GetAttributeListName(Item));
                        break;
                    case AttributeType.Series:
                        l_getData = GetAttListByFilterValue(GetAttributeListName(Item));
                        break;
                    case AttributeType.Gender:
                        l_getData = GetAttListByValue(GetAttributeListName(Item));//**
                        break;
                    case AttributeType.Cuff:
                        l_getData = GetAttListByFilterValue(GetAttributeListName(Item));
                        break;
                    case AttributeType.Making:
                        l_getData = GetAttListByFilterValue(GetAttributeListName(Item));
                        break;
                    case AttributeType.Fit:
                        l_getData = GetAttListByFilterValue(GetAttributeListName(Item));
                        break;
                    case AttributeType.Pocket:
                        l_getData = GetAttListByFilterValue(GetAttributeListName(Item));
                        break;
                    case AttributeType.Collar:
                        l_getData = GetAttListByFilterValue(GetAttributeListName(Item));
                        break;
                    case AttributeType.Placket:
                        l_getData = GetAttListByFilterValue(GetAttributeListName(Item));
                        break;
                    case AttributeType.Sleeve:
                        l_getData = GetAttListByFilterValue(GetAttributeListName(Item));
                        break;
                    case AttributeType.Styling:
                        l_getData = GetAttListByFilterValue(GetAttributeListName(Item));
                        break;
                    case AttributeType.Washing:
                        l_getData = GetAttListByValue(GetAttributeListName(Item));//**
                        break;
                    default:
                        break;
                }

                if (l_getData != null)
                {
                    mc_List.Add(l_getData);
                }
            }
        }

        private bool CheckValue_MC(AttList ListItem, string fieldValue)
        {
            if (string.IsNullOrEmpty(fieldValue))
            {
                return true;
            }
            bool bln_flag = ListItem.Value.FirstOrDefault(t => t.label.ToLower() == fieldValue.ToLower()) == null ? false : true;
            if (!bln_flag)
            {
                bln_flag = ListItem.Value.FirstOrDefault(t => t.value.ToLower() == fieldValue.ToLower()) == null ? false : true;
            }

            return bln_flag;
        }

        private bool CheckFilterValue_MC(AttList ListItem, string filter, string fieldValue)
        {
            if (string.IsNullOrEmpty(fieldValue))
            {
                return true;
            }
            bool bln_flag = ListItem.FilterValue.FirstOrDefault(t => t.filter.ToLower() == filter.ToLower() && t.label.ToLower() == fieldValue.ToLower()) == null ? false : true;
            if (!bln_flag)
            {
                bln_flag = ListItem.FilterValue.FirstOrDefault(t => t.filter.ToLower() == filter.ToLower() && t.value.ToLower() == fieldValue.ToLower()) == null ? false : true;
            }
            return bln_flag;
        }

        //------------------------------
        private string GetValue_ByLabel(AttList ListItem, string fieldlabel)
        {
            string l_returnValue = string.Empty;
            var getValue = ListItem.Value.FirstOrDefault(t => t.value.ToLower() == fieldlabel.ToLower());
            if (getValue == null)
            {
                var getValueByValue = ListItem.Value.FirstOrDefault(t => t.label.ToLower() == fieldlabel.ToLower());
                if (getValueByValue != null)
                {
                    l_returnValue = l_returnValue;
                }
            }
            else
            {
                l_returnValue = getValue.value;
            }
            return l_returnValue;
        }

        private string GetFilterValue_ByLabel(AttList ListItem, string filter, string fieldlabel)
        {
            string l_returnValue = string.Empty;

            var getValue = ListItem.FilterValue.FirstOrDefault(t => t.filter.ToLower() == filter.ToLower() && t.value.ToLower() == fieldlabel.ToLower());
            if (getValue == null)
            {
                var getValueByLabel = ListItem.FilterValue.FirstOrDefault(t => t.filter.ToLower() == filter.ToLower() && t.label.ToLower() == fieldlabel.ToLower());
                if (getValueByLabel != null)
                {
                    l_returnValue = getValueByLabel.value;
                }
            }
            else
            {
                l_returnValue = getValue.value;
            }
            return l_returnValue;
        }
        //------------------------------

        private AttList GetAttListByName(string pi_attListName)
        {
            return mc_List.FirstOrDefault(t => t.List.ToLower() == pi_attListName.ToLower());
        }

        //get value from parent
        private string GetAttListFilterValue(AttributeType pi_Value, Item garmentItem)
        {
            string l_returnFilter = string.Empty;

            switch (pi_Value)
            {
                case AttributeType.ProductType:
                    l_returnFilter = GetValue_ByLabel(GetAttListByName(GetAttributeListName(pi_Value)), garmentItem.getProperty(GetAttributeColumnName(pi_Value), ""));
                    break;
                case AttributeType.ProductCatetory:
                    l_returnFilter = GetFilterValue_ByLabel(GetAttListByName(GetAttributeListName(pi_Value)), GetAttListFilterValue(AttributeType.ProductType, garmentItem), garmentItem.getProperty(GetAttributeColumnName(pi_Value), ""));
                    break;
                case AttributeType.SubCatetory:
                    l_returnFilter = GetFilterValue_ByLabel(GetAttListByName(GetAttributeListName(pi_Value)), GetAttListFilterValue(AttributeType.ProductCatetory, garmentItem), garmentItem.getProperty(GetAttributeColumnName(pi_Value), ""));
                    break;
                //case AttributeType.GarmentType:
                //    break;
                case AttributeType.Collection:
                case AttributeType.Series:
                //case AttributeType.Gender:
                //    break;
                case AttributeType.Cuff:
                case AttributeType.Making:
                case AttributeType.Fit:
                case AttributeType.Pocket:
                case AttributeType.Collar:
                case AttributeType.Placket:
                case AttributeType.Sleeve:
                case AttributeType.Styling:
                    l_returnFilter = GetFilterValue_ByLabel(GetAttListByName(GetAttributeListName(pi_Value)), GetAttListFilterValue(AttributeType.SubCatetory, garmentItem), garmentItem.getProperty(GetAttributeColumnName(pi_Value), ""));
                    break;
                //case AttributeType.Washing:
                //    break;
                default:
                    break;
            }


            return l_returnFilter;
        }

        private CheckItemStatus CheckGarmentItem(Item garmentItem)
        {
            CheckItemStatus StatusItem = new CheckItemStatus();

            StatusItem.ItemNumber = garmentItem.getProperty("item_number", "");
            StatusItem.id = garmentItem.getProperty("id", "");

            #region foreach

            foreach (AttributeType Item in (AttributeType[])System.Enum.GetValues(typeof(AttributeType)))
            {
                switch (Item)
                {
                    case AttributeType.ProductType:
                        StatusItem.bln_ProductType = CheckValue_MC(GetAttListByName(GetAttributeListName(Item)), garmentItem.getProperty(GetAttributeColumnName(Item), ""));//***
                        if (!StatusItem.bln_ProductType)
                        {
                            StatusItem.bln_Check = false;
                        }
                        break;
                    case AttributeType.ProductCatetory:
                        if (!StatusItem.bln_ProductType)
                        {
                            StatusItem.bln_ProductCategory = false;
                            StatusItem.bln_Check = false;
                        }
                        else
                        {
                            StatusItem.bln_ProductCategory = CheckFilterValue_MC(GetAttListByName(GetAttributeListName(Item)), GetAttListFilterValue(AttributeType.ProductType,garmentItem), garmentItem.getProperty(GetAttributeColumnName(Item), ""));                            
                        }
                        break;
                    case AttributeType.SubCatetory:
                        if (!StatusItem.bln_ProductCategory)
                        {
                            StatusItem.bln_SubCategory = false;
                            StatusItem.bln_Check = false;
                        }
                        else
                        {
                            StatusItem.bln_SubCategory = CheckFilterValue_MC(GetAttListByName(GetAttributeListName(Item)), GetAttListFilterValue(AttributeType.ProductCatetory, garmentItem), garmentItem.getProperty(GetAttributeColumnName(Item), ""));
                        }
                        break;
                    case AttributeType.GarmentType:
                        StatusItem.bln_GarmentType = CheckValue_MC(GetAttListByName(GetAttributeListName(Item)), garmentItem.getProperty(GetAttributeColumnName(Item), ""));//***
                        if (!StatusItem.bln_GarmentType)
                        {
                            StatusItem.bln_Check = false;
                        }
                        break;
                    case AttributeType.Collection:
                        if (!StatusItem.bln_SubCategory)
                        {
                            StatusItem.bln_Collection = false;
                            StatusItem.bln_Check = false;
                        }
                        else
                        {
                            StatusItem.bln_Collection = CheckFilterValue_MC(GetAttListByName(GetAttributeListName(Item)), GetAttListFilterValue(AttributeType.SubCatetory, garmentItem), garmentItem.getProperty(GetAttributeColumnName(Item), ""));
                            if(!StatusItem.bln_Collection)
                            {
                                StatusItem.bln_Check = false;
                            }
                        }
                        break;
                    case AttributeType.Series:
                        if (!StatusItem.bln_SubCategory)
                        {
                            StatusItem.bln_Series = false;
                            StatusItem.bln_Check = false;
                        }
                        else
                        {
                            StatusItem.bln_Series = CheckFilterValue_MC(GetAttListByName(GetAttributeListName(Item)), GetAttListFilterValue(AttributeType.SubCatetory, garmentItem), garmentItem.getProperty(GetAttributeColumnName(Item), ""));
                            if (!StatusItem.bln_Series)
                            {
                                StatusItem.bln_Check = false;
                            }
                        }
                        break;
                    case AttributeType.Gender:
                        StatusItem.bln_Gender = CheckValue_MC(GetAttListByName(GetAttributeListName(Item)), garmentItem.getProperty(GetAttributeColumnName(Item), ""));//***
                        if (!StatusItem.bln_Gender)
                        {
                            StatusItem.bln_Check = false;
                        }
                        break;
                    case AttributeType.Cuff:
                        if (!StatusItem.bln_SubCategory)
                        {
                            StatusItem.bln_Cuff = false;
                            StatusItem.bln_Check = false;
                        }
                        else
                        {
                            StatusItem.bln_Cuff = CheckFilterValue_MC(GetAttListByName(GetAttributeListName(Item)), GetAttListFilterValue(AttributeType.SubCatetory, garmentItem), garmentItem.getProperty(GetAttributeColumnName(Item), ""));
                            if (!StatusItem.bln_Cuff)
                            {
                                StatusItem.bln_Check = false;
                            }
                        }
                        break;
                    case AttributeType.Making:
                        if (!StatusItem.bln_SubCategory)
                        {
                            StatusItem.bln_Making = false;
                            StatusItem.bln_Check = false;
                        }
                        else
                        {
                            StatusItem.bln_Making = CheckFilterValue_MC(GetAttListByName(GetAttributeListName(Item)), GetAttListFilterValue(AttributeType.SubCatetory, garmentItem), garmentItem.getProperty(GetAttributeColumnName(Item), ""));
                            if (!StatusItem.bln_Making)
                            {
                                StatusItem.bln_Check = false;
                            }
                        }
                        break;
                    case AttributeType.Fit:
                        if (!StatusItem.bln_SubCategory)
                        {
                            StatusItem.bln_Fit = false;
                            StatusItem.bln_Check = false;
                        }
                        else
                        {
                            StatusItem.bln_Fit = CheckFilterValue_MC(GetAttListByName(GetAttributeListName(Item)), GetAttListFilterValue(AttributeType.SubCatetory, garmentItem), garmentItem.getProperty(GetAttributeColumnName(Item), ""));
                            if (!StatusItem.bln_Fit)
                            {
                                StatusItem.bln_Check = false;
                            }
                        }
                        break;
                    case AttributeType.Pocket:
                        if (!StatusItem.bln_SubCategory)
                        {
                            StatusItem.bln_Pocket = false;
                            StatusItem.bln_Check = false;
                        }
                        else
                        {
                            StatusItem.bln_Pocket = CheckFilterValue_MC(GetAttListByName(GetAttributeListName(Item)), GetAttListFilterValue(AttributeType.SubCatetory, garmentItem), garmentItem.getProperty(GetAttributeColumnName(Item), ""));
                            if (!StatusItem.bln_Pocket)
                            {
                                StatusItem.bln_Check = false;
                            }
                        }
                        break;
                    case AttributeType.Collar:
                        if (!StatusItem.bln_SubCategory)
                        {
                            StatusItem.bln_Collar = false;
                            StatusItem.bln_Check = false;
                        }
                        else
                        {
                            StatusItem.bln_Collar = CheckFilterValue_MC(GetAttListByName(GetAttributeListName(Item)), GetAttListFilterValue(AttributeType.SubCatetory, garmentItem), garmentItem.getProperty(GetAttributeColumnName(Item), ""));
                            if (!StatusItem.bln_Collar)
                            {
                                StatusItem.bln_Check = false;
                            }
                        }
                        break;
                    case AttributeType.Placket:
                        if (!StatusItem.bln_SubCategory)
                        {
                            StatusItem.bln_Placket = false;
                            StatusItem.bln_Check = false;
                        }
                        else
                        {
                            StatusItem.bln_Placket = CheckFilterValue_MC(GetAttListByName(GetAttributeListName(Item)), GetAttListFilterValue(AttributeType.SubCatetory, garmentItem), garmentItem.getProperty(GetAttributeColumnName(Item), ""));
                            if (!StatusItem.bln_Placket)
                            {
                                StatusItem.bln_Check = false;
                            }
                        }
                        break;
                    case AttributeType.Sleeve:
                        if (!StatusItem.bln_SubCategory)
                        {
                            StatusItem.bln_Sleeve = false;
                            StatusItem.bln_Check = false;
                        }
                        else
                        {
                            StatusItem.bln_Sleeve = CheckFilterValue_MC(GetAttListByName(GetAttributeListName(Item)), GetAttListFilterValue(AttributeType.SubCatetory, garmentItem), garmentItem.getProperty(GetAttributeColumnName(Item), ""));
                            if (!StatusItem.bln_Sleeve)
                            {
                                StatusItem.bln_Check = false;
                            }
                        }
                        break;
                    case AttributeType.Styling:
                        if (!StatusItem.bln_SubCategory)
                        {
                            StatusItem.bln_Styling = false;
                            StatusItem.bln_Check = false;
                        }
                        else
                        {
                            StatusItem.bln_Styling = CheckFilterValue_MC(GetAttListByName(GetAttributeListName(Item)), GetAttListFilterValue(AttributeType.SubCatetory, garmentItem), garmentItem.getProperty(GetAttributeColumnName(Item), ""));
                            if (!StatusItem.bln_Styling)
                            {
                                StatusItem.bln_Check = false;
                            }
                        }
                        break;
                    case AttributeType.Washing:
                        StatusItem.bln_Washing = CheckValue_MC(GetAttListByName(GetAttributeListName(Item)), garmentItem.getProperty(GetAttributeColumnName(Item), ""));//***
                        if (!StatusItem.bln_Washing)
                        {
                            StatusItem.bln_Check = false;
                        }
                        break;
                    default:
                        break;
                }
            }

            #endregion

            return StatusItem;
        }

        private Item FixGarmentItem(Item garmentItem)
        {
            CheckItemStatus StatusItem = new CheckItemStatus();
            Item l_returnItem = garmentItem;

            #region foreach

            foreach (AttributeType Item in (AttributeType[])System.Enum.GetValues(typeof(AttributeType)))
            {
                switch (Item)
                {
                    case AttributeType.ProductType:
                        StatusItem.bln_ProductType = CheckValue_MC(GetAttListByName(GetAttributeListName(Item)), garmentItem.getProperty(GetAttributeColumnName(Item), ""));//***
                        if (!StatusItem.bln_ProductType)
                        {
                            StatusItem.bln_Check = false;
                        }
                        else
                        {
                            l_returnItem.setProperty(GetAttributeColumnName(Item), GetValue_ByLabel(GetAttListByName(GetAttributeListName(Item)), garmentItem.getProperty(GetAttributeColumnName(Item), "")));
                        }
                        break;
                    case AttributeType.ProductCatetory:
                        if (!StatusItem.bln_ProductType)
                        {
                            StatusItem.bln_ProductCategory = false;
                            StatusItem.bln_Check = false;
                        }
                        else
                        {
                            StatusItem.bln_ProductCategory = CheckFilterValue_MC(GetAttListByName(GetAttributeListName(Item)), GetAttListFilterValue(AttributeType.ProductType, garmentItem), garmentItem.getProperty(GetAttributeColumnName(Item), ""));
                            if(StatusItem.bln_ProductCategory)
                            {
                                l_returnItem.setProperty(GetAttributeColumnName(Item), GetFilterValue_ByLabel(GetAttListByName(GetAttributeListName(Item)), GetAttListFilterValue(AttributeType.ProductType, garmentItem), garmentItem.getProperty(GetAttributeColumnName(Item), "")));
                            }
                        }
                        break;
                    case AttributeType.SubCatetory:
                        if (!StatusItem.bln_ProductCategory)
                        {
                            StatusItem.bln_SubCategory = false;
                            StatusItem.bln_Check = false;
                        }
                        else
                        {
                            StatusItem.bln_SubCategory = CheckFilterValue_MC(GetAttListByName(GetAttributeListName(Item)), GetAttListFilterValue(AttributeType.ProductCatetory, garmentItem), garmentItem.getProperty(GetAttributeColumnName(Item), ""));
                            if (StatusItem.bln_SubCategory)
                            {
                                l_returnItem.setProperty(GetAttributeColumnName(Item), GetFilterValue_ByLabel(GetAttListByName(GetAttributeListName(Item)), GetAttListFilterValue(AttributeType.ProductCatetory, garmentItem), garmentItem.getProperty(GetAttributeColumnName(Item), "")));
                            }
                        }
                        break;
                    case AttributeType.GarmentType:
                        StatusItem.bln_GarmentType = CheckValue_MC(GetAttListByName(GetAttributeListName(Item)), garmentItem.getProperty(GetAttributeColumnName(Item), ""));//***
                        if (!StatusItem.bln_GarmentType)
                        {
                            StatusItem.bln_Check = false;
                        }
                        else
                        {
                            l_returnItem.setProperty(GetAttributeColumnName(Item), GetValue_ByLabel(GetAttListByName(GetAttributeListName(Item)), garmentItem.getProperty(GetAttributeColumnName(Item), "")));
                        }
                        break;
                    case AttributeType.Collection:
                        if (!StatusItem.bln_SubCategory)
                        {
                            StatusItem.bln_Collection = false;
                            StatusItem.bln_Check = false;
                        }
                        else
                        {
                            StatusItem.bln_Collection = CheckFilterValue_MC(GetAttListByName(GetAttributeListName(Item)), GetAttListFilterValue(AttributeType.SubCatetory, garmentItem), garmentItem.getProperty(GetAttributeColumnName(Item), ""));
                            if (!StatusItem.bln_Collection)
                            {
                                StatusItem.bln_Check = false;
                            }
                            else if(StatusItem.bln_Collection)
                            {
                                l_returnItem.setProperty(GetAttributeColumnName(Item), GetFilterValue_ByLabel(GetAttListByName(GetAttributeListName(Item)), GetAttListFilterValue(AttributeType.SubCatetory, garmentItem), garmentItem.getProperty(GetAttributeColumnName(Item), "")));
                            }
                        }
                        break;
                    case AttributeType.Series:
                        if (!StatusItem.bln_SubCategory)
                        {
                            StatusItem.bln_Series = false;
                            StatusItem.bln_Check = false;
                        }
                        else
                        {
                            StatusItem.bln_Series = CheckFilterValue_MC(GetAttListByName(GetAttributeListName(Item)), GetAttListFilterValue(AttributeType.SubCatetory, garmentItem), garmentItem.getProperty(GetAttributeColumnName(Item), ""));
                            if (!StatusItem.bln_Series)
                            {
                                StatusItem.bln_Check = false;
                            }
                            else if (StatusItem.bln_Series)
                            {
                                l_returnItem.setProperty(GetAttributeColumnName(Item), GetFilterValue_ByLabel(GetAttListByName(GetAttributeListName(Item)), GetAttListFilterValue(AttributeType.SubCatetory, garmentItem), garmentItem.getProperty(GetAttributeColumnName(Item), "")));
                            }
                        }
                        break;
                    case AttributeType.Gender:
                        StatusItem.bln_Gender = CheckValue_MC(GetAttListByName(GetAttributeListName(Item)), garmentItem.getProperty(GetAttributeColumnName(Item), ""));//***
                        if (!StatusItem.bln_Gender)
                        {
                            StatusItem.bln_Check = false;
                        }
                        else if (StatusItem.bln_Gender)
                        {
                            l_returnItem.setProperty(GetAttributeColumnName(Item), GetValue_ByLabel(GetAttListByName(GetAttributeListName(Item)), garmentItem.getProperty(GetAttributeColumnName(Item), "")));
                        }
                        break;
                    case AttributeType.Cuff:
                        if (!StatusItem.bln_SubCategory)
                        {
                            StatusItem.bln_Cuff = false;
                            StatusItem.bln_Check = false;
                        }
                        else
                        {
                            StatusItem.bln_Cuff = CheckFilterValue_MC(GetAttListByName(GetAttributeListName(Item)), GetAttListFilterValue(AttributeType.SubCatetory, garmentItem), garmentItem.getProperty(GetAttributeColumnName(Item), ""));
                            if (!StatusItem.bln_Cuff)
                            {
                                StatusItem.bln_Check = false;
                            }
                            else if (StatusItem.bln_Cuff)
                            {
                                l_returnItem.setProperty(GetAttributeColumnName(Item), GetFilterValue_ByLabel(GetAttListByName(GetAttributeListName(Item)), GetAttListFilterValue(AttributeType.SubCatetory, garmentItem), garmentItem.getProperty(GetAttributeColumnName(Item), "")));
                            }
                        }
                        break;
                    case AttributeType.Making:
                        if (!StatusItem.bln_SubCategory)
                        {
                            StatusItem.bln_Making = false;
                            StatusItem.bln_Check = false;
                        }
                        else
                        {
                            StatusItem.bln_Making = CheckFilterValue_MC(GetAttListByName(GetAttributeListName(Item)), GetAttListFilterValue(AttributeType.SubCatetory, garmentItem), garmentItem.getProperty(GetAttributeColumnName(Item), ""));
                            if (!StatusItem.bln_Making)
                            {
                                StatusItem.bln_Check = false;
                            }
                            else if (StatusItem.bln_Making)
                            {
                                l_returnItem.setProperty(GetAttributeColumnName(Item), GetFilterValue_ByLabel(GetAttListByName(GetAttributeListName(Item)), GetAttListFilterValue(AttributeType.SubCatetory, garmentItem), garmentItem.getProperty(GetAttributeColumnName(Item), "")));
                            }
                        }
                        break;
                    case AttributeType.Fit:
                        if (!StatusItem.bln_SubCategory)
                        {
                            StatusItem.bln_Fit = false;
                            StatusItem.bln_Check = false;
                        }
                        else
                        {
                            StatusItem.bln_Fit = CheckFilterValue_MC(GetAttListByName(GetAttributeListName(Item)), GetAttListFilterValue(AttributeType.SubCatetory, garmentItem), garmentItem.getProperty(GetAttributeColumnName(Item), ""));
                            if (!StatusItem.bln_Fit)
                            {
                                StatusItem.bln_Check = false;
                            }
                            else if (StatusItem.bln_Fit)
                            {
                                l_returnItem.setProperty(GetAttributeColumnName(Item), GetFilterValue_ByLabel(GetAttListByName(GetAttributeListName(Item)), GetAttListFilterValue(AttributeType.SubCatetory, garmentItem), garmentItem.getProperty(GetAttributeColumnName(Item), "")));
                            }
                        }
                        break;
                    case AttributeType.Pocket:
                        if (!StatusItem.bln_SubCategory)
                        {
                            StatusItem.bln_Pocket = false;
                            StatusItem.bln_Check = false;
                        }
                        else
                        {
                            StatusItem.bln_Pocket = CheckFilterValue_MC(GetAttListByName(GetAttributeListName(Item)), GetAttListFilterValue(AttributeType.SubCatetory, garmentItem), garmentItem.getProperty(GetAttributeColumnName(Item), ""));
                            if (!StatusItem.bln_Pocket)
                            {
                                StatusItem.bln_Check = false;
                            }
                            else if (StatusItem.bln_Pocket)
                            {
                                l_returnItem.setProperty(GetAttributeColumnName(Item), GetFilterValue_ByLabel(GetAttListByName(GetAttributeListName(Item)), GetAttListFilterValue(AttributeType.SubCatetory, garmentItem), garmentItem.getProperty(GetAttributeColumnName(Item), "")));
                            }
                        }
                        break;
                    case AttributeType.Collar:
                        if (!StatusItem.bln_SubCategory)
                        {
                            StatusItem.bln_Collar = false;
                            StatusItem.bln_Check = false;
                        }
                        else
                        {
                            StatusItem.bln_Collar = CheckFilterValue_MC(GetAttListByName(GetAttributeListName(Item)), GetAttListFilterValue(AttributeType.SubCatetory, garmentItem), garmentItem.getProperty(GetAttributeColumnName(Item), ""));
                            if (!StatusItem.bln_Collar)
                            {
                                StatusItem.bln_Check = false;
                            }
                            else if (StatusItem.bln_Collar)
                            {
                                l_returnItem.setProperty(GetAttributeColumnName(Item), GetFilterValue_ByLabel(GetAttListByName(GetAttributeListName(Item)), GetAttListFilterValue(AttributeType.SubCatetory, garmentItem), garmentItem.getProperty(GetAttributeColumnName(Item), "")));
                            }
                        }
                        break;
                    case AttributeType.Placket:
                        if (!StatusItem.bln_SubCategory)
                        {
                            StatusItem.bln_Placket = false;
                            StatusItem.bln_Check = false;
                        }
                        else
                        {
                            StatusItem.bln_Placket = CheckFilterValue_MC(GetAttListByName(GetAttributeListName(Item)), GetAttListFilterValue(AttributeType.SubCatetory, garmentItem), garmentItem.getProperty(GetAttributeColumnName(Item), ""));
                            if (!StatusItem.bln_Placket)
                            {
                                StatusItem.bln_Check = false;
                            }
                            else if (StatusItem.bln_Placket)
                            {
                                l_returnItem.setProperty(GetAttributeColumnName(Item), GetFilterValue_ByLabel(GetAttListByName(GetAttributeListName(Item)), GetAttListFilterValue(AttributeType.SubCatetory, garmentItem), garmentItem.getProperty(GetAttributeColumnName(Item), "")));
                            }
                        }
                        break;
                    case AttributeType.Sleeve:
                        if (!StatusItem.bln_SubCategory)
                        {
                            StatusItem.bln_Sleeve = false;
                            StatusItem.bln_Check = false;
                        }
                        else
                        {
                            StatusItem.bln_Sleeve = CheckFilterValue_MC(GetAttListByName(GetAttributeListName(Item)), GetAttListFilterValue(AttributeType.SubCatetory, garmentItem), garmentItem.getProperty(GetAttributeColumnName(Item), ""));
                            if (!StatusItem.bln_Sleeve)
                            {
                                StatusItem.bln_Check = false;
                            }
                            else if (StatusItem.bln_Sleeve)
                            {
                                l_returnItem.setProperty(GetAttributeColumnName(Item), GetFilterValue_ByLabel(GetAttListByName(GetAttributeListName(Item)), GetAttListFilterValue(AttributeType.SubCatetory, garmentItem), garmentItem.getProperty(GetAttributeColumnName(Item), "")));
                            }
                        }
                        break;
                    case AttributeType.Styling:
                        if (!StatusItem.bln_SubCategory)
                        {
                            StatusItem.bln_Styling = false;
                            StatusItem.bln_Check = false;
                        }
                        else
                        {
                            StatusItem.bln_Styling = CheckFilterValue_MC(GetAttListByName(GetAttributeListName(Item)), GetAttListFilterValue(AttributeType.SubCatetory, garmentItem), garmentItem.getProperty(GetAttributeColumnName(Item), ""));
                            if (!StatusItem.bln_Styling)
                            {
                                StatusItem.bln_Check = false;
                            }
                            else if (StatusItem.bln_Styling)
                            {
                                l_returnItem.setProperty(GetAttributeColumnName(Item), GetFilterValue_ByLabel(GetAttListByName(GetAttributeListName(Item)), GetAttListFilterValue(AttributeType.SubCatetory, garmentItem), garmentItem.getProperty(GetAttributeColumnName(Item), "")));
                            }
                        }
                        break;
                    case AttributeType.Washing:
                        StatusItem.bln_Washing = CheckValue_MC(GetAttListByName(GetAttributeListName(Item)), garmentItem.getProperty(GetAttributeColumnName(Item), ""));//***
                        if (!StatusItem.bln_Washing)
                        {
                            StatusItem.bln_Check = false;
                        }
                        else if (StatusItem.bln_Washing)
                        {
                            l_returnItem.setProperty(GetAttributeColumnName(Item), GetValue_ByLabel(GetAttListByName(GetAttributeListName(Item)), garmentItem.getProperty(GetAttributeColumnName(Item), "")));
                        }
                        break;
                    default:
                        break;
                }
            }

            #endregion

            return l_returnItem;
        }

        private TreeNode GetNodeByAttribute(CheckItemStatus pi_statusItem)
        {
            TreeNode l_attNode = new TreeNode();
            l_attNode.Text = "Attribute";

            foreach (AttributeType Item in (AttributeType[])System.Enum.GetValues(typeof(AttributeType)))
            {
                TreeNode l_node = new TreeNode();
                l_node.Text = Item.ToString() + "---------" + GetAttributeColumnName(Item);

                switch (Item)
                {
                    case AttributeType.ProductType:
                        l_node.ImageIndex = pi_statusItem.bln_ProductType ? 0 : 3;
                        l_node.SelectedImageIndex = pi_statusItem.bln_ProductType ? 0 : 3;
                        break;
                    case AttributeType.ProductCatetory:
                        l_node.ImageIndex = pi_statusItem.bln_ProductCategory ? 0 : 3;
                        l_node.SelectedImageIndex = pi_statusItem.bln_ProductCategory ? 0 : 3;
                        break;
                    case AttributeType.SubCatetory:
                        l_node.ImageIndex = pi_statusItem.bln_SubCategory ? 0 : 3;
                        l_node.SelectedImageIndex = pi_statusItem.bln_SubCategory ? 0 : 3;
                        break;
                    case AttributeType.GarmentType:
                        l_node.ImageIndex = pi_statusItem.bln_GarmentType ? 0 : 3;
                        l_node.SelectedImageIndex = pi_statusItem.bln_GarmentType ? 0 : 3;
                        break;
                    case AttributeType.Collection:
                        l_node.ImageIndex = pi_statusItem.bln_Collection ? 0 : 3;
                        l_node.SelectedImageIndex = pi_statusItem.bln_Collection ? 0 : 3;
                        break;
                    case AttributeType.Series:
                        l_node.ImageIndex = pi_statusItem.bln_Series ? 0 : 3;
                        l_node.SelectedImageIndex = pi_statusItem.bln_Series ? 0 : 3;
                        break;
                    case AttributeType.Gender:
                        l_node.ImageIndex = pi_statusItem.bln_Gender ? 0 : 3;
                        l_node.SelectedImageIndex = pi_statusItem.bln_Gender ? 0 : 3;
                        break;
                    case AttributeType.Cuff:
                        l_node.ImageIndex = pi_statusItem.bln_Cuff ? 0 : 3;
                        l_node.SelectedImageIndex = pi_statusItem.bln_Cuff ? 0 : 3;
                        break;
                    case AttributeType.Making:
                        l_node.ImageIndex = pi_statusItem.bln_Making ? 0 : 3;
                        l_node.SelectedImageIndex = pi_statusItem.bln_Making ? 0 : 3;
                        break;
                    case AttributeType.Fit:
                        l_node.ImageIndex = pi_statusItem.bln_Fit ? 0 : 3;
                        l_node.SelectedImageIndex = pi_statusItem.bln_Fit ? 0 : 3;
                        break;
                    case AttributeType.Pocket:
                        l_node.ImageIndex = pi_statusItem.bln_Pocket ? 0 : 3;
                        l_node.SelectedImageIndex = pi_statusItem.bln_Pocket ? 0 : 3;
                        break;
                    case AttributeType.Collar:
                        l_node.ImageIndex = pi_statusItem.bln_Collar ? 0 : 3;
                        l_node.SelectedImageIndex = pi_statusItem.bln_Collar ? 0 : 3;
                        break;
                    case AttributeType.Placket:
                        l_node.ImageIndex = pi_statusItem.bln_Placket ? 0 : 3;
                        l_node.SelectedImageIndex = pi_statusItem.bln_Placket ? 0 : 3;
                        break;
                    case AttributeType.Sleeve:
                        l_node.ImageIndex = pi_statusItem.bln_Sleeve ? 0 : 3;
                        l_node.SelectedImageIndex = pi_statusItem.bln_Sleeve ? 0 : 3;
                        break;
                    case AttributeType.Styling:
                        l_node.ImageIndex = pi_statusItem.bln_Styling ? 0 : 3;
                        l_node.SelectedImageIndex = pi_statusItem.bln_Styling ? 0 : 3;
                        break;
                    case AttributeType.Washing:
                        l_node.ImageIndex = pi_statusItem.bln_Washing ? 0 : 3;
                        l_node.SelectedImageIndex = pi_statusItem.bln_Washing ? 0 : 3;
                        break;
                    default:
                        break;
                }

                l_attNode.Nodes.Add(l_node);
            }

            return l_attNode;
        }
        
        //--------------------------------------------------------------------------------------------

        private async void btn_ConnectionAras_Click(object sender, EventArgs e)
        {
            try
            {
                #region check login parameter
                if (string.IsNullOrEmpty(txt_serverurl.Text.Trim()))
                {
                    throw new Exception("Server Url Is Null Or Empty !");
                }

                if (string.IsNullOrEmpty(txt_DB.Text.Trim()))
                {
                    throw new Exception("DB Name Is Null Or Empty !");
                }

                if (string.IsNullOrEmpty(txt_username.Text.Trim()))
                {
                    throw new Exception("User Name Is Null Or Empty !");
                }

                if (string.IsNullOrEmpty(txt_password.Text.Trim()))
                {
                    throw new Exception("Password Is Null Or Empty !");
                }
                #endregion

                #region connection parameter
                ArasConnectionPara l_connectionPara = new ArasConnectionPara()
                {
                    l_serverurl = txt_serverurl.Text.Trim(),
                    l_db = txt_DB.Text.Trim(),
                    l_username = txt_username.Text.Trim(),
                    l_password = txt_password.Text.Trim()
                };
                #endregion

                mc_sourceArasInnovatorConnection = new CancellationTokenSource();
                btn_disconnection.Enabled = true;

                //get connection
                mc_conn= GetConnection(l_connectionPara);

                //innovator login
                Item login_result = mc_conn.Login();                                              
                if (login_result.isError()) throw new Exception("Login failed, please check connection infomation.");                

                //get innovator
                mc_innovator= GetInnovator(mc_conn);                

                //get list attributes
                await Task.Run(() => GetAttributeList());

                await Task.Run(() => SettingConnectionButton(true));

                var msg = mc_sourceArasInnovatorConnection.Token.IsCancellationRequested ? "Aras Cancel Connection" : "Aras Connection Completed";
                MessageBox.Show(msg, "Connection Status");
            }
            catch (Exception ex)
            {                
                ShowError(ex);
            }
        }

        private async void btn_disconnection_Click(object sender, EventArgs e)
        {
            mc_innovator = null;

            if (mc_conn != null)
            {
                mc_conn.Logout();
                mc_conn = null;
            }

            if (mc_sourceArasInnovatorConnection != null)
            {
                await Task.Run(() => SettingConnectionButton(false));
                if (!mc_sourceArasInnovatorConnection.Token.IsCancellationRequested)
                {
                    mc_sourceArasInnovatorConnection.Cancel();
                }
            }
        }

        //--------------------------------------------------------------------------------------------

        private async void btn_CheckItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txt_SearchItem.Text.Trim()))
                {
                    throw new Exception("Please Enter need Check Data.");
                }

                if (string.IsNullOrEmpty(txt_SearchAML.Text.Trim()))
                {
                    throw new Exception("Please Enter Search AML .");
                }

                SettingProcessButton(false);
                mc_sourceCheckGarmentStyle = new CancellationTokenSource();

                StringBuilder l_getItemAML = new StringBuilder();
                string[] l_getDataRow = txt_SearchItem.Text.Trim().Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

                if (!mc_sourceCheckGarmentStyle.Token.IsCancellationRequested)
                {
                    TreeNode l_root = new TreeNode();
                    l_root.Text = "Garment";
                    l_root.ImageIndex = 1;
                    l_root.SelectedImageIndex = 1;
                    tre_Item.Nodes.Clear();
                    tre_Item.Nodes.Add(l_root);
                    tre_Item.Refresh();

                    for (int rowIDX = 0; rowIDX < l_getDataRow.Length; rowIDX++)
                    {
                        if (mc_sourceCheckGarmentStyle.Token.IsCancellationRequested)
                        {
                            break;
                        }

                        int l_columnIdx = 0;
                        SettingProcess(l_getDataRow.Length, rowIDX + 1);

                        #region get parameter

                        string[] l_getDataColumn = l_getDataRow[rowIDX].Split(new char[] { '\t' });
                        if (l_getDataColumn != null && l_getDataColumn.Length != 0)
                        {
                            l_columnIdx = l_getDataColumn.Length;
                        }

                        string l_AML = "";
                        string l_tempAML = txt_SearchAML.Text.Trim();
                        for (int columnIDX = 0; columnIDX < l_columnIdx; columnIDX++)
                        {
                            l_tempAML = l_tempAML.Replace("{" + columnIDX + "}", l_getDataColumn[columnIDX]);
                        }

                        #endregion

                        Item getItem = GetItemByAML(l_tempAML);

                        if (getItem.isError() || getItem.isEmpty())
                        {
                            break;
                        }

                        CheckItemStatus l_status = await Task.Run(() => CheckGarmentItem(getItem));

                        Task.Delay(5000, mc_sourceCheckGarmentStyle.Token);

                        TreeNode garmentStyleNode = new TreeNode(getItem.getProperty("item_number", "Unknow GarmentStyle"));
                        garmentStyleNode.Name = getItem.getProperty("item_number", "");
                        garmentStyleNode.ImageIndex = l_status.bln_Check ? 0 : 3;
                        garmentStyleNode.SelectedImageIndex = l_status.bln_Check ? 0 : 3;

                        garmentStyleNode.Nodes.Add(await Task.Run(() => GetNodeByAttribute(l_status)));

                        tre_Item.Nodes[0].Nodes.Add(garmentStyleNode);
                        tre_Item.Nodes[0].Expand();
                        tre_Item.Refresh();
                    }

                }

                SettingProcessButton(true);
                var msg = mc_sourceCheckGarmentStyle.Token.IsCancellationRequested ? "Check Item Cancel" : "Check Item Completed";
                MessageBox.Show(msg, "Check Item Status");

            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private async void btn_FixGarmentStyle_Click(object sender, EventArgs e)
        {
            try
            {               
                if (string.IsNullOrEmpty(txt_SearchItem.Text.Trim()))
                {
                    throw new Exception("Please Enter need Check Data.");
                }

                if (string.IsNullOrEmpty(txt_SearchAML.Text.Trim()))
                {
                    throw new Exception("Please Enter Search AML .");
                }

                SettingProcessButton(false);
                mc_sourceCheckGarmentStyle = new CancellationTokenSource();

                StringBuilder l_getItemAML = new StringBuilder();
                string[] l_getDataRow = txt_SearchItem.Text.Trim().Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

                for (int rowIDX = 0; rowIDX < l_getDataRow.Length; rowIDX++)
                {
                    //if cancel process
                    if (mc_sourceCheckGarmentStyle.Token.IsCancellationRequested)
                    {
                        break;
                    }

                    int l_columnIdx = 0;

                    SettingProcess(l_getDataRow.Length, rowIDX + 1);

                    #region get parameter

                    string[] l_getDataColumn = l_getDataRow[rowIDX].Split(new char[] { '\t' });
                    if (l_getDataColumn != null && l_getDataColumn.Length != 0)
                    {
                        l_columnIdx = l_getDataColumn.Length;
                    }

                    string l_AML = "";
                    string l_tempAML = txt_SearchAML.Text.Trim();
                    for (int columnIDX = 0; columnIDX < l_columnIdx; columnIDX++)
                    {
                        l_tempAML = l_tempAML.Replace("{" + columnIDX + "}", l_getDataColumn[columnIDX]);
                    }

                    #endregion

                    Item getItem = await Task.Run(() => GetItemByAML(l_tempAML));

                    if (getItem.isError() || getItem.isEmpty())
                    {
                        break;
                    }

                    TreeNode[] l_findNode = tre_Item.Nodes[0].Nodes.Find(getItem.getProperty("item_number", ""), false);
                    if (l_findNode != null && l_findNode.Length > 0)
                    {
                        l_findNode[0].ImageIndex = 2;
                        l_findNode[0].SelectedImageIndex = 2;
                        tre_Item.Refresh();
                    }

                    Item getFixItem = await Task.Run(() => FixGarmentItem(getItem));
                    getFixItem.setAction("edit");
                    Item getReturnItem = getFixItem.apply();

                    if (l_findNode != null && l_findNode.Length > 0)
                    {
                        l_findNode[0].ImageIndex = 4;
                        l_findNode[0].SelectedImageIndex = 4;
                        tre_Item.Refresh();
                    }
                }

                SettingProcessButton(true);
                var msg = mc_sourceCheckGarmentStyle.Token.IsCancellationRequested ? "Fix Item Cancel" : "Fix Item Completed";
                MessageBox.Show(msg, "Fix Item Status");
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private async void btn_Cancel_Click(object sender, EventArgs e)
        {
            if (mc_sourceCheckGarmentStyle != null)
            {
                SettingProcessButton(true);
                if (mc_sourceCheckGarmentStyle.Token.IsCancellationRequested)
                {
                    mc_sourceCheckGarmentStyle.Cancel();
                }
            }
        }
        //--------------------------------------------------------------------------------------------        
    }
}
