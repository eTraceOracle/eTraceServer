Imports System
Imports System.Collections
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Web
Imports System.Web.SessionState
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.IO
Imports System.Net.Dns
Imports System.Linq
Imports System.Collections.Generic
Imports Microsoft.VisualBasic
Imports System.Runtime.CompilerServices

Public Class VBGetQualityData

    Public Shared Function GetDailyResultData(ByVal dtStatistic As DataTable, ByVal dtRepair As DataTable, ByVal dtProductMaster As DataTable) As DataTable

        Dim query1 =
                    From statistic In dtStatistic.AsEnumerable()
                    Group Join repair In dtRepair.AsEnumerable()
                    On statistic.Field(Of String)("Model").ToUpper() Equals repair.Field(Of String)("Model").ToUpper() _
                    And statistic.Field(Of String)("ProcessName").ToUpper() Equals repair.Field(Of String)("TestStation").ToUpper() _
                    And statistic.Field(Of String)("ProdDate").ToUpper() Equals repair.Field(Of String)("DayB").ToUpper() _
                    And statistic.Field(Of String)("ShopFloor").ToUpper() Equals repair.Field(Of String)("Floor").ToUpper() Into gg = Group
                    From repair In gg.DefaultIfEmpty
                    Group Join prodMaster In dtProductMaster.AsEnumerable()
                    On statistic.Field(Of String)("Model").ToUpper() Equals prodMaster.Field(Of String)("Model").ToUpper() Into g1 = Group
                    From prodMaster In g1.DefaultIfEmpty
                    Select New With
                    {
                     .Model = statistic.Field(Of String)("Model"),
                     .ProcessName = statistic.Field(Of String)("ProcessName"),
                     .SUMA = statistic.Field(Of Integer)("SUMA"),
                     .NUM = If(repair Is Nothing, 0, repair.Field(Of Integer)("NUM")),
                     .ProdDate = statistic.Field(Of String)("ProdDate"),
                     .ShopFloor = statistic.Field(Of String)("ShopFloor"),
                     .Description = If(prodMaster Is Nothing, "", prodMaster.Field(Of String)("Description")),
                     .BusinessUnit = If(prodMaster Is Nothing, "", prodMaster.Field(Of String)("BusinessUnit")),
                     .Power = If(prodMaster Is Nothing, "", prodMaster.Field(Of String)("Power")),
                     .VotageType = If(prodMaster Is Nothing, "", prodMaster.Field(Of String)("VotageType"))
                    }

        Dim temp1 As Array = query1.ToArray()

        Dim query2 =
                    From item1 In temp1
                    Group item1 By GG = New With {Key .model = item1.Model, Key .description = item1.Description, Key .processName = item1.ProcessName, Key .suma = item1.SUMA,
                    Key .num = item1.NUM, Key .prodDate = item1.ProdDate, Key .shopFloor = item1.ShopFloor, Key .businessUnit = item1.BusinessUnit,
                    Key .power = item1.Power, Key .votageType = item1.VotageType} Into g = Group
                    Select New With
                    {
                     .Model = GG.model, .Description = GG.description, .ProcessName = GG.processName, .SUMA = GG.suma, .NUM = GG.num,
                     .ProdDate = GG.prodDate, .ShopFloor = GG.shopFloor, .BusinessUnit = GG.businessUnit, .Power = GG.power, .VotageType = GG.votageType
                    }

        Dim query3 =
                    From item2 In query2
                    Select item2
                    Order By item2.Model Ascending, item2.ProdDate Ascending, item2.ProcessName Ascending


        Dim dt2 As DataTable = New DataTable("Daily")
        Dim dcModel As DataColumn = New DataColumn("Model", System.Type.GetType("System.String"))
        dt2.Columns.Add(dcModel)
        Dim dcStation As DataColumn = New DataColumn("Station", System.Type.GetType("System.String"))
        dt2.Columns.Add(dcStation)
        Dim dcTotal As DataColumn = New DataColumn("Total", System.Type.GetType("System.String"))
        dt2.Columns.Add(dcTotal)
        Dim dcSuccess As DataColumn = New DataColumn("Success", System.Type.GetType("System.String"))
        dt2.Columns.Add(dcSuccess)
        Dim dcFailed As DataColumn = New DataColumn("Failed", System.Type.GetType("System.String"))
        dt2.Columns.Add(dcFailed)
        Dim dcYield As DataColumn = New DataColumn("Yield", System.Type.GetType("System.String"))
        dt2.Columns.Add(dcYield)
        Dim dcPPM As DataColumn = New DataColumn("PPM", System.Type.GetType("System.String"))
        dt2.Columns.Add(dcPPM)
        Dim dcDate As DataColumn = New DataColumn("ProdDate", System.Type.GetType("System.String"))
        dt2.Columns.Add(dcDate)
        Dim dcshopfloor As DataColumn = New DataColumn("Floor", System.Type.GetType("System.String"))
        dt2.Columns.Add(dcshopfloor)
        Dim dcDescription As DataColumn = New DataColumn("Description", System.Type.GetType("System.String"))
        dt2.Columns.Add(dcDescription)
        Dim dcBusinessUnit As DataColumn = New DataColumn("BusinessUnit", System.Type.GetType("System.String"))
        dt2.Columns.Add(dcBusinessUnit)
        Dim dcPower As DataColumn = New DataColumn("Power", System.Type.GetType("System.String"))
        dt2.Columns.Add(dcPower)
        Dim dcVotageType As DataColumn = New DataColumn("VoltageType", System.Type.GetType("System.String"))
        dt2.Columns.Add(dcVotageType)
                    'Dim dcModel As DataColumn = New DataColumn("Model", System.Type.GetType("System.String"))
                    'dt2.Columns.Add(dcModel)
                    'Dim dcDescription As DataColumn = New DataColumn("Description", System.Type.GetType("System.String"))
                    'dt2.Columns.Add(dcDescription)
                    'Dim dcshopfloor As DataColumn = New DataColumn("Floor", System.Type.GetType("System.String"))
                    'dt2.Columns.Add(dcshopfloor)
                    'Dim dcBusinessUnit As DataColumn = New DataColumn("BusinessUnit", System.Type.GetType("System.String"))
                    'dt2.Columns.Add(dcBusinessUnit)
                    'Dim dcStation As DataColumn = New DataColumn("Station", System.Type.GetType("System.String"))
                    'dt2.Columns.Add(dcStation)
                    'Dim dcTotal As DataColumn = New DataColumn("Total", System.Type.GetType("System.String"))
                    'dt2.Columns.Add(dcTotal)
                    'Dim dcSuccess As DataColumn = New DataColumn("Success", System.Type.GetType("System.String"))
                    'dt2.Columns.Add(dcSuccess)
                    'Dim dcFailed As DataColumn = New DataColumn("Failed", System.Type.GetType("System.String"))
                    'dt2.Columns.Add(dcFailed)
                    'Dim dcYield As DataColumn = New DataColumn("Yield", System.Type.GetType("System.String"))
                    'dt2.Columns.Add(dcYield)
                    'Dim dcPPM As DataColumn = New DataColumn("PPM", System.Type.GetType("System.String"))
                    'dt2.Columns.Add(dcPPM)
                    'Dim dcVotageType As DataColumn = New DataColumn("VoltageType", System.Type.GetType("System.String"))
                    'dt2.Columns.Add(dcVotageType)
                    'Dim dcDate As DataColumn = New DataColumn("ProdDate", System.Type.GetType("System.String"))
                    'dt2.Columns.Add(dcDate)
                    'Dim dcPower As DataColumn = New DataColumn("Power", System.Type.GetType("System.String"))
                    'dt2.Columns.Add(dcPower)

        For Each dailyItem In query3
            Dim dr As DataRow = dt2.NewRow()
            dr("Model") = dailyItem.Model
            dr("Description") = dailyItem.Description
            dr("Floor") = dailyItem.ShopFloor
            dr("BusinessUnit") = dailyItem.BusinessUnit
            dr("Station") = dailyItem.ProcessName
            If dailyItem.NUM > dailyItem.SUMA Then
                dr("Success") = dailyItem.NUM - dailyItem.NUM
                dr("Total") = dailyItem.NUM
                dr("Failed") = dailyItem.NUM

                dr("PPM") = Format(Convert.ToDouble(1), "###0.0000") * 1000000
            Else
                dr("Success") = dailyItem.SUMA - dailyItem.NUM
                dr("Total") = dailyItem.SUMA
                dr("Failed") = dailyItem.NUM

                If dailyItem.NUM = 0 Then
                    dr("PPM") = Format(Convert.ToDouble(0), "###0.0000") * 1000000
                Else
                    dr("PPM") = Format(Convert.ToDouble(dailyItem.NUM / dailyItem.SUMA), "###0.0000") * 1000000
                            End If
                        End If


            dr("Yield") = Format(Convert.ToDouble((Convert.ToDouble(dr("Success"))) / Convert.ToDouble(dr("Total"))), "###0.0000") * 100
            dr("VoltageType") = dailyItem.VotageType
            dr("ProdDate") = dailyItem.ProdDate
            dr("Power") = dailyItem.Power
            dt2.Rows.Add(dr)
        Next

        Return dt2
    End Function

    Public Shared Function GetDailyStatisticData(ByVal dt As DataTable) As DataTable
        Dim findrows() As DataRow

        findrows = dt.AsEnumerable().ToArray()

        Dim query =
                From product In findrows
                Group product By PP = New With {Key .model = product.Field(Of String)("Model").ToUpper(), Key .processname = product.Field(Of String)("ProcessName").ToUpper(),
                Key .po = product.Field(Of String)("PO").ToUpper(), Key .shopfloor = product.Field(Of String)("ShopFloor").ToUpper()} Into g = Group
                Select New With {.Model = PP.model, .PO = PP.po, .processName = PP.processname, .ShopFloor = PP.shopfloor,
                                 .SUMA = g.Sum(Function(product) product.Field(Of Integer)("ANumber"))}

        Dim dt1 As DataTable = New DataTable("dtStatistic")
        Dim dcModel As DataColumn = New DataColumn("Model", System.Type.GetType("System.String"))
        dt1.Columns.Add(dcModel)
        Dim dcProcessName As DataColumn = New DataColumn("ProcessName", System.Type.GetType("System.String"))
        dt1.Columns.Add(dcProcessName)
        Dim dcProdOrder As DataColumn = New DataColumn("ProdOrder", System.Type.GetType("System.String"))
        dt1.Columns.Add(dcProdOrder)
        Dim dcShopFloor As DataColumn = New DataColumn("ShopFloor", System.Type.GetType("System.String"))
        dt1.Columns.Add(dcShopFloor)
        Dim dcNumber As DataColumn = New DataColumn("SUMA", System.Type.GetType("System.Int32"))
        dt1.Columns.Add(dcNumber)

        For Each item In query
            Dim dr As DataRow = dt1.NewRow()
            dr("Model") = item.Model
            dr("ProcessName") = item.processName
            dr("ProdOrder") = item.PO
            dr("ShopFloor") = item.ShopFloor
            dr("SUMA") = item.SUMA
            dt1.Rows.Add(dr)
        Next

        Return dt1
    End Function

    Public Shared Function GetDailyResultData(ByVal dtStatistic As DataTable, ByVal dtRepair As DataTable, ByVal dtProductMaster As DataTable, ByVal dtDJQty As DataTable) As DataTable
        Dim query1 =
                   From statistic In dtStatistic.AsEnumerable()
                   Group Join djqty In dtDJQty.AsEnumerable()
                   On statistic.Field(Of String)("ProdOrder").ToUpper() Equals djqty.Field(Of String)("PO").ToUpper() Into g2 = Group
                   From test In g2.DefaultIfEmpty()
                   Group Join repair In dtRepair.AsEnumerable()
                   On statistic.Field(Of String)("Model").ToUpper() Equals repair.Field(Of String)("Model").ToUpper() _
                   And statistic.Field(Of String)("ProcessName").ToUpper() Equals repair.Field(Of String)("TestStation").ToUpper() _
                   And statistic.Field(Of String)("ProdOrder").ToUpper() Equals repair.Field(Of String)("ProdOrder").ToUpper() _
                   And statistic.Field(Of String)("ShopFloor").ToUpper() Equals repair.Field(Of String)("shopFloor").ToUpper() Into gg = Group
                   From repair In gg.DefaultIfEmpty
                   Group Join prodMaster In dtProductMaster.AsEnumerable()
                   On statistic.Field(Of String)("Model").ToUpper() Equals prodMaster.Field(Of String)("Model").ToUpper() Into g1 = Group
                   From prodMaster In g1.DefaultIfEmpty
                   Order By statistic.Field(Of String)("Model") Ascending, statistic.Field(Of String)("ProcessName") Ascending
                   Select New With
                   {
                    .Model = statistic.Field(Of String)("Model"),
                    .ProcessName = statistic.Field(Of String)("ProcessName"),
                    .SUMA = statistic.Field(Of Integer)("SUMA"),
                    .NUM = If(repair Is Nothing, 0, repair.Field(Of Integer)("NUMB")),
                    .ProdOrder = statistic.Field(Of String)("ProdOrder"),
                    .POQty = test.Field(Of Int32)("POQty"),
                    .ShopFloor = statistic.Field(Of String)("ShopFloor"),
                    .Description = If(prodMaster Is Nothing, "", prodMaster.Field(Of String)("Description")),
                    .BusinessUnit = If(prodMaster Is Nothing, "", prodMaster.Field(Of String)("BusinessUnit")),
                    .Power = If(prodMaster Is Nothing, "", prodMaster.Field(Of String)("Power")),
                    .VotageType = If(prodMaster Is Nothing, "", prodMaster.Field(Of String)("VoltageType"))
                   }


        Dim dt2 As DataTable = New DataTable("Daily")
        Dim dcModel As DataColumn = New DataColumn("Model", System.Type.GetType("System.String"))
        dt2.Columns.Add(dcModel)
        Dim dcDescription As DataColumn = New DataColumn("Description", System.Type.GetType("System.String"))
        dt2.Columns.Add(dcDescription)
        Dim dcshopfloor As DataColumn = New DataColumn("Floor", System.Type.GetType("System.String"))
        dt2.Columns.Add(dcshopfloor)
        Dim dcBusinessUnit As DataColumn = New DataColumn("BusinessUnit", System.Type.GetType("System.String"))
        dt2.Columns.Add(dcBusinessUnit)
        Dim dcStation As DataColumn = New DataColumn("Station", System.Type.GetType("System.String"))
        dt2.Columns.Add(dcStation)
        Dim dcTotal As DataColumn = New DataColumn("Total", System.Type.GetType("System.String"))
        dt2.Columns.Add(dcTotal)
        Dim dcSuccess As DataColumn = New DataColumn("Success", System.Type.GetType("System.String"))
        dt2.Columns.Add(dcSuccess)
        Dim dcFailed As DataColumn = New DataColumn("Failed", System.Type.GetType("System.String"))
        dt2.Columns.Add(dcFailed)
        Dim dcYield As DataColumn = New DataColumn("Yield", System.Type.GetType("System.String"))
        dt2.Columns.Add(dcYield)
        Dim dcPPM As DataColumn = New DataColumn("PPM", System.Type.GetType("System.String"))
        dt2.Columns.Add(dcPPM)
        Dim dcVotageType As DataColumn = New DataColumn("VoltageType", System.Type.GetType("System.String"))
        dt2.Columns.Add(dcVotageType)
        Dim dcDJ As DataColumn = New DataColumn("ProdOrder", System.Type.GetType("System.String"))
        dt2.Columns.Add(dcDJ)
        Dim POQty As DataColumn = New DataColumn("POQty", System.Type.GetType("System.Int32"))
        dt2.Columns.Add(POQty)
        Dim dcPower As DataColumn = New DataColumn("Power", System.Type.GetType("System.String"))
        dt2.Columns.Add(dcPower)
        Dim dcCurrentDate As DataColumn = New DataColumn("GenerateDate", System.Type.GetType("System.String"))
        dt2.Columns.Add(dcCurrentDate)
        Try
            For Each dailyItem In query1
                Dim dr As DataRow = dt2.NewRow()
                dr("Model") = dailyItem.Model
                dr("Description") = dailyItem.Description
                dr("Floor") = dailyItem.ShopFloor
                dr("BusinessUnit") = dailyItem.BusinessUnit
                dr("Station") = dailyItem.ProcessName
                If dailyItem.NUM > dailyItem.SUMA Then
                    dr("Success") = dailyItem.NUM - dailyItem.NUM
                    dr("Total") = dailyItem.NUM
                    dr("Failed") = dailyItem.NUM

                    dr("PPM") = Format(Convert.ToDouble(1), "###0.0000") * 1000000
                Else
                    dr("Success") = dailyItem.SUMA - dailyItem.NUM
                    dr("Total") = dailyItem.SUMA
                    dr("Failed") = dailyItem.NUM

                    If dailyItem.NUM = 0 Then
                        dr("PPM") = Format(Convert.ToDouble(0), "###0.0000") * 1000000
                    Else
                        dr("PPM") = Format(Convert.ToDouble(dailyItem.NUM / dailyItem.SUMA), "###0.0000") * 1000000
                        End If
                    End If

                dr("Yield") = Format(Convert.ToDouble((Convert.ToDouble(dr("Success"))) / Convert.ToDouble(dr("Total"))), "###0.0000") * 100
                dr("VoltageType") = dailyItem.VotageType
                dr("ProdOrder") = dailyItem.ProdOrder
                dr("POQty") = dailyItem.POQty
                dr("Power") = dailyItem.Power
                dr("GenerateDate") = System.DateTime.Now.Date.ToShortDateString()
                dt2.Rows.Add(dr)
            Next

            Return dt2

        Catch ex As Exception

            End Try
    End Function

End Class
