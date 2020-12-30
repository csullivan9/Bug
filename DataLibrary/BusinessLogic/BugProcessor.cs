using System;
using System.Collections.Generic;
using System.Text;
using DataLibrary.Models;
using System.Data.SqlClient;
using DataLibrary.DataAccess;

namespace DataLibrary.BusinessLogic
{
    public class BugProcessor
    {
        // Maps MVC model to data model
        public static int CreateBug(string userId, string name, string description, string bugSeverity, string status)
        {
            Bug newBug = new Bug
            {
                Id = 0,
                UserId = userId,
                Name = name,
                Description = description,
                BugSeverity = bugSeverity,
                Status = status
            };
            string sql = @"insert into Bugs (UserId, Name, Description, BugSeverity, Status) 
                values (@UserId, @Name, @Description, @BugSeverity, @Status);";

            return SqlDataAccess.SaveData(sql, newBug);
        }

        public static List<Bug> LoadBugs(string userId)
        {
            string sql = @"select Id, UserId, Name, Description, BugSeverity, Status from dbo.Bugs where UserId=@userId";
            //string sql = @"select Id, UserId, Name, Description, BugSeverity, Status from dbo.Bugs";

            return SqlDataAccess.LoadData<Bug>(sql, userId);
        }

        public static int DeleteBug(int Id)
        {
            string sql = @"delete from Bugs where Id=@Id";
            return SqlDataAccess.DeleteData(sql, Id);
        }

        
    }
}
