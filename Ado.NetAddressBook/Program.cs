// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator Name="Aseem Anand"/>
// --------------------------------------------------------------------------------------------------------------------
namespace Ado.NetAddressBook
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            AddressBookRepository addressBookRepo = new AddressBookRepository();

            //UC 2
            addressBookRepo.GetAllContacts();

            //UC 3
            AddNewContact();

            //UC 4
            Console.WriteLine(addressBookRepo.EditExistingContactUsingName("Virat","Kohli","PROFESSION")?"Update done successfully\n":"Update failed");

            //UC 5
            Console.WriteLine(addressBookRepo.DeleteContact("Virat Kohli")?"Contact deleted successfully":"Deletion failed");

            //UC 6
            addressBookRepo.RetrieveContactFromACityOrState();
        }

        /// <summary>
        /// UC 3: Adds the new contact into DB table.
        /// </summary>
        public static void AddNewContact()
        {
            AddressBookRepository repository = new AddressBookRepository();
            AddressBookModel model = new AddressBookModel();
            model.FirstName = "Virat";
            model.LastName = "Kohli";
            model.Address = "Cannaught place";
            model.City = "New Delhi";
            model.State = "New Delhi";
            model.Zip = 555666;
            model.PhoneNumber = 6574567899;
            model.Email = "viratkohli@gmail.com";
            model.AddressBookName = "MS Dhoni";
            model.ContactType = "FRIENDS";
            Console.WriteLine(repository.AddContact(model) ? "Record inserted successfully\n" : "Failed");
        }
    }
}
