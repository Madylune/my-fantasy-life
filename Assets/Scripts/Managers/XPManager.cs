using System;

static class XPManager
{
    public static int CalculateXP(Enemy enemy)
    {
        int baseXP = (Player.MyInstance.MyLevel * 5) + 45;

        int grayLevel = CalculateGrayLevel();

        int totalXP = 0;

        if (enemy.MyLevel >= Player.MyInstance.MyLevel) // High level mobs
        {
            totalXP = (int)(baseXP * (1 + 0.05 * (enemy.MyLevel - Player.MyInstance.MyLevel)));
        }
        else if (enemy.MyLevel > grayLevel) // Lower level mobs
        {
            totalXP = (baseXP) * (1 - (Player.MyInstance.MyLevel - enemy.MyLevel) / ZeroDifference());
        }

        return totalXP;
    }

    public static int CalculateXP(Quest quest)
    {
        if (Player.MyInstance.MyLevel <= quest.MyLevel + 5)
        {
            return quest.MyXp;
        }
        if (Player.MyInstance.MyLevel == quest.MyLevel + 6)
        {
            return (int)(quest.MyXp * 0.8 / 5) * 5;
        }
        if (Player.MyInstance.MyLevel == quest.MyLevel + 7)
        {
            return (int)(quest.MyXp * 0.6 / 5) * 5;
        }
        if (Player.MyInstance.MyLevel == quest.MyLevel + 8)
        {
            return (int)(quest.MyXp * 0.4 / 5) * 5;
        }
        if (Player.MyInstance.MyLevel == quest.MyLevel + 9)
        {
            return (int)(quest.MyXp * 0.2 / 5) * 5;
        }
        if (Player.MyInstance.MyLevel >= quest.MyLevel + 10)
        {
            return (int)(quest.MyXp * 0.1 / 5) * 5;
        }

        return 0;
    }

    private static int ZeroDifference()
    {
        if (Player.MyInstance.MyLevel <= 7)
        {
            return 5;
        }
        if (Player.MyInstance.MyLevel >= 8 && Player.MyInstance.MyLevel <= 9)
        {
            return 6;
        }
        if (Player.MyInstance.MyLevel >= 10 && Player.MyInstance.MyLevel <= 11)
        {
            return 7;
        }
        if (Player.MyInstance.MyLevel >= 12 && Player.MyInstance.MyLevel <= 15)
        {
            return 8;
        }
        if (Player.MyInstance.MyLevel >= 16 && Player.MyInstance.MyLevel <= 19)
        {
            return 9;
        }
        if (Player.MyInstance.MyLevel >= 20 && Player.MyInstance.MyLevel <= 29)
        {
            return 11;
        }
        if (Player.MyInstance.MyLevel >= 30 && Player.MyInstance.MyLevel <= 39)
        {
            return 12;
        }
        if (Player.MyInstance.MyLevel >= 40 && Player.MyInstance.MyLevel <= 44)
        {
            return 13;
        }
        if (Player.MyInstance.MyLevel >= 45 && Player.MyInstance.MyLevel <= 49)
        {
            return 14;
        }
        if (Player.MyInstance.MyLevel >= 50 && Player.MyInstance.MyLevel <= 54)
        {
            return 15;
        }
        if (Player.MyInstance.MyLevel >= 55 && Player.MyInstance.MyLevel <= 59)
        {
            return 16;
        }

        return 17;
    }

    public static int CalculateGrayLevel()
    {
        if (Player.MyInstance.MyLevel <= 5)
        {
            return 0;
        }
        else if (Player.MyInstance.MyLevel >= 6 && Player.MyInstance.MyLevel <= 49)
        {
            return Player.MyInstance.MyLevel - (Player.MyInstance.MyLevel / 10) - 5;
        }
        else if (Player.MyInstance.MyLevel == 50)
        {
            return Player.MyInstance.MyLevel - 10;
        }
        else if (Player.MyInstance.MyLevel >= 51 && Player.MyInstance.MyLevel <= 59)
        {
            return Player.MyInstance.MyLevel - (Player.MyInstance.MyLevel / 5) - 1;
        }

        return Player.MyInstance.MyLevel - 9;
    }
}
