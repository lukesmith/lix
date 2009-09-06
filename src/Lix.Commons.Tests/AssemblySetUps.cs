﻿using System;
using MbUnit.Framework;

namespace Lix.Commons.Tests
{
    [AssemblyFixture]
    public class AssemblySetUps
    {
        [FixtureSetUp]
        public void SetUp()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}
