namespace VxShutdownTimerService
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.VxShutdownTimerSvcProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.VxShutdownTimerSvcInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // VxShutdownTimerSvcProcessInstaller
            // 
            this.VxShutdownTimerSvcProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.VxShutdownTimerSvcProcessInstaller.Password = null;
            this.VxShutdownTimerSvcProcessInstaller.Username = null;
            // 
            // VxShutdownTimerSvcInstaller
            // 
            this.VxShutdownTimerSvcInstaller.DisplayName = "Vx Shutdown Timer Service";
            this.VxShutdownTimerSvcInstaller.ServiceName = "VxShutdownTimerSvc";
            this.VxShutdownTimerSvcInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.VxShutdownTimerSvcProcessInstaller,
            this.VxShutdownTimerSvcInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller VxShutdownTimerSvcProcessInstaller;
        private System.ServiceProcess.ServiceInstaller VxShutdownTimerSvcInstaller;
    }
}