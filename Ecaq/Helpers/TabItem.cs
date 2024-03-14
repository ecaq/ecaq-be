namespace Ecaq.Helpers
{
    public class TabItem
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Href { get; set; }
        public string? ClassColor { get; set; }
        public bool IsCurrent { get; set; }


        public static List<TabItem> TabItems()
        {
            List<TabItem> TabItems = new();
            TabItems.Add(new TabItem { Id = 1, Name = "General", Href = "", ClassColor = "bg-green-50", IsCurrent = false });
            TabItems.Add(new TabItem { Id = 2, Name = "Advance", Href = "", ClassColor = "bg-indigo-50", IsCurrent = false });
            TabItems.Add(new TabItem { Id = 3, Name = "Support", Href = "", ClassColor = "bg-orange-50", IsCurrent = false });

            return TabItems;
        }

        public static List<TabItem> TabItemsAdditionalInfo()
        {
            List<TabItem> TabItems = new();
            TabItems.Add(new TabItem { Id = 1, Name = "Features", Href = "", ClassColor = "bg-green-50", IsCurrent = false });
            TabItems.Add(new TabItem { Id = 2, Name = "Specifications", Href = "", ClassColor = "bg-indigo-50", IsCurrent = false });

            return TabItems;
        }

    }

}
