using System;
using System.Collections;
using System.Collections.Generic;
using Modules.Scheduler;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Modules.Sheduler
{
    public sealed class ScheduledActionTests
    {
        private List<IDisposable> _disposables = new();
        
        [UnityTest]
        public IEnumerator OneCallForEachAction()
        {
            //Arrange
            var sheduledAction = new ScheduledAction();
            _disposables.Add(sheduledAction);
            int actionOneCallCount = 0;
            int actionTwoCallCount = 0;
            
            float seconds = 1f;
            
            //Act
            sheduledAction.Schedule(() => actionOneCallCount++, seconds);
            sheduledAction.Schedule(() => actionTwoCallCount++, seconds);
            yield return new WaitForSeconds(seconds + 0.05f);
            
            //Assert
            Assert.That(actionOneCallCount, Is.EqualTo(1));
            Assert.That(actionTwoCallCount, Is.EqualTo(1));
        }
        
        [TearDown]
        public void Dispose()
        {
            _disposables.ForEach(disposable => disposable.Dispose());
        }
    }
}