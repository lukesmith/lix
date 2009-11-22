using System;
using MbUnit.Framework;

namespace Lix.Commons.Tests.when_registering_types_in_the_container
{
    [TestFixture]
    public class and_the_type_is_null
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void should_throw_an_argument_null_exception_for_fortype_parameter()
        {
            var container = new Container();
            container.RegisterForType(null, typeof(string));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void should_throw_an_argument_null_exception_for_type_parameter()
        {
            var container = new Container();
            container.RegisterForType(typeof(string), null);
        }

        [Test]
        public void should_throw_an_argument_null_exception_for_fortype_parameter_with_paramname_set()
        {
            var container = new Container();

            try
            {
                container.RegisterForType(null, typeof(string));
            }
            catch (ArgumentNullException ex)
            {
                ex.ParamName.ShouldBeEqualTo("forType");
            }
        }

        [Test]
        public void should_throw_an_argument_null_exception_for_type_parameter_with_paramname_set()
        {
            var container = new Container();

            try
            {
                container.RegisterForType(typeof(string), null);
            }
            catch (ArgumentNullException ex)
            {
                ex.ParamName.ShouldBeEqualTo("type");
            }
        }
    }
}
