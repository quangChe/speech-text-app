using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class SessionResultModel
{
    public int id = 0;
    public int userId = 0; // FK for UserModel
    public int wordId = 0; // fk for WordProgressModel
    public string word = null;
    public string datePerformed = null;
    public int reps = 0;
    public int targetReps = 0;
    public int timeSpent = 0;
    public int targetTime = 0;
}
