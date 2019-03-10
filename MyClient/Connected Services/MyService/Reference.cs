﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyClient.MyService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="MyService.ITransferService")]
    public interface ITransferService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITransferService/GetDisciplinesList", ReplyAction="http://tempuri.org/ITransferService/GetDisciplinesListResponse")]
        System.Collections.Generic.List<MyModelLibrary.Discipline> GetDisciplinesList(MyModelLibrary.accounts MyAcc);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITransferService/GetDisciplinesList", ReplyAction="http://tempuri.org/ITransferService/GetDisciplinesListResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<MyModelLibrary.Discipline>> GetDisciplinesListAsync(MyModelLibrary.accounts MyAcc);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITransferService/GetStudentsGroup", ReplyAction="http://tempuri.org/ITransferService/GetStudentsGroupResponse")]
        System.Collections.Generic.List<MyModelLibrary.Users> GetStudentsGroup(MyModelLibrary.accounts MyAcc, int idGroup);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITransferService/GetStudentsGroup", ReplyAction="http://tempuri.org/ITransferService/GetStudentsGroupResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<MyModelLibrary.Users>> GetStudentsGroupAsync(MyModelLibrary.accounts MyAcc, int idGroup);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITransferService/GetSpecialityCodes", ReplyAction="http://tempuri.org/ITransferService/GetSpecialityCodesResponse")]
        System.Collections.Generic.List<MyModelLibrary.Speciality_codes> GetSpecialityCodes();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITransferService/GetSpecialityCodes", ReplyAction="http://tempuri.org/ITransferService/GetSpecialityCodesResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<MyModelLibrary.Speciality_codes>> GetSpecialityCodesAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITransferService/GetGroups", ReplyAction="http://tempuri.org/ITransferService/GetGroupsResponse")]
        System.Collections.Generic.List<MyModelLibrary.Groups> GetGroups();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITransferService/GetGroups", ReplyAction="http://tempuri.org/ITransferService/GetGroupsResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<MyModelLibrary.Groups>> GetGroupsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITransferService/GetTeacherList", ReplyAction="http://tempuri.org/ITransferService/GetTeacherListResponse")]
        System.Collections.Generic.List<MyModelLibrary.Users> GetTeacherList(MyModelLibrary.accounts MyAcc);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITransferService/GetTeacherList", ReplyAction="http://tempuri.org/ITransferService/GetTeacherListResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<MyModelLibrary.Users>> GetTeacherListAsync(MyModelLibrary.accounts MyAcc);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITransferService/GetTeacherDisciplines", ReplyAction="http://tempuri.org/ITransferService/GetTeacherDisciplinesResponse")]
        System.Collections.Generic.List<MyModelLibrary.Discipline> GetTeacherDisciplines(MyModelLibrary.accounts MyAcc, MyModelLibrary.Users Teacher, byte param);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITransferService/GetTeacherDisciplines", ReplyAction="http://tempuri.org/ITransferService/GetTeacherDisciplinesResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<MyModelLibrary.Discipline>> GetTeacherDisciplinesAsync(MyModelLibrary.accounts MyAcc, MyModelLibrary.Users Teacher, byte param);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITransferService/AddTeacherDiscipline", ReplyAction="http://tempuri.org/ITransferService/AddTeacherDisciplineResponse")]
        bool AddTeacherDiscipline(MyModelLibrary.accounts MyAcc, MyModelLibrary.Users Teacher, MyModelLibrary.Discipline Discipline);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITransferService/AddTeacherDiscipline", ReplyAction="http://tempuri.org/ITransferService/AddTeacherDisciplineResponse")]
        System.Threading.Tasks.Task<bool> AddTeacherDisciplineAsync(MyModelLibrary.accounts MyAcc, MyModelLibrary.Users Teacher, MyModelLibrary.Discipline Discipline);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITransferService/AddDiscipline", ReplyAction="http://tempuri.org/ITransferService/AddDisciplineResponse")]
        bool AddDiscipline(MyModelLibrary.accounts MyAcc, MyModelLibrary.Discipline NewDiscipline);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITransferService/AddDiscipline", ReplyAction="http://tempuri.org/ITransferService/AddDisciplineResponse")]
        System.Threading.Tasks.Task<bool> AddDisciplineAsync(MyModelLibrary.accounts MyAcc, MyModelLibrary.Discipline NewDiscipline);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITransferService/AddStudentInGroup", ReplyAction="http://tempuri.org/ITransferService/AddStudentInGroupResponse")]
        bool AddStudentInGroup(MyModelLibrary.accounts MyAcc, MyModelLibrary.StudentsGroup Student);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITransferService/AddStudentInGroup", ReplyAction="http://tempuri.org/ITransferService/AddStudentInGroupResponse")]
        System.Threading.Tasks.Task<bool> AddStudentInGroupAsync(MyModelLibrary.accounts MyAcc, MyModelLibrary.StudentsGroup Student);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITransferService/RemoveStudentInGroup", ReplyAction="http://tempuri.org/ITransferService/RemoveStudentInGroupResponse")]
        bool RemoveStudentInGroup(MyModelLibrary.accounts MyAcc, MyModelLibrary.StudentsGroup Student);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITransferService/RemoveStudentInGroup", ReplyAction="http://tempuri.org/ITransferService/RemoveStudentInGroupResponse")]
        System.Threading.Tasks.Task<bool> RemoveStudentInGroupAsync(MyModelLibrary.accounts MyAcc, MyModelLibrary.StudentsGroup Student);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITransferService/AddGroup", ReplyAction="http://tempuri.org/ITransferService/AddGroupResponse")]
        bool AddGroup(MyModelLibrary.accounts MyAcc, MyModelLibrary.Groups NewGroup);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITransferService/AddGroup", ReplyAction="http://tempuri.org/ITransferService/AddGroupResponse")]
        System.Threading.Tasks.Task<bool> AddGroupAsync(MyModelLibrary.accounts MyAcc, MyModelLibrary.Groups NewGroup);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITransferService/GetAllUsersList", ReplyAction="http://tempuri.org/ITransferService/GetAllUsersListResponse")]
        System.Collections.Generic.List<MyModelLibrary.Users> GetAllUsersList(MyModelLibrary.accounts MyAcc);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITransferService/GetAllUsersList", ReplyAction="http://tempuri.org/ITransferService/GetAllUsersListResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<MyModelLibrary.Users>> GetAllUsersListAsync(MyModelLibrary.accounts MyAcc);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITransferService/GetAllAccountsList", ReplyAction="http://tempuri.org/ITransferService/GetAllAccountsListResponse")]
        System.Collections.Generic.List<MyModelLibrary.accounts> GetAllAccountsList(MyModelLibrary.accounts MyAcc);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITransferService/GetAllAccountsList", ReplyAction="http://tempuri.org/ITransferService/GetAllAccountsListResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<MyModelLibrary.accounts>> GetAllAccountsListAsync(MyModelLibrary.accounts MyAcc);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITransferService/AddAccount", ReplyAction="http://tempuri.org/ITransferService/AddAccountResponse")]
        bool AddAccount(MyModelLibrary.accounts AddAcc, MyModelLibrary.accounts CurrentAcc);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITransferService/AddAccount", ReplyAction="http://tempuri.org/ITransferService/AddAccountResponse")]
        System.Threading.Tasks.Task<bool> AddAccountAsync(MyModelLibrary.accounts AddAcc, MyModelLibrary.accounts CurrentAcc);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITransferService/EditAccount", ReplyAction="http://tempuri.org/ITransferService/EditAccountResponse")]
        bool EditAccount(MyModelLibrary.accounts MyAcc, MyModelLibrary.accounts EditAcc);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITransferService/EditAccount", ReplyAction="http://tempuri.org/ITransferService/EditAccountResponse")]
        System.Threading.Tasks.Task<bool> EditAccountAsync(MyModelLibrary.accounts MyAcc, MyModelLibrary.accounts EditAcc);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITransferService/GetAccount", ReplyAction="http://tempuri.org/ITransferService/GetAccountResponse")]
        MyModelLibrary.accounts GetAccount(MyModelLibrary.accounts MyAcc, int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITransferService/GetAccount", ReplyAction="http://tempuri.org/ITransferService/GetAccountResponse")]
        System.Threading.Tasks.Task<MyModelLibrary.accounts> GetAccountAsync(MyModelLibrary.accounts MyAcc, int id);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ITransferServiceChannel : MyClient.MyService.ITransferService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class TransferServiceClient : System.ServiceModel.ClientBase<MyClient.MyService.ITransferService>, MyClient.MyService.ITransferService {
        
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
        
        public System.Collections.Generic.List<MyModelLibrary.Discipline> GetDisciplinesList(MyModelLibrary.accounts MyAcc) {
            return base.Channel.GetDisciplinesList(MyAcc);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<MyModelLibrary.Discipline>> GetDisciplinesListAsync(MyModelLibrary.accounts MyAcc) {
            return base.Channel.GetDisciplinesListAsync(MyAcc);
        }
        
        public System.Collections.Generic.List<MyModelLibrary.Users> GetStudentsGroup(MyModelLibrary.accounts MyAcc, int idGroup) {
            return base.Channel.GetStudentsGroup(MyAcc, idGroup);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<MyModelLibrary.Users>> GetStudentsGroupAsync(MyModelLibrary.accounts MyAcc, int idGroup) {
            return base.Channel.GetStudentsGroupAsync(MyAcc, idGroup);
        }
        
        public System.Collections.Generic.List<MyModelLibrary.Speciality_codes> GetSpecialityCodes() {
            return base.Channel.GetSpecialityCodes();
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<MyModelLibrary.Speciality_codes>> GetSpecialityCodesAsync() {
            return base.Channel.GetSpecialityCodesAsync();
        }
        
        public System.Collections.Generic.List<MyModelLibrary.Groups> GetGroups() {
            return base.Channel.GetGroups();
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<MyModelLibrary.Groups>> GetGroupsAsync() {
            return base.Channel.GetGroupsAsync();
        }
        
        public System.Collections.Generic.List<MyModelLibrary.Users> GetTeacherList(MyModelLibrary.accounts MyAcc) {
            return base.Channel.GetTeacherList(MyAcc);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<MyModelLibrary.Users>> GetTeacherListAsync(MyModelLibrary.accounts MyAcc) {
            return base.Channel.GetTeacherListAsync(MyAcc);
        }
        
        public System.Collections.Generic.List<MyModelLibrary.Discipline> GetTeacherDisciplines(MyModelLibrary.accounts MyAcc, MyModelLibrary.Users Teacher, byte param) {
            return base.Channel.GetTeacherDisciplines(MyAcc, Teacher, param);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<MyModelLibrary.Discipline>> GetTeacherDisciplinesAsync(MyModelLibrary.accounts MyAcc, MyModelLibrary.Users Teacher, byte param) {
            return base.Channel.GetTeacherDisciplinesAsync(MyAcc, Teacher, param);
        }
        
        public bool AddTeacherDiscipline(MyModelLibrary.accounts MyAcc, MyModelLibrary.Users Teacher, MyModelLibrary.Discipline Discipline) {
            return base.Channel.AddTeacherDiscipline(MyAcc, Teacher, Discipline);
        }
        
        public System.Threading.Tasks.Task<bool> AddTeacherDisciplineAsync(MyModelLibrary.accounts MyAcc, MyModelLibrary.Users Teacher, MyModelLibrary.Discipline Discipline) {
            return base.Channel.AddTeacherDisciplineAsync(MyAcc, Teacher, Discipline);
        }
        
        public bool AddDiscipline(MyModelLibrary.accounts MyAcc, MyModelLibrary.Discipline NewDiscipline) {
            return base.Channel.AddDiscipline(MyAcc, NewDiscipline);
        }
        
        public System.Threading.Tasks.Task<bool> AddDisciplineAsync(MyModelLibrary.accounts MyAcc, MyModelLibrary.Discipline NewDiscipline) {
            return base.Channel.AddDisciplineAsync(MyAcc, NewDiscipline);
        }
        
        public bool AddStudentInGroup(MyModelLibrary.accounts MyAcc, MyModelLibrary.StudentsGroup Student) {
            return base.Channel.AddStudentInGroup(MyAcc, Student);
        }
        
        public System.Threading.Tasks.Task<bool> AddStudentInGroupAsync(MyModelLibrary.accounts MyAcc, MyModelLibrary.StudentsGroup Student) {
            return base.Channel.AddStudentInGroupAsync(MyAcc, Student);
        }
        
        public bool RemoveStudentInGroup(MyModelLibrary.accounts MyAcc, MyModelLibrary.StudentsGroup Student) {
            return base.Channel.RemoveStudentInGroup(MyAcc, Student);
        }
        
        public System.Threading.Tasks.Task<bool> RemoveStudentInGroupAsync(MyModelLibrary.accounts MyAcc, MyModelLibrary.StudentsGroup Student) {
            return base.Channel.RemoveStudentInGroupAsync(MyAcc, Student);
        }
        
        public bool AddGroup(MyModelLibrary.accounts MyAcc, MyModelLibrary.Groups NewGroup) {
            return base.Channel.AddGroup(MyAcc, NewGroup);
        }
        
        public System.Threading.Tasks.Task<bool> AddGroupAsync(MyModelLibrary.accounts MyAcc, MyModelLibrary.Groups NewGroup) {
            return base.Channel.AddGroupAsync(MyAcc, NewGroup);
        }
        
        public System.Collections.Generic.List<MyModelLibrary.Users> GetAllUsersList(MyModelLibrary.accounts MyAcc) {
            return base.Channel.GetAllUsersList(MyAcc);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<MyModelLibrary.Users>> GetAllUsersListAsync(MyModelLibrary.accounts MyAcc) {
            return base.Channel.GetAllUsersListAsync(MyAcc);
        }
        
        public System.Collections.Generic.List<MyModelLibrary.accounts> GetAllAccountsList(MyModelLibrary.accounts MyAcc) {
            return base.Channel.GetAllAccountsList(MyAcc);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<MyModelLibrary.accounts>> GetAllAccountsListAsync(MyModelLibrary.accounts MyAcc) {
            return base.Channel.GetAllAccountsListAsync(MyAcc);
        }
        
        public bool AddAccount(MyModelLibrary.accounts AddAcc, MyModelLibrary.accounts CurrentAcc) {
            return base.Channel.AddAccount(AddAcc, CurrentAcc);
        }
        
        public System.Threading.Tasks.Task<bool> AddAccountAsync(MyModelLibrary.accounts AddAcc, MyModelLibrary.accounts CurrentAcc) {
            return base.Channel.AddAccountAsync(AddAcc, CurrentAcc);
        }
        
        public bool EditAccount(MyModelLibrary.accounts MyAcc, MyModelLibrary.accounts EditAcc) {
            return base.Channel.EditAccount(MyAcc, EditAcc);
        }
        
        public System.Threading.Tasks.Task<bool> EditAccountAsync(MyModelLibrary.accounts MyAcc, MyModelLibrary.accounts EditAcc) {
            return base.Channel.EditAccountAsync(MyAcc, EditAcc);
        }
        
        public MyModelLibrary.accounts GetAccount(MyModelLibrary.accounts MyAcc, int id) {
            return base.Channel.GetAccount(MyAcc, id);
        }
        
        public System.Threading.Tasks.Task<MyModelLibrary.accounts> GetAccountAsync(MyModelLibrary.accounts MyAcc, int id) {
            return base.Channel.GetAccountAsync(MyAcc, id);
        }
    }
}
