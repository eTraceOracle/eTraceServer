﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace eTraceWebApi.SMTServiceReference {
    using System.Data;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="SMTServiceReference.SMTDataServiceSoap")]
    public interface SMTDataServiceSoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/getMachineType", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string getMachineType(string equipment);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/getMachineType", ReplyAction="*")]
        System.Threading.Tasks.Task<string> getMachineTypeAsync(string equipment);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/getSMTPlanByDate", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataTable getSMTPlanByDate(System.DateTime dt);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/getSMTPlanByDate", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataTable> getSMTPlanByDateAsync(System.DateTime dt);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/getFixedLocatorByPN", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataTable getFixedLocatorByPN(string pn, string floor);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/getFixedLocatorByPN", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataTable> getFixedLocatorByPNAsync(string pn, string floor);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/modifyFixedLocator", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string modifyFixedLocator(string mode, string pn, string location, string feeder, string floor);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/modifyFixedLocator", ReplyAction="*")]
        System.Threading.Tasks.Task<string> modifyFixedLocatorAsync(string mode, string pn, string location, string feeder, string floor);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/getMSL", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataTable getMSL(int lineID, string program, string machineType);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/getMSL", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataTable> getMSLAsync(int lineID, string program, string machineType);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/getLineList", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataTable getLineList();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/getLineList", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataTable> getLineListAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/getSiplaceLineList", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataTable getSiplaceLineList();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/getSiplaceLineList", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataTable> getSiplaceLineListAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/getMachineListByLine", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataTable getMachineListByLine(int lineID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/getMachineListByLine", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataTable> getMachineListByLineAsync(int lineID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/getSiplaceProgramList", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataTable getSiplaceProgramList(int lineID, string dj, string side);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/getSiplaceProgramList", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataTable> getSiplaceProgramListAsync(int lineID, string dj, string side);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/getMSL_Beta", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataTable getMSL_Beta(int lineID, string program, string machineType);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/getMSL_Beta", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataTable> getMSL_BetaAsync(int lineID, string program, string machineType);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Get_SMTPlan_For_Remind", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet Get_SMTPlan_For_Remind(string line);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Get_SMTPlan_For_Remind", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataSet> Get_SMTPlan_For_RemindAsync(string line);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Modify_SMTPlan", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        void Modify_SMTPlan(string line, string hh, string mi);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Modify_SMTPlan", ReplyAction="*")]
        System.Threading.Tasks.Task Modify_SMTPlanAsync(string line, string hh, string mi);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Modify_SMTPlan_Status", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        void Modify_SMTPlan_Status(string id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Modify_SMTPlan_Status", ReplyAction="*")]
        System.Threading.Tasks.Task Modify_SMTPlan_StatusAsync(string id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetSMTInspectionData", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet GetSMTInspectionData(string barcode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetSMTInspectionData", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataSet> GetSMTInspectionDataAsync(string barcode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/getWIPBYIntSN", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string getWIPBYIntSN(string barcode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/getWIPBYIntSN", ReplyAction="*")]
        System.Threading.Tasks.Task<string> getWIPBYIntSNAsync(string barcode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/getQtyByClid", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        int getQtyByClid(string clid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/getQtyByClid", ReplyAction="*")]
        System.Threading.Tasks.Task<int> getQtyByClidAsync(string clid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/getAOIBoardData", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet getAOIBoardData(string startTime, string endTime, string model, string equipmentID, string result, string dj, string barcode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/getAOIBoardData", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataSet> getAOIBoardDataAsync(string startTime, string endTime, string model, string equipmentID, string result, string dj, string barcode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/pmFeederNewPM", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string pmFeederNewPM(string equipmentID, string prodLine, string location, string useCount, string scheduledDate);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/pmFeederNewPM", ReplyAction="*")]
        System.Threading.Tasks.Task<string> pmFeederNewPMAsync(string equipmentID, string prodLine, string location, string useCount, string scheduledDate);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/checkPackagingUnitChain", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string checkPackagingUnitChain(string equipmentID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/checkPackagingUnitChain", ReplyAction="*")]
        System.Threading.Tasks.Task<string> checkPackagingUnitChainAsync(string equipmentID);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface SMTDataServiceSoapChannel : eTraceWebApi.SMTServiceReference.SMTDataServiceSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SMTDataServiceSoapClient : System.ServiceModel.ClientBase<eTraceWebApi.SMTServiceReference.SMTDataServiceSoap>, eTraceWebApi.SMTServiceReference.SMTDataServiceSoap {
        
        public SMTDataServiceSoapClient() {
        }
        
        public SMTDataServiceSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SMTDataServiceSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SMTDataServiceSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SMTDataServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string getMachineType(string equipment) {
            return base.Channel.getMachineType(equipment);
        }
        
        public System.Threading.Tasks.Task<string> getMachineTypeAsync(string equipment) {
            return base.Channel.getMachineTypeAsync(equipment);
        }
        
        public System.Data.DataTable getSMTPlanByDate(System.DateTime dt) {
            return base.Channel.getSMTPlanByDate(dt);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataTable> getSMTPlanByDateAsync(System.DateTime dt) {
            return base.Channel.getSMTPlanByDateAsync(dt);
        }
        
        public System.Data.DataTable getFixedLocatorByPN(string pn, string floor) {
            return base.Channel.getFixedLocatorByPN(pn, floor);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataTable> getFixedLocatorByPNAsync(string pn, string floor) {
            return base.Channel.getFixedLocatorByPNAsync(pn, floor);
        }
        
        public string modifyFixedLocator(string mode, string pn, string location, string feeder, string floor) {
            return base.Channel.modifyFixedLocator(mode, pn, location, feeder, floor);
        }
        
        public System.Threading.Tasks.Task<string> modifyFixedLocatorAsync(string mode, string pn, string location, string feeder, string floor) {
            return base.Channel.modifyFixedLocatorAsync(mode, pn, location, feeder, floor);
        }
        
        public System.Data.DataTable getMSL(int lineID, string program, string machineType) {
            return base.Channel.getMSL(lineID, program, machineType);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataTable> getMSLAsync(int lineID, string program, string machineType) {
            return base.Channel.getMSLAsync(lineID, program, machineType);
        }
        
        public System.Data.DataTable getLineList() {
            return base.Channel.getLineList();
        }
        
        public System.Threading.Tasks.Task<System.Data.DataTable> getLineListAsync() {
            return base.Channel.getLineListAsync();
        }
        
        public System.Data.DataTable getSiplaceLineList() {
            return base.Channel.getSiplaceLineList();
        }
        
        public System.Threading.Tasks.Task<System.Data.DataTable> getSiplaceLineListAsync() {
            return base.Channel.getSiplaceLineListAsync();
        }
        
        public System.Data.DataTable getMachineListByLine(int lineID) {
            return base.Channel.getMachineListByLine(lineID);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataTable> getMachineListByLineAsync(int lineID) {
            return base.Channel.getMachineListByLineAsync(lineID);
        }
        
        public System.Data.DataTable getSiplaceProgramList(int lineID, string dj, string side) {
            return base.Channel.getSiplaceProgramList(lineID, dj, side);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataTable> getSiplaceProgramListAsync(int lineID, string dj, string side) {
            return base.Channel.getSiplaceProgramListAsync(lineID, dj, side);
        }
        
        public System.Data.DataTable getMSL_Beta(int lineID, string program, string machineType) {
            return base.Channel.getMSL_Beta(lineID, program, machineType);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataTable> getMSL_BetaAsync(int lineID, string program, string machineType) {
            return base.Channel.getMSL_BetaAsync(lineID, program, machineType);
        }
        
        public System.Data.DataSet Get_SMTPlan_For_Remind(string line) {
            return base.Channel.Get_SMTPlan_For_Remind(line);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> Get_SMTPlan_For_RemindAsync(string line) {
            return base.Channel.Get_SMTPlan_For_RemindAsync(line);
        }
        
        public void Modify_SMTPlan(string line, string hh, string mi) {
            base.Channel.Modify_SMTPlan(line, hh, mi);
        }
        
        public System.Threading.Tasks.Task Modify_SMTPlanAsync(string line, string hh, string mi) {
            return base.Channel.Modify_SMTPlanAsync(line, hh, mi);
        }
        
        public void Modify_SMTPlan_Status(string id) {
            base.Channel.Modify_SMTPlan_Status(id);
        }
        
        public System.Threading.Tasks.Task Modify_SMTPlan_StatusAsync(string id) {
            return base.Channel.Modify_SMTPlan_StatusAsync(id);
        }
        
        public System.Data.DataSet GetSMTInspectionData(string barcode) {
            return base.Channel.GetSMTInspectionData(barcode);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> GetSMTInspectionDataAsync(string barcode) {
            return base.Channel.GetSMTInspectionDataAsync(barcode);
        }
        
        public string getWIPBYIntSN(string barcode) {
            return base.Channel.getWIPBYIntSN(barcode);
        }
        
        public System.Threading.Tasks.Task<string> getWIPBYIntSNAsync(string barcode) {
            return base.Channel.getWIPBYIntSNAsync(barcode);
        }
        
        public int getQtyByClid(string clid) {
            return base.Channel.getQtyByClid(clid);
        }
        
        public System.Threading.Tasks.Task<int> getQtyByClidAsync(string clid) {
            return base.Channel.getQtyByClidAsync(clid);
        }
        
        public System.Data.DataSet getAOIBoardData(string startTime, string endTime, string model, string equipmentID, string result, string dj, string barcode) {
            return base.Channel.getAOIBoardData(startTime, endTime, model, equipmentID, result, dj, barcode);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> getAOIBoardDataAsync(string startTime, string endTime, string model, string equipmentID, string result, string dj, string barcode) {
            return base.Channel.getAOIBoardDataAsync(startTime, endTime, model, equipmentID, result, dj, barcode);
        }
        
        public string pmFeederNewPM(string equipmentID, string prodLine, string location, string useCount, string scheduledDate) {
            return base.Channel.pmFeederNewPM(equipmentID, prodLine, location, useCount, scheduledDate);
        }
        
        public System.Threading.Tasks.Task<string> pmFeederNewPMAsync(string equipmentID, string prodLine, string location, string useCount, string scheduledDate) {
            return base.Channel.pmFeederNewPMAsync(equipmentID, prodLine, location, useCount, scheduledDate);
        }
        
        public string checkPackagingUnitChain(string equipmentID) {
            return base.Channel.checkPackagingUnitChain(equipmentID);
        }
        
        public System.Threading.Tasks.Task<string> checkPackagingUnitChainAsync(string equipmentID) {
            return base.Channel.checkPackagingUnitChainAsync(equipmentID);
        }
    }
}
