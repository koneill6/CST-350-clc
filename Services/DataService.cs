namespace Milestone_cst_350.Services
{
    // If there is a way to do this with an interface I would do that but idk how you'd inherit the values...

    public class DataService
    {
        /// <summary>
        /// This should make it easy to maintain a single value across all our data services.
        /// The protected keyword should also allow classes deriving this value access to it.
        /// </summary>
        protected string ConnectionString { get { return @"Server=tcp:tempgcu.database.windows.net,1433;Initial Catalog=minesweeper;Persist Security Info=False;User ID=clc.student;Password=GCU@access1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"; } }
    }
}
