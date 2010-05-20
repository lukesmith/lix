using System.Linq;
using Lix.Commands;
using Lix.Commons.Tests;
using MbUnit.Framework;
using StructureMap;

namespace Lix.StructureMapAdapter.Tests
{
    [TestFixture]
    public class CommandHandlerContainer_Tests
    {
        [Test]
        [ExpectedArgumentExceptionAttribute]
        public void TCommand_is_a_icommand_should_throw_exception()
        {
            var container = new Container();
            container.Configure(x => x.For<ICurrentCommandProvider>().Use(t => null));
            container.Configure(x => x.For<ICommandHandler<FakeCommand>>().Use<FakeCommandHandler>());
            
            var commandHandlerContainer = new CommandHandlerContainer(container);
            var command = new FakeCommand();
            commandHandlerContainer.GetInstances<ICommand>(command);
        }

        [Test]
        public void should_return_handler_of_correct_type_using_generic_method()
        {
            var container = new Container();
            container.Configure(x => x.For<ICurrentCommandProvider>().Use(t => null));
            container.Configure(x => x.For<ICommandHandler<FakeCommand>>().Use<FakeCommandHandler>());

            var commandHandlerContainer = new CommandHandlerContainer(container);
            var command = new FakeCommand();
            var handlers = commandHandlerContainer.GetInstances(command);

            typeof(FakeCommandHandler).IsAssignableFrom(handlers.First().GetType()).ShouldBeEqualTo(true);
        }

        [Test]
        public void should_return_handler_of_correct_type()
        {
            var container = new Container();
            container.Configure(x => x.For<ICurrentCommandProvider>().Use(t => null));
            container.Configure(x => x.For<ICommandHandler<FakeCommand>>().Use<FakeCommandHandler>());

            var commandHandlerContainer = new CommandHandlerContainer(container);
            var command = new FakeCommand();
            var handlers = commandHandlerContainer.GetInstances(command.GetType());

            typeof(FakeCommandHandler).IsAssignableFrom(handlers.First().GetType()).ShouldBeEqualTo(true);
        }

        [Test]
        public void TCommand_is_a_command_should_return_handler_of_correct_type()
        {
            var container = new Container();
            container.Configure(x => x.For<ICurrentCommandProvider>().Use(t => null));
            container.Configure(x => x.For<ICommandHandler<FakeCommand>>().Use<FakeCommandHandler>());

            var commandHandlerContainer = new CommandHandlerContainer(container);
            var command = new FakeCommand();
            var handlers = commandHandlerContainer.GetInstances(command);

            handlers.First().ShouldBeTheSameTypeAs(typeof(FakeCommandHandler));
        }
    }
}
