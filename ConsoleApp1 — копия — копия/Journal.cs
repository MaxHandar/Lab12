using ClassLibraryLab10;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab12_4

{
    public class JournalEntry
    {
        public string CollectionName { get; set; }
        public string ChangeType { get; set; }
        public string ChangedObjectData { get; set; }

        public JournalEntry(string collectionName, string changeType, string changedObjectData)
        {
            CollectionName = collectionName;
            ChangeType = changeType;
            ChangedObjectData = changedObjectData;

        }

        public override string ToString()
        {
            return $"Collection: {CollectionName}, Change: {ChangeType}, Object: {ChangedObjectData}";
        }
    }

    // Класс журнала для хранения записей об изменениях
    public class Journal
    {
        private List<JournalEntry> entries = new List<JournalEntry>();

        public void CollectionChanged(object source, CollectionHandlerEventArgs args)
        {
            entries.Add(new JournalEntry(((MyObservableCollection<Vehicle>)source).Name, args.ChangeType, args.ChangedItem.ToString()));
        }

        public void PrintJournal()
        {
            foreach (var entry in entries)
            {
                Console.WriteLine(entry);
            }
        }
    }
}
