using Godot;
using Godot.Collections;
namespace TwoDGame

{
    public partial class Inventory : Control
    {
        public ItemList itemList;

        public override void _Ready()
        {
            itemList = (ItemList)GetNode("ItemList");
            itemList.Clear();
        }

        public void OpenInventory(Array<Item> items)
        {
            itemList.Clear();
            foreach (var item in items)
            {
                itemList.AddItem(item.name, item.icon);
            }
            Visible = true;
        }

        public void CloseInventory(Array<Item> items)
        {
            itemList.Clear();
            Visible = false;
        }
    }
}