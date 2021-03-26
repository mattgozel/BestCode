﻿using FlooringMasteryRefactored.Data.ADO;
using FlooringMasteryRefactored.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMasteryRefactored.Data.Factories
{
    public static class TaxInfoRepositoryFactory
    {
        public static ITaxInfoRepository GetRepository()
        {
            switch (Settings.GetMode())
            {
                case "PROD":
                    return new TaxInfoRepositoryADO();
                default:
                    throw new Exception("Could not find valid Mode configuration value.");
            }
        }
    }
}