using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Contact
{
    public int Id { get; private set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }

    private static int nextId = 1; 
    private string filePath;


    public Contact(string name, string phoneNumber, string filePath)
    {
        Id = nextId++; 
        Name = name;
        PhoneNumber = phoneNumber;
        this.filePath = filePath;
    }


    public string Serialize()
    {
        return $"{Id},{Name},{PhoneNumber}";
    }

    
    public static Contact Parse(string txtLine)
    {
        var parts = txtLine.Split(',');
        if (parts.Length == 3 && int.TryParse(parts[0], out int id))
        {
            var contact = new Contact(parts[1], parts[2], "");
            contact.Id = id; 
            return contact;
        }
        return null; 
    }

    public override string ToString()
    {
        return $"{Id}: {Name} - {PhoneNumber}";
    }

    public static void SaveContacts(Dictionary<int, Contact> contacts, string filePath)
    {
        using (var writer = new StreamWriter(filePath))
        {
            foreach (var contact in contacts.Values)
            {
                writer.WriteLine(contact.Serialize());
            }
        }
    }

    public static Dictionary<int, Contact> LoadContacts(string filePath)
    {
        var contacts = new Dictionary<int, Contact>();
        if (File.Exists(filePath))
        {
            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                var contact = Parse(line);
                if (contact != null)
                {
                    contacts[contact.Id] = contact;
                }
            }
        }
        UpdateNextId(contacts);
        return contacts;
    }

    
    private static void UpdateNextId(Dictionary<int, Contact> contacts)
    {
        if (contacts.Count > 0)
        {
            nextId = contacts.Keys.Max() + 1; 
        }
    }

    public static Contact SearchContact(Dictionary<int, Contact> contacts, int id)
    {
        contacts.TryGetValue(id, out var contact);
        return contact;
    }

    public static bool DeleteContact(Dictionary<int, Contact> contacts, int id)
    {
        return contacts.Remove(id);
    }
}
