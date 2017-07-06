﻿using CreditCards.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace CreditCards.Tests.Controllers
{
    public class ValuesControllerShould
    {
        [Fact]
        public void ReturnValues()
        {
            var sut = new ValuesController();

            var result = sut.Get().ToArray();

            Assert.Equal(2, result.Length);
            Assert.Equal("value1", result[0]);
            Assert.Equal("value2", result[1]);
        }

        [Fact]
        public void ReturnBadRequest()
        {
            var sut = new ValuesController();

            var result = sut.Get(0);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);

            Assert.Equal("Invalid request for id 0", badRequestResult.Value);
        }

        [Fact]
        public void StartJobOk()
        {
            var sut = new ValuesController();

            var result = sut.StartJob();

            var okResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal("Batch Job Started", okResult.Value);
        }
    }
}
