namespace Milestone_cst_350.Models
{
    public class SaveGameModel
    {
        // Save Game Model properties 
        public int Id { get; set; }
        public int user_id { get; set; }
        public DateTime save_date { get; set; }
        public string save_state { get; set; }

        // Default Constructor
        public SaveGameModel()
        {
        }

        // Save Game Model Constructor
        public SaveGameModel(int id, int user_id, DateTime save_date, string save_state)
        {
            Id = id;
            this.user_id = user_id;
            this.save_date = save_date;
            this.save_state = save_state;
        }
    }
}
