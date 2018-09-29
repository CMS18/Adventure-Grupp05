using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure
{
    class KeywordCheck : World
    {

        public void CheckPrimaryKeywordTake(int roomId, List<string> words, ref List<Item> tempItems)
        {
            // Jämför input med keyword i tillgängligt item
            for (int i = 0; i < words.Count; i++)
            {
                for (int j = 0; j < RoomList[roomId].inventory.Count; j++)
                {
                    if (words[i].ToUpper() == RoomList[roomId].inventory[j].Keyword.ToUpper())
                    {
                        tempItems.Add(RoomList[roomId].inventory[j]);
                    }
                }
            }
        }

        public void CheckPrimaryKeyword(List<string> words, ref List<Item> tempItems, List<Item>inventory)
        {
            for (int i = 0; i < words.Count; i++)
            {
                for (int j = 0; j < inventory.Count; j++)
                {
                    if (words[i].ToUpper() == inventory[j].Keyword.ToUpper())
                    {
                        tempItems.Add(inventory[j]);
                    }
                }
            }
        }

        public void CheckSecondaryKeywordTake(int roomId, List<string> words, ref List<Item> tempItems)
        {
            for (int i = 0; i < words.Count; i++)
            {
                for (int j = 0; j < tempItems.Count; j++)
                {
                    for (int k = 0; k < RoomList[roomId].inventory[j].SecondaryKeyword.Length; k++)
                    {
                        if (words[i].ToUpper() == RoomList[roomId].inventory[j].SecondaryKeyword[k].ToUpper())
                        {
                            tempItems.Clear();
                            tempItems.Add(RoomList[roomId].inventory[j]);
                        }
                    }
                }
            }
        }

        public void CheckSecondaryKeywordDrop(int roomId, List<string> words, ref List<Item> tempItems, List<Item> inventory)
        {
            for (int i = 0; i < words.Count; i++)
            {
                for (int j = 0; j < tempItems.Count; j++)
                {
                    for (int k = 0; k < inventory[j].SecondaryKeyword.Length; k++)
                    {
                        if (words[i].ToUpper() == inventory[j].SecondaryKeyword[k].ToUpper())
                        {
                            tempItems.RemoveAt(k);
                            tempItems.Add(RoomList[roomId].inventory[j]);
                        }
                    }
                }
            }
        }

        public void CheckSecondaryKeyword(List<string> words, ref List<Item> tempItems, ref List<Item> secondaryList, ref List<string> usedKeyword)
        {
            for (int i = 0; i < words.Count; i++)
            {
                for (int j = 0; j < tempItems.Count; j++)
                {
                    for (int k = 0; k < tempItems[j].SecondaryKeyword.Length; k++)
                    {
                        if (words[i].ToUpper() == tempItems[j].SecondaryKeyword[k].ToUpper())
                        {
                            secondaryList.Add(tempItems[j]);
                            usedKeyword.Add(words[i].ToLower());
                        }
                    }
                }
            }
        }
    }
}


