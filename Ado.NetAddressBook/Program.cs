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
            addressBookRepo.GetAllContacts();
        }
    }
}
