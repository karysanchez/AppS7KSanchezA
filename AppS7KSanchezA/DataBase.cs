﻿using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace AppS7KSanchezA
{
    public interface DataBase
    {
        SQLiteAsyncConnection GetConnection();
    }
}
