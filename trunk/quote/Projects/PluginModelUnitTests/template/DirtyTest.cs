namespace PluginModelUnitTests.Template
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class DirtyTest
    {
        [Test]
        public void TestMethod()
        {
            Host.App.RegisteredClasses.Clear();
                
            Model.Template.Header header = new Model.Template.Header();
            Model.Product product = new Model.Product();
            Model.Template.Detail detail = new Model.Template.Detail(header, product);
            header.Details.Add(detail);
            
            header.IsDirty = false;
            Assert.IsFalse(header.IsDirty);
            
            Model.Template.NoteProperties notes = header.NoteProperties as Model.Template.NoteProperties;
            Assert.IsFalse(notes.IsDirty);
            notes.Note = "test";
            Assert.IsTrue(notes.IsDirty);
            Assert.IsTrue(header.IsDirty);
            
            header.IsDirty = false;
            Assert.IsFalse(notes.IsDirty);
            
            detail.Qty = 2;
            Assert.IsTrue(header.IsDirty);
            header.IsDirty = false;
            Assert.IsFalse(detail.IsDirty);
            
        }
    }
}
