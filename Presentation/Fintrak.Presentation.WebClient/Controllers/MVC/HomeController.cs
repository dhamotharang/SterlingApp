using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web.Mvc;
using Fintrak.Client.SystemCore.Contracts;
using Fintrak.Presentation.WebClient.Models;
using System.Data;
using System.Reflection;
//using Fintrak.Client.MPR.Entities;
//using System.IO;
//using NPOI.HSSF.UserModel;
//using NPOI.SS.UserModel;

namespace Fintrak.Presentation.WebClient
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Authorize]
    [RoutePrefix("home")]
    public class HomeController : Controller
    {
        [ImportingConstructor]
        public HomeController(ICoreService coreService)
        {
            _CoreService = coreService;
        }

        ICoreService _CoreService;
        List<MenuData> _AllMenu;

        public ActionResult LandingPage()
        {
            ViewBag.Title = "Landing Page";

            return View();
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            var preparedMenus = new List<MenuModel>();

            try
            {
                _AllMenu = _CoreService.GetMenuByLoginID(User.Identity.Name).OrderBy(c => c.ModuleId).ToList();

                //_AllMenu = _CoreService.GetMenus().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            var parentMenus = _AllMenu.Where(c => c.ParentId == 0);

            foreach (var menu in parentMenus)
            {
                var rootMenu = new MenuModel()
                {
                    Name = menu.Name,
                    Alias = menu.Alias,
                    Action = menu.Action,
                    ActionUrl = menu.ActionUrl,
                    ModuleName = menu.ModuleName,
                    Image = menu.Image,
                    ImageUrl = menu.ImageUrl,
                    Position = menu.Position,
                    Parent = menu.ParentId,
                    MenuLevel = 1,
                    Children = new List<MenuModel>()
                };

                rootMenu.Children = PrepareMenu(menu, rootMenu.MenuLevel);

                preparedMenus.Add(rootMenu);
            }

            ViewBag.Menus = preparedMenus;
            return View();
        }

        public ActionResult ReportPage()
        {
            ViewBag.Title = "Report Page";

            return View();
        }

        private List<MenuModel> PrepareMenu(MenuData parentMenu, int menuLevel)
        {
            var children = _AllMenu.Where(c => c.ParentId == parentMenu.MenuId).ToList();
            var lchildren = new List<MenuModel>();

            foreach (var subMenu in children)
            {
                var childMenu = new MenuModel()
                {
                    Name = subMenu.Name,
                    Alias = subMenu.Alias,
                    Action = subMenu.Action,
                    ActionUrl = subMenu.ActionUrl,
                    ModuleName = subMenu.ModuleName,
                    Image = subMenu.Image,
                    ImageUrl = subMenu.ImageUrl,
                    Position = subMenu.Position,
                    Parent = parentMenu.ParentId,
                    MenuLevel = menuLevel + 1,
                    Children = new List<MenuModel>()
                };

                var returnMenu = PrepareMenu(subMenu, childMenu.MenuLevel);
                childMenu.Children = returnMenu;
                lchildren.Add(childMenu);
            }

            return lchildren;
        }

        //[HttpGet()]
        //public ActionResult LoadExcel()
        //{
        //    string contentType = "application/vnd.ms-excel";
        //    if (Session["result"] != null)
        //    {
        //        var lstData = (MPRBalanceSheet[])Session["result"];
        //        var dt = LINQToDataTable<MPRBalanceSheet>(lstData.AsEnumerable());
        //        var ms = GenerateExcelSheet("BalanceSheet", dt);
        //        this.Response.Clear();
        //        Response.ClearHeaders();
        //        Response.ClearContent();
        //        Response.AddHeader("content-disposition", "attachment; filename=" + "Download.xls");

        //        this.Response.ContentType = contentType;
        //        this.Response.BinaryWrite(ms.ToArray());
        //        this.Response.End();

        //    }
        //    return new FileStreamResult(Response.OutputStream, contentType);
        //}


        //[HttpGet()]
        //public ActionResult LoadExcelrevenue()
        //{
        //    string contentType = "application/vnd.ms-excel";
        //    if (Session["result"] != null)
        //    {
        //        var lstData = (Revenue[])Session["result"];
        //        var dt = LINQToDataTable<Revenue>(lstData.AsEnumerable());
        //        var ms = GenerateExcelSheet("Revenue", dt);
        //        this.Response.Clear();
        //        Response.ClearHeaders();
        //        Response.ClearContent();
        //        Response.AddHeader("content-disposition", "attachment; filename=" + "Download.xls");

        //        this.Response.ContentType = contentType;
        //        this.Response.BinaryWrite(ms.ToArray());
        //        this.Response.End();

        //    }
        //    return new FileStreamResult(Response.OutputStream, contentType);
        //}



        public DataTable LINQToDataTable<T>(IEnumerable<T> varlist)
        {
            DataTable dtReturn = new DataTable();

            // column names
            PropertyInfo[] oProps = null;

            if (varlist == null) return dtReturn;

            foreach (T rec in varlist)
            {
                // Use reflection to get property names, to create table, Only first time, others will follow
                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }
                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }

                DataRow dr = dtReturn.NewRow();

                foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
                    (rec, null);
                }


                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }



        //public MemoryStream GenerateExcelSheet(string reportCode, DataTable sourceTable)
        //{
        //    var ms = new MemoryStream();
        //    HSSFWorkbook workbook = new HSSFWorkbook();
        //    Sheet sheet = workbook.CreateSheet(reportCode);
        //    Row headerRow = sheet.CreateRow(0);
        //    int rowIndex = 1;

        //    foreach (DataRow row in sourceTable.Rows)
        //    {
        //        foreach (DataColumn col in sourceTable.Columns)
        //        {
        //            headerRow.CreateCell(col.Ordinal).SetCellValue(col.ColumnName);
        //        }
        //        break;
        //    }

        //    foreach (DataRow row in sourceTable.Rows)
        //    {s
        //        Row dataRow = sheet.CreateRow(rowIndex);
        //        foreach (DataColumn column in sourceTable.Columns)
        //        {
        //            dataRow.CreateCell(column.Ordinal).SetCellValue(row[column.ColumnName].ToString());
        //        }
        //        rowIndex += 1;
        //    }
        //    workbook.Write(ms);
        //    ms.Flush();
        //    ms.Position = 0;
        //    return ms;
        //}

    }
}
