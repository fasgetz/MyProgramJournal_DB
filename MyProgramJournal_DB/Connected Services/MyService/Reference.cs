﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyProgramJournal_DB.MyService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="MyService.ITransferService")]
    public interface ITransferService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITransferService/DoWork", ReplyAction="http://tempuri.org/ITransferService/DoWorkResponse")]
        void DoWork();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITransferService/DoWork", ReplyAction="http://tempuri.org/ITransferService/DoWorkResponse")]
        System.Threading.Tasks.Task DoWorkAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITransferService/login", ReplyAction="http://tempuri.org/ITransferService/loginResponse")]
        string login();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITransferService/login", ReplyAction="http://tempuri.org/ITransferService/loginResponse")]
        System.Threading.Tasks.Task<string> loginAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ITransferServiceChannel : MyProgramJournal_DB.MyService.ITransferService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class TransferServiceClient : System.ServiceModel.ClientBase<MyProgramJournal_DB.MyService.ITransferService>, MyProgramJournal_DB.MyService.ITransferService {
        
        public TransferServiceClient() {
        }
        
        public TransferServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public TransferServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TransferServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TransferServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void DoWork() {
            base.Channel.DoWork();
        }
        
        public System.Threading.Tasks.Task DoWorkAsync() {
            return base.Channel.DoWorkAsync();
        }
        
        public string login() {
            return base.Channel.login();
        }
        
        public System.Threading.Tasks.Task<string> loginAsync() {
            return base.Channel.loginAsync();
        }
    }
}