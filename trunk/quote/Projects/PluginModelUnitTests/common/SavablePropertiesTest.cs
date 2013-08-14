namespace PluginModelUnitTests.Common
{
    using System;
    using System.Diagnostics;
    using Model.Common;
    using NUnit.Framework;

    [TestFixture]
    public class SavablePropertiesTest
    {
        [Test]
        public void TestIsDirty()
        {
            TestSavableProperties properties = new TestSavableProperties();
            
            bool flag = false;
            properties.Dirty += delegate { flag = true; };
            
            properties.SampleProperty = true;
            
            Assert.That(properties.SampleProperty, Is.True);
            Assert.That(flag, Is.True);
            Assert.That(properties.IsDirty, Is.True);
        }
        
        [Test]
        public void TestPropertyChanged()
        {
            TestSavableProperties properties = new TestSavableProperties();
            
            bool flag = false;
            properties.PropertyChanged += delegate { flag = true; };
            
            properties.SampleProperty = true;
            
            Assert.That(properties.SampleProperty, Is.True);
            Assert.That(flag, Is.True);
        }
        
        [Test]
        public void TestDependent()
        {
            TestSavableProperties properties = new TestSavableProperties();
            TestSavableProperties properties2 = new TestSavableProperties();
            properties.Add(properties2);
            
            bool flag = false;
            properties.Dirty += delegate { flag = true; };
            
            bool flag2 = false;
            properties2.Dirty += delegate { flag2 = true; };
            
            properties2.SampleProperty = true;
            
            Assert.That(properties.SampleProperty, Is.False);
            Assert.That(flag, Is.True);
            
            Assert.That(properties2.SampleProperty, Is.True);
            Assert.That(flag2, Is.True);
        }
        
        [Test]
        public void TestDependent2()
        {
            TestSavableProperties properties = new TestSavableProperties();
            TestSavableProperties properties2 = new TestSavableProperties();
            properties.Add(properties2);
            properties.Remove(properties2);
            
            bool flag = false;
            properties.Dirty += delegate { flag = true; };
            
            bool flag2 = false;
            properties2.Dirty += delegate { flag2 = true; };
            
            properties2.SampleProperty = true;
            
            Assert.That(properties.SampleProperty, Is.False);
            Assert.That(flag, Is.False);
            
            Assert.That(properties2.SampleProperty, Is.True);
            Assert.That(flag2, Is.True);
        }
        
        [Test]
        public void TestDependent3()
        {
            TestSavableProperties properties = new TestSavableProperties();
            TestSavableProperties properties2 = new TestSavableProperties();
            properties.Add(properties2);
            
            properties2.SampleProperty = true;
            
            Assert.That(properties.SampleProperty, Is.False);
            Assert.That(properties.IsDirty, Is.True);
            
            Assert.That(properties2.SampleProperty, Is.True);
            Assert.That(properties2.IsDirty, Is.True);
            
            properties.IsDirty = false;
            Assert.That(properties.IsDirty, Is.False);
            Assert.That(properties2.IsDirty, Is.False);
        }
    }
}
