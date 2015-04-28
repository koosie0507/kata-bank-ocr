using System.ComponentModel;

using KataBankOCR.Tests.Properties;

using NUnit.Framework;

namespace KataBankOCR.Tests
{


    [TestFixture]
    public sealed class ScanViewModelTests
    {
        [Test]
        public void Ctor_HappyFlow_InitializesINPC()
        {
            Assert.IsInstanceOf<INotifyPropertyChanged>(new ScanViewModel());
        }

        [Test]
        public void Scan_HappyFlow_RaisesPropertyChangedOnAccountNumbers()
        {
            var viewModel = new ScanViewModel();
            string propertyName = null;
            viewModel.PropertyChanged += (s, e) => { propertyName = e.PropertyName; };
            viewModel.Scan(Resources.FileWith1Entries);

            Assert.IsNotNull(propertyName);
            Assert.That(propertyName, Is.EqualTo("AccountNumbers"));
        }
    }
}