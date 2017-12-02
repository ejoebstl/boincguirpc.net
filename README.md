**What is this for?**

This is a .Net implementation of a basic [Boinc Gui RPC](http://boinc.berkeley.edu/trac/wiki/GuiRpc) client, which is capable of connecting to and manage [Boinc](http://boinc.berkeley.edu/).
The project is intentionally implemented in .NET 2.0, so it can be easily run with mono.

**Usage Example - Basic**

This example shows how basic Boinc functions can be accessed and called. 

```csharp
//Creates a new Boinc Gui RPC client for the local host 
Boinc.BoincClient b = new Boinc.BoincClient();

//Connect. Find the password for connecting automatically. 
b.Connect();

//Attaches to a project
//b.AttachToProject("http://www.worldcommunitygrid.org", "PROJECT TOKEN HERE", "WCG");

//Gets and lists current projects
Console.WriteLine("Current projects:");
Console.WriteLine();
foreach(Project p in b.GetProjects()) {
    Console.WriteLine("{0} ({1})", p.ProjectName, p.MasterUrl);
    Console.WriteLine("Disk Usage: {0} MB", p.DiskUsage);
    if (p.Suspended)
        Console.WriteLine("Suspended via gui");
    if (p.MoreWorkAllowed)
        Console.WriteLine("Allow more work");
    if (p.HasEnded)
        Console.WriteLine("Project has ended");
    
    //Resume or suspend project. 
    //p.Suspend();
    //p.Resume();
}

//Gets the client version
Console.WriteLine("Client version: {0}", b.GetVersion());

//Gets results, proxy settings and preferences
IEnumerable<Result> results = b.GetResults();
ProxySettings prox = b.GetProxySettings();
Preferences pref = b.GetPreferences();

//Sets preferences
b.SetPreferences(new Preferences(false, false, 50, 10, 1));

//Closes the connection
b.Close();
```

**Usage Example - Event Driven**

This example shows how the BoincWatcher class can be used to work with Boinc using events for state changes. 

```csharp
BoincClient client;
BoincWatcher watcher;

//Call this method to initialize the system. 
private void Initialize() 
{
      client = new BoincClient();     
      client.Connect();

      //Create a new watcher.
      watcher = new BoincWatcher(b);

      //Register events of choice.
      watcher.ProjectAdded += new EventHandler<CollectionModifiedEventArgs<Project>>(watcher_ProjectAdded);
      watcher.ProjectRemoved += new EventHandler<CollectionModifiedEventArgs<Project>>(watcher_ProjectRemoved);
     watcher.TaskAdded += new EventHandler<CollectionModifiedEventArgs<Result>>(watcher_TaskAdded);
     watcher.TaskRemoved += new EventHandler<CollectionModifiedEventArgs<Result>>(watcher_TaskRemoved);
     watcher.TaskStateChanged += new EventHandler<TaskStateChangedEventArgs>(watcher_TaskStateChanged);
}

//Call this method periodically, using a timer or similar. 
//Take care about thread synchronization.
private void Refresh() 
{
      //Query new information from Boinc and raise events, if applicable. 
      watcher.RefreshState();
}

//Event handlers
static void watcher_TaskStateChanged(object sender, TaskStateChangedEventArgs e)
{
      Console.WriteLine("Task Changed: " + e.NewState.Name);
      Console.WriteLine("     Percentage: " + e.NewState.FractionDone);
      Console.WriteLine("     Remaining Time: " + e.NewState.EstimatedCpuTimeRemaining);
      Console.WriteLine("     Ready To Report: " + e.NewState.ReadyToReport);
      Console.WriteLine("     Ack: " + e.NewState.Acknowledged);
}

static void watcher_TaskRemoved(object sender, CollectionModifiedEventArgs<Result> e)
{
      Console.WriteLine("Task Removed: " + e.ModifiedItem.Name);
}

static void watcher_TaskAdded(object sender, CollectionModifiedEventArgs<Result> e)
{
      Console.WriteLine("Task Added: " + e.ModifiedItem.Name);
}

static void watcher_ProjectRemoved(object sender, CollectionModifiedEventArgs<Project> e)
{
      Console.WriteLine("Project Removed: " + e.ModifiedItem.ProjectName);
}

static void watcher_ProjectAdded(object sender, CollectionModifiedEventArgs<Project> e)
{
      Console.WriteLine("Project Added: " + e.ModifiedItem.ProjectName);
}
```
