using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Election_StoredProcedures
{
    class ElectionManager
    {
        private readonly string ConnectionString;

        public ElectionManager(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public void AddVoter(string name, int age, string address)
        {
            ExecuteStoredProcedure("spAddVoter", ("@Name", name), ("@Age", age), ("@Address", address));
        }

        public void AddCandidate(string name, string party, string position)
        {
            ExecuteStoredProcedure("spAddCandidate", ("@Name", name), ("@Party", party), ("@Position", position));
        }

        public void RecordVote(int voterId, int candidateId)
        {
            ExecuteStoredProcedure("spRecordVote", ("@VoterID", voterId), ("@CandidateID", candidateId));
        }

        public void UpdateVoter(int voterId, string name, int age, string address)
        {
            ExecuteStoredProcedure("spUpdateVoter", ("@VoterID", voterId), ("@Name", name), ("@Age", age), ("@Address", address));
        }

        public void UpdateCandidate(int candidateId, string name, string party, string position)
        {
            ExecuteStoredProcedure("spUpdateCandidate", ("@CandidateID", candidateId), ("@Name", name), ("@Party", party), ("@Position", position));
        }

        public void DeleteVoter(int voterId)
        {
            ExecuteStoredProcedure("spDeleteVoter", ("@VoterID", voterId));
        }

        public void DeleteCandidate(int candidateId)
        {
            ExecuteStoredProcedure("spDeleteCandidate", ("@CandidateID", candidateId));
        }

        public void DeleteVote(int voteId)
        {
            ExecuteStoredProcedure("spDeleteVote", ("@VoteID", voteId));
        }

        public void DisplayResults()
        {
            using (var connection = new SqlConnection(ConnectionString))
            using (var command = new SqlCommand("spGetResults", connection))
            {
                connection.Open();
                command.CommandType = CommandType.StoredProcedure;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string candidateName = reader["CandidateName"].ToString();
                        string party = reader["CandidateParty"].ToString();
                        int votesReceived = Convert.ToInt32(reader["VotesReceived"]);

                        Console.WriteLine($"Candidate: {candidateName}, Party: {party}, Votes Received: {votesReceived}");
                    }
                }
            }
        }

        private void ExecuteStoredProcedure(string storedProcedureName, params (string, object)[] parameters)
        {
            using (var connection = new SqlConnection(ConnectionString))
            using (var command = new SqlCommand(storedProcedureName, connection))
            {
                connection.Open();
                command.CommandType = CommandType.StoredProcedure;

                foreach (var (parameterName, value) in parameters)
                {
                    command.Parameters.AddWithValue(parameterName, value);
                }

                command.ExecuteNonQuery();
            }
        }
    }
}
