// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddressBookRepository.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator Name="Aseem Anand"/>
// --------------------------------------------------------------------------------------------------------------------
namespace Ado.NetAddressBook
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class AddressBookRepository
    {
        public static SqlConnection connection { get; set; }

        /// <summary>
        /// UC1-2: Initializes connection and Gets all contacts.
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void GetAllContacts()
        {
            //Creates a new connection for every method to avoid "ConnectionString property not initialized" exception
            DBConnection dbc = new DBConnection();
            connection = dbc.GetConnection();
            AddressBookModel model = new AddressBookModel();
            string query = @"select * from dbo.address_book";
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            model.FirstName = reader.GetString(0);
                            model.LastName = reader.GetString(1);
                            model.Address = reader.GetString(2);
                            model.City = reader.GetString(3);
                            model.State = reader.GetString(4);
                            model.Zip = reader.GetInt32(5);
                            model.PhoneNumber = reader.GetInt64(6);
                            model.Email = reader.GetString(7);
                            model.AddressBookName = reader.GetString(8);
                            model.ContactType = reader.GetString(9);
                            Console.WriteLine($"First Name: {model.FirstName}\nLast Name: {model.LastName}\nAddress: {model.Address}\nCity: {model.City}\nState: {model.State}\nZip: {model.Zip}\nPhone Number: {model.PhoneNumber}\nContact Type: {model.ContactType}\nAddress Book Name : {model.AddressBookName}");
                            Console.WriteLine("\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data found");
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (connection.State.Equals("Open"))
                    connection.Close();
            }
        }

        /// <summary>
        /// UC 3 : Inserts the contact into DB.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool AddContact(AddressBookModel model)
        {
            DBConnection dbc = new DBConnection();
            connection = dbc.GetConnection();
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("dbo.spAddContactDetails", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@firstname", model.FirstName);
                    command.Parameters.AddWithValue("@lastname", model.LastName);
                    command.Parameters.AddWithValue("@address", model.Address);
                    command.Parameters.AddWithValue("@city", model.City);
                    command.Parameters.AddWithValue("@state", model.State);
                    command.Parameters.AddWithValue("@zip", model.Zip);
                    command.Parameters.AddWithValue("@phoneNo", model.PhoneNumber);
                    command.Parameters.AddWithValue("@email", model.Email);
                    command.Parameters.AddWithValue("@addressbookname", model.AddressBookName);
                    command.Parameters.AddWithValue("@contactType", model.ContactType);
                    connection.Open();
                    var result = command.ExecuteNonQuery();
                    connection.Close();
                    if (result != 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (connection.State.Equals("Open"))
                    connection.Close();
            }
        }

        /// <summary>
        /// UC 4 : Edit the contactType of the existing contact.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="contactType">Type of the contact.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool EditExistingContactUsingName(string firstName,string lastName,string contactType)
        {
            DBConnection dbc = new DBConnection();
            connection = dbc.GetConnection();
            try
            {
                using (connection)
                {
                    connection.Open();
                    string query = $@"update dbo.address_book set ContactType='{contactType}' where FirstName='{firstName}' and LastName='{lastName}'";
                    SqlCommand command = new SqlCommand(query, connection);                   
                    var result = command.ExecuteNonQuery();
                    connection.Close();
                    if (result > 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (connection.State.Equals("Open"))
                    connection.Close();
            }
        }

        /// <summary>
        /// UC 5 : Deletes the contact with given full name.
        /// </summary>
        /// <param name="fullName">The full name.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool DeleteContact(string fullName)
        {
            string[] split = fullName.Split(" ");
            string firstName = split[0];
            string lastName = split[1];
            DBConnection dbc = new DBConnection();
            connection = dbc.GetConnection();
            try
            {
                using (connection)
                {
                    connection.Open();
                    string query = $@"delete from dbo.address_book where FirstName='{firstName}' and LastName='{lastName}'";
                    SqlCommand command = new SqlCommand(query, connection);
                    var result = command.ExecuteNonQuery();
                    connection.Close();
                    if (result > 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (connection.State.Equals("Open"))
                    connection.Close();
            }
        }
    }
}
