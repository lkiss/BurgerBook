﻿using System;
namespace BurgerBook.Models.Database
{
	public class BurgerBookDatabaseSettings
	{
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string BurgerPlacesCollectionName { get; set; } = null!;
    }
}

