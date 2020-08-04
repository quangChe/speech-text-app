using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class WordProgressModel
{
    public int id = 0;
    public string word = null;
    public int timesHit = 0;
    public int timesAttempted = 0;
    public int userId = 0;
    public int categoryId = 0; // FK for WordCategoryModel
}
