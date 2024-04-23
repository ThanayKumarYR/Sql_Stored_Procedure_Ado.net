using System;
using System.Data;
using System.Data.SqlClient;

namespace Election_StoredProcedures
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string connectionString = "server=CAPEDBOLDY\\SQLEXPRESS; Initial Catalog = Election_SP_DB; Integrated Security = SSPI";
                var electionManager = new ElectionManager(connectionString);

                // Add voters and candidates
                electionManager.AddVoter("John Doe", 30, "123 Main St");
                electionManager.AddVoter("Alice Johnson", 45, "456 Elm St");
                electionManager.AddVoter("Bob Smith", 28, "789 Oak St");
                electionManager.AddVoter("Emma Wilson", 35, "321 Pine St");
                electionManager.AddVoter("David Lee", 50, "654 Maple St");

                electionManager.AddCandidate("Jane Smith", "Independent", "Mayor");
                electionManager.AddCandidate("Michael Johnson", "Republican", "Governor");
                electionManager.AddCandidate("Emily Brown", "Democrat", "Senator");
                electionManager.AddCandidate("Chris Taylor", "Green Party", "City Council");

                // Record votes
                electionManager.RecordVote(1, 1); // John Doe voted for Jane Smith
                electionManager.RecordVote(2, 2); // Alice Johnson voted for Michael Johnson
                electionManager.RecordVote(3, 3); // Bob Smith voted for Emily Brown
                electionManager.RecordVote(4, 2); // Emma Wilson voted for Michael Johnson
                electionManager.RecordVote(5, 2); // David Lee voted for Michael Johnson

                // Display results
                electionManager.DisplayResults();

                // Update voter
                electionManager.UpdateVoter(1, "John Doe", 32, "123 Main St");

                // Update candidate
                electionManager.UpdateCandidate(2, "Michael Johnson", "Republican", "President");

                // Delete voter
                electionManager.DeleteVoter(3);

                // Delete candidate
                electionManager.DeleteCandidate(4);

                // Delete vote
                electionManager.DeleteVote(5);

                // Display results after updates and deletions
                electionManager.DisplayResults();

                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();

            }
            catch(Exception ex) { Console.WriteLine(ex.Message); }
        }
    }
}
