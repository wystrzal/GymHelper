using GymHelper.Data;
using GymHelper.Data.Interfaces;
using GymHelper.Helpers;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymHelper.Test
{
    public static class TestHelper
    {
        public static void PrepareUnitOfWork(out Mock<IUnitOfWork> mockUnitOfWork, out Mock<IAlertService> mockAlertService)
        {
            mockUnitOfWork = new Mock<IUnitOfWork>();
            mockAlertService = new Mock<IAlertService>();

            App.Data = new DataStorage(It.IsAny<string>())
            {
                DataContext = new DataContext(It.IsAny<string>()),
                AlertService = mockAlertService.Object
            };
            App.Data.UnitOfWork = mockUnitOfWork.Object;
        }

        public static void PrepareUnitOfWork(out Mock<IUnitOfWork> mockUnitOfWork)
        {
            mockUnitOfWork = new Mock<IUnitOfWork>();

            App.Data = new DataStorage(It.IsAny<string>())
            {
                DataContext = new DataContext(It.IsAny<string>()),
                AlertService = new Mock<IAlertService>().Object
            };
            App.Data.UnitOfWork = mockUnitOfWork.Object;
        }
    }
}