using System;
using System.Collections.Generic;

public class ContactManager
{
    private Dictionary<int, Contact> contacts;
    private string filePath;

    public ContactManager(Dictionary<int, Contact> contacts, string filePath)
    {
        this.contacts = contacts;
        this.filePath = filePath;
    }

    public void AddContact()
    {
        Console.Write("Enter Name: ");
        string name = Console.ReadLine();

        Console.Write("Enter Phone Number: ");
        string phoneNumber = Console.ReadLine();

        var contact = new Contact(name, phoneNumber, filePath);
        contacts[contact.Id] = contact;
        Contact.SaveContacts(contacts, filePath);
        Console.WriteLine($"Contact added successfully with ID: {contact.Id}");
    }

    public void SearchContact()
    {
        Console.Write("Enter ID to search: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID. Please enter a number.");
            return;
        }

        var contact = Contact.SearchContact(contacts, id);
        if (contact != null)
        {
            Console.WriteLine($"Found: {contact}");
        }
        else
        {
            Console.WriteLine("Contact not found.");
        }
    }

    public void DeleteContact()
    {
        Console.Write("Enter ID to delete: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID. Please enter a number.");
            return;
        }

        if (Contact.DeleteContact(contacts, id))
        {
            Contact.SaveContacts(contacts, filePath);
            Console.WriteLine("Contact deleted successfully.");
        }
        else
        {
            Console.WriteLine("Contact not found.");
        }
    }

    public void DisplayAllContacts()
    {
        if (contacts.Count == 0)
        {
            Console.WriteLine("No contacts available.");
            return;
        }

        Console.WriteLine("Contact List:");
        foreach (var contact in contacts.Values)
        {
            Console.WriteLine(contact);
        }
    }
}
