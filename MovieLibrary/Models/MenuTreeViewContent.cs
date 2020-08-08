using System.Collections.Generic;

namespace MovieLibrary.Models
{
    public class MenuTreeViewContent
    {
        public List<TreeCategory> Content { get; }

        public MenuTreeViewContent()
        {
            Content = new List<TreeCategory>
            {
                new TreeCategory("Жанр",
                    new TreeItem("Детектив", "Crime", false),
                    new TreeItem("Комедия", "Comedy", false),
                    new TreeItem("Приключения", "Adventure", false),
                    new TreeItem("Триллер", "Thriller", false),
                    new TreeItem("Ужасы", "Horror", false),
                    new TreeItem("Фантастика", "Sci-Fi", false),
                    new TreeItem("Фэнтези", "Fantasy", false)),

                new TreeCategory("Страна",
                    new TreeItem("Австралия", "Australia", false),
                    new TreeItem("Великобритания", "UK", false),
                    new TreeItem("Германия", "Germany", false),
                    new TreeItem("Испания", "Spain", false),
                    new TreeItem("Италия", "Italy", false),
                    new TreeItem("Канада", "Canada", false),
                    new TreeItem("Мексика", "Mexico", false),
                    new TreeItem("Россия", "Russia", false),
                    new TreeItem("США", "USA", false),
                    new TreeItem("Франция", "France", false),
                    new TreeItem("Япония", "Japan", false))
            };
        }
    }

    public class TreeCategory
    {
        public string Name { get; }

        public List<TreeItem> TreeItems { get; }

        public TreeCategory(string name, params TreeItem[] items)
        {
            Name = name;
            TreeItems = new List<TreeItem>(items);
        }
    }

    public class TreeItem
    {
        public string Name { get; }
        public string SearchParamName { get; }
        public bool IsChecked { get; set; }

        public TreeItem(string name, string searchParamName, bool isChecked)
        {
            Name = name;
            SearchParamName = searchParamName;
            IsChecked = isChecked;
        }
    }
}
