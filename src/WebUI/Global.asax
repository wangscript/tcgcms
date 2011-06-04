﻿<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup

    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown
        System.Timers.Timer myTimer = new System.Timers.Timer(60000);
        myTimer.Elapsed += new System.Timers.ElapsedEventHandler(OnTimedEvent);
        myTimer.Interval = 60000;
        myTimer.Enabled = true;

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started

    }
    
    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }

    void OnTimedEvent(object source, System.Timers.ElapsedEventArgs e)
    {
        
    }
       
</script>