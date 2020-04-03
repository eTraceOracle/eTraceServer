using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.SqlServerDAL.V2.Report.WM
{
    class Test
    {
    }
}

//private void Search3(bool StandAlone)
//{
//    try
//    {
//        ShowSearchJS = "document.getElementById('SearchScreen').style.visibility = 'hidden'; ";
//        ToolBar.Visible = false;
//    }
//    catch (Exception ex)
//    {
//        if (StandAlone)
//        {
//            ShowSearchJS = "document.getElementById('SearchScreen').style.visibility = 'hidden'; ";
//            ToolBar.Visible = true;
//        }
//        else
//        {
//            ShowSearchJS = "document.getElementById('SearchScreen').style.visibility = 'visible'; ";
//            ToolBar.Visible = true;
//            return;
//        }
//    }


//    if (string.IsNullOrEmpty(MaterialNo.Text.Trim) || string.IsNullOrEmpty(Subassembly.Text.Trim))
//    {
//        Response.Write("Item NO and Subassembly can not be empty !");
//        return;
//    }
//    else
//        // If Subassembly.Text.Trim.Contains(",") Then
//        // labmsg.Text = "Subassembly can not contain comma!"
//        // Exit Sub
//        // End If

//        if (MaterialNo.Text.Trim.Contains(","))
//    {
//        Response.Write("Item NO can not contain comma!");
//        return;
//    }



//    SqlConnection thisConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("eTraceConnection").ConnectionString);
//    string sessionID = Guid.NewGuid().ToString();

//    string sql1 = "";
//    string sql2 = "";
//    string sql3 = "";
//    string sql = "";
//    thisConnection.Open();

//    SqlDataAdapter thisAdapter = new SqlDataAdapter(sql, thisConnection);

//    SqlCommandBuilder cb = new SqlCommandBuilder(thisAdapter);

//    DataSet ds = new DataSet();


//    if (!string.IsNullOrEmpty(MaterialNo.Text.Trim))
//        sql2 = " vc.materialno in ('" + MaterialNo.Text.Trim.Replace(",", "','") + "') ";

//    if (!string.IsNullOrEmpty(Subassembly.Text.Trim))
//        sql1 = " c.materialno in ('" + Subassembly.Text.Trim.Replace(",", "','") + "') ";



//    if (sql1 == string.Empty)
//        sql1 = " 1 = 1 ";



//    string sqlx = @" select distinct tmp.* from ( "
//+ " ( "
//+ " select t1.po as TLA_DJ, t1.materialno as [Assembly], vp.PO as SMT_DJ, t1.clid as suba_clid,t1.suba_clidqty,t1.suba_issueDate,vc.clid,vc.materialno,vc.datecode, vc.lotno, vc.manufacturer,vc.manufacturerpn, vp.clidqty, vp.issueDate, vc.RecDate, vc.purOrdNo "
//+ " from "
//+ " v_po_clid as vp with(nolock) "
//+ " inner join v_clmaster as vc with(nolock) on vc.clid=vp.clid "
//+ " inner join  "
//+ " (		 "
//+ " select distinct c.clid, c.purOrdNo, p.po, c.materialno, p.clidqty as suba_clidqty, p.issueDate as suba_issueDate  "
//+ " from v_po_clid as p with(nolock)  "
//+ " inner join v_clmaster as c with(nolock) on p.clid=c.clid "
//+ " where " + sql1 + " "
//+ " union  "
//+ " select distinct c.clid, c.purOrdNo, o.po, c.materialno, p.clidqty as suba_clidqty, p.issueDate as suba_issueDate  "
//+ " from v_po_clid as p with(nolock) 	 "
//+ " inner join v_pdto_po as o with(nolock) on p.pdto = o.pdto		 "
//+ " inner join v_clmaster as c with(nolock) on p.clid=c.clid	 "
//+ " where " + sql1 + " "
//+ " and p.pdto is not null	 "
//+ " ) as t1					 "
//+ " on t1.purordno = vp.po		 "
//+ " where " + sql2 + " "
//+ " and t1.purordno <> ''	 "
//+ " ) "
//+ " union all "
//+ " ( "
//+ " select t1.po as TLA_DJ, t1.materialno as [Assembly], pd.PO as SMT_DJ, t1.clid as suba_clid,t1.suba_clidqty,t1.suba_issueDate,vc.clid,vc.materialno,vc.datecode, vc.lotno, vc.manufacturer,vc.manufacturerpn, vp.clidqty, vp.issueDate, vc.RecDate, vc.purOrdNo "
//+ " from v_pdto_po as pd with(nolock) "
//+ " inner join v_po_clid as vp with(nolock) on pd.pdto=vp.pdto	 "
//+ " inner join v_clmaster as vc with(nolock) on vc.clid=vp.clid	 "
//+ " inner join  "
//+ " (		 "
//+ " select distinct c.clid, c.purOrdNo, p.po, c.materialno, p.clidqty as suba_clidqty, p.issueDate as suba_issueDate "
//+ " from v_po_clid as p with(nolock) 	 "
//+ " inner join v_clmaster as c with(nolock) on p.clid=c.clid	 "
//+ " where " + sql1 + " "
//+ " union "
//+ " select distinct c.clid, c.purOrdNo, o.po, c.materialno, p.clidqty as suba_clidqty, p.issueDate as suba_issueDate "
//+ " from v_po_clid as p with(nolock) 	 "
//+ " inner join v_pdto_po as o with(nolock) on p.pdto = o.pdto	 "
//+ " inner join v_clmaster as c with(nolock) on p.clid=c.clid "
//+ " where " + sql1 + " "
//+ " and p.pdto is not null		 "
//+ " ) as t1	 "
//+ " on t1.purordno = pd.po		 "
//+ " where " + sql2 + " "
//+ " and t1.purordno <> ''		 "
//+ " and pd.pdto is not null	 "
//+ " ) "
//   + " ) as tmp "
//   + " where(tmp.clidqty > 0) "
//+ " order by tmp.Assembly,tmp.TLA_DJ, tmp.SMT_DJ ";

//    // common.WriteReportLogs(sessionID, "Components Userd In PO ", "IP: " + Me.Request.UserHostAddress + "  ComputerName: " + System.Net.Dns.Resolve(Request.UserHostAddress).HostName, sql.Replace("'", ""), "")
//    // common.WriteReportLogs(sessionID, "Components Userd In PO ", "IP: " + Me.Request.UserHostAddress + "  ComputerName: " + Resolve(Request.UserHostAddress).HostName, sql.Replace("'", ""), "")

//    thisAdapter = new SqlDataAdapter(sqlx, thisConnection);

//    try
//    {
//        thisAdapter.SelectCommand.CommandTimeout = 1200;
//        thisAdapter.Fill(ds, "Component");
//    }
//    catch (Exception ex)
//    {
//        Response.Write(ex.Message);
//        return;
//    }

//    thisConnection.Close();

//    common.WriteReportLogs(sessionID, "", "", "", "");

//    // ds.Tables(0).ExtendedProperties.Add("PO_Tab", DateTime.Now.ToLongTimeString)
//    // Cache.Insert("PO_Cache", ds.Tables(0), Nothing, DateTime.Now.AddMinutes(5), TimeSpan.Zero)

//    if (ds.Tables(0).Rows.Count == 0)
//    {
//        Response.Write("No any record!");
//        return;
//    }

//    GenerateExcel(ds.Tables(0));
//}




