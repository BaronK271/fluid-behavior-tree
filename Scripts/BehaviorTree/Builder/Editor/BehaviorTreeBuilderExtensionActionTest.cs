using System;
using Adnc.FluidBT.Tasks;
using Adnc.FluidBT.Tasks.Actions;
using NUnit.Framework;

namespace Adnc.FluidBT.Trees.Testing {
    public static class BehaviorTreeExtensionActionExamples {
        public static BehaviorTreeBuilder ExampleAction (this BehaviorTreeBuilder builder, string name, Action callback) {
            builder.AddNode(new BehaviorTreeBuilderExtensionActionTest.ExtensionAction {
                Name = name,
                callback = callback,
            });
            
            return builder;
        }
    }
    
    public class BehaviorTreeBuilderExtensionActionTest {
        public class ExtensionAction : ActionBase {
            public Action callback;
        
            protected override TaskStatus OnUpdate () {
                callback();
                return TaskStatus.Success;
            }
        }
        
        [Test]
        public void It_should_run_the_custom_action () {
            var result = false;
            var tree = new BehaviorTreeBuilder(null)
                .Sequence()
                    .ExampleAction("test", () => result = true)
                .End()
                .Build();

            tree.Tick();
            
            Assert.IsTrue(result);
        }
    }
}