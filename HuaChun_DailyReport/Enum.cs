using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaChun_DailyReport
{
    enum ChartType
    {
        WeatherChart = 0,
        ExpectFinishChart
    };

    enum AuthorityLevle
    {
        NON = 0,
        GENERAL_EMPLOYEE,
        MANAGER,
        POWER_USE,
    };

    //因為這個連結到資料庫，因此順序不能改動
    enum ConstantCondition
    {
        None = 0,
        RestrictSchedule,
        CalenderDay,
        WorkingDayNoWeekend,
        WorkingDaySun,
        WorkingDaySatSun,
    };

    //因為這個連結到資料庫，因此順序不能改動
    enum VariedCondition
    {
        None = 0,
        Sunny,
        Rainy,
        HeavyRain,
        Typhoon,
        Hot,
        Mud,
        PowerShutdown,
        PauseWorking,
        Others,
    };

    //因為這個連結到資料庫，因此順序不能改動
    enum WeatherCondition
    {
        NoCountAll = 0,
        NoCountHalf,
        StillCount,
    };

}
